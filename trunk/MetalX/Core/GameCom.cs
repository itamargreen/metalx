using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX
{
    public class GameCom
    {
        protected Game game;

        bool enable = false;
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }
        bool visible = false;
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }
        bool controllable = false;
        public bool Controllable
        {
            get
            {
                return controllable;
            }
            set
            {
                controllable = value;
            }
        }

        public void EnableAll()
        {
            controllable = enable = visible = true;
        }
        public void DisableAll()
        {
            controllable = enable = visible = false;
        }

        Color colorFilter = Color.FromArgb(255, Color.White);
        public Color ColorFilter
        {
            get
            {
                return colorFilter;
            }
            set
            {
                colorFilter = value;
            }
        }

        public GameCom(Game game)
        {
            this.game = game;
            EnableAll();
        }

        public virtual void Code()
        {
            Shock();
            Fallout();
        }
        public virtual void Draw()
        {

        }
        #region for shock
        protected Point GlobalOffset;
        DateTime ShockBeginTime;
        int ShockTime = 500;
        int ShockRange = 4;
        bool IsShocking;
        public void ShockScreen(int ms)
        {
            ShockScreen(ms, 4);
        }
        public void ShockScreen(int ms, int range)
        {
            if (IsShocking)
            {
                
            }
            else
            {
                ShockBeginTime = DateTime.Now;
                ShockTime = ms;
                ShockRange = range * 2;
                IsShocking = true;
            }
        }
        protected void Shock()
        {
            if (IsShocking)
            {
                GlobalOffset = new Point();
                TimeSpan ts = DateTime.Now - ShockBeginTime;
                if (ts.TotalMilliseconds > ShockTime)
                {
                    IsShocking = false;
                }
                else
                {
                    int x = Util.Roll(0, ShockRange);
                    int y = Util.Roll(0, ShockRange);
                    x -= (ShockRange / 2);
                    y -= (ShockRange / 2);
                    GlobalOffset.X += x;
                    GlobalOffset.Y += y;
                }
            }
        }
        #endregion
        #region for fallout
        bool IsFallouting = false;
        DateTime FalloutBegineTime;
        int FalloutTime;
        double fallout_interval;
        bool IsFallin;
        public void FalloutSceen(int ms)
        {
            FalloutBegineTime = DateTime.Now;
            FalloutTime = ms;
            fallout_interval = FalloutTime / 255;
            IsFallouting = true;
            IsFallin = false;
        }
        public void FallinSceen(int ms)
        {
            FalloutSceen(ms);
            IsFallin = true;
        }
        protected void Fallout()
        {
            TimeSpan ts = DateTime.Now - FalloutBegineTime;
            if (IsFallouting)
            {
                if (ts.TotalMilliseconds > FalloutTime)
                {
                    if (IsFallin)
                    {
                        ColorFilter = Color.FromArgb(255, 255, 255);
                    }
                    else
                    {
                        ColorFilter = Color.FromArgb(0, 0, 0);
                    }
                    IsFallouting = false;
                }
                else
                {
                    int frame = (int)((ts.TotalMilliseconds / (double)(FalloutTime)) * 255);
                    if (IsFallin)
                    {
                        ColorFilter = Color.FromArgb(frame, frame, frame);
                    }
                    else
                    {
                        ColorFilter = Color.FromArgb(255 - frame, 255 - frame, 255 - frame);
                    }
                }
            }
        }
        #endregion
    }

}
