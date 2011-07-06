using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using Microsoft.DirectX;
namespace MetalX
{
    public class FileLoader
    {
        public static byte[] FileData;
        public static int Size = 0;
        public static int Loaded = 0;
        public static TimeSpan TakeTime;
        static DateTime startTime;
        public static double Progress
        {
            get
            {
                return (double)Loaded / (double)Size;
            }
        }
        public static void Load(string fileName)
        {
            startTime = DateTime.Now;
            Loaded = 0;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            Size = (int)fs.Length;
            FileData = new byte[Size];
            fs.BeginRead(FileData, 0, Size, new AsyncCallback(read), fs);
        }
        static void read(IAsyncResult iar)
        {
            FileStream fs = (FileStream)iar.AsyncState;
            Loaded += fs.EndRead(iar);
            if (iar.IsCompleted)
            {
                TakeTime = DateTime.Now - startTime;
                return;
            }
            else
            {
                fs.BeginRead(FileData, Loaded, 512, new AsyncCallback(read), fs);
            }
        }
    }
    public class Util
    {
        public static Point PointMulInt(Point v3, int unit)
        {
            return new Point(v3.X * unit, v3.Y * unit);
        }
        public static Point PointDivInt(Point v3, int unit)
        {
            return new Point(v3.X / unit, v3.Y / unit);
        }
        public static Vector3 Vector3MulInt(Vector3 v3, int unit)
        {
            return new Vector3(v3.X * unit, v3.Y * unit, v3.Z * unit);
        }
        public static Vector3 Vector3DivInt(Vector3 v3, int unit)
        {
            return new Vector3(v3.X / unit, v3.Y / unit, v3.Z / unit);
        }
        public static Vector3 Point2Vector3(Point p, float z)
        {
            return new Vector3(p.X, p.Y, z);
        }
        public static Point Vector32Point(Vector3 v3)
        {
            return new Point((int)v3.X, (int)v3.Y);
        }
        public static bool Is2PowSize(Size size)
        {
            if (Is2Pow(size.Width) && Is2Pow(size.Height))
            {
                return true;
            }
            return false;
        }
        public static Vector3 Vector3AddVector3(Vector3 v31, Vector3 v32)
        {
            return new Vector3(v31.X + v32.X, v31.Y + v32.Y, v31.Z + v32.Z);
        }
        public static Vector3 Vector3SubVector3(Vector3 v31, Vector3 v32)
        {
            return new Vector3(v31.X - v32.X, v31.Y - v32.Y, v31.Z - v32.Z);
        }
        public static bool Is2Pow(int num)
        {
            int j = 1;
            for (int i = 0; i < 16; i++)
            {
                j <<= 1;
                if (num == j)
                {
                    return true;
                }
            }
            return false;
        }
        public static void EnumDir(string root, List<string> list)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(root);
            System.IO.DirectoryInfo[] dis = di.GetDirectories();
            for (int i = 0; i < dis.Length; i++)
            {
                list.Add(dis[i].FullName);
                EnumDir(dis[i].FullName, list);
            }
        }
        public static byte[] Compress(byte[] input)
        {
            System.IO.MemoryStream os = new System.IO.MemoryStream();
            zlib.ZOutputStream zos = new zlib.ZOutputStream(os, zlib.zlibConst.Z_BEST_COMPRESSION);
            zos.Write(input, 0, input.Length);
            zos.finish();
            zos.Close();
            return os.ToArray();
        }
        public static byte[] Decompress(byte[] input)
        {
            System.IO.MemoryStream os = new System.IO.MemoryStream();
            zlib.ZInputStream zis = new zlib.ZInputStream(new System.IO.MemoryStream(input));

            byte[] output = new byte[256];
            int count = 0;
            do
            {
                count = zis.read(output, 0, 256);
                if (count > 0)
                {
                    os.Write(output, 0, count);
                }
            }
            while (count > 0);
            output = os.ToArray();
            os.Close();
            return output;
        }
        public static byte[] Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter serializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            serializer.Serialize(memStream, obj);
            return memStream.ToArray();
        }
        public static object Deserialize(byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter deserializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream memStream = new System.IO.MemoryStream(data);
            object newobj = deserializer.Deserialize(memStream);
            memStream.Close();
            return newobj;
        }
        public static object LoadObjectXML(string FileName, Type type)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type);
            object o = xs.Deserialize(System.IO.File.OpenRead(FileName));
            return o;
        }
        public static void SaveObjectXML(string FileName, object obj)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            xs.Serialize(System.IO.File.Open(FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write), obj);
        }
        public static object LoadObject(string FileName)
        {
            byte[] data = System.IO.File.ReadAllBytes(FileName);
            data = Decompress(data);
            return Deserialize(data);
        }
        public static void SaveObject(string FileName, object Object)
        {
            byte[] filebuff = Serialize(Object);
            filebuff = Compress(filebuff);
            System.IO.File.WriteAllBytes(FileName, filebuff);
        }
        static System.Random Random = new System.Random(DateTime.Now.Millisecond);
        public static float Roll()
        {
            return (float)Random.NextDouble();
        }
        public static int Roll(int from, int to)
        {
            return Random.Next(from, to + 1);
        }
        public static bool Roll(float border)
        {
            if (Roll() < border)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Color MixColor(Color targetColor, Color filterColor)
        {
            return Color.FromArgb(targetColor.ToArgb() & filterColor.ToArgb());
        }

        public static string[] SplitString(string data, int everyline)
        {
            int line = data.Length / everyline + 1;
            string[] tmp = new string[line];
            if (line > 1)
            {
                int i = 0;
                while (i < line)
                {
                    try
                    {
                        tmp[i] = data.Substring(i * everyline, everyline);
                    }
                    catch
                    {
                        tmp[i] = data.Substring(i * everyline);
                    }
                    i++;
                }
            }
            else
            {
                tmp[0] = data;
            }
            return tmp;
        }

        public static string GenFileName(string header, string ext)
        {
            int i = System.DateTime.Now.GetHashCode();
            i = System.Math.Abs(i);
            return header + i.ToString() + ext;
        }
        public static Point PointAddPoint(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
        public static Point PointSubPoint(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }
        public static System.Drawing.PointF GetPointOnCircle(float r, float deg)
        {
            deg %= 360;
            float degb = deg;
            deg = Radian2Degree(deg);
            float x = 0, y = 0;
            y = (float)System.Math.Sin(deg) * r;
            x = (float)System.Math.Sqrt(r * r - y * y);
            if (degb < 90)
            {
            }
            else if (degb < 180)
            {
                x = 0 - x;
            }
            else if (degb < 270)
            {
                x = 0 - x;
                //y = 0 - y;
            }
            else
            {
                //y = 0 - y;
            }
            return new System.Drawing.PointF((float)(x), (float)(y));
        }
        public static float Radian2Degree(float rad)
        {
            return (float)(rad * System.Math.PI / 180);
        }
    }

}
