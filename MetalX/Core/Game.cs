﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Threading;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using MetalX.Data;
using MetalX.Component;
using MetalX.Resource;

namespace MetalX
{
    public class Game : IDisposable
    {
        #region 成员
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
        //public Audios Audios;
        public Scenes Scenes;
        public Characters Characters;
        public FormBoxes FormBoxes;
        /// <summary>
        /// 帧开始时间
        /// </summary>
        public DateTime frameBeginTime;
        /// <summary>
        /// 帧开始时间
        /// </summary>
        public TimeSpan frameTimeSpan;
        SoundManager SoundManager;
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
        public int SpriteOffsetPixel
        {
            get
            {
                //return 0;
                return -Options.TilePixel / 3;
            }
        }
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
        }

        void GameWindowPaint(object sender, PaintEventArgs e)
        {
            if (isRunning)
            {
                frameBeginTime = DateTime.Now;

                frame();

                frameTimeSpan = DateTime.Now - frameBeginTime;

                Devices.GameWindow.Invalidate();

                //WaitFrameByAverageFPS();
                //Devices.GameWindow.Refresh();
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
            Devices = new Devices(control, this);
        }

        #endregion
        #region 方法

        public void InitData()
        {
            Models = new Models();
            Textures = new Textures();
            //Audios = new Audios();
            Scenes = new Scenes();
            FormBoxes = new FormBoxes();
            Characters = new Characters();
        }
        public void InitCom()
        {
            KeyboardManager = new KeyboardManager(this);
            MountGameCom(KeyboardManager);

            SoundManager = new SoundManager(this);
            MountGameCom(SoundManager);
            
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
        /// <summary>
        /// 每帧
        /// </summary>
        void frame()
        {
            if (Devices.D3DDev.Disposed)
            {
                return;
            }
            Devices.D3DDev.Clear(Microsoft.DirectX.Direct3D.ClearFlags.Target, Color.Black, 0, 0);
            Devices.D3DDev.BeginScene();
            for (int i = 0; i < GameComs.Count; i++)
            {
                if (GameComs[i].Enable)
                {
                    GameComs[i].Code();
                } 
                if (GameComs[i].Visible)
                {
                    GameComs[i].Draw();
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
            if (Devices.D3DDev.Disposed)
            {
                return;
            }
            Devices.D3DDev.EndScene();
            Devices.D3DDev.Present();
            totalFrames++;
            Application.DoEvents();
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
        void WaitMilliseconds(double timespan)
        {
            DateTime startTime = DateTime.Now;
            TimeSpan pastTimeSpan;
            double pastMS = 0;
            do
            {
                pastTimeSpan = DateTime.Now - startTime;
                pastMS = pastTimeSpan.TotalMilliseconds;
                Application.DoEvents();
            }
            while (timespan >= pastMS);
        }
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


            Devices.D3DDev.RenderState.AlphaBlendEnable = true;
            Devices.D3DDev.RenderState.SourceBlend = Microsoft.DirectX.Direct3D.Blend.SourceAlpha;
            Devices.D3DDev.RenderState.DestinationBlend = Microsoft.DirectX.Direct3D.Blend.InvSourceAlpha;

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
        }
        #region Load File Method
        public void LoadCheckPoint(int i)
        {
            CheckPoint checkPoint = (CheckPoint)Util.LoadObject("cp" + i.ToString("d2") + ".MXCheckPoint");
            Options.TileSizePixel = Scenes[checkPoint.SceneName].TileSizePixel;
            Characters.ME = checkPoint.ME;
            Characters.ME.TextureIndex = -1;
            //Characters.ME.MoveSpeed = 1;
            //Characters.ME.RealLocation.X -= 96;
        }
        public void SaveCheckPoint(int i)
        {
            if (Scenes[SceneManager.SceneIndex] == null)
            {
                return;
            }
            CheckPoint checkPoint = new CheckPoint();
            checkPoint.SceneName = Scenes[SceneManager.SceneIndex].Name;
            checkPoint.SceneRealLocation = Scenes[SceneManager.SceneIndex].RealLocation;
            checkPoint.ME = Characters.ME;
            Util.SaveObject("CP" + i.ToString("d2") + ".MXCheckPoint", checkPoint);
        }
        //public void LoadAllDotMXA(string pathName)
        //{
        //    List<string> dirName = new List<string>();
        //    Util.EnumDir(pathName, dirName);
        //    foreach (string pName in dirName)
        //    {
        //        DirectoryInfo di = new DirectoryInfo(pName);
        //        FileInfo[] fis = di.GetFiles("*.mxa");
        //        foreach (FileInfo fi in fis)
        //        {
        //            Audios.LoadDotMXA(fi.FullName);
        //        }
        //    }
        //}
        //public void LoadAllDotMP3(string pathName)
        //{
        //    List<string> dirName = new List<string>();
        //    Util.EnumDir(pathName, dirName);
        //    foreach (string pName in dirName)
        //    {
        //        DirectoryInfo di = new DirectoryInfo(pName);
        //        FileInfo[] fis = di.GetFiles("*.mp3");
        //        foreach (FileInfo fi in fis)
        //        {
        //            //Audios.LoadDotMP3(fi.FullName);
        //            MetalXAudio mxa = Audios.LoadDotMP3(fi.FullName);
        //            Audios.Add(mxa);
        //        }
        //    }
        //}
        public void LoadAllDotMXT(string pathName)
        {
            List<string> dirName = new List<string>();
            {
                Util.EnumDir(pathName, dirName);
                foreach (string pName in dirName)
                {
                    DirectoryInfo di = new DirectoryInfo(pName);
                    FileInfo[] fis = di.GetFiles("*.mxt");
                    foreach (FileInfo fi in fis)
                    {
                        Textures.LoadDotMXT(this,fi.FullName);
                    }
                }
            }
        }
        public void LoadAllDotPNG(string pathName,Size defTileSize)
        {
            List<string> dirName = new List<string>();
            Util.EnumDir(pathName, dirName);
            foreach (string pName in dirName)
            {
                DirectoryInfo di = new DirectoryInfo(pName);
                FileInfo[] fis = di.GetFiles("*.png");
                foreach (FileInfo fi in fis)
                {
                    //Textures.LoadDotPNG(this, fi.FullName);
                    MetalXTexture mxt = Textures.LoadDotPNG(this, fi.FullName, defTileSize);
                    Textures.Add(mxt);
                }
            }
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
        public void DrawMetalXTexture(MetalXTexture t, Rectangle dz, Point point, Color color)
        {
            if (Options.TextureDrawMode == TextureDrawMode.Direct3D)
                DrawMetalXTextureDirect3D(t, dz, new Vector3(point.X, point.Y, 0), dz.Size, color);
            else if (Options.TextureDrawMode == TextureDrawMode.Direct2D)
                DrawMetalXTextureDirect2D(t, dz, point, dz.Size, color);
        }
        //public void DrawMetalXTexture(MetalXTexture t, Rectangle dz, Rectangle ddz, Color color)
        //{
        //    if (Options.TextureDrawMode == TextureDrawMode.Direct3D)
        //        DrawMetalXTextureDirect3D(t, dz, new Vector3(ddz.X, ddz.Y, 0), ddz.Size, color);
        //    else if (Options.TextureDrawMode == TextureDrawMode.Direct2D)
        //        DrawMetalXTextureDirect2D(t, dz, ddz.Location, ddz.Size, color);
        //}
        public void DrawMetalXTexture(MetalXTexture t, Rectangle dz, Point point, Size size, Color color)
        {
            if (Options.TextureDrawMode == TextureDrawMode.Direct3D)
                DrawMetalXTextureDirect3D(t, dz, new Vector3(point.X, point.Y, 0), size, color);
            else if (Options.TextureDrawMode == TextureDrawMode.Direct2D)
                DrawMetalXTextureDirect2D(t, dz, point, size, color);
        }
        //public void DrawMetalXTexture(MetalXTexture t, Rectangle dz, PointF point, Size size, Color color)
        //{
        //    if (Options.TextureDrawMode == TextureDrawMode.Direct3D)
        //        DrawMetalXTextureDirect3D(t, dz, new Vector3(point.X, point.Y, 0), size, color);
        //    else if (Options.TextureDrawMode == TextureDrawMode.Direct2D)
        //        DrawMetalXTextureDirect2D(t, dz, new Vector3(point.X, point.Y, 0), size, color);
        //}
        public void DrawMetalXTexture(MetalXTexture t, Rectangle dz, Vector3 v3, Size size, Color color)
        {
            if (Options.TextureDrawMode == TextureDrawMode.Direct3D)
                DrawMetalXTextureDirect3D(t, dz, v3, size, color);
            else if (Options.TextureDrawMode == TextureDrawMode.Direct2D)
                DrawMetalXTextureDirect2D(t, dz, Util.Vector32Point(v3), size, color);
        }
        /// <summary>
        /// 绘制MetalX格式纹理
        /// </summary>
        /// <param name="t">MetalX格式纹理</param>
        /// <param name="loc">位置</param>
        /// <param name="c">颜色</param>
        void DrawMetalXTextureDirect3D(MetalXTexture t, Rectangle dz, Vector3 loc, Size size, Color color)
        {
            if (Devices.D3DDev.Disposed)
            {
                return;
            }
            if (t == null)
            {
                return;
            }

            int w, h;
            w = Devices.D3DDev.PresentationParameters.BackBufferWidth;
            h = Devices.D3DDev.PresentationParameters.BackBufferHeight;

            loc.X -= w / 2;
            loc.Y -= h / 2;

            loc.Y = 0 - loc.Y;

            Size s = t.SizePixel;

            float fx, fy, tx, ty;
            float offset = 0.1f;

            if (Util.Is2PowSize(t.SizePixel))
            {
                fx = ((float)dz.X) / (float)s.Width;
                fy = ((float)dz.Y) / (float)s.Height;
                tx = ((float)dz.X + (float)dz.Width) / (float)s.Width;
                ty = ((float)dz.Y + (float)dz.Height) / (float)s.Height;
            }
            else
            {
                fx = ((float)dz.X + offset) / (float)s.Width;
                fy = ((float)dz.Y + offset) / (float)s.Height;
                tx = ((float)dz.X + (float)dz.Width + offset) / (float)s.Width;
                ty = ((float)dz.Y + (float)dz.Height + offset) / (float)s.Height;
            }
            Devices.D3DDev.VertexFormat = CustomVertex.PositionColoredTextured.Format;
            Devices.D3DDev.TextureState[0].AlphaOperation = TextureOperation.Modulate;
            Devices.D3DDev.SetTexture(0, t.MEMTexture);

            CustomVertex.PositionColoredTextured[] vertexs = new CustomVertex.PositionColoredTextured[6];
            vertexs[0] = new CustomVertex.PositionColoredTextured(loc, color.ToArgb(), fx, fy);
            vertexs[1] = new CustomVertex.PositionColoredTextured(loc.X + size.Width, loc.Y - size.Height, loc.Z, color.ToArgb(), tx, ty);
            vertexs[2] = new CustomVertex.PositionColoredTextured(loc.X, loc.Y - size.Height, loc.Z, color.ToArgb(), fx, ty);

            vertexs[3] = new CustomVertex.PositionColoredTextured(loc, color.ToArgb(), fx, fy);
            vertexs[4] = new CustomVertex.PositionColoredTextured(loc.X + size.Width, loc.Y, loc.Z, color.ToArgb(), tx, fy);
            vertexs[5] = new CustomVertex.PositionColoredTextured(loc.X + size.Width, loc.Y - size.Height, loc.Z, color.ToArgb(), tx, ty);

            //Devices.D3DDev.DrawUserPrimitives(PrimitiveType.TriangleList, 2, vertexs);

            Devices.VertexBuffer.SetData(vertexs, 0, LockFlags.None);
            Devices.D3DDev.SetStreamSource(0, Devices.VertexBuffer, 0);
            Devices.D3DDev.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
        }
        void DrawMetalXTextureDirect2D(MetalXTexture t, Rectangle dz, Point loc, Size size, Color color)
        {
            if (Devices.D3DDev.Disposed)
            {
                return;
            }
            if (t == null)
            {
                return;
            }

            //loc.Z = 0;
            //using (Sprite sprite = new Sprite(Devices.D3DDev))
            {
                Devices.Sprite.Begin(SpriteFlags.AlphaBlend);

                //if (dz.Width == 0 || size.Width == 0 || dz.Height == 0 || size.Height == 0)
                //{
                //    return;
                //}
                if (size.Width / dz.Width == 2)
                {
                    dz.X = dz.X * 2;
                    dz.Y = dz.Y * 2;
                    dz.Size = size;
                    Devices.Sprite.Draw(t.MEMTexture2X, dz, new Vector3(), Util.Point2Vector3(loc, 0), color);
                    //sprite.Draw2D(t.MEMTexture2X, dz, dz, loc, color);
                }
                else
                {
                    Devices.Sprite.Draw(t.MEMTexture, dz, new Vector3(), Util.Point2Vector3(loc, 0), color);
                    //sprite.Draw2D(t.MEMTexture, dz, new Rectangle(dz.Location, size), loc, color);
                }

                Devices.Sprite.End();
            }
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
            DrawText(text, point, "新宋体", 12, color);
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

                    Devices.Sprite.Begin(SpriteFlags.AlphaBlend);

                    Devices.Font.DrawText(Devices.Sprite, text, point, color);

                    Devices.Sprite.End();

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
        public void PlayMetalXAudio(string name)
        {
            SoundManager.PlayMetalXAudio(name);
        }
        public void PlayMP3(string fileName)
        {
            SoundManager.PlayMP3(fileName);
        }
        public void StopAudio()
        {
            SoundManager.Stop();
        }
        public double AudioPlayingProgress
        {
            get
            {
                return SoundManager.Progress;
            }
            set
            {
                SoundManager.Progress = value;
            }
        }
        public bool AudioIsPlaying
        {
            get
            {
                return SoundManager.Playing;
            }
        }
        #endregion
        public void ExecuteScript()
        {
            ScriptManager.Execute();
        }
        public void AppendScript(string cmd)
        {
            ScriptManager.AppendCommand(cmd);
        }
        public void ExecuteMetalXScript(string file)
        {
            ScriptManager.ExecuteDotMXScript(file);
        }
        //public void AppearFormBox(string name)
        //{
        //    FormBoxManager.Appear(name);
        //}
        //public void AppearFormBox(int i)
        //{
        //    FormBoxManager.Appear(i);
        //}

        //List<FormBoxes2Play> formBoxes2Play;
        //Thread playFormBoxThd;
        //public void PlayFormBox(List< FormBoxes2Play> fb2ps)
        //{
        //    formBoxes2Play = fb2ps;
        //    playFormBoxThd = new Thread(playFormBoxThdFunc);
        //    playFormBoxThd.IsBackground = true;
        //    playFormBoxThd.Start();
        //}
        //void playFormBoxThdFunc()
        //{
        //    foreach (FormBoxes2Play f in formBoxes2Play)
        //    {
        //        FormBoxManager.FallInSceen(0);
        //        AppearFormBox(f.Name);
        //        if (f.TextureEffectList != null)
        //        {
        //            foreach (TextureEffect te in f.TextureEffectList)
        //            {
        //                if (te.Type == TextureEffectType.None)
        //                {
        //                }
        //                else if (te.Type == TextureEffectType.Shock)
        //                {
        //                    FormBoxManager.ShockScreen(te.TimeSpan.TotalMilliseconds);
        //                }
        //                else if (te.Type == TextureEffectType.FallIn)
        //                {
        //                    FormBoxManager.FallInSceen(te.TimeSpan.TotalMilliseconds);
        //                }
        //                else if (te.Type == TextureEffectType.FallOut)
        //                {
        //                    FormBoxManager.FallOutSceen(te.TimeSpan.TotalMilliseconds);
        //                }
        //                if (te.IsBlock)
        //                {
        //                    Thread.Sleep((int)te.TimeSpan.TotalMilliseconds);
        //                }
        //            }
        //        }
        //        FormBoxManager.Disappear();
        //    }
        //}
        #endregion
    }
}
