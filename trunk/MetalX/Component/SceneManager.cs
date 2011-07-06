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
        Scene scene;
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
                    if (scene.RealLocation.Y + scene.SizePixel.Height > game.Options.WindowSizePixel.Height)
                    {
                        scene.RealLocation.Y -= movePixel;
                    }
                }
                else if (me.Direction == Direction.R)
                {
                    me.RealLocation.X += movePixel;
                    if (scene.RealLocation.X + scene.SizePixel.Width > game.Options.WindowSizePixel.Width)
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
            //game.DrawText("RealLoc:\n" + me.RealLocation + "\nLastLoc:\n" + me.LastLocation + "\nFrontLoc:\n" + me.FrontLocation, new Point(0, 120), Color.White);
        }

        public void EnterScene(string fileName, Vector3 realLoc)
        {
            scene = game.Scenes.LoadDotMXScene(game, fileName);
            scene.RealLocation = realLoc;
            game.Options.TileSizePixel = scene.TileSizePixel;
        }

        public void MoveMe(Vector3 v3)
        {
            me.NextLocation = me.LastLocation = v3;
            me.RealLocation = Util.Vector3MulInt(v3, game.Options.TilePixel);
        }
        public void SkinMe(string name)
        {
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
                int drawl = s.CodeLayer[me.GetDrawLocation(game.Options.TilePixel, lastl, nextl)].DrawLayer;
                int nodrawl = s.CodeLayer[me.NextLocation].RchDisappear;

                if (l == drawl)
                {
                    DrawPC(me);
                }

                foreach (Tile t in tl.Tiles)
                {
                    if (nodrawl != l)
                    {
                        if (IsInWindow(Util.PointAddPoint(Util.PointMulInt(t.LocationPoint, game.Options.TilePixel), scene.RealLocationPoint)))
                        {
                            int fi = t.FrameIndex;
                            if (t.IsAnimation)
                            {
                                fi = frameIndex;
                            }
                            game.DrawMetalXTexture(
                                game.Textures[t[fi].TextureIndex],
                                t[fi].DrawZone,
                                //Util.Vector3AddVector3(Util.Vector3AddVector3( s.RealLocation, ScreenOffsetPixel),Util.Point2Vector3( t.RealLocation,0f)),
                                Util.Vector3AddVector3(Util.Vector3AddVector3(s.RealLocation, ScreenOffset), Util.Vector3MulInt(t.Location, game.Options.TilePixel)),
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
            Vector3 v31 = chr.RealLocation;
            v31.Y += game.SpriteOffsetPixel;
            v31.X += scene.RealLocation.X;
            v31.Y += scene.RealLocation.Y;
            v31.Z += scene.RealLocation.Z;
            v31 = Util.Vector3AddVector3(v31, ScreenOffset);
            game.DrawMetalXTexture(
                game.Textures[chr.TextureIndex],
                dz,
                v31,
                game.Options.TileSizePixelX,
                Color.White);
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
                    Vector3 loc = me.FrontLocation;
                    if (scene.CodeLayer[loc].CHRCanRch)
                    {
                        me.LastLocation = Util.Vector3DivInt(me.RealLocation, game.Options.TilePixel);
                        me.NextLocation = loc;
                        me.NeedMovePixel += game.Options.TilePixel;
                    }
                }
            }
        }
    }
}
