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
        Scene scene
        {
            get
            {
                if (sceneIndex < 0)
                {
                    return null;
                }
                return game.Scenes[sceneIndex];
            }
        }
        PC me
        {
            get
            {
                return game.Characters.ME;
            }
        }
        int frameIndex = 0;
        DateTime lastFrameBeginTime = DateTime.Now;

        public SceneManager(Game g)
            : base(g)
        {
        }
        void moveCode()
        {
            if (me.NeedMovePixel > 0)
            {
                float movePixel = me.MoveSpeed;
                if (me.NeedMovePixel < me.MoveSpeed)
                {
                    movePixel = me.NeedMovePixel;
                }

                me.NeedMovePixel -= movePixel;

                if (me.Direction == Direction.U)
                {
                    me.RealLocation.Y -= movePixel;
                    if (scene.RealLocation.Y < 0)
                    {
                        scene.RealLocation.Y += movePixel;
                    }
                }
                else if (me.Direction == Direction.L)
                {
                    me.RealLocation.X -= movePixel;
                    if (scene.RealLocation.X < 0)
                    {
                        scene.RealLocation.X += movePixel;
                    }
                }
                else if (me.Direction == Direction.D)
                {
                    me.RealLocation.Y += movePixel;
                    if (scene.RealLocation.Y + scene.SizePixel.Height > game.Options.WindowSize.Height)
                    {
                        scene.RealLocation.Y -= movePixel;
                    }
                }
                else if (me.Direction == Direction.R)
                {
                    me.RealLocation.X += movePixel;
                    if (scene.RealLocation.X + scene.SizePixel.Width > game.Options.WindowSize.Width)
                    {
                        scene.RealLocation.X -= movePixel;
                    }
                }
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
            DrawScene(scene);
            game.DrawText("FPS: " + game.AverageFPS, new Point(), Color.White);
            //game.DrawText("RealLoc: " + scene.RealLocation + "\nLastLoc: " + me.LastLocation + "\nNextLoc: " + me.NextLocation, new Point(0, 120), Color.White);
        }

        public void LoadScene(int i, Vector3 realLoc)
        {
            sceneIndex = i;
            scene.RealLocation = realLoc;
        }

        bool IsInWindow(Point p)
        {
            if (p.X < (0 - 1) * game.Options.TileSizeX.Width || p.Y < (0 - 1) * game.Options.TileSizeX.Height || p.X > (game.Options.WindowSize.Width / game.Options.TileSizeX.Width + 1) * game.Options.TileSizeX.Width || p.Y > (game.Options.WindowSize.Height / game.Options.TileSizeX.Height + 1) * game.Options.TileSizeX.Height)
            {
                return false;
            }
            return true;
        }
        bool IsInScene(Point p)
        {
            return true;
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
                int drawl = s.CodeLayer[me.GetDrawLocation(scene.TilePixel, lastl, nextl)].DrawLayer;
                int nodrawl = s.CodeLayer[me.NextLocation].RchDisappear;

                if (l == drawl)
                {
                    DrawPC(me);
                }

                foreach (Tile t in tl.Tiles)
                {
                    if (nodrawl != l)
                    {
                        if (IsInWindow(Util.PointAddPoint(Util.PointMulInt(t.LocationPoint, scene.TilePixel), scene.RealLocationPoint)))
                        {
                            int fi = t.FrameIndex;
                            if(t.IsAnimation)
                            {
                                fi = frameIndex;
                            }
                            game.DrawMetalXTexture(
                                game.Textures[t[fi].TextureIndex],
                                t[fi].DrawZone,
                                //Util.Vector3AddVector3(Util.Vector3AddVector3( s.RealLocation, ScreenOffsetPixel),Util.Point2Vector3( t.RealLocation,0f)),
                                Util.Vector3AddVector3(Util.Vector3AddVector3(s.RealLocation, ScreenOffset), t.Location),
                                s.TileSizePixel,
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
            if (chr.TextureFileName == null)
            {
                return;
            }
            if (chr.TextureIndex < 0)
            {
                chr.TextureIndex = game.Textures.GetIndex(chr.TextureFileName);
            }
            Rectangle dz = new Rectangle();
            dz.Y = (int)chr.Direction * game.Textures[chr.TextureIndex].TileSizePixel.Height;
            if (chr.NeedMovePixel > 0)
            {
                dz.X = (((int)((float)game.Options.TileSizeX.Width - chr.NeedMovePixel)) / (game.Options.TileSizeX.Width / 4) + 1) * game.Textures[chr.TextureIndex].TileSizePixel.Width;
            }
            else
            {
                dz.X = 0;
            }
            dz.Size = game.Textures[chr.TextureIndex].TileSizePixel;
            Point p1 = new Point((int)chr.RealLocation.X, (int)chr.RealLocation.Y + game.SpriteOffsetPixel);
            Point p2 = new Point((int)scene.RealLocation.X, (int)scene.RealLocation.Y);
            game.DrawMetalXTexture(
                game.Textures[chr.TextureIndex],
                dz,
                Util.PointAddPoint(p1, p2),
                game.Options.TileSizeX,
                Color.White);
        }
        public override void OnKeyboardDownCode(int key)
        {
            Key k = (Key)key;
            if (k == Key.L)
            {
                LoadScene(0, new Vector3());
                me.TextureFileName = "mm-chr0001";
                //me.RealLocation = game.CenterLocation;
                me.NextLocation = me.LastLocation = me.RealLocation;
            }
        }
        public override void OnKeyboardDownHoldCode(int key)
        {
            Key k = (Key)key;

            if (scene == null)
            {
                return;
            }
            if (me.NeedMovePixel == 0)
            {
                if (k == Key.W || k == Key.A || k == Key.S || k == Key.D)
                {
                    if (k == Key.W)
                    {
                        me.Direction = Direction.U;
                    }
                    else if (k == Key.A)
                    {
                        me.Direction = Direction.L;
                    }
                    else if (k == Key.S)
                    {
                        me.Direction = Direction.D;
                    }
                    else if (k == Key.D)
                    {
                        me.Direction = Direction.R;
                    } 
                    Vector3 loc = me.GetFrontLocation(scene.TilePixel);
                    try
                    {
                        if (scene.CodeLayer[loc].CHRCanRch)
                        {
                            me.LastLocation = me.RealLocation;
                            me.NextLocation = loc;
                            me.NeedMovePixel = scene.TilePixel;
                        }
                    }
                    catch
                    { 
                    }
                }
            }
        }
    }
}
