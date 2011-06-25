﻿using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX.DirectInput;

using MetalX.Data;

namespace MetalX.Component
{
    public class SceneManager : GameCom
    {
        int sceneIndex = -1;
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
        //Point sceneLocation = new Point();
        int frameIndex = 0;
        DateTime lastFrameBeginTime = DateTime.Now;

        public SceneManager(Game g)
            : base(g)
        {
        }

        public override void Code()
        {
            base.Code();
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
                    me.Location.Y = me.Location.Y - movePixel;
                    scene.Location.Y = scene.Location.Y + movePixel;
                }
                else if (me.Direction == Direction.L)
                {
                    me.Location.X = me.Location.X - movePixel;
                    scene.Location.X = scene.Location.X + movePixel;
                }
                else if (me.Direction == Direction.D)
                {
                    me.Location.Y = me.Location.Y + movePixel;
                    scene.Location.Y = scene.Location.Y - movePixel;
                }
                else if (me.Direction == Direction.R)
                {
                    me.Location.X = me.Location.X + movePixel;
                    scene.Location.X = scene.Location.X - movePixel;
                }
            }
        }

        public override void Draw()
        {
            //base.Draw();
            DrawTerrain(scene);
            DrawPC(me);
            game.DrawText("FPS: " + game.AverageFPS, new Point(), Color.White);
            //game.DrawText(FileLoader.Loaded + " / " + FileLoader.Size + " - " + FileLoader.TakeTime.TotalSeconds, new Point(), Color.White);
        }

        public void LoadScene(int i)
        {
            sceneIndex = i;
        }

        bool IsInWindow(Point pos)
        {
            Point p = Util.PointAddPoint(pos, scene.LocationPoint);
            if (p.X < (0 - 1) * game.Options.TileSizeX.Width || p.Y < (0 - 1) * game.Options.TileSizeX.Height || p.X > (game.Options.WindowSize.Width / game.Options.TileSizeX.Width + 1) * game.Options.TileSizeX.Width || p.Y > (game.Options.WindowSize.Height / game.Options.TileSizeX.Height + 1) * game.Options.TileSizeX.Height)
            {
                return false;
            }
            return true;
        }

        void DrawTerrain(Scene s)
        {
            if (s == null)
            {
                return;
            }
            foreach (TileLayer tl in s.TileLayers)
            {
                foreach (Tile t in tl.Tiles)
                {
                    if (IsInWindow(t.Location))
                    {
                        game.DrawMetalXTexture(
                            game.Textures[t[frameIndex].TextureIndex],
                            t[frameIndex].DrawZone,
                            Util.Point2Vector3(Util.PointAddPoint(s.LocationPoint, Util.PointAddPoint(t.Location, GlobalOffset)), (scene.Location.Z + GlobalOffset.X + GlobalOffset.Y) * 8),
                            s.TileSizePixel,
                            Util.MixColor(t[frameIndex].ColorFilter, ColorFilter)
                        );
                    }
                    else
                    {
                        //Console.Beep();
                    }
                }
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
            dz.Width = game.Textures[chr.TextureIndex].TileSizePixel.Width;
            dz.Height = game.Textures[chr.TextureIndex].TileSizePixel.Height;
            Point p1 = new Point((int)chr.Location.X, (int)chr.Location.Y);
            Point p2 = new Point((int)scene.Location.X, (int)scene.Location.Y);
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
                game.Scenes.LoadDotMXScene(game,@"scenes\test1.mxscene");
                LoadScene(0);
                me.TextureFileName = "CHRS0001";
                me.MoveSpeed = 4.7f;
                me.Location = Util.Point2Vector3(game.CenterLocation, 0);
            }
            if (k == Key.O)
            {
                base.ShockScreen(5000);
            }

        }
        public override void OnKeyboardDownHoldCode(int key)
        {
            Key k = (Key)key;

            if (scene == null)
            {
                return;
            }
            if (me.NeedMovePixel <= 0)
            {
                if (k == Key.W)
                {
                    me.Direction = Direction.U;
                    me.NeedMovePixel = game.TilePixel;
                }
                else if (k == Key.A)
                {
                    me.Direction = Direction.L;
                    me.NeedMovePixel = game.TilePixel;
                }
                else if (k == Key.S)
                {
                    me.Direction = Direction.D;
                    me.NeedMovePixel = game.TilePixel;
                }
                else if (k == Key.D)
                {
                    me.Direction = Direction.R;
                    me.NeedMovePixel = game.TilePixel;
                }
            }
        }
    }
}
