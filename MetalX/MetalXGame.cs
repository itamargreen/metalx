using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace MetalX
{
    public class MetalXGame:IDisposable
    {
        #region 成员
        List<MetalXGameCom> metalXGameComs = new List<MetalXGameCom>();
        bool isRunning;
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
        public Settings Settings;
        /// <summary>
        /// 模型管理器
        /// </summary>
        public Models Models;
        /// <summary>
        /// 纹理管理器
        /// </summary>
        public Textures Textures;

        DateTime gameBeginTime;

        //DateTime frameBeginTime, frameEndTime;
        //DateTime frameBeginTimeBak, frameEndTimeBak;
        //bool frameTotalTimeCanRead;

        float targetFPS = 60;
        float FPSValue;

        ulong totalFrames;
        #endregion
        #region 属性
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
        TimeSpan GameTotalTime
        {
            get
            {
                return DateTime.Now - gameBeginTime;
            }
        }
        float GetAverageFPS()
        {
            return (float)(totalFrames / GameTotalTime.TotalSeconds);
        }
        /// <summary>
        /// 平均帧速
        /// </summary>
        public float AverageFPS
        {
            get
            {
                return FPSValue;
            }
            set
            {
                targetFPS = value <= 0 ? 60 : (value >= 60 ? 60 : value);
            }
        }
        //public float FPS
        //{
        //    get
        //    {
        //        return FPSValue;
        //    }
        //    set
        //    {
        //        targetFPS = value <= 0 ? 60 : (value >= 60 ? 60 : value);
        //    }
        //}
        #endregion
        #region 构造方法
        public MetalXGame(string name)
        {
            Name = name;

            Settings = new Settings();

            Devices = new Devices(this);

            Textures = new Textures();
        }
        public MetalXGame()
            : this("MetalXGame")
        { }

        public MetalXGame(string name, System.Windows.Forms.Control control)
        {

            Name = name;

            Settings = new Settings();

            Devices = new Devices(control, this);

            Models = new Models();

            Textures = new Textures();
        }
        public MetalXGame(System.Windows.Forms.Control control)
            : this("MetalXGame", control)
        { }
        #endregion
        #region 方法
        /// <summary>
        /// 启动
        /// </summary>
        public void GO()
        {
            gameBeginTime = DateTime.Now;
            if (Devices.Window != null)
            {
                Devices.Window.Show();
            }
            totalFrames = 0;
            SetLight(false);
            SetCamera(
                new Vector3(0, 0, -(float)(Devices.D3DDevSizePixel.Height / 2f / Math.Tan(22.5 * Math.PI / 180.0))),
                new Vector3(0, 0, 0));
            isRunning = true;
            while (isRunning)
            {
                Frame();

                FPSValue = GetAverageFPS();
                WaitFrameByAverageFPS();
            }
        }
        /// <summary>
        /// 每帧
        /// </summary>
        void Frame()
        {
            Devices.D3DDev.Clear(Microsoft.DirectX.Direct3D.ClearFlags.Target, Color.White, 1, 0);
            Devices.D3DDev.BeginScene();
            foreach (MetalXGameCom metalXGameCom in metalXGameComs)
            {
                if (metalXGameCom.Enable)
                {
                    metalXGameCom.Code();
                }
                if (metalXGameCom.Visible)
                {
                    metalXGameCom.Draw();
                }
            }
            //Devices.sprite.End();
            Devices.D3DDev.EndScene();
            Devices.D3DDev.Present();
            Application.DoEvents();
            totalFrames++;
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
            do
            {
                FPSValue = GetAverageFPS();
            }
            while (AverageFPS > targetFPS);
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
        public void Exit()
        {
            isRunning = false;
        }
        public void Dispose()
        {
            isRunning = false;
            Devices.Dispose();
            if (Devices.Window != null)
            {
                Devices.Window.Close();
            }
            Application.Exit();
        }
        /// <summary>
        /// 挂载MetalX组件
        /// </summary>
        /// <param name="metalXCom"></param>
        public void MountGameCom(MetalXGameCom metalXGameCom)
        {
            metalXGameComs.Add(metalXGameCom);
        }
        /// <summary>
        /// 设置灯光
        /// </summary>
        /// <param name="value">开/关</param>
        public void SetLight(bool value)
        {
            //Devices.D3DDev.Lights[0].Type = LightType.Directional;
            //Devices.D3DDev.Lights[0].Ambient = Color.White;
            //Devices.D3DDev.Lights[0].Diffuse = Color.White;
            //Devices.D3DDev.Lights[0].Range = 10000f;
            //Devices.D3DDev.Lights[0].Update();
            //Devices.D3DDev.Lights[0].Enabled = value;

            Devices.D3DDev.RenderState.Lighting = value;

            Devices.D3DDev.RenderState.AlphaBlendEnable = true;
            Devices.D3DDev.RenderState.SourceBlend = Microsoft.DirectX.Direct3D.Blend.BothSourceAlpha;
            Devices.D3DDev.RenderState.DestinationBlend = Microsoft.DirectX.Direct3D.Blend.SourceAlpha;

        }
        /// <summary>
        /// 设置镜头
        /// </summary>
        /// <param name="location">位置</param>
        /// <param name="lookAt">视点位置</param>
        public void SetCamera(Vector3 location, Vector3 lookAt)
        {
            Devices.D3DDev.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, (float)Devices.D3DDev.PresentationParameters.BackBufferWidth / (float)Devices.D3DDev.PresentationParameters.BackBufferHeight, -100, 100);
            Devices.D3DDev.Transform.View = Matrix.LookAtLH(location, lookAt, new Vector3(0, 1, 0));

            Devices.D3DDev.Lights[0].Direction = lookAt;
            Devices.D3DDev.Lights[0].Position = location;
        }
        #region Load File Method
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
                    FileStream fs = File.OpenRead(extendedMaterials[i].TextureFilename);
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
            MetalXModel model = (MetalXModel)UtilLib.LoadObject(fileName);

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

            return model;
        }
        /// <summary>
        /// 加载.MXT文件
        /// </summary>
        /// <param name="fileName">文件路径+文件名</param>
        /// <returns>MetalX纹理</returns>
        public MetalXTexture LoadDotMXT(string fileName)
        {
            MetalXTexture texture = new MetalXTexture();
            texture = (MetalXTexture)UtilLib.LoadObject(fileName);
            texture.MEMTexture = TextureLoader.FromStream(Devices.D3DDev, new MemoryStream(texture.TextureData));
            return texture;
        }
        public void LoadAllDotMXT(string pathName)
        {
            //Textures.
            //Textures ts = new Textures();
            List<string> dirName = new List<string>();

            DirectoryInfo di = new DirectoryInfo(pathName);
            //FileInfo[] fis = di.GetFiles("*.mxt");
            //foreach (FileInfo fi in fis)
            //{
            //    MetalXTexture mxt = (MetalXTexture)UtilLib.LoadObject(fi.FullName);
            //    mxt.MEMTexture = TextureLoader.FromStream(Devices.D3DDev, new MemoryStream(mxt.TextureData));
            //    Textures.Add(mxt);
            //}
            FileInfo[] fis;
            UtilLib.EnumDir(pathName, dirName);
            foreach (string pName in dirName)
            {
                di = new DirectoryInfo(pName);
                fis = di.GetFiles("*.mxt");
                foreach (FileInfo fi in fis)
                {
                    MetalXTexture mxt = (MetalXTexture)UtilLib.LoadObject(fi.FullName);
                    mxt.MEMTexture = TextureLoader.FromStream(Devices.D3DDev, new MemoryStream(mxt.TextureData));
                    Textures.Add(mxt);
                }
            }
            //return ts;
        }
        public Scene LoadDotMXScene(string pathName)
        {
            Scene scene = new Scene();
            scene = (Scene)UtilLib.LoadObject(pathName);
            //foreach (TileLayer tl in scene.TileLayers)
            //{
            //    foreach (Tile t in tl.Tiles)
            //    {
            //        foreach (TileFrame tf in t.Frames)
            //        {
            //            string tfname = tf.TextureFileName;
            //            GetTextureIndex(tfname);
            //        }
            //    }
            //}
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
        /// <summary>
        /// 绘制MetalX格式纹理
        /// </summary>
        /// <param name="t">MetalX格式纹理</param>
        /// <param name="loc">位置</param>
        /// <param name="c">颜色</param>
        //public void DrawMetalXTexture(MetalXTexture t, Location loc, Rectangle dz, Color color)
        //{
        //    DrawMetalXTexture(t, new Vector3(loc.Pixel.X, loc.Pixel.Y, 0), dz, color);
        //}
        public void DrawMetalXTexture(MetalXTexture t, Rectangle dz, Point point, Color color)
        {
            DrawMetalXTexture(t, dz, new Vector3(point.X, point.Y, 0), dz.Size, color);
        }
        public void DrawMetalXTexture(MetalXTexture t, Rectangle dz, Rectangle ddz, Color color)
        {
            DrawMetalXTexture(t, dz, new Vector3(ddz.X, ddz.Y, 0), ddz.Size, color);
        }
        public void DrawMetalXTexture(MetalXTexture t, Rectangle dz, Point point, Size size, Color color)
        {
            DrawMetalXTexture(t, dz, new Vector3(point.X, point.Y, 0), size, color);
        }
        /// <summary>
        /// 绘制MetalX格式纹理
        /// </summary>
        /// <param name="t">MetalX格式纹理</param>
        /// <param name="loc">位置</param>
        /// <param name="c">颜色</param>
        public void DrawMetalXTexture(MetalXTexture t, Rectangle dz, Vector3 loc, Size size, Color color)
        {
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
            fx = (float)dz.X / (float)s.Width;
            fy = (float)dz.Y / (float)s.Height;
            tx = ((float)dz.X + (float)dz.Width) / (float)s.Width;
            ty = ((float)dz.Y + (float)dz.Height) / (float)s.Height;

            CustomVertex.PositionColoredTextured[] vertexs = new CustomVertex.PositionColoredTextured[6];
            vertexs[0] = new CustomVertex.PositionColoredTextured(loc, color.ToArgb(), fx, fy);
            vertexs[1] = new CustomVertex.PositionColoredTextured(loc.X + size.Width, loc.Y - size.Height, loc.Z, color.ToArgb(), tx, ty);
            vertexs[2] = new CustomVertex.PositionColoredTextured(loc.X, loc.Y - size.Height, loc.Z, color.ToArgb(), fx, ty);

            vertexs[3] = new CustomVertex.PositionColoredTextured(loc, color.ToArgb(), fx, fy);
            vertexs[4] = new CustomVertex.PositionColoredTextured(loc.X + size.Width, loc.Y, loc.Z, color.ToArgb(), tx, fy);
            vertexs[5] = new CustomVertex.PositionColoredTextured(loc.X + size.Width, loc.Y - size.Height, loc.Z, color.ToArgb(), tx, ty);

            Devices.D3DDev.VertexFormat = CustomVertex.PositionColoredTextured.Format;
            Devices.D3DDev.SetTexture(0, t.MEMTexture);
            Devices.D3DDev.DrawUserPrimitives(PrimitiveType.TriangleList, 2, vertexs);
        }
        public void DrawMetalXTexture(MetalXTexture t, Vector3 loc, Color color)
        {
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
            Rectangle dz = new Rectangle(new Point(), s);

            float fx, fy, tx, ty;
            fx = (float)dz.X / (float)s.Width;
            fy = (float)dz.Y / (float)s.Height;
            tx = ((float)dz.X + (float)dz.Width) / (float)s.Width;
            ty = ((float)dz.Y + (float)dz.Height) / (float)s.Height;

            CustomVertex.PositionColoredTextured[] vertexs = new CustomVertex.PositionColoredTextured[6];
            vertexs[0] = new CustomVertex.PositionColoredTextured(loc, color.ToArgb(), fx, fy);
            vertexs[1] = new CustomVertex.PositionColoredTextured(loc.X + dz.Width, loc.Y - dz.Height, loc.Z, color.ToArgb(), tx, ty);
            vertexs[2] = new CustomVertex.PositionColoredTextured(loc.X, loc.Y - dz.Height, loc.Z, color.ToArgb(), fx, ty);

            vertexs[3] = new CustomVertex.PositionColoredTextured(loc, color.ToArgb(), fx, fy);
            vertexs[4] = new CustomVertex.PositionColoredTextured(loc.X + dz.Width, loc.Y, loc.Z, color.ToArgb(), tx, fy);
            vertexs[5] = new CustomVertex.PositionColoredTextured(loc.X + dz.Width, loc.Y - dz.Height, loc.Z, color.ToArgb(), tx, ty);

            Devices.D3DDev.VertexFormat = CustomVertex.PositionColoredTextured.Format;
            Devices.D3DDev.SetTexture(0, t.MEMTexture);
            Devices.D3DDev.DrawUserPrimitives(PrimitiveType.TriangleList, 2, vertexs);
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
            DrawText(text, point, "新宋体", 10, color);
        }
        public void DrawText(string text, Point point, string fontName, float fontSize, Color color)
        {
            Devices.Sprite.Begin(SpriteFlags.AlphaBlend);
            Devices.Font.DrawText(Devices.Sprite, text, point, color);
            Devices.Sprite.End();
        }
        #endregion
        #region Direct2D
        public void DrawLine(Point fp, Point tp, Color color)
        {
            DrawLine(fp.X, fp.Y, tp.X, tp.Y, color);
        }
        public void DrawLine(float fx, float fy, float tx, float ty, Color color)
        {
            int w, h;
            w = Devices.D3DDev.PresentationParameters.BackBufferWidth;
            h = Devices.D3DDev.PresentationParameters.BackBufferHeight;

            fx -= w / 2;
            tx -= w / 2;
            fy -= h / 2;
            ty -= h / 2;

            fy = 0 - fy;
            ty = 0 - ty;

            Devices.D3DDev.VertexFormat = CustomVertex.PositionColored.Format;

            CustomVertex.PositionColored[] verts = new CustomVertex.PositionColored[2];
            verts[0].Position = new Vector3(fx, fy, 0);
            verts[0].Color = color.ToArgb();
            verts[1].Position = new Vector3(tx, ty, 0);
            verts[1].Color = color.ToArgb();

            Devices.D3DDev.DrawUserPrimitives(PrimitiveType.LineList, 1, verts);
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
        #endregion
    }
}
