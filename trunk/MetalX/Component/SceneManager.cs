using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;

using MetalX.Data;

namespace MetalX.Component
{
    public class SceneManager : GameCom
    {
        int sceneIndex = -1;
        public int SceneIndex
        {
            get
            {
                return sceneIndex;
            }
            //set
            //{
            //    sceneIndex = value;
            //}
        }
        //Scene scene
        //{
        //    get
        //    {
        //        if (sceneIndex < 0)
        //        {
        //            return null;
        //        }
        //        return game.SceneFiles[sceneIndex];
        //    }
        //}
        protected bool PCbeforeNPC = true;
        public Scene SCN;
        public PC ME = new PC();
        public List<NPC> NPCs = new List<NPC>();
        public NPC GetNPC(string name)
        {
            foreach (NPC npc in NPCs)
            {
                if (npc.Name == name)
                {
                    return npc;
                }
            }
            return null;
        }
        public NPC GetNPC(CHR chr)
        {
            //get
            {
                foreach (NPC npc in NPCs)
                {
                    if (npc.RealLocation == chr.FrontLocation)
                    {
                        return npc;
                    }
                }
                foreach (NPC npc in NPCs)
                {
                    if (npc.RealLocation == chr.RangeLocation)
                    {
                        if (SCN.CodeLayer[chr.FrontLocation].IsDesk)
                        {
                            return npc;
                        }
                    }
                }
                return null;
                //if (SCN == null)
                //{
                //    return null;
                //}
                //NPC n = SCN.GetNPC(ME.FrontLocation); 
                //if (n == null)
                //{
                //    if (SCN.CodeLayer[ME.FrontLocation].IsDesk)
                //    {

                //        n = SCN.GetNPC(ME.RangeLocation);
                //    }
                //}
            }
        }
        protected int frameIndex = 0;
        protected DateTime lastFrameBeginTime = DateTime.Now;

        public SceneManager(Game g)
            : base(g)
        {
        }
        //void moveCode(CHR chr)
        //{
        //    if (chr.NeedMovePixel > 0)
        //    {
        //        float movePixel = chr.MoveSpeed;
        //        if (chr.NeedMovePixel < chr.MoveSpeed)
        //        {
        //            movePixel = chr.NeedMovePixel;
        //        }

        //        chr.NeedMovePixel -= movePixel;

        //        if (chr.RealDirection == Direction.U)
        //        {
        //            chr.RealLocationPixel.Y -= movePixel;
        //            if (chr is PC)
        //            {
        //                SCN.RealLocationPixel.Y += movePixel;
        //            }
        //        }
        //        else if (chr.RealDirection == Direction.L)
        //        {
        //            chr.RealLocationPixel.X -= movePixel;
        //            if (chr is PC)
        //            {
        //                SCN.RealLocationPixel.X += movePixel;
        //            }
        //        }
        //        else if (chr.RealDirection == Direction.D)
        //        {
        //            chr.RealLocationPixel.Y += movePixel;
        //            if (chr is PC)
        //            {
        //                SCN.RealLocationPixel.Y -= movePixel;
        //            }
        //        }
        //        else if (chr.RealDirection == Direction.R)
        //        {
        //            chr.RealLocationPixel.X += movePixel;
        //            if (chr is PC)
        //            {
        //                SCN.RealLocationPixel.X -= movePixel;
        //            }
        //        }

        //        if (chr is PC)
        //        {
        //            if (chr.NeedMovePixel ==0)
        //            {
        //                try
        //                {
        //                    if (SCN != null)
        //                    {
        //                        string sname = SCN.CodeLayer[chr.RealLocation].SceneFileName;
        //                        if (sname != null)
        //                        {
        //                            //int dx = scene.CodeLayer[chr.RealLocation].DefaultLocation.X;
        //                            //int dy = scene.CodeLayer[chr.RealLocation].DefaultLocation.Y;
        //                            game.SceneManager.Enter(sname, SCN.CodeLayer[chr.RealLocation].DefaultLocation);
        //                            chr.RealDirection = SCN.CodeLayer[chr.RealLocation].DefaultDirection;
        //                        }
        //                    }
        //                }
        //                catch { }
        //            }
        //        }
        //    }
        //}
        protected void moveCode()
        {
            foreach (NPC npc in NPCs)
            {
                npc.MovePixel();
            }
            if (ME.NeedMovePixel > 0)
            {
                SCN.MovePixel(ME.MoveSpeed);

                ME.MovePixel();

                if (ME.NeedMovePixel == 0)
                {
                    string sname = SCN[(int)ME.RealLocation.Y,(int)ME.RealLocation.X].SceneFileName;
                    if (sname != null)
                    {
                        Enter(sname, SCN[(int)ME.RealLocation.Y,(int)ME.RealLocation.X].DefaultLocation);
                        ME.Face(SCN[(int)ME.RealLocation.Y,(int)ME.RealLocation.X].DefaultDirection);
                    }
                }
            }
        }
        protected void frameCode()
        {
            if (SCN == null)
            {
                return;
            }
            TimeSpan ts = DateTime.Now - lastFrameBeginTime;
            if (ts.TotalMilliseconds > SCN.FrameInterval)
            {
                lastFrameBeginTime = DateTime.Now;
                frameIndex++;
            }
        }
        protected void doorCode()
        {
            foreach (NPC npc in NPCs)
            {
                if (npc.IsDoor)
                {
                    Vector3 v33 = npc.RealLocation;
                    if (v33 == ME.FrontLocation)
                    {
                        PCbeforeNPC = true;
                    }
                    else
                    {
                        //if (ME.NeedMovePixel > 0)
                        v33.Y++;
                        if (v33 == ME.RealLocation)
                        {
                            if (npc.Invisible == false)
                            {
                                npc.Invisible = true;
                                game.PlayMP3Audio(2, game.AudioFiles["door"].FullName);
                            }
                        }
                        else if (v33 == ME.LastLocation)
                        {
                            //

                            if (ME.RealDirection == Direction.U)
                            {
                                PCbeforeNPC = true;
                            }
                            else
                            {
                                PCbeforeNPC = false;
                            }
                            if (npc.Invisible)
                            {
                                npc.Invisible = false;
                                game.PlayMP3Audio(2, game.AudioFiles["door"].FullName);
                            }
                        }
                    }
                }
            }
        }
        void scriptCode()
        {
            if (ME.CanControl == false)
            {
                return;
            }
            if (ME.NeedMovePixel > 0)
            {
                return;
            }
            string spt = SCN[ME.RealLocation].Script;
            if (spt != null)
            {
                if (spt != string.Empty)
                {
                    ME.CanControl = false;
                    game.AppendScript(spt);
                    game.AppendScript("me setctrl");
                    game.ExecuteScript();
                }
            }
        }
        public override void Code()
        {
            if (SCN == null)
            {
                return;
            }

            frameCode();
            moveCode();
            doorCode();
            scriptCode();
        }

        public override void Draw()
        {
            if (SCN == null)
            {
                return;
            }
            DrawBorder(SCN);
            DrawSceneTest(SCN);
        }

        public void Enter(string fileName, Point p)
        {
            Enter(fileName, Util.Point2Vector3(p, 0));
        }
        public void Enter(string fileName, Vector3 realLoc)
        {
            Delay(500);

            game.AppendScript(@"scene fallout 0");
            game.ExecuteScript();

            game.PlayMP3Audio(2, game.AudioFiles["scene"].FullName);
            string mus = "";
            try
            {
                mus = SCN.MusicNames[0];
            }
            catch { }
            SCN = null;
            SCN = game.LoadDotMXScene(game.SceneFiles[fileName].FullName);
            SCN.Init();
            game.Options.TileSizePixel = SCN.TileSizePixel;

            InitNPCs();

            SceneJump(realLoc);

            if (SCN.MusicNames != null)
            {
                if (SCN.MusicNames.Count > 0)
                {
                    if (mus != SCN.MusicNames[0])
                    {
                        game.PlayMP3Audio(1, game.AudioFiles[SCN.MusicNames[0]].FullName, true);
                    }
                }
                else
                {
                    game.StopAudio();
                }
            }

            game.AppendScript(@"delay 500");
            game.AppendScript(@"scene fallin 300");
            game.AppendScript(@"delay 300");
            game.ExecuteScript();
        }
        public void InitNPCs()
        {
            NPCs.Clear();
            if (SCN.NPCNames != null)
            {
                for (int i = 0; i < SCN.NPCNames.Count; i++)
                {
                    string name = SCN.NPCNames[i];
                    try
                    {
                        NPCs.Add(game.LoadDotMXNPC(game.NPCFiles[name].FullName));
                    }
                    catch
                    {
                        SCN.NPCNames.Remove(name);
                    }
                }
                //foreach (string name in SCN.NPCNames)
                //{
                //    try
                //    {
                //        NPCs.Add(game.LoadDotMXNPC(game.NPCFiles[name].FileName));
                //    }
                //    catch
                //    {
                //        SCN.NPCNames.Remove(name);
                //    }
                //}
            }
        }
        public void SceneJump(Vector3 realLoc)
        {
            Vector3 cp = new Vector3(game.Options.WindowSize.Width / 2, game.Options.WindowSize.Height / 2, 0);

            ME.SetRealLocation(realLoc, game.Options.TilePixel);
            Vector3 v3 = new Vector3(-realLoc.X, -realLoc.Y, 0);
            v3 = Util.Vector3AddVector3(v3, cp);
            SCN.SetRealLocation(v3, game.Options.TilePixel);
        }

        //bool IsInWindow(Point p)
        //{
        //    if (p.X < (0 - 1) * game.Options.TileSizePixelX.Width || p.Y < (0 - 1) * game.Options.TileSizePixelX.Height || p.X > (game.Options.WindowSizePixel.Width / game.Options.TileSizePixelX.Width + 1) * game.Options.TileSizePixelX.Width || p.Y > (game.Options.WindowSizePixel.Height / game.Options.TileSizePixelX.Height + 1) * game.Options.TileSizePixelX.Height)
        //    {
        //        return false;
        //    }
        //    return true;
        //}      
        //bool IsInWindow(Point p)
        //{
        //    if (p.X < (0 - 1) || p.Y < (0 - 1)  || p.X > (game.Options.WindowSizePixel.Width / game.Options.TileSizePixelX.Width + 1)  || p.Y > (game.Options.WindowSizePixel.Height / game.Options.TileSizePixelX.Height + 1) )
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        protected void DrawBorder(Scene s)
        {
            if (s.BottomTile == null)
            {
                return;
            }
            int wr = game.Options.WindowSizePixel.Width / game.Options.TileSizePixelX.Width;
            int hr = game.Options.WindowSizePixel.Height / game.Options.TileSizePixelX.Height;

            //int x = 0;
            //int y = 0;
            int w = 0;
            int h = 0;
            Vector3 loc;
            Vector3 loco;

            Point sp = Util.PointDivInt(s.RealLocationPixelPoint, game.Options.TilePixel);
            w = sp.X;
            h = sp.Y;
            //if (w > 0)
            //{
            //    w++;
            //}
            //if (h > 0)
            //{
            //    h++;
            //}
            if (sp.X >= 0)
            {
                loc = new Vector3();
                loco = new Vector3();

                int ti = s.BottomTile.Frames[0].TextureIndex;
                Rectangle dz = s.BottomTile.Frames[0].DrawZone;
                Color cf = s.BottomTile.Frames[0].ColorFilter;

                loc.X = s.RealLocationPixel.X;
                loc.Y = s.RealLocationPixel.Y;
                for (int yy = 0; yy < hr+1 ; yy++)
                {
                    for (int xx = 0; xx < w+1; xx++)
                    {
                        loco.X = xx - w-1;
                        loco.Y = yy - h;
                        game.DrawMetalXTexture(
                            game.Textures[ti],
                            dz,
                            Util.Vector3AddVector3(Util.Vector3AddVector3(loc, ScreenOffsetPixel), Util.Vector3MulInt(loco, game.Options.TilePixel)),
                            game.Options.TileSizePixelX,
                            Util.MixColor(cf, ColorFilter)
                        );
                    }
                }
            }
            if (sp.Y >= 0)
            {
                loc = new Vector3();
                loco = new Vector3();

                int ti = s.BottomTile.Frames[0].TextureIndex;
                Rectangle dz = s.BottomTile.Frames[0].DrawZone;
                Color cf = s.BottomTile.Frames[0].ColorFilter;

                loc.X = s.RealLocationPixel.X;
                loc.Y = s.RealLocationPixel.Y;
                for (int yy = 0; yy < h+1; yy++)
                {
                    for (int xx = 0; xx < wr +1; xx++)
                    {
                        loco.X = xx - w;
                        loco.Y = yy - h - 1;
                        game.DrawMetalXTexture(
                            game.Textures[ti],
                            dz,
                            Util.Vector3AddVector3(Util.Vector3AddVector3(loc, ScreenOffsetPixel), Util.Vector3MulInt(loco, game.Options.TilePixel)),
                            game.Options.TileSizePixelX,
                            Util.MixColor(cf, ColorFilter)
                        );
                    }
                }
            }
            if (sp.X <= 0)
            {
                loc = new Vector3();
                loco = new Vector3();

                int ti = s.BottomTile.Frames[0].TextureIndex;
                Rectangle dz = s.BottomTile.Frames[0].DrawZone;
                Color cf = s.BottomTile.Frames[0].ColorFilter;

                loc.X = s.RealLocationPixel.X;
                loc.Y = s.RealLocationPixel.Y;
                int ww = -w;
                ww += wr - s.Size.Width;
                for (int yy = 0; yy < hr + 1; yy++)
                {
                    for (int xx = 0; xx < ww + 1; xx++)
                    {
                        loco.X = xx + s.Size.Width;
                        loco.Y = yy - h;
                        game.DrawMetalXTexture(
                            game.Textures[ti],
                            dz,
                            Util.Vector3AddVector3(Util.Vector3AddVector3(loc, ScreenOffsetPixel), Util.Vector3MulInt(loco, game.Options.TilePixel)),
                            game.Options.TileSizePixelX,
                            Util.MixColor(cf, ColorFilter)
                        );
                    }
                }
            }
            if (sp.Y <= 0)
            {
                loc = new Vector3();
                loco = new Vector3();

                int ti = s.BottomTile.Frames[0].TextureIndex;
                Rectangle dz = s.BottomTile.Frames[0].DrawZone;
                Color cf = s.BottomTile.Frames[0].ColorFilter;

                loc.X = s.RealLocationPixel.X;
                loc.Y = s.RealLocationPixel.Y;
                int hh = -h;
                hh += hr - s.Size.Height;
                for (int yy = 0; yy < hh + 1; yy++)
                {
                    for (int xx = 0; xx < wr + 1; xx++)
                    {
                        loco.X = xx - w;
                        loco.Y = yy + s.Size.Height;
                        game.DrawMetalXTexture(
                            game.Textures[ti],
                            dz,
                            Util.Vector3AddVector3(Util.Vector3AddVector3(loc, ScreenOffsetPixel), Util.Vector3MulInt(loco, game.Options.TilePixel)),
                            game.Options.TileSizePixelX,
                            Util.MixColor(cf, ColorFilter)
                        );
                    }
                }
            }
            //if (sp.Y + s.Size.Height <= hr)
            //{
            //    loc = new Vector3();
            //    loco = new Vector3();

            //    int ti = s.BottomTile.Frames[0].TextureIndex;
            //    Rectangle dz = s.BottomTile.Frames[0].DrawZone;
            //    Color cf = s.BottomTile.Frames[0].ColorFilter;

            //    loc.X = s.RealLocationPixel.X;
            //    loc.Y = s.RealLocationPixel.Y;
            //    for (int yy = y; yy < Math.Abs(h) + 1; yy++)
            //    {
            //        for (int xx = x; xx < wr + 1; xx++)
            //        {
            //            loco.X = xx - w ;
            //            loco.Y = yy + s.Size.Height;
            //            game.DrawMetalXTexture(
            //                game.Textures[ti],
            //                dz,
            //                Util.Vector3AddVector3(Util.Vector3AddVector3(loc, ScreenOffsetPixel), Util.Vector3MulInt(loco, game.Options.TilePixel)),
            //                game.Options.TileSizePixelX,
            //                Util.MixColor(cf, ColorFilter)
            //            );
            //        }
            //    }
            //}

            //int x = 0, y = 0;
            //int w = s.Size.Width;
            //int h = s.Size.Height;
            //int wr = game.Options.WindowSizePixel.Width / game.Options.TileSizePixelX.Width;
            //int hr = game.Options.WindowSizePixel.Height / game.Options.TileSizePixelX.Height;
            //w += wr;
            //h += hr;

            //x -= wr / 2;
            //y -= hr / 2;

            //int ti = 0;
            //Rectangle dz;
            //Color cf;

            //ti = s.BottomTile.Frames[0].TextureIndex;
            //dz = s.BottomTile.Frames[0].DrawZone;
            //cf = s.BottomTile.Frames[0].ColorFilter;

            //Vector3 loc = new Vector3(x, y, 0);
            //Vector3 loco = new Vector3();
            //for (int yy = y; yy < h; yy++)
            //{
            //    for (int xx = x; xx < w; xx++)
            //    {
            //        loco.X = xx;
            //        loco.Y = yy;
            //        game.DrawMetalXTexture(
            //            game.Textures[ti],
            //            dz,
            //            Util.Vector3AddVector3(Util.Vector3AddVector3(loc, ScreenOffsetPixel), Util.Vector3MulInt(loco, game.Options.TilePixel)),
            //            game.Options.TileSizePixelX,
            //            Util.MixColor(cf, ColorFilter)
            //        );
            //    }
            //}
        }
        protected void DrawSceneTest(Scene s)
        {
            if (s == null)
            {
                return;
            }
            for (int l = 0; l < s.TileLayers.Count; l++)
            {
                int lastl = s[(int)ME.LastLocation.Y,(int)ME.LastLocation.X].DrawLayer;
                int nextl = s[(int)ME.NextLocation.Y,(int)ME.NextLocation.X].DrawLayer;
                Vector3 drawloc=ME.GetDrawLocation(game.Options.TilePixel, lastl, nextl);
                int drawl = s[(int)drawloc.Y,(int)drawloc.X].DrawLayer;
                if (PCbeforeNPC)
                {
                    if (l == drawl)
                    {
                        DrawCHR(ME);
                    }
                }
                if (NPCs != null)
                {
                    foreach (NPC npc in NPCs)
                    {
                        int npcdrawl = s.CodeLayer[npc.GetDrawLocation(game.Options.TilePixel, lastl, nextl)].DrawLayer;
                        if (l == npcdrawl)
                        {
                            DrawCHR(npc);
                        }
                    }
                }
                if (PCbeforeNPC == false)
                {
                    if (l == drawl)
                    {
                        DrawCHR(ME);
                    }
                }
                for (int y = 0; y < s.Size.Height; y++)
                {
                    if (y + SCN.RealLocation.Y < 0 + -1)
                    {
                        //y++;
                        continue;
                    }
                    if (y + SCN.RealLocation.Y > game.Options.WindowSize.Height + 1)
                    {
                        //y++;
                        continue;
                    }
                    for (int x = 0; x < s.Size.Width; x++)
                    {
                        if (x + SCN.RealLocation.X < 0 + -1)
                        {
                            //x++;
                            continue;
                        }
                        if (x + SCN.RealLocation.X > game.Options.WindowSize.Width + 1)
                        {
                            //x++;
                            continue;
                        }
                        Tile t = s[l,y,x];
                        if (t != null)
                        {
                            //if (IsInWindow(Util.PointAddPoint(t.LocationPoint, SCN.RealLocationPoint)))
                            {
                                int fi = t.FrameIndex;
                                if (t.IsAnimation)
                                {
                                    fi = frameIndex;
                                }
                                else
                                {
                                    fi = 0;
                                }
                                game.DrawMetalXTexture(
                                    game.Textures[t[fi].TextureIndex],
                                    t[fi].DrawZone,
                                    //Util.Vector3AddVector3(Util.Vector3AddVector3( s.RealLocation, ScreenOffsetPixel),Util.Point2Vector3( t.RealLocation,0f)),
                                    Util.Vector3AddVector3(Util.Vector3AddVector3(s.RealLocationPixel, ScreenOffsetPixel), Util.Vector3MulInt(t.Location, game.Options.TilePixel)),
                                    game.Options.TileSizePixelX,
                                    Util.MixColor(t[fi].ColorFilter, ColorFilter)
                                );
                            }
                        }
                    }
                }
            }
        }
        //void DrawScene(Scene s)
        //{
        //    if (s == null)
        //    {
        //        return;
        //    }
        //    int l = 0;

        //    foreach (TileLayer tl in s.TileLayers)
        //    {
        //        int lastl = s.CodeLayer[ME.LastLocation].DrawLayer;
        //        int nextl = s.CodeLayer[ME.NextLocation].DrawLayer;
        //        int drawl = s.CodeLayer[ME.GetDrawLocation(game.Options.TilePixel, lastl, nextl)].DrawLayer;
        //        //int nodrawl = s.CodeLayer[ME.FrontLocation].RchDisappear;
        //        if (PCbeforeNPC)
        //        {
        //            if (l == drawl)
        //            {
        //                DrawCHR(ME);
        //            }
        //        }                
        //        if (s.NPCs != null)
        //        {
        //            foreach (NPC GetNPC(ME) in s.NPCs)
        //            {
        //                int npcdrawl = s.CodeLayer[GetNPC(ME).GetDrawLocation(game.Options.TilePixel, lastl, nextl)].DrawLayer;
        //                if (l == npcdrawl)
        //                {
        //                    DrawCHR(GetNPC(ME));
        //                }
        //            }
        //        }
        //        if (PCbeforeNPC == false)
        //        {
        //            if (l == drawl)
        //            {
        //                DrawCHR(ME);
        //            }
        //        }
        //        foreach (Tile t in tl.Tiles)
        //        {
        //            //int nodrawl_now = s.CodeLayer[ME.RealLocation].RchDisappear;
        //            //int nodrawl_last = s.CodeLayer[ME.LastLocation].RchDisappear;
        //            //if (t.Location == ME.RealLocation)
        //            //{
        //            //    if (nodrawl_now == l && nodrawl_last == l)
        //            //    {
        //            //        continue;
        //            //    }
        //            //}
        //            //else if (t.Location == ME.LastLocation)
        //            //{
        //            //    if (nodrawl_last == l)
        //            //    {
        //            //        continue;
        //            //    }
        //            //}

        //            //if (t.Location == ME.LastLocation)
        //            //{
        //            //    int nodrawl = s.CodeLayer[t.Location].RchDisappear;
        //            //    if (nodrawl == l)
        //            //    {
        //            //        continue;
        //            //    }
        //            //}
        //            //if (nodrawl == l)
        //            //{
        //            //    if (t.Location == ME.RealLocation || t.Location == ME.FrontLocation)
        //            //    {
        //            //        continue;
        //            //    }
        //            //}
        //            //if (t.Location != ME.RealLocation)
        //            {
        //                //if (IsInWindow(Util.PointAddPoint(Util.PointMulInt(t.LocationPoint, game.Options.TilePixel), SCN.RealLocationPixelPoint)))
        //                {
        //                    int fi = t.FrameIndex;
        //                    if (t.IsAnimation)
        //                    {
        //                        fi = frameIndex;
        //                    }
        //                    else
        //                    {
        //                        fi = 0;
        //                    }
        //                    game.DrawMetalXTexture(
        //                        game.Textures[t[fi].TextureIndex],
        //                        t[fi].DrawZone,
        //                        //Util.Vector3AddVector3(Util.Vector3AddVector3( s.RealLocation, ScreenOffsetPixel),Util.Point2Vector3( t.RealLocation,0f)),
        //                        Util.Vector3AddVector3(Util.Vector3AddVector3(s.RealLocationPixel, ScreenOffsetPixel), Util.Vector3MulInt(t.Location, game.Options.TilePixel)),
        //                        game.Options.TileSizePixelX,
        //                        Util.MixColor(t[fi].ColorFilter, ColorFilter)
        //                    );
        //                }
        //            }

        //        }
        //        l++;
        //    }
        //}
        //void DrawPC(CHR chr)
        //{
        //    if (chr == null)
        //    {
        //        return;
        //    }
        //    if (chr.Invisible)
        //    {
        //        return;
        //    }
        //    if (chr.TextureName == null)
        //    {
        //        return;
        //    }
        //    if (chr.TextureIndex < 0)
        //    {
        //        chr.TextureIndex = game.Textures.GetIndex(chr.TextureName);
        //    }
        //    Rectangle dz = new Rectangle();
        //    dz.Y = (int)chr.Direction * game.Textures[chr.TextureIndex].TileSizePixel.Height;
        //    if (chr.NeedMovePixel > 0)
        //    {
        //        dz.X = (((int)((float)game.Options.TilePixel - chr.NeedMovePixel)) / (game.Options.TileSizePixelX.Width / 4) + 1) * game.Textures[chr.TextureIndex].TileSizePixel.Width;
        //    }
        //    else
        //    {
        //        dz.X = 0;
        //    }
        //    dz.Size = game.Textures[chr.TextureIndex].TileSizePixel;
        //    Vector3 v31 = chr.RealLocationPixel;
        //    v31.Y += game.Options.SpriteOffsetPixel;
        //    v31.X += scene.RealLocationPixel.X;
        //    v31.Y += scene.RealLocationPixel.Y;
        //    v31.Z += scene.RealLocationPixel.Z;
        //    v31 = Util.Vector3AddVector3(v31, ScreenOffsetPixel);
        //    game.DrawMetalXTexture(
        //        game.Textures[chr.TextureIndex],
        //        dz,
        //        v31,
        //        game.Options.TileSizePixelX,
        //        Util.MixColor( chr.ColorFilter,ColorFilter));
        //}
        protected void DrawCHR(CHR chr)
        {
            if (chr == null)
            {
                return;
            }
            if (chr.TextureName == null)
            {
                return;
            }
            if (chr.Invisible)
            {
                return;
            }
            if (chr.TextureIndex < 0)
            {
                chr.TextureIndex = game.Textures.GetIndex(chr.TextureName);
            }
            Rectangle dz = new Rectangle();
            dz.Y = (int)chr.RealDirection * game.Textures[chr.TextureIndex].TileSizePixel.Height;
            if (chr.NeedMovePixel > 0 && chr.IsRigor == false)
            {
                dz.X = (((int)((float)game.Options.TilePixel - chr.NeedMovePixel)) / (game.Options.TileSizePixelX.Width / 4) + 1) * game.Textures[chr.TextureIndex].TileSizePixel.Width;
            }
            else
            {
                dz.X = 0;
            }
            dz.Size = game.Textures[chr.TextureIndex].TileSizePixel;
            int w = dz.Size.Width * chr.Size.Width;
            int h = dz.Size.Height * chr.Size.Height;
            dz.Size = new Size(w, h);
            Vector3 v31 = chr.RealLocationPixel;
            v31.Y += game.Options.SpriteOffsetPixel;
            v31.X += SCN.RealLocationPixel.X;
            v31.Y += SCN.RealLocationPixel.Y;
            v31.Z += SCN.RealLocationPixel.Z;
            v31 = Util.Vector3AddVector3(v31, ScreenOffsetPixel);

            Size dsize = game.Options.TileSizePixelX;
            dsize.Width *= chr.Size.Width;
            dsize.Height *= chr.Size.Height;

            game.DrawMetalXTexture(
                game.Textures[chr.TextureIndex],
                dz,
                v31,
                dsize,
                Util.MixColor(chr.ColorFilter, ColorFilter));
        }

        //public bool IsNobody()
        //{
        //    return IsNobody(scene, ME.FrontLocation);
        //}
        //bool IsNobody(Scene s, Vector3 v3)
        //{
        //    if (s == null)
        //    {
        //        return true;
        //    }
        //    if (s.NPCs == null)
        //    {
        //        return true;
        //    }
        //    foreach (NPC GetNPC(ME) in s.NPCs)
        //    {
        //        if (GetNPC(ME).RealLocation == v3)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        //bool IsNobody(Scene s, Vector3 v3, out int index)
        //{
        //    if (s == null)
        //    {
        //        index = -1;
        //        return true;
        //    }
        //    if (s.NPCs == null)
        //    {
        //        index = -1;
        //        return true;
        //    } 
        //    index = 0;
        //    foreach (NPC GetNPC(ME) in s.NPCs)
        //    {
        //        if (GetNPC(ME).RealLocation == v3)
        //        {
        //            return false;
        //        }
        //        index++;
        //    }
        //    index = -1;
        //    return true;
        //}
        //public void Move(CHR chr, int stp)
        //{
        //    Move(SCN, chr, chr.RealDirection, stp);
        //}
        //public void Move(Scene s, CHR chr, Direction dir, int stp)
        //{
        //    if (chr.CanMove == false)
        //    {
        //        return;
        //    }
        //    if (chr.CanTurn)
        //    {
        //        chr.LastDirection = chr.RealDirection;
        //        chr.RealDirection = dir;
        //    }
        //    Vector3 loc = chr.FrontLocation;
        //    if (s.IsInScene(loc) == false)
        //    {
        //        return;
        //    }
        //    NPC n = s.GetNPC(loc);
        //    if (n != null)
        //    {
        //        if (n.IsDoor)
        //        {
        //            game.PlayMP3(2, game.AudioFiles["door"].FileName);
        //            n.Invisible = true;
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    try
        //    {
        //        if (s.CodeLayer[loc].CHRCanRch == false)
        //        {
        //            return;
        //        }
        //    }
        //    catch
        //    {
        //        return;
        //    }
        //    if (chr is PC)
        //    {
        //        if (chr.CanControl == false)
        //        {
        //            //chr.RealDirection = chr.LastDirection;
        //            return;
        //        }
        //        if (dir == Direction.U)
        //        {
        //            PCbeforeNPC = true;
        //        }

        //        if (dir == Direction.U)
        //        {
        //            s.RealLocation.Y++;
        //        }
        //        else if (dir == Direction.L)
        //        {
        //            s.RealLocation.X++;
        //        }
        //        else if (dir == Direction.D)
        //        {
        //            s.RealLocation.Y--;
        //        }
        //        else if (dir == Direction.R)
        //        {
        //            s.RealLocation.X--;
        //        }
        //    }
        //    //ME leave door
 
        //    n = s.GetNPC(chr.RealLocation);
        //    if (n != null)
        //    {
        //        if (n.IsDoor)
        //        {
        //            game.PlayMP3(2, game.AudioFiles["door"].FileName);
        //            n.Invisible = false;

        //            if (dir == Direction.D)
        //            {
        //                PCbeforeNPC = false;
        //            }
        //        }

        //        //else if (dir == Direction.U)
        //        //{
        //        //    PCbeforeNPC = true;
        //        //}
        //    }

        //    chr.LastLocation = Util.Vector3DivInt(chr.RealLocationPixel, game.Options.TilePixel);
        //    chr.RealLocation = chr.NextLocation = loc;
        //    chr.NeedMovePixel += game.Options.TilePixel;
        //}
        public override void OnKeyboardDownHoldCode(object sender, int key)
        {
            if (SCN == null)
            {
                return;
            }
            if (ME.CanControl == false)
            {
                return;
            }
            Key k = (Key)key;
            if (ME.NeedMovePixel == 0)
            {

                {
                    if ((k == game.Options.KeyUP || k == game.Options.KeyLEFT || k == game.Options.KeyDOWN || k == game.Options.KeyRIGHT))
                    {
                        Direction dir = Direction.U;
                        if (k == game.Options.KeyUP)
                        {
                            dir = Direction.U;
                        }
                        else if (k == game.Options.KeyLEFT)
                        {
                            dir = Direction.L;
                        }
                        else if (k == game.Options.KeyDOWN)
                        {
                            dir = Direction.D;
                        }
                        else if (k == game.Options.KeyRIGHT)
                        {
                            dir = Direction.R;
                        }

                        if (ME.Face(dir))
                        {
                            SCN.Face(ME.OppositeDirection);
                        }
                        if (ME.Move(SCN, game.SceneManager.GetNPC(ME), game.Options.TilePixel))
                        {
                            SCN.Move(1, game.Options.TilePixel);
                        }
                    }
                }
            }
        }        
        public override void OnKeyboardUpCode(object sender, int key)
        {
            if (SCN == null)
            {
                return;
            }
            if (ME.CanControl == false)
            {
                return;
            }
            Key k = (Key)key;
            if (k == game.Options.KeyYES)
            {
                if (GetNPC(ME) != null)
                {
                    game.AppendScript("me clrctrl");
                    GetNPC(ME).FocusOnMe(ME);
                    if (GetNPC(ME).IsBox)
                    {
                        if (GetNPC(ME).Bag.Count > 0)
                        {
                            game.AppendScript(GetNPC(ME).Script);
                            game.AppendScript("me setctrl");
                            game.ExecuteScript();
                        }
                        else
                        {
                            game.AppendScript("msg 什么都没有");
                            game.AppendScript("untilpress y");
                            game.AppendScript("msg");
                            game.AppendScript("me setctrl");
                            game.ExecuteScript();
                        }
                    }
                    else if (GetNPC(ME).IsDoor)
                    { 
                    }
                    else
                    {
                        //if (GetNPC(ME).Code == string.Empty)
                        //{
                        //    game.AppendScript("msg " + GetNPC(ME).DialogText);
                        //    game.AppendScript("untilpress j");
                        //    game.AppendScript("gui close MessageBox");
                        //    game.AppendScript("unfreezeme");
                        //    game.ExecuteScript();

                        //}
                        //else
                        {
                            game.AppendScript(GetNPC(ME).Script);
                            game.AppendScript("me setctrl");
                            game.AppendScript("npc " + GetNPC(ME).Name + " dir def");
                            game.ExecuteScript();                            
                        }
                    }
                }
                //if (sm.IsNobody() == false)
                //{
                //    if (sm.ME.IsTalking)
                //    {
                //        sm.GetNPC(ME).FocusOnMe(sm.ME);
                //        game.AppendAndExecuteScript("GetNPC(ME) say " + sm.GetNPC(ME).DialogText);
                //    }
                //    else
                //    {
                //        sm.GetNPC(ME).RecoverDirection();
                //        game.AppendAndExecuteScript("gui close NPCTalk");
                //    }
                //}
            }
            //else if (k == Key.K)
            //{
            //    if (sm.GetNPC(ME) != null)
            //    {
            //        game.AppendAndExecuteScript("unfreezeme");
            //        sm.GetNPC(ME).RecoverDirection();
            //        game.AppendAndExecuteScript("gui close NPCTalk");
            //    }
            //}

        }
    }
}
