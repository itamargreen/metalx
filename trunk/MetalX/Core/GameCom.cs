﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX
{
    public class GameCom
    {
        public event KeyboardEvent OnKeyboardDown;
        public event KeyboardEvent OnKeyboardDownHold;
        public event KeyboardEvent OnKeyboardUp;

        protected Game game;

        public bool Enable = false;
        public bool Visible = false;
        public bool Controllable = false;

        public void EnableAll()
        {
            Controllable = Enable = Visible = true;
        }
        public void DisableAll()
        {
            Controllable = Enable = Visible = true;
        }

        public Color ColorFilter = Color.FromArgb(255, Color.White);

        public GameCom(Game game)
        {
            this.game = game;
            OnKeyboardDown = new KeyboardEvent(OnKeyboardDownCode);
            OnKeyboardUp = new KeyboardEvent(OnKeyboardUpCode);
            OnKeyboardDownHold = new KeyboardEvent(OnKeyboardDownHoldCode);
            EnableAll();
        }
        public virtual void OnKeyboardDownCode(int key)
        {
        }
        public virtual void OnKeyboardUpCode(int key)
        {
        }
        public virtual void OnKeyboardDownHoldCode(int key)
        {
        }

        public virtual void Code()
        {
            Shock();
            FallOut();
        }
        public virtual void Draw()
        {

        }
        public void SetKeyboardEvent(int key, KeyState keyState)
        {
            if (!Controllable)
            {
                return;
            }
            if (keyState == KeyState.Down)
            {
                OnKeyboardDown(key);
            } 
            else if (keyState == KeyState.Up)
            {
                OnKeyboardUp(key);
            } 
            else if (keyState == KeyState.DownHold)
            {
                OnKeyboardDownHold(key);
            }
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
        bool IsFallOuting = false;
        DateTime FalloutBegineTime;
        int FalloutTime;
        double fallout_interval;
        bool IsFallin;
        public void FallOutSceen(int ms)
        {
            FalloutBegineTime = DateTime.Now;
            FalloutTime = ms;
            fallout_interval = FalloutTime / 255;
            IsFallOuting = true;
            IsFallin = false;
        }
        public void FallInSceen(int ms)
        {
            FallOutSceen(ms);
            IsFallin = true;
        }
        protected void FallOut()
        {
            TimeSpan ts = DateTime.Now - FalloutBegineTime;
            if (IsFallOuting)
            {
                if (ts.TotalMilliseconds > FalloutTime)
                {
                    if (IsFallin)
                    {
                        ColorFilter = Color.FromArgb(255, 255, 255, 255);
                    }
                    else
                    {
                        ColorFilter = Color.FromArgb(255, 0, 0, 0);
                    }
                    IsFallOuting = false;
                }
                else
                {
                    int frame = (int)((ts.TotalMilliseconds / (double)(FalloutTime)) * 255);
                    if (IsFallin)
                    {
                        ColorFilter = Color.FromArgb(255, frame, frame, frame);
                    }
                    else
                    {
                        ColorFilter = Color.FromArgb(255, 255 - frame, 255 - frame, 255 - frame);
                    }
                }
            }
        }
        #endregion
    }

}