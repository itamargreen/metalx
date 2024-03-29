﻿using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.DirectX;

using MetalX.Define;

namespace MetalX
{
    public class GameCom
    {
        public event KeyboardEvent OnKeyDown;
        public event KeyboardEvent OnKeyDownHold;
        public event KeyboardEvent OnKeyUp;

        protected Game game;

        public bool Enable = false;
        public bool Visible = false;
        public bool Controllable = false;

        public void EnableAll()
        {
            Visible = true;
            Controllable = true;
            Enable = true;
        }
        public void DisableAll()
        {
            Visible = false;
            Controllable = false;
            Enable = false;
        }

        public Color ColorFilter = Color.FromArgb(255, Color.White);

        public GameCom(Game game)
        {
            this.game = game;
            OnKeyDown = new KeyboardEvent(OnKeyDownCode);
            OnKeyUp = new KeyboardEvent(OnKeyUpCode);
            OnKeyDownHold = new KeyboardEvent(OnKeyDownHoldCode);
            EnableAll();
        }
        public virtual void OnKeyDownCode(object sender, int key)
        {
        }
        public virtual void OnKeyUpCode(object sender, int key)
        {
        }
        public virtual void OnKeyDownHoldCode(object sender, int key)
        {
        }

        public virtual void BaseCode()
        { 
            shock();
            if (FallOutAlpha)
            {
                fallOutAlpha();
            }
            else
            {
                fallOut();
            }
            delay();
        }
        public virtual void Code()
        {
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
                OnKeyDown(this, key);
            }
            else if (keyState == KeyState.Up)
            {
                OnKeyUp(this, key);
            }
            else if (keyState == KeyState.DownHold)
            {
                OnKeyDownHold(this, key);
            }
        }
        protected DateTime DelayBeginTime;
        protected bool IsDelaying;
        protected double DelayTime;
        public void Delay(int ms)
        {
            if (IsDelaying)
            {
                DelayTime += ms;
            }
            else
            {
                DelayBeginTime = DateTime.Now;
                Enable = Controllable = false;
                IsDelaying = true;
                DelayTime = ms;
            }
        }
        void delay()
        {
            if (IsDelaying)
            {
                TimeSpan ts = DateTime.Now - DelayBeginTime;
                if (ts.TotalMilliseconds > DelayTime)
                {
                    EnableAll();
                    IsDelaying = false;
                }
            }
        } 
        #region for shock
        protected Vector3 ScreenOffsetPixel;
        protected Point ScreenOffsetPixelPoint
        {
            get
            {
                return new Point((int)ScreenOffsetPixel.X, (int)ScreenOffsetPixel.Y);
            }
        }

        DateTime ShockBeginTime;
        double ShockTime = 500;
        int ShockRange = 4;
        bool IsShocking;
        public void ShockScreen(double ms)
        {
            ShockScreen(ms, 2);
        }
        public void ShockScreen(double ms, int range)
        {
            //if (IsShocking)
            //{
                
            //}
            //else
            {
                ShockBeginTime = DateTime.Now;
                ShockTime = ms;
                ShockRange = range * 2;
                IsShocking = true;
            }
        }
        void shock()
        {
            if (IsShocking)
            {
                ScreenOffsetPixel = new Vector3();
                TimeSpan ts = DateTime.Now - ShockBeginTime;
                if (ts.TotalMilliseconds > ShockTime)
                {
                    IsShocking = false;
                }
                else
                {
                    int x = Util.Roll(0, ShockRange);
                    int y = Util.Roll(0, ShockRange);
                    int z = Util.Roll(0, ShockRange);
                    x -= (ShockRange / 2);
                    y -= (ShockRange / 2);
                    z -= (ShockRange / 2);
                    z *= 10;
                    ScreenOffsetPixel.X += x;
                    ScreenOffsetPixel.Y += y;
                    ScreenOffsetPixel.Z += z;
                }
            }
        }
        #endregion
        #region for fallout
        bool IsFallOuting = false;
        DateTime FalloutBegineTime;
        double FalloutTime;
        double fallout_interval;
        bool IsFallin;
        public void FallOutSceen(double ms)
        {
            FalloutBegineTime = DateTime.Now;
            FalloutTime = ms;
            fallout_interval = FalloutTime / 255;
            IsFallOuting = true;
            IsFallin = false;
        }
        public void FallInSceen(double ms)
        {
            FallOutSceen(ms);
            IsFallin = true;
        }
        public bool FallOutAlpha = false;
        void fallOut()
        {
            
            if (IsFallOuting)
            {TimeSpan ts = DateTime.Now - FalloutBegineTime;
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
                    if (frame < 0)
                    {
                        frame = 255;
                    }
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
        void fallOutAlpha()
        {
            
            if (IsFallOuting)
            {TimeSpan ts = DateTime.Now - FalloutBegineTime;
                if (ts.TotalMilliseconds > FalloutTime)
                {
                    if (IsFallin)
                    {
                        ColorFilter = Color.FromArgb(255, 255, 255, 255);
                    }
                    else
                    {
                        ColorFilter = Color.FromArgb(0, 0, 0, 0);
                    }
                    IsFallOuting = false;
                }
                else
                {
                    int frame = (int)((ts.TotalMilliseconds / (double)(FalloutTime)) * 255);
                    if (frame < 0)
                    {
                        frame = 255;
                    }
                    if (IsFallin)
                    {
                        ColorFilter = Color.FromArgb(frame, frame, frame, frame);
                    }
                    else
                    {
                        ColorFilter = Color.FromArgb(255-frame, 255 - frame, 255 - frame, 255 - frame);
                    }
                }
            }

        }
        #endregion
    }

}
