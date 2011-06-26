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
                    if (scene.RealLocation.Y > -scene.SizePixel.Height/2)
                    {
                        scene.RealLocation.Y -= movePixel;
                    }
                }
                else if (me.Direction == Direction.R)
                {
                    me.RealLocation.X += movePixel;
                    if (scene.RealLocation.X > -(scene.SizePixel.Width - game.TilePixel) / 2)
                    {
                        scene.RealLocation.X -= movePixel;
                    }
                }
            }
        }
        public override void Code()
        {
            base.Code();
            moveCode();
        }

        public override void Draw()
        {
            //base.Draw();
            DrawScene(scene);
            //DrawPC(me);
            if(scene!=null)

            game.DrawText("FPS: " + game.AverageFPS + "\n" + scene.RealLocation, new Point(), Color.White);
            //game.DrawText(FileLoader.Loaded + " / " + FileLoader.Size + " - " + FileLoader.TakeTime.TotalSeconds, new Point(), Color.White);
        }

        public void LoadScene(int i)
        {
            sceneIndex = i;
        }

        bool IsInWindow(Point pos)
        {
            Point p = Util.PointAddPoint(pos, scene.RealLocationPoint);
            if (p.X < (0 - 1) * game.Options.TileSizeX.Width || p.Y < (0 - 1) * game.Options.TileSizeX.Height || p.X > (game.Options.WindowSize.Width / game.Options.TileSizeX.Width + 1) * game.Options.TileSizeX.Width || p.Y > (game.Options.WindowSize.Height / game.Options.TileSizeX.Height + 1) * game.Options.TileSizeX.Height)
            {
                return false;
            }
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
                int drawl = s.CodeLayer[me.GetDrawLocation(game.TilePixel, lastl, nextl)].DrawLayer;


                if (l == drawl)
                {
                    DrawPC(me);
                    //game.DrawText("drawing layer:\n" + l, new Point(0, 120), Color.White);
                }
     
                foreach (Tile t in tl.Tiles)
                {
                    if (IsInWindow(t.LocationPoint))
                    {
                        game.DrawMetalXTexture(
                            game.Textures[t[frameIndex].TextureIndex],
                            t[frameIndex].DrawZone,
                            //Util.Vector3AddVector3(Util.Vector3AddVector3( s.RealLocation, ScreenOffsetPixel),Util.Point2Vector3( t.RealLocation,0f)),
                            Util.Vector3AddVector3(Util.Vector3AddVector3(s.RealLocation, ScreenOffsetPixel), t.Location),
                            s.TileSizePixel,
                            Util.MixColor(t[frameIndex].ColorFilter, ColorFilter)
                        );
                    }
                    else
                    {
                        //Console.Beep();
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
            Point p1 = new Point((int)chr.RealLocation.X, (int)chr.RealLocation.Y +game.SpriteOffsetPixel);
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
                //game.Scenes.LoadDotMXScene(game,@"scenes\test1.mxscene");
                LoadScene(0);
                me.TextureFileName = "CHRS0001";
                me.MoveSpeed = 3f;
                me.RealLocation = Util.Point2Vector3(game.CenterLocation, 0);
                me.RealLocation.X += 3 * game.TilePixel;
            }
            else if (k == Key.O)
            {
                base.ShockScreen(5000);
            }
            else if (k == Key.U)
            {
                base.FallOutSceen(1000);
            }
            else if (k == Key.I)
            {
                base.FallInSceen(1000);
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
                if (k == Key.W)
                {
                    me.Direction = Direction.U;
                    Vector3 loc = me.RealLocation;
                    loc.Y -= game.TilePixel;
                    if (scene.CodeLayer[loc].CHRCanRch)
                    {
                        me.LastLocation = me.RealLocation;
                        me.NextLocation = loc;
                        me.NeedMovePixel = game.TilePixel;
                    }
                }
                else if (k == Key.A)
                {
                    me.Direction = Direction.L;
                    Vector3 loc = me.RealLocation;
                    loc.X -= game.TilePixel;
                    if (scene.CodeLayer[loc].CHRCanRch)
                    {
                        me.LastLocation = me.RealLocation;
                        me.NextLocation = loc;
                        me.NeedMovePixel = game.TilePixel;
                    }
                }
                else if (k == Key.S)
                {
                    me.Direction = Direction.D;
                    Vector3 loc = me.RealLocation;
                    loc.Y += game.TilePixel;
                    if (scene.CodeLayer[loc].CHRCanRch)
                    {
                        me.LastLocation = me.RealLocation;
                        me.NextLocation = loc;
                        me.NeedMovePixel = game.TilePixel;
                    }
                }
                else if (k == Key.D)
                {
                    me.Direction = Direction.R;
                    Vector3 loc = me.RealLocation;
                    loc.X += game.TilePixel;
                    if (scene.CodeLayer[loc].CHRCanRch)
                    {
                        me.LastLocation = me.RealLocation;
                        me.NextLocation = loc;
                        me.NeedMovePixel = game.TilePixel;
                    }
                }
            }
        }
    }
}
