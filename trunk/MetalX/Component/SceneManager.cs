using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;

using MetalX.Define;

namespace MetalX.Component
{
    public class SceneManager : GameCom
    {
        //int sceneIndex = -1;
        //public int SceneIndex
        //{
        //    get
        //    {
        //        return sceneIndex;
        //    }
        //    //set
        //    //{
        //    //    sceneIndex = value;
        //    //}
        //}
        protected bool PCbeforeNPC = true;

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
        //                game.SCN.RealLocationPixel.Y += movePixel;
        //            }
        //        }
        //        else if (chr.RealDirection == Direction.L)
        //        {
        //            chr.RealLocationPixel.X -= movePixel;
        //            if (chr is PC)
        //            {
        //                game.SCN.RealLocationPixel.X += movePixel;
        //            }
        //        }
        //        else if (chr.RealDirection == Direction.D)
        //        {
        //            chr.RealLocationPixel.Y += movePixel;
        //            if (chr is PC)
        //            {
        //                game.SCN.RealLocationPixel.Y -= movePixel;
        //            }
        //        }
        //        else if (chr.RealDirection == Direction.R)
        //        {
        //            chr.RealLocationPixel.X += movePixel;
        //            if (chr is PC)
        //            {
        //                game.SCN.RealLocationPixel.X -= movePixel;
        //            }
        //        }

        //        if (chr is PC)
        //        {
        //            if (chr.NeedMovePixel ==0)
        //            {
        //                try
        //                {
        //                    if (game.SCN != null)
        //                    {
        //                        string sname = game.SCN.CodeLayer[chr.RealLocation].SceneFileName;
        //                        if (sname != null)
        //                        {
        //                            //int dx = scene.CodeLayer[chr.RealLocation].DefaultLocation.X;
        //                            //int dy = scene.CodeLayer[chr.RealLocation].DefaultLocation.Y;
        //                            game.SceneManager.Enter(sname, game.SCN.CodeLayer[chr.RealLocation].DefaultLocation);
        //                            chr.RealDirection = game.SCN.CodeLayer[chr.RealLocation].DefaultDirection;
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
            foreach (NPC npc in game.NPCs)
            {
                npc.MovePixel();
            }
            if (game.ME.NeedMovePixel > 0)
            {
                game.SCN.MovePixel(game.ME.MoveSpeed);

                game.ME.MovePixel();

                if (game.ME.NeedMovePixel == 0)
                {
                    string sname = null;
                    try
                    {
                        sname = game.SCN[(int)game.ME.RealLocation.Y, (int)game.ME.RealLocation.X].SceneFileName;
                    }
                    catch
                    { }
                    int i = game.SCN.Fight(game.ME.RealLocation);
                    if (i > -1)
                    {
                        string bgt = game.SCN.MonsterZones[i].BGTextureName;
                        List<string> monsterNames = game.SCN.MonsterZones[i].RollMonsters();
                        game.BattleManager.GetIn(bgt, monsterNames);
                      
                        game.AppendScript("btl fallout 0");
                        game.AppendScript("btl shock 1200");
                        game.AppendScript("btl fallin 1000");
                        game.AppendScript("mp3 1");
                        game.AppendScript("mp3 2 " + game.SCN.MonsterZones[i].EnterSoundName);
                        game.AppendScript("delay 1200");
                        game.AppendScript("mp3 1 " + game.SCN.MonsterZones[i].BGMusicName + " true");
                        game.AppendScript("delay 300");
                        game.AppendScript("mp3 2");
                        game.ExecuteScript();
                    }
                    else if (sname != null)
                    {
                        Enter(sname, Util.Point2Vector3(game.SCN[game.ME.RealLocation].DefaultLocation), game.ME.RealDirection);
                        Delay(300);
                        game.AppendScript("scn clrctrl");
                        game.AppendScript("scn fallout 200");
                        game.AppendScript("delay 300");
                        game.AppendScript("scn fallin 200");
                        game.AppendScript("delay 200");
                        game.AppendScript("scn setctrl");
                        game.ExecuteScript();
                    }
                }
            }
        }
        protected void frameCode()
        {
            if (game.SCN == null)
            {
                return;
            }
            TimeSpan ts = DateTime.Now - lastFrameBeginTime;
            if (ts.TotalMilliseconds > game.SCN.FrameInterval)
            {
                lastFrameBeginTime = DateTime.Now;
                frameIndex++;
            }
        }
        protected void doorCode()
        {
            foreach (NPC npc in game.NPCs)
            {
                if (npc.IsDoor)
                {
                    Vector3 v33 = npc.RealLocation;
                    v33.Y--;
                    if (v33 == game.ME.FrontLocation)
                    {
                        PCbeforeNPC = true;
                    }
                    else
                    {
                        v33.Y++;
                        if (v33 == game.ME.RealLocation)
                        {
                            if (npc.Invisible == false)
                            {
                                npc.Invisible = true;
                                game.PlayMP3Audio(2, game.AudioFiles["163"].FullName);
                            }
                        }
                        else if (v33 == game.ME.LastLocation)
                        {
                            if (game.ME.RealDirection == Direction.U)
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
                                game.PlayMP3Audio(2, game.AudioFiles["163"].FullName);
                            }
                        }
                    }
                }
            }
        }
        void scriptCode()
        {
            if (game.SceneManager.Controllable == false)
            {
                return;
            }
            if (game.ME.NeedMovePixel > 0)
            {
                return;
            }
            string spt = null;
            try
            {
                spt = game.SCN[game.ME.RealLocation].Script;
            }
            catch
            { }
            if (spt != null)
            {
                if (spt != string.Empty)
                {
                    game.AppendScript("scn clrctrl");
                    game.AppendScript(spt);
                    game.AppendScript("scn setctrl");
                    game.ExecuteScript();
                }
            }
        }
        string nextSCNName = null;
        Vector3 nextSCNLoc;
        Direction nextSCNDir;

        void changeSCNCode()
        {
            if (nextSCNName != null)
            {
                //game.SceneManager.game.ME.CanControl = false;

                string mus = "";
                try
                {
                    mus = game.SCN.BGMusicNames[0];
                }
                catch { }
                game.ME.Face(nextSCNDir);
                game.SCN = null;
                game.SCN = game.LoadDotMXScene(game.SceneFiles[nextSCNName].FullName);
                game.SCN.Init(game.Options.WindowSize);
                game.Options.TileSizePixel = game.SCN.TileSizePixel;

                InitNPCs();

                game.ME.SetRealLocation(nextSCNLoc, game.Options.TilePixelX);
                SceneJump(nextSCNLoc);
                game.SCN.Face(Util.GetOppositeDirection(nextSCNDir));

                if (game.SCN.BGMusicNames != null)
                {
                    if (game.SCN.BGMusicNames.Count > 0)
                    {
                        if (mus != game.SCN.BGMusicNames[0])
                        {
                            game.PlayMP3Audio(1, game.AudioFiles[game.SCN.BGMusicNames[0]].FullName, true);
                        }
                    }
                    else
                    {
                        game.StopAudio(1);
                    }
                }
                nextSCNName = null;
            }
        }
        public override void Code()
        {
            //if (game.SCN == null && nextSCNName != null)
            //{
            //    return;
            //}
            changeSCNCode();
            scriptCode();
            moveCode();
            doorCode();
        }

        public override void Draw()
        {
            if (game.SCN == null)
            {
                return;
            }
            //DrawBorder(game.SCN);
            DrawScene(game.SCN);
        }
        
        public void Enter(string fileName, Vector3 realLoc, Direction dir)
        {
            nextSCNName = fileName;
            nextSCNLoc = realLoc;
            nextSCNDir = dir;
        }
        public void InitNPCs()
        {
            game.NPCs.Clear();
            if (game.SCN.NPCNames != null)
            {
                for (int i = 0; i < game.SCN.NPCNames.Count; i++)
                {
                    string name = game.SCN.NPCNames[i];
                    try
                    {
                        game.NPCs.Add(game.LoadDotMXNPC(game.NPCFiles[name].FullName));
                    }
                    catch
                    {
                        game.SCN.NPCNames.Remove(name);
                    }
                }
            }
        }
        public void SceneJump(Vector3 realLoc)
        {
            Vector3 cp = new Vector3(game.Options.WindowSize.Width / 2, game.Options.WindowSize.Height / 2, 0);

            Vector3 v3 = new Vector3(-realLoc.X, -realLoc.Y, 0);
            v3 = Util.Vector3AddVector3(v3, cp);
            game.SCN.SetRealLocation(v3, game.Options.TilePixelX);
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
            if (p.X < (0 - 1) || p.Y < (0 - 1) || p.X > (game.Options.WindowSize.Width + 1) || p.Y > (game.Options.WindowSize.Height + 1))
            {
                return false;
            }
            return true;
        }
        //protected void DrawBorder(Scene s)
        //{
        //    if (s.BottomTile == null)
        //    {
        //        return;
        //    }
        //    int wr = game.Options.WindowSizePixel.Width / game.Options.TileSizePixelX.Width;
        //    int hr = game.Options.WindowSizePixel.Height / game.Options.TileSizePixelX.Height;

        //    //int x = 0;
        //    //int y = 0;
        //    int w = 0;
        //    int h = 0;
        //    Vector3 loc;
        //    Vector3 loco;

        //    Point sp = Util.PointDivInt(s.RealLocationPixelPoint, game.Options.TilePixelX);
        //    w = sp.X;
        //    h = sp.Y;
        //    //if (w > 0)
        //    //{
        //    //    w++;
        //    //}
        //    //if (h > 0)
        //    //{
        //    //    h++;
        //    //}
        //    int ti = s.BottomTile.Frames[0].TextureIndex;
        //    Rectangle dz = s.BottomTile.Frames[0].DrawZone;
        //    Color cf = s.BottomTile.Frames[0].ColorFilter;


        //    if (sp.X >= 0)
        //    {
        //        loc = new Vector3();
        //        loco = new Vector3();

        //        loc.X = s.RealLocationPixel.X;
        //        loc.Y = s.RealLocationPixel.Y;
        //        for (int yy = 0; yy < hr + 1; yy++)
        //        {
        //            for (int xx = 0; xx < w + 1; xx++)
        //            {
        //                loco.X = xx - w - 1;
        //                loco.Y = yy - h;
        //                Vector3 eloc = Util.Vector3AddVector3(Util.Vector3AddVector3(loc, EffectOffsetPixel), Util.Vector3MulInt(loco, game.Options.TilePixelX));
        //                float rot = 0;
        //                //eloc=Util.Vector3AddVector3(eloc
        //                game.DrawMetalXTexture(
        //                    game.Textures[ti],
        //                    dz,
        //                    eloc,
        //                    game.Options.TileSizePixelX,
        //                    rot,
        //                    Util.MixColor(cf, ColorFilter)
        //                );
        //            }
        //        }
        //    }
        //    if (sp.Y >= 0)
        //    {
        //        loc = new Vector3();
        //        loco = new Vector3();

        //        loc.X = s.RealLocationPixel.X;
        //        loc.Y = s.RealLocationPixel.Y;
        //        for (int yy = 0; yy < h + 1; yy++)
        //        {
        //            for (int xx = 0; xx < wr + 1; xx++)
        //            {
        //                loco.X = xx - w;
        //                loco.Y = yy - h - 1;
        //                float rot = 0;
        //                Vector3 eloc = Util.Vector3AddVector3(Util.Vector3AddVector3(loc, EffectOffsetPixel), Util.Vector3MulInt(loco, game.Options.TilePixelX));
        //                game.DrawMetalXTexture(
        //                    game.Textures[ti],
        //                    dz,
        //                    eloc,
        //                    game.Options.TileSizePixelX,
        //                    rot,
        //                    Util.MixColor(cf, ColorFilter)
        //                );
        //            }
        //        }
        //    }
        //    if (sp.X <= 0)
        //    {
        //        loc = new Vector3();
        //        loco = new Vector3();

        //        loc.X = s.RealLocationPixel.X;
        //        loc.Y = s.RealLocationPixel.Y;
        //        int ww = -w;
        //        ww += wr - s.Size.Width;
        //        for (int yy = 0; yy < hr + 1; yy++)
        //        {
        //            for (int xx = 0; xx < ww + 1; xx++)
        //            {
        //                loco.X = xx + s.Size.Width;
        //                loco.Y = yy - h;
        //                float rot=0;
        //                Vector3 eloc = Util.Vector3AddVector3(Util.Vector3AddVector3(loc, EffectOffsetPixel), Util.Vector3MulInt(loco, game.Options.TilePixelX));
        //                game.DrawMetalXTexture(
        //                    game.Textures[ti],
        //                    dz,
        //                    eloc,
        //                    game.Options.TileSizePixelX,
        //                    rot,
        //                    Util.MixColor(cf, ColorFilter)
        //                );
        //            }
        //        }
        //    }
        //    if (sp.Y <= 0)
        //    {
        //        loc = new Vector3();
        //        loco = new Vector3();

        //        loc.X = s.RealLocationPixel.X;
        //        loc.Y = s.RealLocationPixel.Y;
        //        int hh = -h;
        //        hh += hr - s.Size.Height;
        //        for (int yy = 0; yy < hh + 1; yy++)
        //        {
        //            for (int xx = 0; xx < wr + 1; xx++)
        //            {
        //                loco.X = xx - w;
        //                loco.Y = yy + s.Size.Height;
        //                float rot = 0;
        //                Vector3 eloc = Util.Vector3AddVector3(Util.Vector3AddVector3(loc, EffectOffsetPixel), Util.Vector3MulInt(loco, game.Options.TilePixelX));
        //                game.DrawMetalXTexture(
        //                    game.Textures[ti],
        //                    dz,
        //                    eloc,
        //                    game.Options.TileSizePixelX,
        //                    rot,
        //                    Util.MixColor(cf, ColorFilter)
        //                );
        //            }
        //        }
        //    }
        //}
        //protected void DrawSceneTest(Scene s)
        //{
        //    if (s == null)
        //    {
        //        return;
        //    }
        //    int ii = 0;
        //    for (int l = 0; l < s.TileLayers.Count; l++)
        //    {
        //        int lastl = 3, nextl = 3, drawl = 3;
        //        try
        //        {
        //            lastl = s[(int)game.ME.LastLocation.Y, (int)game.ME.LastLocation.X].DrawLayer;
        //            nextl = s[(int)game.ME.NextLocation.Y, (int)game.ME.NextLocation.X].DrawLayer;
        //            Vector3 drawloc = game.ME.GetDrawLocation(game.Options.TilePixelX, lastl, nextl);
        //            drawl = s[(int)drawloc.Y, (int)drawloc.X].DrawLayer;
        //        }
        //        catch { }
        //        if (PCbeforeNPC)
        //        {
        //            if (l == drawl)
        //            {
        //                DrawCHR(game.ME);
        //            }
        //        }
        //        if (game.NPCs != null)
        //        {
        //            foreach (NPC npc in game.NPCs)
        //            {
        //                int npcdrawl = s.CodeLayer[npc.GetDrawLocation(game.Options.TilePixelX, lastl, nextl)].DrawLayer;
        //                if (l == npcdrawl)
        //                {
        //                    DrawCHR(npc);
        //                }
        //            }
        //        }
        //        if (PCbeforeNPC == false)
        //        {
        //            if (l == drawl)
        //            {
        //                DrawCHR(game.ME);
        //            }
        //        }
        //        for (int y = 0; y < s.Tiles[l].Length; y++)
        //        {
        //            if (y + game.SCN.RealLocation.Y < 0 + -1)
        //            {
        //                //y++;
        //                continue;
        //            }
        //            if (y + game.SCN.RealLocation.Y > game.Options.WindowSize.Height + 1)
        //            {
        //                //y++;
        //                continue;
        //            }
        //            for (int x = 0; x < s.Tiles[l][y].Length; x++)
        //            {
        //                if (x + game.SCN.RealLocation.X < 0 + -1)
        //                {
        //                    //x++;
        //                    continue;
        //                }
        //                if (x + game.SCN.RealLocation.X > game.Options.WindowSize.Width + 1)
        //                {
        //                    //x++;
        //                    continue;
        //                }
        //                Tile t = s[l, y, x];
        //                if (t != null)
        //                {
        //                    //if (IsInWindow(Util.PointAddPoint(t.LocationPoint, game.SCN.RealLocationPoint)))
        //                    {
        //                        int fi = 0;
        //                        if (t.IsAnimation)
        //                        {
        //                            fi = frameIndex;
        //                        }
        //                        Vector3 loc = Util.Vector3AddVector3(Util.Vector3AddVector3(s.RealLocationPixel, EffectOffsetPixel), Util.Vector3MulInt(t.Location, game.Options.TilePixelX));
        //                        //loc = Util.Vector3AddVector3(loc, TileRollOutOffset);
        //                        Color col = Util.MixColor(t[fi].ColorFilter, ColorFilter);
        //                        //col = Util.MixColor(col, TileRollOutColorFilter);
        //                        float rot = 0 % 360;
        //                        game.DrawMetalXTexture(
        //                            game.Textures[t[fi].TextureIndex],
        //                            t[fi].DrawZone,
        //                            //Util.Vector3AddVector3(Util.Vector3AddVector3( s.RealLocation, ScreenOffsetPixel),Util.Point2Vector3( t.RealLocation,0f)),
        //                            loc,
        //                            game.Options.TileSizePixelX,
        //                            rot,
        //                            col
        //                        );
        //                        ii++;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        void DrawScene(Scene s)
        {
            if (s == null)
            {
                return;
            }
            int l = 0;

            foreach (TileLayer tl in s.TileLayers)
            {
                int lastl = 3, nextl = 3, drawl = 3;
                try
                {
                    lastl = s[(int)game.ME.LastLocation.Y, (int)game.ME.LastLocation.X].DrawLayer;
                    nextl = s[(int)game.ME.NextLocation.Y, (int)game.ME.NextLocation.X].DrawLayer;
                    Vector3 drawloc = game.ME.GetDrawLocation(game.Options.TilePixelX, lastl, nextl);
                    drawl = s[(int)drawloc.Y, (int)drawloc.X].DrawLayer;
                }
                catch { }
                //int lastl = s.CodeLayer[game.ME.LastLocation].DrawLayer;
                //int nextl = s.CodeLayer[game.ME.NextLocation].DrawLayer;
                //int drawl = s.CodeLayer[game.ME.GetDrawLocation(game.Options.TilePixelX, lastl, nextl)].DrawLayer;
                //int nodrawl = s.CodeLayer[game.ME.FrontLocation].RchDisappear;
                if (PCbeforeNPC)
                {
                    if (l == drawl)
                    {
                        DrawCHR(game.ME);
                    }
                }
                if (game.NPCs != null)
                {
                    foreach (NPC npc in game.NPCs)
                    {
                        int npcdrawl = s.CodeLayer[npc.GetDrawLocation(game.Options.TilePixelX, lastl, nextl)].DrawLayer;
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
                        DrawCHR(game.ME);
                    }
                }
                foreach (Tile t in tl.Tiles)
                {
                    if (IsInWindow(Util.PointAddPoint( t.LocationPoint,s.RealLocationPoint)))
                    {
                        int fi = 0;
                        //if (t.IsAnimation)
                        {
                            fi = frameIndex;
                        }
                        float rot = 0 % 360;
                        game.DrawMetalXTexture(
                            game.Textures[t[fi].TextureIndex],
                            t[fi].DrawZone,
                            Util.Vector3AddVector3(Util.Vector3AddVector3(s.RealLocationPixel, ScreenOffsetPixel), Util.Vector3MulInt(t.Location, game.Options.TilePixelX)),
                            game.Options.TileSizePixelX,
                            rot,
                            Util.MixColor(t[fi].ColorFilter, ColorFilter)
                        );
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
        //        dz.X = (((int)((float)game.Options.TilePixelX - chr.NeedMovePixel)) / (game.Options.TileSizePixelX.Width / 4) + 1) * game.Textures[chr.TextureIndex].TileSizePixel.Width;
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
            dz.Y = (int)chr.RealDirection * game.Textures[chr.TextureIndex].TileSize.Height;
            if (chr.NeedMovePixel > 0 && chr.IsRigor == false)
            {
                dz.X = (((int)((float)game.Options.TilePixelX - chr.NeedMovePixel)) / (game.Options.TileSizePixelX.Width / 4) + 1) * game.Textures[chr.TextureIndex].TileSize.Width;
            }
            else
            {
                dz.X = 0;
            }
            //if (chr is NPC)
            //{
            //    if (((NPC)chr).IsDoor)
            //    {
            //        dz.Y += (2 - chr.Size.Height) * game.Textures[chr.TextureIndex].TileSize.Height;
            //    }
            //}
            Size tdz = game.Textures[chr.TextureIndex].TileSize;
            tdz.Width *= chr.Size.Width;
            tdz.Height *= chr.Size.Height;
            //if (chr is NPC)
            //{
            //    if (((NPC)chr).IsDoor)
            //    {
            //        tdz.Height *= 2;
            //    }
            //}
            dz.Size = tdz;
            Vector3 v31 = chr.RealLocationPixel;
            v31.Y += game.Options.SpriteOffsetPixel;
            v31.X += game.SCN.RealLocationPixel.X;
            v31.Y += game.SCN.RealLocationPixel.Y;
            v31.Z += game.SCN.RealLocationPixel.Z;
            v31 = Util.Vector3AddVector3(v31, ScreenOffsetPixel);
            if (chr is NPC)
            {
                if (((NPC)chr).IsDoor)
                {
                    v31.Y -= game.Options.TileSizePixelX.Height * (chr.Size.Height - 1);
                }
            }
            Size dsize = game.Options.TileSizePixelX;
            dsize.Width *= chr.Size.Width;
            dsize.Height *= chr.Size.Height;
            //if (chr is NPC)
            //{
            //    if (((NPC)chr).IsDoor)
            //    {
            //        dsize.Height *= 2;
            //    }
            //}
            float rot = 0;
            game.DrawMetalXTexture(
                game.Textures[chr.TextureIndex],
                dz,
                v31,
                dsize,
                rot,
                Util.MixColor(chr.ColorFilter, ColorFilter));
        }
        public override void BaseCode()
        {
            //shock();
            //if (FallOutAlpha)
            //{
            //    fallOutAlpha();
            //}
            //else
            //{
            //    fallOut();
            //}
            
            //tileRollOut();
            frameCode();
            base.BaseCode();
        }
        #region for tile roll out effect
        //DateTime TileRollOutBeginTime;
        //double TileRollOutTime = 1000;
        //bool IsTileRollOuting;
        //Vector3 TileRollOutOffset;
        ////Color TileRollOutColorFilter = Color.White;
        //public Vector3 EffectOffsetPixel
        //{
        //    get
        //    {
        //        return Util.Vector3AddVector3(TileRollOutOffset, ScreenOffsetPixel);
        //    }
        //}
        //public void TileRollOut(int ms)
        //{
        //    TileRollOutBeginTime = DateTime.Now;
        //    TileRollOutTime = ms;
        //    IsTileRollOuting = true;
        //}
        //void tileRollOut()
        //{
        //    if (IsTileRollOuting)
        //    {
        //        TimeSpan ts = DateTime.Now - TileRollOutBeginTime;
        //        if (ts.TotalMilliseconds > TileRollOutTime)
        //        {
        //            IsTileRollOuting = false;
        //        }
        //        else
        //        {
        //            TileRollOutOffset.X = (float)(ts.TotalMilliseconds * (double)game.Options.WindowSizePixel.Width / TileRollOutTime);
        //            //byte alpha = (byte)(ts.TotalMilliseconds * 255 / TileRollOutTime);
        //            //TileRollOutColorFilter = Color.FromArgb(alpha, Color.White);
        //        }
        //    }
        //}
        #endregion
        //public bool IsNobody()
        //{
        //    return IsNobody(scene, game.ME.FrontLocation);
        //}
        //bool IsNobody(Scene s, Vector3 v3)
        //{
        //    if (s == null)
        //    {
        //        return true;
        //    }
        //    if (s.game.NPCs == null)
        //    {
        //        return true;
        //    }
        //    foreach (NPC GetNPC(game.ME) in s.game.NPCs)
        //    {
        //        if (GetNPC(game.ME).RealLocation == v3)
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
        //    if (s.game.NPCs == null)
        //    {
        //        index = -1;
        //        return true;
        //    } 
        //    index = 0;
        //    foreach (NPC GetNPC(game.ME) in s.game.NPCs)
        //    {
        //        if (GetNPC(game.ME).RealLocation == v3)
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
        //    Move(game.SCN, chr, chr.RealDirection, stp);
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
        //    //game.ME leave door

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

        //    chr.LastLocation = Util.Vector3DivInt(chr.RealLocationPixel, game.Options.TilePixelX);
        //    chr.RealLocation = chr.NextLocation = loc;
        //    chr.NeedMovePixel += game.Options.TilePixelX;
        //}
        public override void OnKeyboardDownHoldCode(object sender, int key)
        {
            if (game.SCN == null)
            {
                return;
            }
            Key k = (Key)key;
            if (game.ME.NeedMovePixel == 0)
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

                    if (game.ME.Face(dir))
                    {
                        game.SCN.Face(game.ME.OppositeDirection);
                    }
                    if (game.ME.Move(game.SCN, game.GetNPC(game.ME), game.Options.TilePixelX))
                    {
                        game.SCN.Move(1, game.Options.TilePixelX);
                    }
                }
            }
        }
        public override void OnKeyboardUpCode(object sender, int key)
        {
            if (game.SCN == null)
            {
                return;
            }
            Key k = (Key)key;
            #region key yes
            if (k == game.Options.KeyYES)
            {
                NPC npc = game.GetNPC(game.ME);
                if (npc == null)
                {
                    return;
                }                
                if (npc.Script == null)
                {
                    return;
                }
                if (npc.Script == string.Empty)
                {
                    return;
                }
                game.SceneManager.Controllable = false;
                npc.FocusOnMe(game.ME);
                if (npc.IsBox)
                {
                    game.AppendScript(npc.Script);
                    game.AppendScript("scn setctrl");
                    game.ExecuteScript();
                }
                else if (npc.IsDoor)
                {
                }
                else
                {
                    //if (GetNPC(game.ME).Code == string.Empty)
                    //{
                    //    game.AppendScript("msg " + GetNPC(game.ME).DialogText);
                    //    game.AppendScript("untilpress j");
                    //    game.AppendScript("gui close MessageBox");
                    //    game.AppendScript("unfreezeme");
                    //    game.ExecuteScript();

                    //}
                    //else
                    game.AppendScript(npc.Script);

                    game.AppendScript("scn setctrl");
                    game.AppendScript("npc " + npc.Name + " dir def");
                    game.ExecuteScript();
                    //if (sm.IsNobody() == false)
                    //{
                    //    if (sm.game.ME.IsTalking)
                    //    {
                    //        sm.GetNPC(game.ME).FocusOnMe(sm.game.ME);
                    //        game.AppendAndExecuteScript("GetNPC(game.ME) say " + sm.GetNPC(game.ME).DialogText);
                    //    }
                    //    else
                    //    {
                    //        sm.GetNPC(game.ME).RecoverDirection();
                    //        game.AppendAndExecuteScript("gui close NPCTalk");
                    //    }
                    //}
                }
                //else if (k == Key.K)
                //{
                //    if (sm.GetNPC(game.ME) != null)
                //    {
                //        game.AppendAndExecuteScript("unfreezeme");
                //        sm.GetNPC(game.ME).RecoverDirection();
                //        game.AppendAndExecuteScript("gui close NPCTalk");
                //    }
                //}


            }
            #endregion
            //else if (k == Key.T)
            //{
            //    TileRollOut(1000);
            //}
        }
    }
}
