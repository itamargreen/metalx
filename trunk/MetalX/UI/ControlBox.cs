﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX.UI
{
    [Serializable]
    public class ControlBox
    {
        protected Game game;
        public bool Visible = true;
        public string Name;
        public int Index = -1;
        public Color BgColor;
        public string BgImage;
        public string BgSound;
        public Size Size;
        public Point Location;
        public ControlBox(Game g)
        {
            game = g;
        }
    }
    [Serializable]
    public class FormBox : ControlBox
    {
        public event FormBoxEvent OnFormBoxAppear;
        public event FormBoxEvent OnFormBoxDisappear;
        public List<ControlBox> ControlBoxes = new List<ControlBox>();
        public void Appear()
        {
            if (Visible)
            {
                return;
            }
            foreach (ControlBox cb in ControlBoxes)
            {
                if (cb is ButtonBox)
                {
                    ((ButtonBox)cb).ButtonBoxState = ButtonBoxState.Wait;
                }
            }
            Visible = true;
        }
        public void Disappear()
        {
            Visible = false;
        }
        public FormBox(Game g)
            : base(g)
        {
            OnFormBoxAppear = new FormBoxEvent(OnFormBoxAppearCode);
            OnFormBoxDisappear = new FormBoxEvent(OnFormBoxDisappearCode);
        }

        public virtual void OnFormBoxAppearCode(int formBoxIndex)
        {
            //throw new NotImplementedException();
        }

        public virtual void OnFormBoxDisappearCode(int formBoxIndex)
        {
            //throw new NotImplementedException();
        }
    }
    [Serializable]
    public class TextBox : ControlBox
    {
        public string Text;
        public Color TextColor;
        public int TextIndex = -1;
        public string TextFileName;
        public string[] Lines
        {
            get
            {
                return Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
        }
        public bool OneByOne = true;
        public int Interval = 33;
        DateTime lastCharacterTime;
        public Size FontSize;
        public int SubTextIndex = 1;
        public event TextBoxEvent OnSubTextShowDone;
        public string SubText
        {
            get
            {
                TimeSpan timespan = DateTime.Now - lastCharacterTime;
                if (timespan.Milliseconds > Interval)
                {
                    if (SubTextIndex < Text.Length)
                    {
                        SubTextIndex++;
                        lastCharacterTime = DateTime.Now;
                    }
                    else
                    {
                        OneByOne = false;
                        //OnTextBoxShowDone();
                    }
                }
                return Text.Substring(0, SubTextIndex);
            }
        }
        public TextBox(Game g)
            : base(g)
        {
            OnSubTextShowDone = new TextBoxEvent(OnSubTextShowDoneCode);
        }
        public virtual void OnSubTextShowDoneCode(int textBoxIndex)
        { 
        }
        public void LoadText(string pathName, bool onebyone, int interval)
        {
            TextFileName = pathName;
            Text = System.IO.File.ReadAllText(TextFileName);
            OneByOne = onebyone;
            Interval = interval;
        }
    }
    [Serializable]
    public class TextureBox : ControlBox
    {
        public string TextureFileName;
        public int TextureIndex = -1;
        public Color TextureColor;
        public TextureBox(Game g)
            : base(g)
        { }
    }
    [Serializable]
    public class ButtonBox : ControlBox
    {
        public ButtonBoxState ButtonBoxState;

        public TextureBox WaitTextureBox;
        public TextureBox FocusTextureBox;
        public TextureBox DownTextureBox;
        public TextureBox UpTextureBox;

        public TextBox WaitTextBox;
        public TextBox FocusTextBox;
        public TextBox DownTextBox;
        public TextBox UpTextBox;

        public event ButtonBoxEvent OnButtonDown;
        public event ButtonBoxEvent OnButtonFocus;
        public event ButtonBoxEvent OnButtonUp;
        public event ButtonBoxEvent OnButtonWait;

        void HideAll()
        {
            WaitTextureBox.Visible = FocusTextureBox.Visible = DownTextureBox.Visible = UpTextureBox.Visible = false;
        }
        /// <summary>
        /// 转换到等待状态
        /// </summary>
        public void Wait()
        {
            HideAll();
            WaitTextureBox.Visible = true;
        }
        /// <summary>
        /// 转换到选中状态
        /// </summary>
        public void Focus()
        {
            HideAll();
            FocusTextureBox.Visible = true;
        }
        /// <summary>
        /// 转换到按下状态
        /// </summary>
        public void Down()
        {
            HideAll();
            DownTextureBox.Visible = true;
        }
        /// <summary>
        /// 转换到抬起状态
        /// </summary>
        public void Up()
        {
            HideAll();
            UpTextureBox.Visible = true;
        }

        public ButtonBox(Game g)
            : base(g)
        {
            OnButtonWait = new ButtonBoxEvent(OnButtonWaitCode);
            OnButtonFocus = new ButtonBoxEvent(OnButtonFocusCode);
            OnButtonDown = new ButtonBoxEvent(OnButtonDownCode);
            OnButtonUp = new ButtonBoxEvent(OnButtonUpCode);
        }

        public virtual void OnButtonUpCode(int buttnBoxIndex)
        {
        }

        public virtual void OnButtonDownCode(int buttnBoxIndex)
        {
        }

        public virtual void OnButtonFocusCode(int buttnBoxIndex)
        {
        }

        public virtual void OnButtonWaitCode(int buttnBoxIndex)
        {
        }
    }
}
