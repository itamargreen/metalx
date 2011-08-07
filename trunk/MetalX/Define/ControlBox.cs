using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX.Define
{
    [Serializable]
    public class ControlBox
    {
        protected Game game;
        public bool Visible = false;
        public string Name;
        public int Index = -1;

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
        public int BigStep = 1;
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
            int t = getButtonActuallyIndex(i);
            if (t > -1)
            {
                int j = 0;
                while ((
                    ((ButtonBox)ControlBoxes[t]).WaitTextBox.Text == null
                    ||
                    ((ButtonBox)ControlBoxes[t]).WaitTextBox.Text == ""
                    ) && j++<ButtonBoxCount)
                {                    
                    i++;
                    if (i >= ButtonBoxCount)
                    {
                        i = 0;
                    }
                    t = getButtonActuallyIndex(i);
                }
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
            int t = getButtonActuallyIndex(i);
            if (t > -1)
            {
                int j = 0;
                while ((
                    ((ButtonBox)ControlBoxes[t]).WaitTextBox.Text == null
                    ||
                    ((ButtonBox)ControlBoxes[t]).WaitTextBox.Text == ""
                    ) && j++<ButtonBoxCount)
                {
                    i--;
                    if (i < 0)
                    {
                        i = ButtonBoxCount - 1;
                    }
                    t = getButtonActuallyIndex(i);
                }
            }

            WaitAllButtonBox();
            setButtonBoxState(i, ButtonBoxState.Focus);
            NowButtonBoxIndex = i;
        }
        int getButtonActuallyIndex(int i)
        {
            int j = 0;
            for (int k = 0; k < ControlBoxes.Count; k++)
            {
                if (ControlBoxes[k] is ButtonBox)
                {
                    if (j == i)
                    {
                        return k;
                    }
                    j++;
                }
            }
            return -1;
        }
        void setButtonBoxState(int i, ButtonBoxState bbs)
        {
            int j = getButtonActuallyIndex(i);
            if (j > -1)
            {
                ((ButtonBox)ControlBoxes[j]).ButtonBoxState = bbs;
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
        public string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                SubTextIndex = 0;
                text = value;
            }
        }
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
                if (Text == null)
                {
                    return "";
                }
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
                        OnSubTextShowDone(this, null);
                    }
                }
                string str = Text;
                try
                {
                    str = Text.Substring(0, SubTextIndex);
                }
                catch
                { }
                return str;
            }
        }
        public TextBox(Game g)
            : base(g)
        {
            OnSubTextShowDone = new TextBoxEvent(OnSubTextShowDoneCode);
        }
        public virtual void OnSubTextShowDoneCode(object sender,object arg)
        { 
        }
        public void FromFile(string pathName, bool onebyone, int interval)
        {
            SubTextIndex = 0;
            TextFileName = pathName;
            Text = System.IO.File.ReadAllText(TextFileName);
            OneByOne = onebyone;
            Interval = interval;
        }
        public TextBox GetClone()
        {
            return (TextBox)MemberwiseClone();
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
        public TextureBox GetClone()
        {
            return (TextureBox)MemberwiseClone();
        }
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
                    OnButtonUp(this,null);
                }
                else if (value == ButtonBoxState.Down)
                {
                    OnButtonDown(this,null);
                }
                else if (value == ButtonBoxState.Focus)
                {
                    OnButtonFocus(this,null);
                }
                else if (value == ButtonBoxState.Wait)
                {
                    OnButtonWait(this,null);
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

        public void SameAsWait()
        {
            FocusTextBox = WaitTextBox.GetClone();
            FocusTextBox.FontColor = Color.CornflowerBlue;
            DownTextBox = WaitTextBox.GetClone();
            DownTextBox.FontColor = Color.Yellow;
            UpTextBox = WaitTextBox.GetClone();

            FocusTextureBox = WaitTextureBox.GetClone();
            DownTextureBox = WaitTextureBox.GetClone();
            UpTextureBox = WaitTextureBox.GetClone();
        }

        public virtual void OnButtonUpCode(object sender, object arg)
        {
        }

        public virtual void OnButtonDownCode(object sender, object arg)
        {
        }

        public virtual void OnButtonFocusCode(object sender, object arg)
        {
        }

        public virtual void OnButtonWaitCode(object sender, object arg)
        {
        }
    }
}
