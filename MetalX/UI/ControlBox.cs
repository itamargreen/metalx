using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX.UI
{
    public delegate void FormBoxEvent(int formBoxIndex);
    public delegate void ButtonBoxEvent(int buttnBoxIndex);
    public delegate void TextBoxEvent(int textBoxIndex);
    public enum ButtonBoxState
    {
        Wait,
        Focus,
        Down,
        Up,
    }
    public class ControlBox
    {
        public bool Visible = true;
        public string Name;
        public int Index = -1;
        public Color TextColor;
        public Color BgColor;
        public string BgImage;
        public string BgSound;
        public Size Size;
        public Point Location;
    }
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
            if (!Visible)
            {
                return;
            }
        }
        public FormBox()
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

    public class TextBox : ControlBox
    {
        public string Text;
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
        public TextBox()
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

    public class TextureBox : ControlBox
    {
        public Microsoft.DirectX.Direct3D.Texture Texture;
        public string TextureFileName;
        public int TextureIndex = -1;
        public void LoadTexture(Microsoft.DirectX.Direct3D.Device dev, string pathName)
        {
            TextureFileName = pathName;
            Texture = Microsoft.DirectX.Direct3D.TextureLoader.FromFile(dev, pathName);
        }
    }

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

        public event ButtonBoxEvent OnButtonWait;
        public event ButtonBoxEvent OnButtonFocus;
        public event ButtonBoxEvent OnButtonDown;
        public event ButtonBoxEvent OnButtonUp;

        public ButtonBox()
        {            
            OnButtonWait = new ButtonBoxEvent(OnButtonWaitCode);
            OnButtonFocus = new ButtonBoxEvent(OnButtonFocusCode);
            OnButtonDown = new ButtonBoxEvent(OnButtonDownCode);
            OnButtonUp = new ButtonBoxEvent(OnButtonUpCode);
        }

        public virtual void OnButtonUpCode(int buttnBoxIndex)
        {
            //throw new NotImplementedException();
        }

        public virtual void OnButtonDownCode(int buttnBoxIndex)
        {
            //throw new NotImplementedException();
        }

        public virtual void OnButtonFocusCode(int buttnBoxIndex)
        {
            //throw new NotImplementedException();
        }

        public virtual void OnButtonWaitCode(int buttnBoxIndex)
        {
            //throw new NotImplementedException();
        }
    }
}
