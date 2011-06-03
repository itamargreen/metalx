using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX.UI
{
    public class ControlBox
    {
        public string Name;
        public Color TextColor;
        public Color BgColor;
        public string BgImage;
        public string BgSound;
        public Size Size;
        public Point Location;
    }
    public class FormBox : ControlBox
    {
        public List<ControlBox> ControlBoxes = new List<ControlBox>();
    }

    public class TextBox : ControlBox
    {
        public string Text;
        public string[] Lines;
    }
    public class ButtonBox : ControlBox
    {
    }
    public class ImageBox : ControlBox
    {
    }
}
