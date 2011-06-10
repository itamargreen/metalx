using System;
using System.Collections.ObjectModel;

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
}
