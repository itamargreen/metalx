using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Management;
namespace MetalX.Net
{
    public enum SessionTerminateType
    {
        Normal,
        Force,
    }
    public class NetEventArgs : EventArgs
    {
        public Session Session;
        public NetEventArgs(Session s)
        {
            Session = s;
        }
    }
    public delegate void NetEvent(Session s);
    public class Session : IDisposable
    {
        public SessionTerminateType TerminateType;
        public bool IsOnline = false;
        public Socket Socket;
        public IntPtr Handle
        {
            get
            {
                return Socket.Handle;
            }
        }
        public string RemoteIPAddress
        {
            get
            {
                return Socket.RemoteEndPoint.ToString();
            }
        }
        public string LocalIPAddress
        {
            get
            {
                return Socket.LocalEndPoint.ToString();
            }
        }
        public byte[] Data;

        public string CPUID;
        public string DiskID;
        public string MainBoardID;
        public string OS;
        public string OSID;
        public string MACAddress;
        public string PCName;
        public string UserName;

        public Session()
        {
            IsOnline = false;
        }
        public Session(Socket s)
        {
            Socket = s;
        }
        public void Dispose()
        {
            Terminate(SessionTerminateType.Normal);
            Data = null;
        }
        public void Terminate(SessionTerminateType type)
        {
            TerminateType = type;
            IsOnline = false;
            Socket.Close();
        }
        public void Terminate()
        {
            Terminate(SessionTerminateType.Normal);
        }
    }
    public class Sessions : IDisposable
    {
        List<Session> sessions = new List<Session>();
        public int Count
        {
            get
            {
                return sessions.Count;
            }
        }
        public int GetIndex(IntPtr handle)
        {
            for (int i = 0; i < sessions.Count; i++)
            {
                if (sessions[i].Handle == handle)
                {
                    return i;
                }
            }
            return -1;
        }
        public int GetIndex(Session s)
        {
            return GetIndex(s.Handle);
        }
        public void Add(Session s)
        {
            if (GetIndex(s) == -1)
            {
                sessions.Add(s);
            }
        }
        public void Del(int i)
        {
            sessions[i].Dispose();
            sessions.RemoveAt(i);
        }
        public void Del(IntPtr handle)
        {
            Del(GetIndex(handle));
        }
        public void Del(Session s)
        {
            Del(GetIndex(s));
        }
        public Session this[int i]
        {
            get
            {
                return sessions[i];
            }
            set
            {
                sessions[i] = value;
            }
        }
        public void Dispose()
        {
            foreach (Session s in sessions)
            {
                s.Dispose();
            }
            sessions.Clear();
        }
    }
    public class Server
    {
        public int ClientCount
        {
            get
            {
                return sessions.Count;
            }
        }
        public int Capacity = 100;
        public event NetEvent OnDataReceived;
        public event NetEvent OnDataSent;
        public event NetEvent OnClientConnected;
        public event NetEvent OnClientDisconnected;
        public event NetEvent OnServerFull;
                
        //List<ArraySegment<byte>> buffer = new List<ArraySegment<byte>>();
        byte[] rbuffer;

        Sessions sessions;

        Socket socket;
        bool isRunning;
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
        }

        public Server(string ip, int port)
        {
            IPAddress[] ipa = Dns.GetHostAddresses(Dns.GetHostName());
            rbuffer = new byte[1024 * 1024];
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            
            socket.Bind(ipEndPoint);
        }
        public void Start()
        {
            sessions = new Sessions();
            socket.Listen(8);
            socket.BeginAccept(new AsyncCallback(AcceptFunc), socket);
            isRunning = true;
        }
        public void Stop()
        {
            isRunning = false;
            socket.Disconnect(false);
            socket.Close();
            sessions.Dispose();
        }
        int sendcount;
        public void Send(Session session, byte[] data)
        {
            sendcount = data.Length;
            session.Socket.BeginSend(data, 0, sendcount, SocketFlags.None, new AsyncCallback(SendFunc), session);
        }
        void SendFunc(IAsyncResult iar)
        {
            Socket socket = ((Session)iar.AsyncState).Socket;
            int count = socket.EndSend(iar);
            if (OnDataSent != null)
            {
                OnDataSent((Session)iar.AsyncState);
            }
        }
        void AcceptFunc(IAsyncResult iar)
        {
            Socket serverSocket = (Socket)iar.AsyncState;

            Socket clientSocket = serverSocket.EndAccept(iar);

            Session s = new Session(clientSocket);

            if (ClientCount < Capacity)
            {
                sessions.Add(s);
                if (OnClientConnected != null)
                {
                    OnClientConnected(s);
                }
            }
            else
            {
                if (OnServerFull != null)
                {
                    OnServerFull(s);
                }
            }

            clientSocket.BeginReceive(rbuffer,0,rbuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveFunc), s);

            serverSocket.BeginAccept(new AsyncCallback(AcceptFunc), serverSocket);
        }

        void ReceiveFunc(IAsyncResult iar)
        {
            Socket clientSocket = ((Session)iar.AsyncState).Socket;
            int i = sessions.GetIndex(clientSocket.Handle);
            try
            {
                int count = clientSocket.EndReceive(iar);
                if (count > 0)
                {
                    sessions[i].Data = new byte[count];
                    Array.Copy(rbuffer, 0, sessions[i].Data, 0, count);
                    if (OnDataReceived != null)
                    {
                        OnDataReceived(sessions[i]);
                    }
                }
                else
                {
                    sessions[i].Terminate();
                    if (OnClientDisconnected != null)
                    {
                        OnClientDisconnected(sessions[i]);
                    }
                }
                clientSocket.BeginReceive(rbuffer, 0, rbuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveFunc), (Session)iar.AsyncState);
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode == 10054)
                {
                    //客户端强制关闭
                    sessions[i].Terminate(SessionTerminateType.Force);
                    if (OnClientDisconnected != null)
                    {
                        OnClientDisconnected(sessions[i]);
                    }
                }
            }
        }
    }
    public class Clinet
    {
        //List<ArraySegment<byte>> buffer = new List<ArraySegment<byte>>();
        byte[] rbuffer; 
        Session session;
        public event NetEvent OnConnected;
        public event NetEvent OnServerDisconnected;
        
        public event NetEvent OnDataReceived;
        public event NetEvent OnDataSent;

        public Clinet()
        {
            rbuffer = new byte[1024 * 1024];
            session = new Session();
        }
        public void Connect(string ip, int port)
        {
            if (session.IsOnline)
            {
                return;
            }
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), port);
            session.Socket = newsock;
            session.Socket.BeginConnect(iep, new AsyncCallback(ConnectFunc), session);
        }
        public void Disconnect()
        {
            session.IsOnline = false;
            session.Socket.Close();
        }
        int sendcount;
        public void Send(byte[] data)
        {
            sendcount = data.Length;
            session.Socket.BeginSend(data, 0, sendcount, SocketFlags.None, new AsyncCallback(SendFunc), session);
        }
        void SendFunc(IAsyncResult iar)
        {
            Socket socket = ((Session)iar.AsyncState).Socket;
            int count = socket.EndSend(iar);
            if (OnDataSent != null)
            {
                OnDataSent((Session)iar.AsyncState);
            }
        }
        void ConnectFunc(IAsyncResult iar)
        {
            Socket serverSocket = ((Session)iar.AsyncState).Socket;
            serverSocket.EndConnect(iar);
            session.Socket = serverSocket;
            session.IsOnline = true;
            if (OnConnected != null)
            {
                OnConnected(session);
            }
            session.Socket.BeginReceive(rbuffer, 0, rbuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveFunc), (Session)iar.AsyncState);
        }
        void ReceiveFunc(IAsyncResult iar)
        {
            Socket serverSocket = ((Session)iar.AsyncState).Socket;
            try
            {
                int count = serverSocket.EndReceive(iar);
                if (count > 0)
                {
                    session.Data = new byte[count];
                    Array.Copy(rbuffer, 0, session.Data, 0, count);
                    if (OnDataReceived != null)
                    {
                        OnDataReceived(session);
                    }
                }
                else
                {
                    session.Terminate();
                    if (OnServerDisconnected != null)
                    {
                        OnServerDisconnected(session);
                    }
                }
                serverSocket.BeginReceive(rbuffer, 0, rbuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveFunc), (Session)iar.AsyncState);
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode == 10054)
                {
                    //客户端强制关闭
                    session.Terminate(SessionTerminateType.Force);
                    if (OnServerDisconnected != null)
                    {
                        OnServerDisconnected(session);
                    }
                }
            }
        }
        
    }
}


namespace Soyee
{
    /// <summary> 
    /// Computer Information 
    /// </summary> 
    public class Computer
    {
        public string CpuID;
        public string MacAddress;
        public string DiskID;
        public string IpAddress;
        public string LoginUserName;
        public string ComputerName;
        public string SystemType;
        public string TotalPhysicalMemory; //单位：M 
        private static Computer _instance;
        public static Computer Instance()
        {
            if (_instance == null)
                _instance = new Computer();
            return _instance;
        }
        public Computer()
        {
            CpuID = GetCpuID();
            MacAddress = GetMacAddress();
            DiskID = GetDiskID();
            IpAddress = GetIPAddress();
            LoginUserName = GetUserName();
            SystemType = GetSystemType();
            TotalPhysicalMemory = GetTotalPhysicalMemory();
            ComputerName = GetComputerName();
        }
        string GetCpuID()
        {
            try
            {
                //获取CPU序列号代码 
                string cpuInfo = "";//cpu序列号 
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
                return cpuInfo;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }
        string GetMacAddress()
        {
            try
            {
                //获取网卡硬件地址 
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString() + " " + mac;
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }
        string GetIPAddress()
        {
            try
            {
                //获取IP地址 
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        st = mo["IpAddress"].ToString();
                        //System.Array ar; 
                        //ar = (System.Array)(mo.Properties["IpAddress"].ToString); 
                        //st = ar.Get(0).ToString(); 
                        break;
                    }
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }

        string GetDiskID()
        {
            try
            {
                //String HDid = "";
                //ManagementClass mc = new ManagementClass("Win32_PhysicalMedia");
                //ManagementObjectCollection moc = mc.GetInstances();
                //foreach (ManagementObject mo in moc)
                //{
                //    HDid = (string)mo.Properties["SerialNumber"].ToString();
                //}
                //moc = null;
                //mc = null;
                //return HDid;
                string result = "";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");
                ManagementObjectCollection moCollection = searcher.Get();
                foreach (ManagementObject mObject in moCollection)
                {
                    result += mObject["SerialNumber"].ToString() + " ";
                }
                return result;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }
        public static string GetHd()
        {
            ManagementObjectSearcher wmiSearcher = new ManagementObjectSearcher();

            wmiSearcher.Query = new SelectQuery(
            "Win32_DiskDrive",
            "",
            new string[] { "PNPDeviceID" }
            );
            ManagementObjectCollection myCollection = wmiSearcher.Get();
            ManagementObjectCollection.ManagementObjectEnumerator em =
            myCollection.GetEnumerator();
            em.MoveNext();
            ManagementBaseObject mo = em.Current;
            string id = mo.Properties["PNPDeviceID"].Value.ToString().Trim();
            return id;
        }
        /// <summary> 
        /// 操作系统的登录用户名 
        /// </summary> 
        /// <returns></returns> 
        string GetUserName()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["UserName"].ToString();

                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }


        /// <summary> 
        /// PC类型 
        /// </summary> 
        /// <returns></returns> 
        string GetSystemType()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["SystemType"].ToString();

                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }

        /// <summary> 
        /// 物理内存 
        /// </summary> 
        /// <returns></returns> 
        string GetTotalPhysicalMemory()
        {
            try
            {

                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["TotalPhysicalMemory"].ToString();

                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }
        /// <summary> 
        ///  
        /// </summary> 
        /// <returns></returns> 
        string GetComputerName()
        {
            try
            {
                return System.Environment.GetEnvironmentVariable("ComputerName");
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }
    }
}
namespace WMIDemo
{
    /// <summary>
    /// 说明：这个类主要用来展示如何利用WMI来获取一些操作系统的信息
    /// 作者：周公
    /// 时间：2008-1-14
    /// 首发地址：http://blog.csdn.net/zhoufoxcn
    /// </summary>
    public class GetSystemInfo
    {
        /// <summary>
        /// 获取操作系统序列号
        /// </summary>
        /// <returns></returns>
        public string GetSerialNumber()
        {
            string result = "";
            ManagementClass mClass = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection moCollection = mClass.GetInstances();
            foreach (ManagementObject mObject in moCollection)
            {
                result += mObject["SerialNumber"].ToString() + " ";
            }
            return result;
        }
        /// <summary>
        /// 查询CPU编号
        /// </summary>
        /// <returns></returns>
        public string GetCpuID()
        {
            string result = "";
            ManagementClass mClass = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moCollection = mClass.GetInstances();
            foreach (ManagementObject mObject in moCollection)
            {
                result += mObject["ProcessorId"].ToString() + " ";
            }
            return result;
        }
        /// <summary>
        /// 查询硬盘编号
        /// </summary>
        /// <returns></returns>
        public string GetMainHardDiskId()
        {
            string result = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            ManagementObjectCollection moCollection = searcher.Get();
            foreach (ManagementObject mObject in moCollection)
            {
                result += mObject["SerialNumber"].ToString() + " ";
            }
            return result;
        }

        /// <summary>
        /// 主板编号
        /// </summary>
        /// <returns></returns>
        public string GetMainBoardId()
        {
            string result = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("rootCIMV2", "SELECT * FROM Win32_BaseBoard");
            ManagementObjectCollection moCollection = searcher.Get();
            foreach (ManagementObject mObject in moCollection)
            {
                result += mObject["SerialNumber"].ToString() + " ";
            }
            return result;
        }

        /// <summary>
        /// 主板编号
        /// </summary>
        /// <returns></returns>
        public string GetNetworkAdapterId()
        {
            string result = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT MACAddress FROM Win32_NetworkAdapter WHERE ((MACAddress Is Not NULL)AND (Manufacturer <> 'Microsoft'))");
            ManagementObjectCollection moCollection = searcher.Get();
            foreach (ManagementObject mObject in moCollection)
            {
                result += mObject["MACAddress"].ToString() + " ";
            }
            return result;
        }

        /// <summary>
        /// 主板编号
        /// </summary>
        /// <returns></returns>
        public string GetGroupName()
        {
            string result = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("rootCIMV2", "SELECT * FROM Win32_Group");
            ManagementObjectCollection moCollection = searcher.Get();
            foreach (ManagementObject mObject in moCollection)
            {
                result += mObject["Name"].ToString() + " ";
            }
            return result;
        }

        /// <summary>
        /// 获取本地驱动器信息
        /// </summary>
        /// <returns></returns>
        public string GetDriverInfo()
        {
            string result = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("rootCIMV2", "SELECT * FROM Win32_LogicalDisk");
            ManagementObjectCollection moCollection = searcher.Get();
            foreach (ManagementObject mObject in moCollection)
            {
                //mObject["DriveType"]共有6中可能值，分别代表如下意义：
                //1:No type   2:Floppy disk   3:Hard disk
                //4:Removable drive or network drive   5:CD-ROM   6:RAM disk
                //本处只列出固定驱动器（硬盘分区）的情况
                if (mObject["DriveType"].ToString() == "3")
                {
                    result += string.Format("Name=,FileSystem=,Size=,FreeSpace= ", mObject["Name"].ToString(),
                        mObject["FileSystem"].ToString(), mObject["Size"].ToString(), mObject["FreeSpace"].ToString());
                }
            }
            return result;
        }
    }
}
