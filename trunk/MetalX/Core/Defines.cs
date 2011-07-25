using System;
using System.Collections.Generic;

namespace MetalX
{
    public delegate void KeyboardEvent(object sender, int key);
    public delegate void FormBoxEvent(object arg);
    public delegate void ButtonBoxEvent(object arg);
    public delegate void TextBoxEvent(object arg);
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
    public enum TextureEffectType
    {
        Shock,
        FallOut,
        FallIn,
        None,
        NotDisappear,
    }
    [Serializable]
    public enum Direction
    {
        /// <summary>
        /// 上
        /// </summary>
        U = 0,
        /// <summary>
        /// 下
        /// </summary>
        L = 1,
        /// <summary>
        /// 左
        /// </summary>
        D = 2,
        /// <summary>
        /// 右
        /// </summary>
        R = 3,
    }
    //public class TextureEffect
    //{
    //    public TextureEffectType Type;
    //    public TimeSpan TimeSpan;
    //    public bool IsBlock;
    //    public TextureEffect(TextureEffectType type, double ms, bool isBlock)
    //    {
    //        IsBlock = isBlock;
    //        Type = type;
    //        TimeSpan = TimeSpan.FromMilliseconds(ms);
    //    }
    //}
    //public class FormBoxes2Play
    //{
    //    public string Name;
    //    public List<TextureEffect> TextureEffectList;
    //    public FormBoxes2Play(string name, List<TextureEffect> tel)
    //    {
    //        Name = name;
    //        TextureEffectList = tel;
    //    }
    //}
}
