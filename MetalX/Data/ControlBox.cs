using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX.Data
{
    [Serializable]
    public class ControlBox
    {
        protected Game game;
        public bool Visible = false;
        public string Name;
        //public int Index = -1;

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
        public int ButtonBoxCount
        {
            get
            {
                int c = 0;
                foreach (ControlBox cb in ControlBoxes)
                {
                    if (cb is ButtonBox)
                    {
                        c++;
                    }
                }
                return c;
            }
        }
        public int NowButtonBoxIndex = -1;
        public int BigStep = 8;
        //public int FocusButtonBoxIndex
        //{
        //    get
        //    {
        //        int c = 0;
        //        foreach (ControlBox cb in ControlBoxes)
        //        {
        //            if (cb is ButtonBox)
        //            {
        //                if (((ButtonBox)cb).ButtonBoxState == ButtonBoxState.Focus)
        //                {
        //                    return c;
        //                }
        //                c++;
        //            }
        //        }
        //        return -1;
        //    }
        //}
        public void DownNowButtonBox()
        {
            setButtonBoxState(NowButtonBoxIndex, ButtonBoxState.Down);
        }
        public void UpNowButtonBox()
        {
            setButtonBoxState(NowButtonBoxIndex, ButtonBoxState.Up);
        }
        public void FocusNowButtonBox()
        {
            setButtonBoxState(NowButtonBoxIndex, ButtonBoxState.Focus);
        }
        public void FocusNextButtonBox()
        {
            int i = NowButtonBoxIndex;
            i++;
            if (i >= ButtonBoxCount)
            {
                i = 0;
            }
            WaitAllButtonBox();
            setButtonBoxState(i, ButtonBoxState.Focus);
            NowButtonBoxIndex = i;
        }
        public void FocusLastButtonBox()
        {
            int i = NowButtonBoxIndex;
            i--;
            if (i < 0)
            {
                i = ButtonBoxCount - 1;
            }
            WaitAllButtonBox();
            setButtonBoxState(i, ButtonBoxState.Focus);
            NowButtonBoxIndex = i;
        }     
        void setButtonBoxState(int i, ButtonBoxState bbs)
        {
            int j = 0;
            for (int k = 0; k < ControlBoxes.Count; k++)
            {
                if (ControlBoxes[k] is ButtonBox)
                {
                    if (j == i)
                    {
                        ((ButtonBox)ControlBoxes[k]).ButtonBoxState = bbs;                        
                        return;
                    }
                    j++;
                }
            }
        }
        public void WaitAllButtonBox()
        {
            for (int i = 0; i < ButtonBoxCount; i++)
            {
                setButtonBoxState(i, ButtonBoxState.Wait);
            }
        }
        public TextureBox BGTextureBox;
        public event FormBoxEvent OnFormBoxAppear;
        public event FormBoxEvent OnFormBoxDisappear;
        public List<ControlBox> ControlBoxes = new List<ControlBox>();
        public void Appear()
        {
            Appear(null);
        }
        public void Appear(object arg)
        {
            //if (Visible)
            //{
            //    return;
            //}
            foreach (ControlBox cb in ControlBoxes)
            {
                if (cb is ButtonBox)
                {
                    ((ButtonBox)cb).ButtonBoxState = ButtonBoxState.Wait;
                } 
                else if (cb is TextBox)
                {
                    ((TextBox)cb).SubTextIndex = 0;
                }
            }
            Visible = true;
            NowButtonBoxIndex = -1;
            FocusNextButtonBox();
            OnFormBoxAppear(this, arg);
        }
        public void Disappear()
        {
            Disappear(null);
        }
        public void Disappear(object arg)
        {
            Visible = false;
            OnFormBoxDisappear(this, arg);
        }
        public FormBox(Game g)
            : base(g)
        {
            BGTextureBox = new TextureBox(g);
            ControlBoxes.Add(BGTextureBox);
            OnFormBoxAppear = new FormBoxEvent(OnFormBoxAppearCode);
            OnFormBoxDisappear = new FormBoxEvent(OnFormBoxDisappearCode);
        }

        public virtual void OnFormBoxAppearCode(object sender, object arg)
        {
        }

        public virtual void OnFormBoxDisappearCode(object sender, object arg)
        {
        }
    }
    [Serializable]
    public class TextBox : ControlBox
    {
        public string Text = "";
        public Color FontColor = Color.White;
        public int TextIndex = -1;
        public string TextFileName;
        public string FontName = "微软雅黑";
        public int FontSize = 15;
        public string[] Lines
        {
            get
            {
                return Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
        }
        public bool OneByOne = false;
        public int Interval = 33;
        DateTime lastCharacterTime;
        public int SubTextIndex = 0;
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
                        //OneByOne = false;
                        OnSubTextShowDone(null);
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
        public virtual void OnSubTextShowDoneCode(object arg)
        { 
        }
        public void LoadText(string pathName, bool onebyone, int interval)
        {
            SubTextIndex = 0;
            TextFileName = pathName;
            Text = System.IO.File.ReadAllText(TextFileName);
            OneByOne = onebyone;
            Interval = interval;
        }
    }
    [Serializable]
    public class TextureBox : ControlBox
    {
        public string TextureName;
        public int TextureIndex = -1;
        public Color TextureFliterColor = Color.White;
        public TextureBox(Game g)
            : base(g)
        { }
    }
    [Serializable]
    public class ButtonBox : ControlBox
    {
        ButtonBoxState buttonBoxState;
        public ButtonBoxState ButtonBoxState
        {
            get
            {
                return buttonBoxState;
            }
            set
            {
                buttonBoxState = value;
                if (value == ButtonBoxState.Up)
                {
                    OnButtonUp(null);
                }
                else if (value == ButtonBoxState.Down)
                {
                    OnButtonDown(null);
                }
                else if (value == ButtonBoxState.Focus)
                {
                    OnButtonFocus(null);
                }
                else if (value == ButtonBoxState.Wait)
                {
                    OnButtonWait(null);
                }
            }
        }

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

        //void HideAll()
        //{
        //    WaitTextureBox.Visible = FocusTextureBox.Visible = DownTextureBox.Visible = UpTextureBox.Visible = false;
        //}
        ///// <summary>
        ///// 转换到等待状态
        ///// </summary>
        //public void Wait()
        //{
        //    HideAll();
        //    WaitTextureBox.Visible = true;
        //}
        ///// <summary>
        ///// 转换到选中状态
        ///// </summary>
        //public void Focus()
        //{
        //    HideAll();
        //    FocusTextureBox.Visible = true;
        //}
        ///// <summary>
        ///// 转换到按下状态
        ///// </summary>
        //public void Down()
        //{
        //    HideAll();
        //    DownTextureBox.Visible = true;
        //}
        ///// <summary>
        ///// 转换到抬起状态
        ///// </summary>
        //public void Up()
        //{
        //    HideAll();
        //    UpTextureBox.Visible = true;
        //}

        public ButtonBox(Game g)
            : base(g)
        {
            WaitTextureBox = new TextureBox(g);
            UpTextureBox = new TextureBox(g);
            DownTextureBox = new TextureBox(g);
            FocusTextureBox = new TextureBox(g);
            WaitTextBox = new TextBox(g);
            UpTextBox = new TextBox(g);
            DownTextBox = new TextBox(g);
            FocusTextBox = new TextBox(g);
            OnButtonWait = new ButtonBoxEvent(OnButtonWaitCode);
            OnButtonFocus = new ButtonBoxEvent(OnButtonFocusCode);
            OnButtonDown = new ButtonBoxEvent(OnButtonDownCode);
            OnButtonUp = new ButtonBoxEvent(OnButtonUpCode);
        }

        public virtual void OnButtonUpCode(object arg)
        {
        }

        public virtual void OnButtonDownCode(object arg)
        {
        }

        public virtual void OnButtonFocusCode(object arg)
        {
        }

        public virtual void OnButtonWaitCode(object arg)
        {
        }
    }
}
