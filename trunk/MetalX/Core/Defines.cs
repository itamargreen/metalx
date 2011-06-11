using System;
using System.Collections.Generic;

namespace MetalX
{
    public delegate void KeyboardEvent(int key);
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
    public enum KeyState
    {
        Up,
        Down,
        DownHold,
    }
    public enum TextureDrawMode
    {
        Direct3D,
        Direct2D,
    }
}
