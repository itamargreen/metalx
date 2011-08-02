using System;
using System.Collections.Generic;

namespace MetalX
{
    public delegate void KeyboardEvent(object sender, int key);
    public delegate void FormBoxEvent(object sender, object arg);
    public delegate void ButtonBoxEvent(object sender,object arg);
    public delegate void TextBoxEvent(object sender, object arg);
    //public delegate void NPCEvent(object sender);
    public struct ScriptReturn
    {
        public bool BOOL;
        public int INT;
        public string STRING;
    }
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

    //[Serializable]
    //public class Direction
    //{
    //    public const int U = 0, L = 1, D = 2, R = 3;
    //}

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
