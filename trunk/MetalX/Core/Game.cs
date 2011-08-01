using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Threading;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.DirectSound;

using MetalX.Data;
using MetalX.Component;
using MetalX.Resource;
using MetalX.Net;
using MetalX.File;

namespace MetalX
{
    public class Game : IDisposable
    {
        #region 成员
        public Clinet NetManager = new Clinet();
        /// <summary>
        /// 名字
        /// </summary>
        public string Name;
        /// <summary>
        /// 硬件资源管理器
        /// </summary>
        public Devices Devices;
        /// <summary>
        /// 设置管理器
        /// </summary>
        public Options Options;
        /// <summary>
        /// 模型管理器
        /// </summary>
        public Models Models;
        /// <summary>
        /// 纹理管理器
        /// </summary>
        public Textures Textures;
        /// <summary>
        /// 音频管理器
        /// </summary>
        public FileLib AudioFiles;
        public FileLib SceneFiles;
        public FileLib NPCFiles;
        public FileLib ScriptFiles;
        public FormBoxes FormBoxes;
        /// <summary>
        /// 帧开始时间
        /// </summary>
        public DateTime frameBeginTime;
        /// <summary>
        /// 帧开始时间
        /// </summary>
        public TimeSpan frameTimeSpan;
        SoundManager SoundManager1;
        SoundManager SoundManager2;
        KeyboardManager KeyboardManager;
        public FormBoxManager FormBoxManager;
        public SceneManager SceneManager;
        ScriptManager ScriptManager;
        //DateTime frameBeginTime, frameEndTime;
        //DateTime frameBeginTimeBak, frameEndTimeBak;
        //bool frameTotalTimeCanRead;

        bool isRunning = true;
        public List<GameCom> GameComs = new List<GameCom>();
        DateTime gameBeginTime;
        float targetFPS = 60;
        ulong totalFrames = 0;

        #endregion
        #region 属性
        //public int TilePixel
        //{
        //    get
        //    {
        //        return Options.TileSizeX.Width;
        //    }
        //}

        //public Vector3 CenterLocation
        //{
        //    get
        //    {
        //        return new Vector3(
        //            (Options.WindowSize.Width / Options.TileSizeX.Width / 2 + 1) * TilePixel,
        //            (Options.WindowSize.Height / Options.TileSizeX.Height / 2 + 1) * TilePixel, 0);
        //    }
        //}
        //public bool FPSCanRead
        //{
        //    get
        //    {
        //        return frameTotalTimeCanRead;
        //    }
        //}
        //DateTime FrameBeginTime
        //{
        //    set
        //    {
        //        //frameEndTimeBak = frameEndTime;
        //        frameTotalTimeCanRead = false;
        //        frameBeginTime = value;
        //    }
        //}
        //DateTime FrameEndTime
        //{
        //    set
        //    {
        //        //frameBeginTimeBak = frameBeginTime;                
        //        frameTotalTimeCanRead = true;
        //        frameEndTime = value;
        //    }
        //}
        //TimeSpan FrameTotalTime
        //{
        //    get
        //    {
        //        return frameEndTime - frameBeginTime;
        //    }
        //}
        /// <summary>
        /// 设置图标
        /// </summary>
        public string Icon
        {
            set
            {
                Devices.GameWindow.Icon = new Icon(value);
            }
        }
        /// <summary>
        /// 运行时常
        /// </summary>
        public TimeSpan GameTotalTime
        {
            get
            {
                return DateTime.Now - gameBeginTime;
            }
        }
        /// <summary>
        /// 平均帧速
        /// </summary>
        public float AverageFPS
        {
            get
            {
                return (float)(totalFrames / GameTotalTime.TotalSeconds);
            }
            set
            {
                targetFPS = value <= 0 ? 60 : (value >= 60 ? 60 : value);
            }
        }
        public float FPS
        {
            get
            {
                try
                {
                    return (float)(1 / frameTimeSpan.TotalSeconds);
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                targetFPS = value <= 0 ? 60 : (value >= 60 ? 60 : value);
            }
        }
        #endregion
        #region 构造方法
        public Game()
            : this("MetalXGame")
        { }

        public Game(System.Windows.Forms.Control control)
            : this("MetalXGame", control)
        { }

        public Game(string name)
        {
            Name = name;
            Options = new Options();
            try
            {
                Options = (Options)Util.LoadObjectXML("options.xml", typeof(Options));
            }
            catch { }
            Devices = new Devices(this);
            Devices.GameWindow.FormClosing += new FormClosingEventHandler(GameWindowClosing);
            Devices.GameWindow.Paint += new PaintEventHandler(GameWindowPaint);

            if (Options.FullScreen)
            {
                ToggleToFullScreen();
            }
        }

        void GameWindowPaint(object sender, PaintEventArgs e)
        {
            if (isRunning)
            {
                frameBeginTime = DateTime.Now;

                frame();

                frameTimeSpan = DateTime.Now - frameBeginTime;

                Devices.GameWindow.Invalidate();
            }
        }
        void GameWindowClosing(object sender, FormClosingEventArgs e)
        {
            Exit();
        }

        public Game(string name, System.Windows.Forms.Control control)
        {
            Name = name;
            Options = new Options();
            try
            {
                Options = (Options)Util.LoadObjectXML("options.xml", typeof(Options));
            }
            catch { }
            Devices = new Devices(this, control);
        }

        #endregion
        #region 方法

        public void InitData()
        {
            AudioFiles = new FileLib();
            SceneFiles = new FileLib();
            NPCFiles = new FileLib();
            ScriptFiles = new FileLib();

            Models = new Models();
            Textures = new Textures();

            FormBoxes = new FormBoxes();

            FormBoxes.Add(new MSGBox(this));
            FormBoxes.Add(new ASKboolBox(this));
        }
        public void InitCom()
        {
            KeyboardManager = new KeyboardManager(this);
            MountGameCom(KeyboardManager);

            SoundManager1 = new SoundManager(this);
            MountGameCom(SoundManager1);

            SoundManager2 = new SoundManager(this);
            MountGameCom(SoundManager2);
            
            SceneManager = new SceneManager(this);
            MountGameCom(SceneManager);

            FormBoxManager = new FormBoxManager(this);
            MountGameCom(FormBoxManager);

            ScriptManager = new ScriptManager(this);
            MountGameCom(ScriptManager);          
        }
        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            gameBeginTime = DateTime.Now;

            totalFrames = 0;

            SetCamera(new Vector3(0, 0, 22.5f), new Vector3(), Options.X);
            SetLight(new Vector3(0, 0, 22.5f), new Vector3(), Options.X, false);

            isRunning = true;
            if (Devices.GameWindow == null)
            {
                while (isRunning)
                {
                    frameBeginTime = DateTime.Now;

                    frame();

                    frameTimeSpan = DateTime.Now - frameBeginTime;
                }
            }
            else
            {
                //Devices.GameWindow.Show();
                //while (isRunning)
                //{
                //    frameBeginTime = DateTime.Now;

                //    frame();

                //    frameTimeSpan = DateTime.Now - frameBeginTime;
                //}
                Application.Run(Devices.GameWindow);
            }
        }
        public void SetResolution(int w,int h)
        {

        }
        public void ToggleToFullScreen()
        {
            isRunning = false;

            Devices.D3DDev.PresentationParameters.Windowed = false;
            Devices.D3DDev.PresentationParameters.BackBufferWidth = Options.WindowSizePixel.Width;
            Devices.D3DDev.PresentationParameters.BackBufferHeight = Options.WindowSizePixel.Height;
            //Devices.D3DDev.Dispose();
            Devices.D3DDev.Reset(Devices.D3DDev.PresentationParameters);

            //Devices.D3DDev.RenderState.AlphaBlendEnable = true;
            ////Devices.D3DDev.RenderState.AlphaTestEnable = true;
            //Devices.D3DDev.RenderState.SourceBlend = Microsoft.DirectX.Direct3D.Blend.SourceAlpha;
            //Devices.D3DDev.RenderState.DestinationBlend = Microsoft.DirectX.Direct3D.Blend.InvSourceAlpha;

            isRunning = true;
        }
        /// <summary>
        /// 每帧
        /// </summary>
        void frame()
        {
            totalFrames++;
            Application.DoEvents();
            if (Devices.D3DDev.Disposed)
            {
                return;
            }
            Devices.D3DDev.Clear(Microsoft.DirectX.Direct3D.ClearFlags.Target, Color.Black, 0, 0);
            Devices.D3DDev.BeginScene();
            for (int i = 0; i < GameComs.Count; i++)
            {
                GameComs[i].BaseCode();
                if (GameComs[i].Enable)
                {
                    GameComs[i].Code();
                } 
                if (GameComs[i].Visible)
                {
                    if (!Devices.D3DDev.Disposed)
                    {
                        if (Options.TextureDrawMode == TextureDrawMode.Direct2D)
                        {
                            Devices.Sprite.Begin(SpriteFlags.AlphaBlend);
                            GameComs[i].Draw();
                            Devices.Sprite.End();
                        }
                        else
                        {
                            GameComs[i].Draw();
                        }
                    }
                }
            }
            //foreach (GameCom metalXGameCom in GameComs)
            //{
            //    if (metalXGameCom.Enable)
            //    {
            //        metalXGameCom.Code();
            //    }
            //    if (metalXGameCom.Visible)
            //    {
            //        metalXGameCom.Draw();
            //    }
            //}
            if (!Devices.D3DDev.Disposed)
            {
                Devices.D3DDev.EndScene();
                Devices.D3DDev.Present();
            }
        }
        //void WaitFrameByFPS()
        //{
        //    while (FPS > targetFPS)
        //    {
        //        FrameEndTime = DateTime.Now;
        //    }
        //}
        /// <summary>
        /// 限制帧速
        /// </summary>
        void WaitFrameByAverageFPS()
        {
            while (AverageFPS > targetFPS) ;
        }
        void WaitFrameByFPS()
        {
            while (FPS > targetFPS) ;
        }
        /// <summary>
        /// 延迟等待
        /// </summary>
        /// <param name="timespan">时长</param>
        //void WaitMilliseconds(double timespan)
        //{
        //    DateTime startTime = DateTime.Now;
        //    TimeSpan pastTimeSpan;
        //    double pastMS = 0;
        //    do
        //    {
        //        pastTimeSpan = DateTime.Now - startTime;
        //        pastMS = pastTimeSpan.TotalMilliseconds;
        //        Application.DoEvents();
        //    }
        //    while (timespan >= pastMS);
        //}
        public void Stop()
        {
            isRunning = false;
        }
        /// <summary>
        /// 结束退出
        /// </summary>
        public void Exit()
        {
            Stop();
            Dispose();
            Application.Exit();
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            isRunning = false;
            Devices.Dispose();
            //if (Devices.Window != null)
            //{
            //    Devices.Window.Close();
            //}
        }
        /// <summary>
        /// 挂载MetalX组件
        /// </summary>
        /// <param name="metalXCom"></param>
        public void MountGameCom(GameCom metalXGameCom)
        {
            GameComs.Add(metalXGameCom);
        }
        /// <summary>
        /// 设置灯光
        /// </summary>
        /// <param name="value">开/关</param>
        public void SetLight(Vector3 location,Vector3 lookAt,float zoom, bool value)
        {
            location.Z = -((float)(Devices.D3DDevSizePixel.Height / 2f / Math.Tan(location.Z * Math.PI / 180.0))) / zoom;

            Devices.D3DDev.Lights[0].Direction = lookAt;
            Devices.D3DDev.Lights[0].Position = location;
            Devices.D3DDev.Lights[0].Enabled = value;

            Devices.D3DDev.RenderState.Lighting = value;

            //Devices.D3DDev.RenderState.CullMode = Cull.None;

            //Devices.D3DDev.Lights[0].Type = LightType.Point;
            //Devices.D3DDev.Lights[0].Ambient = Color.White;
            //Devices.D3DDev.Lights[0].Diffuse = Color.White;

            //Devices.D3DDev.Lights[0].Position = location;
            //Devices.D3DDev.Lights[0].Update();       
        }
        /// <summary>
        /// 设置镜头
        /// </summary>
        /// <param name="location">位置</param>
        /// <param name="lookAt">视点位置</param>
        public void SetCamera(Vector3 location, Vector3 lookAt,float zoom)
        {
            location.Z = -((float)(Devices.D3DDevSizePixel.Height / 2f / Math.Tan(location.Z * Math.PI / 180.0))) / zoom;

            Devices.D3DDev.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, (float)Devices.D3DDev.PresentationParameters.BackBufferWidth / (float)Devices.D3DDev.PresentationParameters.BackBufferHeight, -1000, 100);

            Devices.D3DDev.Transform.View = Matrix.LookAtLH(location, lookAt, new Vector3(0, 1, 0));

            Devices.D3DDev.RenderState.AlphaBlendEnable = true;
            Devices.D3DDev.RenderState.SourceBlend = Microsoft.DirectX.Direct3D.Blend.SourceAlpha;
            Devices.D3DDev.RenderState.DestinationBlend = Microsoft.DirectX.Direct3D.Blend.InvSourceAlpha;

        }
        #region Load File Method
        //public void LoadCheckPoint(int i)
        //{
        //    CheckPoint checkPoint = (CheckPoint)Util.LoadObject("cp" + i.ToString("d2") + ".MXCheckPoint");
        //    Options.TileSizePixel = SceneFiles[checkPoint.SceneName].TileSizePixel;
        //    Characters.ME = checkPoint.ME;
        //    Characters.ME.TextureIndex = -1;
        //    //Characters.ME.MoveSpeed = 1;
        //    //Characters.ME.RealLocation.X -= 96;
        //}
        //public void SaveCheckPoint(int i)
        //{
        //    if (SceneFiles[SceneManager.SceneIndex] == null)
        //    {
        //        return;
        //    }
        //    CheckPoint checkPoint = new CheckPoint();
        //    checkPoint.SceneName = SceneFiles[SceneManager.SceneIndex].Name;
        //    checkPoint.SceneRealLocationPixel = SceneFiles[SceneManager.SceneIndex].RealLocationPixel;
        //    checkPoint.ME = Characters.ME;
        //    Util.SaveObject("CP" + i.ToString("d2") + ".MXCheckPoint", checkPoint);
        //}
        public Scene LoadDotMXScene(string pathName)
        {
            Scene scene = (Scene)Util.LoadObject(pathName);

            for (int i = 0; i < scene.TileLayers.Count; i++)
            {
                for (int j = 0; j < scene.TileLayers[i].Tiles.Count; j++)
                {
                    for (int k = 0; k < scene.TileLayers[i].Tiles[j].Frames.Count; k++)
                    {
                        string tfname = scene.TileLayers[i].Tiles[j].Frames[k].TextureFileName;
                        scene.TileLayers[i].Tiles[j].Frames[k].TextureIndex = Textures.GetIndex(tfname);
                    }
                }
            }
            return scene;
        }
        public NPC LoadDotMXNPC(string pathName)
        {
            NPC npc = (NPC)Util.LoadObject(pathName);
            return npc;
        }
        public Scene LoadDotXMLScene(string pathName)
        {
            Scene scene = (Scene)Util.LoadObjectXML(pathName, typeof(Scene));

            for (int i = 0; i < scene.TileLayers.Count; i++)
            {
                for (int j = 0; j < scene.TileLayers[i].Tiles.Count; j++)
                {
                    for (int k = 0; k < scene.TileLayers[i].Tiles[j].Frames.Count; k++)
                    {
                        string tfname = scene.TileLayers[i].Tiles[j].Frames[k].TextureFileName;
                        scene.TileLayers[i].Tiles[j].Frames[k].TextureIndex = Textures.GetIndex(tfname);
                        scene.TileLayers[i].Tiles[j].Frames[k].ColorFilter = Color.White;
                    }
                }
            }
            //items.Clear();
            //Add(scene);

            return scene;
        }
        public void LoadAllDotMXScene()
        {
            string pathName = Options.RootPath;

            List<string> dirName = new List<string>();
            Util.EnumDir(pathName, dirName);
            foreach (string pName in dirName)
            {
                DirectoryInfo di = new DirectoryInfo(pName);
                FileInfo[] fis = di.GetFiles("*.MXScene");
                foreach (FileInfo fi in fis)
                {
                    SceneFiles.Add(new FileIndexer(fi.FullName));
                }
            }
        }
        /// 加载.PNG文件
        /// </summary>
        /// <param name="fileName">文件路径+文件名</param>
        /// <returns>MetalX纹理</returns>
        public MetalXTexture LoadDotPNG(string fileName, Size defTileSize)
        {
            MetalXTexture texture = new MetalXTexture();
            texture.FileIndexer = new FileIndexer(fileName);
            texture.Name = texture.FileIndexer.FileName;
            texture.TextureData = System.IO.File.ReadAllBytes(fileName);

            Image img = Image.FromStream(new MemoryStream(texture.TextureData));

            Bitmap bmp = new Bitmap(img);

            texture.SizePixel = bmp.Size;
            //bmp.MakeTransparent(Color.Pink);
            texture.TileSizePixel = defTileSize;
            //texture.MEMTexture = new Texture(g.Devices.D3DDev, bmp, Usage.None, Pool.Managed);
            texture.MEMTexture = TextureLoader.FromStream(Devices.D3DDev, new MemoryStream(texture.TextureData), texture.SizePixel.Width, texture.SizePixel.Height, 0, Usage.None, Microsoft.DirectX.Direct3D.Format.A8R8G8B8, Pool.Managed, Filter.Point, Filter.Point, Color.Pink.ToArgb());

            Bitmap bmp2x = new Bitmap(bmp.Size.Width * 2, bmp.Size.Height * 2);
            ////bmp2x.MakeTransparent(Color.Pink);
            //Graphics graph = Graphics.FromImage(bmp2x);
            //graph.InterpolationMode = InterpolationMode.NearestNeighbor;
            //graph.DrawImage(bmp, new Rectangle(new Point(), bmp2x.Size), new Rectangle(new Point(), bmp.Size), GraphicsUnit.Pixel);
            ////texture.MEMTexture2X = new Texture(g.Devices.D3DDev, bmp2x, Usage.None, Pool.Managed);
            //MemoryStream ms = new MemoryStream();
            //bmp2x.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //ms.Position = 0;
            texture.MEMTexture2X = TextureLoader.FromStream(Devices.D3DDev, new MemoryStream(texture.TextureData), bmp2x.Width, bmp2x.Height, 0, Usage.None, Microsoft.DirectX.Direct3D.Format.A8R8G8B8, Pool.Managed, Filter.Point, Filter.Point, Color.Pink.ToArgb());

            bmp.Dispose();
            img.Dispose();
            bmp2x.Dispose();
            //graph.Dispose();

            //g.Textures.Add(texture);
            return texture;
        }
        //public MetalXTexture LoadDotPNG(Bitmap bm, Size defTileSize)
        //{
        //    MetalXTexture texture = new MetalXTexture();

        //    Bitmap bmp = bm;

        //    texture.SizePixel = bmp.Size;
        //    texture.TileSizePixel = defTileSize;
        //    texture.MEMTexture = new Texture(Devices.D3DDev, bmp, Usage.None, Pool.Managed);

        //    Bitmap bmp2x = new Bitmap(bmp.Size.Width * 2, bmp.Size.Height * 2);
        //    texture.MEMTexture2X = new Texture(Devices.D3DDev, bmp2x, Usage.None, Pool.Managed);
        //    //texture.MEMTexture2X.

        //    bmp.Dispose();
        //    //img.Dispose();
        //    bmp2x.Dispose();
        //    //graph.Dispose();

        //    //g.Textures.Add(texture);
        //    return texture;
        //}
        /// <summary>
        /// 加载.MXT文件
        /// </summary>
        /// <param name="fileName">文件路径+文件名</param>
        /// <returns>MetalX纹理</returns>
        public MetalXTexture LoadDotMXT(string fileName)
        {
            MetalXTexture texture = new MetalXTexture();
            texture = (MetalXTexture)Util.LoadObject(fileName);
            texture.MEMTexture = TextureLoader.FromStream(Devices.D3DDev, new MemoryStream(texture.TextureData), texture.SizePixel.Width, texture.SizePixel.Height, 0, Usage.None, Microsoft.DirectX.Direct3D.Format.X8R8G8B8, Pool.Managed, Filter.Point, Filter.Point, Color.Pink.ToArgb());

            return texture;
        }
        public MetalXTexture LoadDotMXT(Bitmap bm)
        {
            MetalXTexture texture = new MetalXTexture();
            texture.MEMTexture = new Texture(Devices.D3DDev, bm, Usage.None, Pool.Managed);

            return texture;
        }
        /// <summary>
        /// 加载.X文件
        /// </summary>
        /// <param name="fileName">文件路径+文件名</param>
        /// <returns>MetalX模型</returns>
        public MetalXModel LoadDotX(string fileName)
        {
            GraphicsStream adj;
            ExtendedMaterial[] extendedMaterials;
            EffectInstance[] effectInstances;

            Mesh mesh = Mesh.FromFile(fileName, MeshFlags.Managed, Devices.D3DDev,
                out adj, out extendedMaterials, out effectInstances);

            MetalXModel model = new MetalXModel();
            MemoryStream tms = new MemoryStream();
            mesh.Save(tms, adj, extendedMaterials, effectInstances, XFileFormat.Compressed);
            //model.MeshData.Position = 0;
            model.MeshData = tms.ToArray();

            model.MEMMesh = mesh;
            model.MEMTextures = new Microsoft.DirectX.Direct3D.Texture[extendedMaterials.Length];
            model.MEMMaterials = new Material[extendedMaterials.Length];
            model.TexturesData = new byte[extendedMaterials.Length][];

            for (int i = 0; i < extendedMaterials.Length; i++)
            {
                if (extendedMaterials[i].TextureFilename != null && extendedMaterials[i].TextureFilename != string.Empty)
                {
                    model.MEMTextures[i] = TextureLoader.FromFile(Devices.D3DDev, extendedMaterials[i].TextureFilename);
                    FileStream fs = System.IO.File.OpenRead(extendedMaterials[i].TextureFilename);
                    byte[] buf = new byte[(int)fs.Length];
                    fs.Read(buf, 0, buf.Length);
                    model.TexturesData[i] = buf;
                    model.MEMMaterials[i] = extendedMaterials[i].Material3D;
                }
            }
            model.MEMCount = extendedMaterials.Length;

            return model;
        }
        /// <summary>
        /// 加载.MXM文件
        /// </summary>
        /// <param name="fileName">文件路径+文件名</param>
        /// <returns>MetalX模型</returns>
        public MetalXModel LoadDotMXM(string fileName)
        {
            MetalXModel model = (MetalXModel)Util.LoadObject(fileName);

            ExtendedMaterial[] extendedMaterials;

            Mesh mesh = Mesh.FromStream(new MemoryStream(model.MeshData), MeshFlags.Managed, Devices.D3DDev,
                   out extendedMaterials);

            model.MEMMesh = mesh;
            model.MEMTextures = new Microsoft.DirectX.Direct3D.Texture[extendedMaterials.Length];
            model.MEMMaterials = new Material[extendedMaterials.Length];

            for (int i = 0; i < extendedMaterials.Length; i++)
            {
                if (extendedMaterials[i].TextureFilename != null && extendedMaterials[i].TextureFilename != string.Empty)
                {
                    model.MEMTextures[i] = TextureLoader.FromStream(Devices.D3DDev, new MemoryStream(model.TexturesData[i]));
                    model.MEMMaterials[i] = extendedMaterials[i].Material3D;
                }
            }
            model.MEMCount = extendedMaterials.Length;

            //Add(model);

            return model;
        }
        public void LoadAllDotMXA()
        {
            string pathName = Options.RootPath;

            List<string> dirName = new List<string>();
            Util.EnumDir(pathName, dirName);
            foreach (string pName in dirName)
            {
                DirectoryInfo di = new DirectoryInfo(pName);
                FileInfo[] fis = di.GetFiles("*.mxa");
                foreach (FileInfo fi in fis)
                {
                    AudioFiles.Add(new FileIndexer(fi.FullName));
                }
            }
        }
        public void LoadAllDotMP3()
        {
            string pathName = Options.RootPath;
            List<string> dirName = new List<string>();
            Util.EnumDir(pathName, dirName);
            foreach (string pName in dirName)
            {
                DirectoryInfo di = new DirectoryInfo(pName);
                FileInfo[] fis = di.GetFiles("*.mp3");
                foreach (FileInfo fi in fis)
                {
                    AudioFiles.Add(new FileIndexer(fi.FullName));
                }
            }
        }
        public void LoadAllDotMXNPC()
        {
            string pathName = Options.RootPath;
            List<string> dirName = new List<string>();
            Util.EnumDir(pathName, dirName);
            foreach (string pName in dirName)
            {
                DirectoryInfo di = new DirectoryInfo(pName);
                FileInfo[] fis = di.GetFiles("*.mxnpc");
                foreach (FileInfo fi in fis)
                {
                    NPCFiles.Add(new FileIndexer(fi.FullName));
                }
            }
        }
        public void LoadAllDotMXScript()
        {
            string pathName = Options.RootPath;
            List<string> dirName = new List<string>();
            Util.EnumDir(pathName, dirName);
            foreach (string pName in dirName)
            {
                DirectoryInfo di = new DirectoryInfo(pName);
                FileInfo[] fis = di.GetFiles("*.mxscript");
                foreach (FileInfo fi in fis)
                {
                    ScriptFiles.Add(new FileIndexer(fi.FullName));
                }
            }
        }
        public void LoadAllDotMXT()
        {
            string pathName = Options.RootPath;

            List<string> dirName = new List<string>();
            {
                Util.EnumDir(pathName, dirName);
                foreach (string pName in dirName)
                {
                    DirectoryInfo di = new DirectoryInfo(pName);
                    FileInfo[] fis = di.GetFiles("*.mxt");
                    foreach (FileInfo fi in fis)
                    {
                        Textures.Add(LoadDotMXT(fi.FullName));
                    }
                }
            }
        }
        public void LoadAllDotPNG( Size defTileSize)
        {
            string pathName = Options.RootPath;

            List<string> dirName = new List<string>();
            Util.EnumDir(pathName, dirName);
            foreach (string pName in dirName)
            {
                DirectoryInfo di = new DirectoryInfo(pName);
                FileInfo[] fis = di.GetFiles("*.png");
                foreach (FileInfo fi in fis)
                {
                    //Textures.LoadDotPNG(this, fi.FullName);
                    MetalXTexture mxt = LoadDotPNG(fi.FullName, defTileSize);
                    Textures.Add(mxt);
                }
            }
        }
        /// <summary>
        /// 加载.MP3文件
        /// </summary>
        /// <param name="fileName">文件路径+文件名</param>
        /// <returns>MetalX音频</returns>
        public MetalXAudio LoadDotMP3(string fileName)
        {
            MetalXAudio mxa = new MetalXAudio();
            mxa.Name = Path.GetFileNameWithoutExtension(fileName);
            mxa.AudioData = System.IO.File.ReadAllBytes(fileName);
            return mxa;
        }
        /// <summary>
        /// 加载.MXA文件
        /// </summary>
        /// <param name="fileName">文件路径+文件名</param>
        /// <returns>MetalX音频</returns>
        public MetalXAudio LoadDotMXA(string fileName)
        {
            MetalXAudio mxa = (MetalXAudio)Util.LoadObject(fileName);
            //Add(mxa);
            return mxa;
        }
        #endregion
        #region DrawMXM
        /// <summary>
        /// 绘制MetalX格式模型
        /// </summary>
        /// <param name="model">MetalX格式模型</param>
        /// <param name="loc">位置</param>
        /// <param name="XYZ">XYZ轴角度</param>
        /// <param name="YPR">YawPitchRoll角度</param>
        public void DrawMetalXModel(MetalXModel model, Vector3 loc, Vector3 XYZ, Vector3 YPR)
        {
            for (int i = 0; i < model.MEMCount; i++)
            {
                Devices.D3DDev.Material = model.MEMMaterials[i];
                Devices.D3DDev.SetTexture(0, model.MEMTextures[i]);
                Devices.D3DDev.Transform.World = Matrix.Translation(loc) * Matrix.RotationX(XYZ.X) * Matrix.RotationY(XYZ.Y) * Matrix.RotationZ(XYZ.Z) * Matrix.RotationYawPitchRoll(YPR.X, YPR.Y, YPR.Z);

                model.MEMMesh.DrawSubset(i);
            }
        }
        #endregion
        #region DrawMXT
        public void DrawMetalXTexture(MetalXTexture t, Rectangle dz, Point point,float rotation, Color color)
        {
            if (Options.TextureDrawMode == TextureDrawMode.Direct3D)
                DrawMetalXTextureDirect3D(t, dz, new Vector3(point.X, point.Y, 0), dz.Size,rotation, color);
            else if (Options.TextureDrawMode == TextureDrawMode.Direct2D)
                DrawMetalXTextureDirect2D(t, dz, point, dz.Size, rotation, color);
        }
        public void DrawMetalXTexture(MetalXTexture t, Rectangle dz, Point point, Size size, float rotation, Color color)
        {
            if (Options.TextureDrawMode == TextureDrawMode.Direct3D)
                DrawMetalXTextureDirect3D(t, dz, new Vector3(point.X, point.Y, 0), size, rotation, color);
            else if (Options.TextureDrawMode == TextureDrawMode.Direct2D)
                DrawMetalXTextureDirect2D(t, dz, point, size,rotation, color);
        }
        public void DrawMetalXTexture(MetalXTexture t, Rectangle dz, Vector3 v3, Size size, float rotation, Color color)
        {
            if (Options.TextureDrawMode == TextureDrawMode.Direct3D)
                DrawMetalXTextureDirect3D(t, dz, v3, size, rotation, color);
            else if (Options.TextureDrawMode == TextureDrawMode.Direct2D)
                DrawMetalXTextureDirect2D(t, dz, Util.Vector32Point(v3), size, rotation, color);
        }        
        //public void DrawMetalXTexture(MetalXTexture t, Rectangle dz, Rectangle ddz, Color color)
        //{
        //    if (Options.TextureDrawMode == TextureDrawMode.Direct3D)
        //        DrawMetalXTextureDirect3D(t, dz, new Vector3(ddz.X, ddz.Y, 0), ddz.Size, color);
        //    else if (Options.TextureDrawMode == TextureDrawMode.Direct2D)
        //        DrawMetalXTextureDirect2D(t, dz, ddz.Location, ddz.Size, color);
        //}        
        //public void DrawMetalXTexture(MetalXTexture t, Rectangle dz, PointF point, Size size, Color color)
        //{
        //    if (Options.TextureDrawMode == TextureDrawMode.Direct3D)
        //        DrawMetalXTextureDirect3D(t, dz, new Vector3(point.X, point.Y, 0), size, color);
        //    else if (Options.TextureDrawMode == TextureDrawMode.Direct2D)
        //        DrawMetalXTextureDirect2D(t, dz, new Vector3(point.X, point.Y, 0), size, color);
        //}
        CustomVertex.PositionColoredTextured[] vertexs = new CustomVertex.PositionColoredTextured[6];
        /// <summary>
        /// 绘制MetalX格式纹理
        /// </summary>
        /// <param name="t">MetalX格式纹理</param>
        /// <param name="loc">位置</param>
        /// <param name="c">颜色</param>
        void DrawMetalXTextureDirect3D(MetalXTexture t, Rectangle dz, Vector3 loc, Size size, float rotation, Color color)
        {
            if (Devices.D3DDev.Disposed)
            {
                return;
            }
            if (t == null)
            {
                return;
            }

            Devices.D3DDev.SetTexture(0, t.MEMTexture);

            int w, h;
            w = Devices.D3DDev.PresentationParameters.BackBufferWidth;
            h = Devices.D3DDev.PresentationParameters.BackBufferHeight;


            loc.X -= w / 2;
            loc.Y -= h / 2;

            loc.Y = -loc.Y;

            Size s = t.SizePixel;

            float fx, fy, tx, ty;


            //if (Util.Is2PowSize(t.SizePixel))
            //{
            //    fx = (float)dz.Left / (float)s.Width;
            //    tx = (float)dz.Right / (float)s.Width;
            //    fy = (float)dz.Top / (float)s.Height;
            //    ty = (float)dz.Bottom / (float)s.Height;
            //}
            //else
            {
                fx = ((float)dz.Left + Options.UVOffsetX) / (float)s.Width;
                tx = ((float)dz.Right + Options.UVOffsetX) / (float)s.Width;
                fy = ((float)dz.Top + Options.UVOffsetY) / (float)s.Height;
                ty = ((float)dz.Bottom + Options.UVOffsetY) / (float)s.Height;
            }

            Vector3 cp = new Vector3(0, 0, 0);

            vertexs[0] = new CustomVertex.PositionColoredTextured(cp, color.ToArgb(), fx, fy);
            vertexs[1] = new CustomVertex.PositionColoredTextured(cp.X + size.Width, cp.Y - size.Height, cp.Z, color.ToArgb(), tx, ty);
            vertexs[2] = new CustomVertex.PositionColoredTextured(cp.X, cp.Y - size.Height, cp.Z, color.ToArgb(), fx, ty);

            vertexs[3] = new CustomVertex.PositionColoredTextured(cp, color.ToArgb(), fx, fy);
            vertexs[4] = new CustomVertex.PositionColoredTextured(cp.X + size.Width, cp.Y, cp.Z, color.ToArgb(), tx, fy);
            vertexs[5] = new CustomVertex.PositionColoredTextured(cp.X + size.Width, cp.Y - size.Height, cp.Z, color.ToArgb(), tx, ty);

            //loc = Util.Vector3MulInt(loc, 2);
            Devices.D3DDev.Transform.World = Matrix.Translation(loc);

            Devices.D3DDev.DrawUserPrimitives(PrimitiveType.TriangleList, 2, vertexs);

            //Devices.VertexBuffer.SetData(vertexs, 0, LockFlags.None);
            //Devices.D3DDev.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
        }
        void DrawMetalXTextureDirect2D(MetalXTexture t, Rectangle dz, Point loc, Size size, float rotation, Color color)
        {
            if (Devices.D3DDev.Disposed)
            {
                return;
            }
            if (t == null)
            {
                return;
            }
            Texture mxt = null;
            if (dz.Width == 0)
            {
                return;
            }
            if (size.Width / dz.Width == 2)
            {
                dz.X = dz.X * 2;
                dz.Y = dz.Y * 2;
                dz.Size = size;
                mxt = t.MEMTexture2X;
            }
            else
            {
                dz.Size = size;
                mxt = t.MEMTexture;
            }
            Devices.Sprite.Draw(mxt, dz, new Vector3(), Util.Point2Vector3(loc, 0), color);
            //Devices.Sprite.Draw2D(mxt, dz, new Rectangle(dz.Location, size), new Point(), 0, loc, color);
        }

        #endregion
        #region DrawText
        /// <summary>
        /// 绘制文本
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="point">位置</param>
        /// <param name="color">颜色</param>
        public void DrawText(string text, Point point, Color color)
        {
            DrawText(text, point, "微软雅黑", 15, color);
        }
        public void DrawText(string text, Point point, string fontName, int fontSize, Color color)
        {
            if (Devices.D3DDev.Disposed)
            {
                return;
            }
            //fontSize = -fontSize;
            if (Devices.Font.Description.FaceName.Substring(0, fontName.Length) != fontName || Devices.Font.Description.Height != fontSize)
            {
                Devices.Font.Dispose();
                Devices.Font = new Microsoft.DirectX.Direct3D.Font(Devices.D3DDev, new System.Drawing.Font(fontName, fontSize - 3));
            }
            //using (Sprite sprite = new Sprite(Devices.D3DDev))
            {
                //using (Microsoft.DirectX.Direct3D.Font font = new Microsoft.DirectX.Direct3D.Font(Devices.D3DDev, new System.Drawing.Font(fontName, fontSize)))
                {
                    //FontDescription fd = new FontDescription();
                    //fd.FaceName = fontName;
                    //fd.Width = fontSize.Width;
                    //fd.Height = fontSize.Height;
                    if (Options.TextureDrawMode == TextureDrawMode.Direct2D)
                    {
                        Devices.Sprite.End();
                        Devices.Sprite.Begin(SpriteFlags.AlphaBlend);
                        Devices.Font.DrawText(Devices.Sprite, text, point, color);
                        Devices.Sprite.End();
                        Devices.Sprite.Begin(SpriteFlags.AlphaBlend);
                    }
                    else
                    {
                        Devices.Sprite.Begin(SpriteFlags.AlphaBlend);

                        Devices.Font.DrawText(Devices.Sprite, text, point, color);

                        Devices.Sprite.End();
                    }
                }
            }
        }
        #endregion
        #region Direct2D
        public void DrawLine(Point fp, Point tp, Color color)
        {
            DrawLine(fp.X, fp.Y, tp.X, tp.Y, color);
        }
        public void DrawLine(float fx, float fy, float tx, float ty, Color color)
        {
            using (Line l = new Line(Devices.D3DDev))
            {
                Vector2[] vects = new Vector2[2];
                vects[0] = new Vector2(fx, fy);
                vects[1] = new Vector2(tx, ty);
                l.Begin();
                l.Draw(vects, color);
                l.End();
            }
            //int w, h;
            //w = Devices.D3DDev.PresentationParameters.BackBufferWidth;
            //h = Devices.D3DDev.PresentationParameters.BackBufferHeight;

            //fx -= w / 2;
            //tx -= w / 2;
            //fy -= h / 2;
            //ty -= h / 2;

            //fy = 0 - fy;
            //ty = 0 - ty;

            //CustomVertex.PositionColored[] verts = new CustomVertex.PositionColored[2];
            //verts[0] = new CustomVertex.PositionColored(fx, fy, 0, color.ToArgb());
            //verts[1] = new CustomVertex.PositionColored(tx, ty, 0, color.ToArgb());

            //Devices.D3DDev.VertexFormat = CustomVertex.PositionColored.Format;
            //Devices.D3DDev.DrawUserPrimitives(PrimitiveType.LineList, 1, verts);

            //vb = new VertexBuffer(typeof(CustomVertex.PositionColored), 2, Devices.D3DDev, Usage.None, CustomVertex.PositionColored.Format, Pool.Managed);
            //vb.SetData(verts, 0, LockFlags.None);
            //Devices.D3DDev.SetStreamSource(0, vb, 0);
            //Devices.D3DDev.DrawPrimitives(PrimitiveType.LineList, 0, 1);
        }
        public void DrawRect(Rectangle rect, Color color)
        {
            DrawRect(rect.X, rect.Y, rect.Width, rect.Height, color);
        }
        public void DrawRect(float fx, float fy, float tx, float ty, Color color)
        {
            tx += fx;
            ty += fy;
            tx--;
            ty--;

            DrawLine(fx, fy, tx, fy, color);
            DrawLine(tx, fy, tx, ty, color);
            DrawLine(tx, ty, fx, ty, color);
            DrawLine(fx, fy, fx, ty, color);
        }
        #endregion
        #region PlayAudio
        public void PlayMetalXAudio(int layer, string name)
        {
            PlayMetalXAudio(layer,name, false);
        }
        public void PlayMetalXAudio(int layer, string name, bool loop)
        {
            if (layer == 1)
            {
                SoundManager1.Loop = loop;
                SoundManager1.PlayMetalXAudio(name);
            }
            else if (layer == 2)
            {
                SoundManager2.Loop = loop;
                SoundManager2.PlayMetalXAudio(name);
            }
        }
        public void PlayMP3Audio(int layer, string fileName)
        {
            PlayMP3Audio(layer, fileName, false);
        }
        public void PlayMP3Audio(int layer, string fileName, bool loop)
        {
            if (layer == 1)
            {
                SoundManager1.Loop = loop;
                SoundManager1.PlayMP3(fileName);
            }
            else if (layer == 2)
            {
                SoundManager2.PlayMP3(fileName);
            }
        }
        public void StopAudio()
        {
            SoundManager1.Stop();
        }
        public void SetVolume(int ch, int val)
        {
            if (ch == 1)
            {
                SoundManager1.Volume = val;
            }
            else if (ch == 2)
            {
                SoundManager2.Volume = val;
            }
        }
        public double AudioPlayingProgress
        {
            get
            {
                return SoundManager1.Progress;
            }
            set
            {
                SoundManager1.Progress = value;
            }
        }
        public bool AudioIsPlaying
        {
            get
            {
                return SoundManager1.Playing;
            }
        }
        #endregion
        public void ReturnScript(bool yes)
        {
            ScriptManager.Return(yes);
        }
        public void ExecuteScript()
        {
            ScriptManager.Execute();
        }
        public void AppendScript(string cmd)
        {
            ScriptManager.AppendCommand(cmd);
        }
        public void AppendDotMetalXScript(string file)
        {
            ScriptManager.AppendDotMetalXScript(file);
        }

        public void OverLoadMessageBox(MSGBox box)
        {
            FormBoxes["MessageBox"] = box;
        }

        public void OverLoadASKboolBox(ASKboolBox box)
        {
            FormBoxes["ASKboolBox"] = box;
        }
        #endregion
    }
}
