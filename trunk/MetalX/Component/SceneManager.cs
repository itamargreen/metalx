﻿using System;
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
        //        return game.Scenes[sceneIndex];
        //    }
        //}
        bool PCbeforeNPC = true;
        public Scene SCENE;
        public PC ME
        {
            get
            {
                return game.Characters.ME;
            }
        }
        public NPC FrontNPC
        {
            get
            {
                if (SCENE == null)
                {
                    return null;
                }
                return SCENE.GetNPC(ME.FrontLocation);
            }
        }
        int frameIndex = 0;
        DateTime lastFrameBeginTime = DateTime.Now;

        public SceneManager(Game g)
            : base(g)
        {
        }
        void moveCode(CHR chr)
        {
            if (chr.NeedMovePixel > 0)
            {
                float movePixel = chr.MoveSpeed;
                if (chr.NeedMovePixel < chr.MoveSpeed)
                {
                    movePixel = chr.NeedMovePixel;
                }

                chr.NeedMovePixel -= movePixel;

                if (chr.Direction == Direction.U)
                {
                    chr.RealLocationPixel.Y -= movePixel;
                    if (chr is PC)
                    {
                        SCENE.RealLocationPixel.Y += movePixel;
                    }
                }
                else if (chr.Direction == Direction.L)
                {
                    chr.RealLocationPixel.X -= movePixel;
                    if (chr is PC)
                    {
                        SCENE.RealLocationPixel.X += movePixel;
                    }
                }
                else if (chr.Direction == Direction.D)
                {
                    chr.RealLocationPixel.Y += movePixel;
                    if (chr is PC)
                    {
                        SCENE.RealLocationPixel.Y -= movePixel;
                    }
                }
                else if (chr.Direction == Direction.R)
                {
                    chr.RealLocationPixel.X += movePixel;
                    if (chr is PC)
                    {
                        SCENE.RealLocationPixel.X -= movePixel;
                    }
                }

                if (chr is PC)
                {
                    if (chr.NeedMovePixel ==0)
                    {
                        try
                        {
                            if (SCENE != null)
                            {
                                string sname = SCENE.CodeLayer[chr.RealLocation].SceneFileName;
                                if (sname != null)
                                {
                                    //int dx = scene.CodeLayer[chr.RealLocation].DefaultLocation.X;
                                    //int dy = scene.CodeLayer[chr.RealLocation].DefaultLocation.Y;
                                    game.SceneManager.Enter(sname, SCENE.CodeLayer[chr.RealLocation].DefaultLocation);
                                    chr.Direction = SCENE.CodeLayer[chr.RealLocation].DefaultDirection;
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
        }
        void moveCode()
        {
            if (SCENE == null)
            {
                return;
            }
            moveCode(ME);
            for (int i = 0; i < SCENE.NPCs.Count; i++)
            {
                moveCode(SCENE.NPCs[i]);
            }
        }
        void frameCode()
        {
            if (SCENE == null)
            {
                return;
            }
            TimeSpan ts = DateTime.Now - lastFrameBeginTime;
            if (ts.TotalMilliseconds > SCENE.FrameInterval)
            {
                lastFrameBeginTime = DateTime.Now;
                frameIndex++;
            }
        }
        public override void Code()
        {
            base.Code();
            moveCode();
            frameCode();
        }

        public override void Draw()
        {
            base.Draw();
            if (SCENE == null)
            {
                return;
            }
            DrawBorder(SCENE);
            DrawSceneTest(SCENE);
            NPC n = SCENE.GetNPC("father");
            //if(n!=null)
            //game.DrawText(n.RealLocation.ToString(), new Point(0, 120), Color.White);
        }

        public void Enter(string fileName, Point p)
        {
            Enter(fileName, Util.Point2Vector3(p, 0));
        }
        public void Enter(string fileName, Vector3 realLoc)
        {
            game.AppendScript(@"scene fallout 1");
            game.ExecuteScript();

            Delay(500);

            game.PlayMP3(2, game.Audios["scene"].FileName);
            string mus = "";
            try
            {
                mus = SCENE.BGMusics[0];
            }
            catch { }
            SCENE = null;
            SCENE = game.LoadDotMXScene(game.Scenes[fileName].FileName);
            SCENE.Init();
            game.Options.TileSizePixel = SCENE.TileSizePixel;

            SceneJump(realLoc);

            if (SCENE.BGMusics.Count > 0)
            {
                if (mus != SCENE.BGMusics[0])
                {
                    game.PlayMP3(1, game.Audios[SCENE.BGMusics[0]].FileName, true);
                }
            }
            else
            {
                game.StopAudio();
            }

            game.AppendScript(@"delay 500");
            game.AppendScript(@"scene fallin 300");
            game.AppendScript(@"delay 300");
            game.ExecuteScript();
        }
        public void SceneJump(Vector3 realLoc)
        {
            Vector3 cp = new Vector3(game.Options.WindowSizePixel.Width / game.Options.TileSizePixelX.Width / 2, game.Options.WindowSizePixel.Height / game.Options.TileSizePixel.Height / 2, 0);
          
            MeJump(realLoc);
            Vector3 v3 = new Vector3(-realLoc.X, -realLoc.Y, 0);
            v3 = Util.Vector3AddVector3(v3, cp);
            SCENE.SetRealLocation(v3, game.Options.TilePixel);
        }
        public void MeJump(Vector3 v3)
        {
            //ME.NextLocation = ME.LastLocation = v3;
            ME.SetRealLocation(v3, game.Options.TilePixel);
        }
        public void MeSkin(string name)
        {
            ME.TextureIndex = -1;
            ME.TextureName = name;
        }

        //bool IsInWindow(Point p)
        //{
        //    if (p.X < (0 - 1) * game.Options.TileSizePixelX.Width || p.Y < (0 - 1) * game.Options.TileSizePixelX.Height || p.X > (game.Options.WindowSizePixel.Width / game.Options.TileSizePixelX.Width + 1) * game.Options.TileSizePixelX.Width || p.Y > (game.Options.WindowSizePixel.Height / game.Options.TileSizePixelX.Height + 1) * game.Options.TileSizePixelX.Height)
        //    {
        //        return false;
        //    }
        //    return true;
        //}      
        bool IsInWindow(Point p)
        {
            if (p.X < (0 - 1) || p.Y < (0 - 1)  || p.X > (game.Options.WindowSizePixel.Width / game.Options.TileSizePixelX.Width + 1)  || p.Y > (game.Options.WindowSizePixel.Height / game.Options.TileSizePixelX.Height + 1) )
            {
                return false;
            }
            return true;
        }
        void DrawBorder(Scene s)
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
        void DrawSceneTest(Scene s)
        {
            if (s == null)
            {
                return;
            }
            for (int l = 0; l < s.Tiles.Length; l++)
            {
                int lastl = s.Codes[(int)ME.LastLocation.Y][(int)ME.LastLocation.X].DrawLayer;
                int nextl = s.Codes[(int)ME.NextLocation.Y][(int)ME.NextLocation.X].DrawLayer;
                Vector3 drawloc=ME.GetDrawLocation(game.Options.TilePixel, lastl, nextl);
                int drawl = s.Codes[(int)drawloc.Y][(int)drawloc.X].DrawLayer;
                if (PCbeforeNPC)
                {
                    if (l == drawl)
                    {
                        DrawCHR(ME);
                    }
                }
                if (s.NPCs != null)
                {
                    foreach (NPC FrontNPC in s.NPCs)
                    {
                        int npcdrawl = s.CodeLayer[FrontNPC.GetDrawLocation(game.Options.TilePixel, lastl, nextl)].DrawLayer;
                        if (l == npcdrawl)
                        {
                            DrawCHR(FrontNPC);
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
                for (int y = 0; y < s.Tiles[l].Length; y++)
                {
                    for (int x = 0; x < s.Tiles[l][y].Length; x++)
                    {
                        Tile t = s.Tiles[l][y][x];
                        if (t != null)
                        {
                            if (IsInWindow(Util.PointAddPoint(t.LocationPoint, SCENE.RealLocationPoint)))
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
        void DrawScene(Scene s)
        {
            if (s == null)
            {
                return;
            }
            int l = 0;

            foreach (TileLayer tl in s.TileLayers)
            {
                int lastl = s.CodeLayer[ME.LastLocation].DrawLayer;
                int nextl = s.CodeLayer[ME.NextLocation].DrawLayer;
                int drawl = s.CodeLayer[ME.GetDrawLocation(game.Options.TilePixel, lastl, nextl)].DrawLayer;
                //int nodrawl = s.CodeLayer[ME.FrontLocation].RchDisappear;
                if (PCbeforeNPC)
                {
                    if (l == drawl)
                    {
                        DrawCHR(ME);
                    }
                }                
                if (s.NPCs != null)
                {
                    foreach (NPC FrontNPC in s.NPCs)
                    {
                        int npcdrawl = s.CodeLayer[FrontNPC.GetDrawLocation(game.Options.TilePixel, lastl, nextl)].DrawLayer;
                        if (l == npcdrawl)
                        {
                            DrawCHR(FrontNPC);
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
                foreach (Tile t in tl.Tiles)
                {
                    //int nodrawl_now = s.CodeLayer[ME.RealLocation].RchDisappear;
                    //int nodrawl_last = s.CodeLayer[ME.LastLocation].RchDisappear;
                    //if (t.Location == ME.RealLocation)
                    //{
                    //    if (nodrawl_now == l && nodrawl_last == l)
                    //    {
                    //        continue;
                    //    }
                    //}
                    //else if (t.Location == ME.LastLocation)
                    //{
                    //    if (nodrawl_last == l)
                    //    {
                    //        continue;
                    //    }
                    //}

                    //if (t.Location == ME.LastLocation)
                    //{
                    //    int nodrawl = s.CodeLayer[t.Location].RchDisappear;
                    //    if (nodrawl == l)
                    //    {
                    //        continue;
                    //    }
                    //}
                    //if (nodrawl == l)
                    //{
                    //    if (t.Location == ME.RealLocation || t.Location == ME.FrontLocation)
                    //    {
                    //        continue;
                    //    }
                    //}
                    //if (t.Location != ME.RealLocation)
                    {
                        if (IsInWindow(Util.PointAddPoint(Util.PointMulInt(t.LocationPoint, game.Options.TilePixel), SCENE.RealLocationPixelPoint)))
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
                l++;
            }
        }
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
        void DrawCHR(CHR chr)
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
            dz.Y = (int)chr.Direction * game.Textures[chr.TextureIndex].TileSizePixel.Height;
            if (chr.NeedMovePixel > 0)
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
            v31.X += SCENE.RealLocationPixel.X;
            v31.Y += SCENE.RealLocationPixel.Y;
            v31.Z += SCENE.RealLocationPixel.Z;
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
        bool IsInScene(Scene s, Vector3 p)
        {
            if (p.X < 0)
            {
                return false;
            }
            if (p.Y < 0)
            {
                return false;
            }
            if (p.X >= s.Size.Width)
            {
                return false;
            }
            if (p.Y >= s.Size.Height)
            {
                return false;
            }
            return true;
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
        //    foreach (NPC FrontNPC in s.NPCs)
        //    {
        //        if (FrontNPC.RealLocation == v3)
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
        //    foreach (NPC FrontNPC in s.NPCs)
        //    {
        //        if (FrontNPC.RealLocation == v3)
        //        {
        //            return false;
        //        }
        //        index++;
        //    }
        //    index = -1;
        //    return true;
        //}
        public void Move(Scene s, CHR chr, Direction dir, int stp)
        {
            if (chr.CanMove == false)
            {
                return;
            }
            if (chr.CanTurn)
            {
                chr.Direction = dir;
            }
            Vector3 loc = chr.FrontLocation;
            if (IsInScene(s, loc) == false)
            {
                return;
            }
            NPC n = s.GetNPC(loc);
            if (n != null)
            {
                if (n.IsDoor)
                {
                    game.PlayMP3(2, game.Audios["door"].FileName);
                    n.Invisible = true;
                }
                else
                {
                    return;
                }
            }
            try
            {
                if (s.CodeLayer[loc].CHRCanRch == false)
                {
                    return;
                }
            }
            catch
            {
                return;
            }
            if (chr is PC)
            {
                if (chr.CanControl == false)
                {
                    return;
                }
                if (dir == Direction.U)
                {
                    PCbeforeNPC = true;
                }
                if (dir == Direction.U)
                {
                    s.RealLocation.Y++;
                }
                else if (dir == Direction.L)
                {
                    s.RealLocation.X++;
                }
                else if (dir == Direction.D)
                {
                    s.RealLocation.Y--;
                }
                else if (dir == Direction.R)
                {
                    s.RealLocation.X--;
                }
            }
            //ME leave door
 
            n = s.GetNPC(chr.RealLocation);
            if (n != null)
            {
                if (n.IsDoor)
                {
                    game.PlayMP3(2, game.Audios["door"].FileName);
                    n.Invisible = false;
                }
                if (dir == Direction.D)
                {
                    PCbeforeNPC = false;
                }
                //else if (dir == Direction.U)
                //{
                //    PCbeforeNPC = true;
                //}
            }

            chr.LastLocation = Util.Vector3DivInt(chr.RealLocationPixel, game.Options.TilePixel);
            chr.RealLocation = chr.NextLocation = loc;
            chr.NeedMovePixel += game.Options.TilePixel;
        }
        public override void OnKeyboardDownHoldCode(object sender, int key)
        {
            Key k = (Key)key;

            if (SCENE == null)
            {
                return;
            }
            if (ME.NeedMovePixel == 0)
            {
                if ((k == Key.W || k == Key.A || k == Key.S || k == Key.D))
                {
                    Direction dir = Direction.U;
                    if (k == Key.W)
                    {
                        dir = Direction.U;
                    }
                    else if (k == Key.A)
                    {
                        dir = Direction.L;
                    }
                    else if (k == Key.S)
                    {
                        dir = Direction.D;
                    }
                    else
                    {
                        dir = Direction.R;
                    }
                    Move(SCENE, ME, dir, 1);
                }
            }
        }        
        public override void OnKeyboardUpCode(object sender, int key)
        {
            Key k = (Key)key;
            if (k == Key.J)
            {
                if (FrontNPC != null)
                {
                    if (ME.CanControl == false)
                    {
                        return;
                    }
                    game.AppendScript("freezeme");
                    FrontNPC.FocusOnMe(ME);
                    if (FrontNPC.IsBox)
                    {
                        if (FrontNPC.Bag.Count > 0)
                        {
                            game.AppendScript(FrontNPC.Code);
                            game.AppendScript("unfreezeme");
                            game.ExecuteScript();
                        }
                        else
                        {
                            game.AppendScript("msg 什么都没有");
                            game.AppendScript("untilpress j");
                            game.AppendScript("gui close MessageBox");
                            game.AppendScript("unfreezeme");
                            game.ExecuteScript();
                        }
                    }
                    else if (FrontNPC.IsDoor)
                    { 
                    }
                    else
                    {
                        if (FrontNPC.Code == null)
                        {
                            game.AppendScript("msg " + FrontNPC.DialogText);
                            game.AppendScript("untilpress j");
                            game.AppendScript("unfreezeme");
                            game.ExecuteScript();

                        }
                        else
                        {
                            game.AppendScript(FrontNPC.Code);
                            game.AppendScript("unfreezeme");
                            game.ExecuteScript();
                        }
                    }
                }
                //if (sm.IsNobody() == false)
                //{
                //    if (sm.ME.IsTalking)
                //    {
                //        sm.FrontNPC.FocusOnMe(sm.ME);
                //        game.AppendAndExecuteScript("FrontNPC say " + sm.FrontNPC.DialogText);
                //    }
                //    else
                //    {
                //        sm.FrontNPC.RecoverDirection();
                //        game.AppendAndExecuteScript("gui close NPCTalk");
                //    }
                //}
            }
            //else if (k == Key.K)
            //{
            //    if (sm.FrontNPC != null)
            //    {
            //        game.AppendAndExecuteScript("unfreezeme");
            //        sm.FrontNPC.RecoverDirection();
            //        game.AppendAndExecuteScript("gui close NPCTalk");
            //    }
            //}

        }
    }
}
