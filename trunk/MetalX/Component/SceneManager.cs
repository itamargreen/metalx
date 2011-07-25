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
        //        return game.Scenes[sceneIndex];
        //    }
        //}
        public Scene scene;
        public PC me
        {
            get
            {
                return game.Characters.ME;
            }
        }
        public NPC npc
        {
            get
            {
                if (scene == null)
                {
                    return null;
                }
                return scene.GetNPC(me.FrontLocation);
            }
        }
        int frameIndex = 0;
        DateTime lastFrameBeginTime = DateTime.Now;

        public SceneManager(Game g)
            : base(g)
        {
        }
        void MoveCode(CHR chr)
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
                    //if (scene.RealLocationPixel.Y < 0)
                    {
                        scene.RealLocationPixel.Y += movePixel;
                    }
                }
                else if (chr.Direction == Direction.L)
                {
                    chr.RealLocationPixel.X -= movePixel;
                    //if (scene.RealLocationPixel.X < 0)
                    {
                        scene.RealLocationPixel.X += movePixel;
                    }
                }
                else if (chr.Direction == Direction.D)
                {
                    chr.RealLocationPixel.Y += movePixel;
                    //if (scene.RealLocationPixel.Y + scene.SizePixel.Height > game.Options.WindowSizePixel.Height)
                    {
                        scene.RealLocationPixel.Y -= movePixel;
                    }
                }
                else if (chr.Direction == Direction.R)
                {
                    chr.RealLocationPixel.X += movePixel;
                    //if (scene.RealLocationPixel.X + scene.SizePixel.Width > game.Options.WindowSizePixel.Width)
                    {
                        scene.RealLocationPixel.X -= movePixel;
                    }
                }

                if (chr is PC)
                {
                    if (chr.NeedMovePixel < 1)
                    {
                        try
                        {
                            if (scene != null)
                            {
                                string sname = scene.CodeLayer[chr.RealLocation].SceneFileName;
                                if (sname != null)
                                {
                                    //int dx = scene.CodeLayer[chr.RealLocation].DefaultLocation.X;
                                    //int dy = scene.CodeLayer[chr.RealLocation].DefaultLocation.Y;
                                    game.SceneManager.Enter(sname, scene.CodeLayer[chr.RealLocation].DefaultLocation);
                                    chr.Direction = scene.CodeLayer[chr.RealLocation].DefaultDirection;
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
            if (scene == null)
            {
                return;
            }
            MoveCode(me);
            for (int i = 0; i < scene.NPCs.Count; i++)
            {
                MoveCode(scene.NPCs[i]);
            }
        }
        void frameCode()
        {
            if (scene == null)
            {
                return;
            }
            TimeSpan ts = DateTime.Now - lastFrameBeginTime;
            if (ts.TotalMilliseconds > scene.FrameInterval)
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
            if (scene == null)
            {
                return;
            }
            DrawBorder(scene);
            DrawScene(scene);
            NPC n=scene.GetNPC("father");
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

            game.PlayMP3(2, game.Audios["door"].FileName);
            string mus = "";
            try
            {
                mus = scene.BGMusics[0];
            }
            catch { }
            scene = null;
            scene = game.LoadDotMXScene(game.Scenes[fileName].FileName);
            game.Options.TileSizePixel = scene.TileSizePixel;

            SceneJump(realLoc);

            if (scene.BGMusics.Count > 0)
            {
                if (mus != scene.BGMusics[0])
                {
                    game.PlayMP3(1,game.Audios[scene.BGMusics[0]].FileName, true);
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
            scene.SetRealLocation(v3, game.Options.TilePixel);
        }
        public void MeJump(Vector3 v3)
        {
            //me.NextLocation = me.LastLocation = v3;
            me.SetRealLocation(v3, game.Options.TilePixel);
        }
        public void MeSkin(string name)
        {
            me.TextureIndex = -1;
            me.TextureName = name;
        }

        bool IsInWindow(Point p)
        {
            if (p.X < (0 - 1) * game.Options.TileSizePixelX.Width || p.Y < (0 - 1) * game.Options.TileSizePixelX.Height || p.X > (game.Options.WindowSizePixel.Width / game.Options.TileSizePixelX.Width + 1) * game.Options.TileSizePixelX.Width || p.Y > (game.Options.WindowSizePixel.Height / game.Options.TileSizePixelX.Height + 1) * game.Options.TileSizePixelX.Height)
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
        void DrawScene(Scene s)
        {
            if (s == null)
            {
                return;
            }
            int l = 0;

            foreach (TileLayer tl in s.TileLayers)
            {
                int lastl = s.CodeLayer[me.LastLocation].DrawLayer;
                int nextl = s.CodeLayer[me.NextLocation].DrawLayer;
                int drawl = s.CodeLayer[me.GetDrawLocation(game.Options.TilePixel, lastl, nextl)].DrawLayer;
                int nodrawl = s.CodeLayer[me.NextLocation].RchDisappear;

                if (l == drawl)
                {
                    DrawPC(me);
                }
                if (s.NPCs != null)
                {
                    foreach (NPC npc in s.NPCs)
                    {
                        int npcdrawl = s.CodeLayer[npc.GetDrawLocation(game.Options.TilePixel, lastl, nextl)].DrawLayer;
                        if (l == npcdrawl)
                        {
                            DrawNPC(npc);
                        }
                    }
                }
                foreach (Tile t in tl.Tiles)
                {
                    if (nodrawl != l)
                    {
                        if (IsInWindow(Util.PointAddPoint(Util.PointMulInt(t.LocationPoint, game.Options.TilePixel), scene.RealLocationPixelPoint)))
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
        void DrawPC(CHR chr)
        {
            if (chr == null)
            {
                return;
            }
            if (chr.TextureName == null)
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
            Vector3 v31 = chr.RealLocationPixel;
            v31.Y += game.Options.SpriteOffsetPixel;
            v31.X += scene.RealLocationPixel.X;
            v31.Y += scene.RealLocationPixel.Y;
            v31.Z += scene.RealLocationPixel.Z;
            v31 = Util.Vector3AddVector3(v31, ScreenOffsetPixel);
            game.DrawMetalXTexture(
                game.Textures[chr.TextureIndex],
                dz,
                v31,
                game.Options.TileSizePixelX,
                Util.MixColor( chr.ColorFilter,ColorFilter));
        }
        void DrawNPC(NPC npc)
        {
            if (npc == null)
            {
                return;
            }
            if (npc.TextureName == null)
            {
                return;
            }
            //if (npc.TextureIndex < 0)
            {
                npc.TextureIndex = game.Textures.GetIndex(npc.TextureName);
            }
            Rectangle dz = new Rectangle();
            dz.Y = (int)npc.Direction * game.Textures[npc.TextureIndex].TileSizePixel.Height;
            if (npc.NeedMovePixel > 0)
            {
                dz.X = (((int)((float)game.Options.TilePixel - npc.NeedMovePixel)) / (game.Options.TileSizePixelX.Width / 4) + 1) * game.Textures[npc.TextureIndex].TileSizePixel.Width;
            }
            else
            {
                dz.X = 0;
            }
            dz.Size = game.Textures[npc.TextureIndex].TileSizePixel;
            Vector3 v31 = npc.RealLocationPixel;
            v31.Y += game.Options.SpriteOffsetPixel;
            v31.X += scene.RealLocationPixel.X;
            v31.Y += scene.RealLocationPixel.Y;
            v31.Z += scene.RealLocationPixel.Z;
            v31 = Util.Vector3AddVector3(v31, ScreenOffsetPixel);
            game.DrawMetalXTexture(
                game.Textures[npc.TextureIndex],
                dz,
                v31,
                game.Options.TileSizePixelX,
                Util.MixColor(npc.ColorFilter, ColorFilter));
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
        //    return IsNobody(scene, me.FrontLocation);
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
        //    foreach (NPC npc in s.NPCs)
        //    {
        //        if (npc.RealLocation == v3)
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
        //    foreach (NPC npc in s.NPCs)
        //    {
        //        if (npc.RealLocation == v3)
        //        {
        //            return false;
        //        }
        //        index++;
        //    }
        //    index = -1;
        //    return true;
        //}
        public void CHRMove(Scene s, CHR chr, Direction dir, int stp)
        {
            if (chr is PC)
            {
                if (chr.CanControl == false)
                {
                    return;
                }
            }
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
            if (s.GetNPC(loc) != null)
            {
                return;
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
            chr.LastLocation = Util.Vector3DivInt(chr.RealLocationPixel, game.Options.TilePixel);
            chr.RealLocation = chr.NextLocation = loc;
            chr.NeedMovePixel += game.Options.TilePixel;
        }
        public override void OnKeyboardDownHoldCode(object sender, int key)
        {
            Key k = (Key)key;

            if (scene == null)
            {
                return;
            }
            if (me.NeedMovePixel == 0)
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
                    CHRMove(scene, me, dir, 1);
                }
            }
        }
        public override void OnKeyboardDownCode(object sender, int key)
        {
            //Key k = (Key)key;
            //if (k == Key.J)
            //{
            //    if (npc != null)
            //    {
            //        if (me.IsTalking)
            //        {
            //            me.IsTalking = false;
            //        }
            //        else
            //        {
            //            me.IsTalking = true;
            //        }
            //    }
            //}
            //else if (k == Key.K)
            //{
            //    if (npc != null)
            //    {
            //        if (me.IsTalking)
            //        {
            //            me.IsTalking = false;
            //        }
            //    }
            //}
        }
    }
}
