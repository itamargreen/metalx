using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX.Data
{
    [Serializable]
    public class ControlBox
    {
        protected Game game;
        public bool Visible = true;
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
            OnFormBoxAppear();
        }
        public void Disappear()
        {
            Visible = false;
            OnFormBoxDisappear();
        }
        public FormBox(Game g)
            : base(g)
        {
            BGTextureBox = new TextureBox(g);
            ControlBoxes.Add(BGTextureBox);
            OnFormBoxAppear = new FormBoxEvent(OnFormBoxAppearCode);
            OnFormBoxDisappear = new FormBoxEvent(OnFormBoxDisappearCode);
        }

        public virtual void OnFormBoxAppearCode()
        {
        }

        public virtual void OnFormBoxDisappearCode()
        {
        }
    }
    [Serializable]
    public class TextBox : ControlBox
    {
        public string Text;
        public Color TextColor = Color.White;
        public int TextIndex = -1;
        public string TextFileName;
        public string TextFont = "新宋体";
        public float TextFontSize = 9f;
        public string[] Lines
        {
            get
            {
                return Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
        }
        public bool OneByOne = false;
        public int Interval = 100;
        DateTime lastCharacterTime;
        public Size FontSize;
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
        public virtual void OnSubTextShowDoneCode()
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
                    OnButtonUp();
                }
                else if (value == ButtonBoxState.Down)
                {
                    OnButtonDown();
                }
                else if (value == ButtonBoxState.Focus)
                {
                    OnButtonFocus();
                }
                else if (value == ButtonBoxState.Wait)
                {
                    OnButtonWait();
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

        public virtual void OnButtonUpCode()
        {
        }

        public virtual void OnButtonDownCode()
        {
        }

        public virtual void OnButtonFocusCode()
        {
        }

        public virtual void OnButtonWaitCode()
        {
        }
    }
}
