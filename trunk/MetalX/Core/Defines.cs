﻿using System;
using System.Collections.Generic;

namespace MetalX
{
    public delegate void KeyboardEvent(int key);
    public delegate void FormBoxEvent();
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
    public enum TextureEffectType
    {
        Shock,
        FallOut,
        FallIn,
        None,
        NotDisappear,
    }
    public class TextureEffect
    {
        public TextureEffectType Type;
        public TimeSpan TimeSpan;
        public bool IsBlock;
        public TextureEffect(TextureEffectType type, double ms, bool isBlock)
        {
            IsBlock = isBlock;
            Type = type;
            TimeSpan = TimeSpan.FromMilliseconds(ms);
        }
    }
    public class FormBoxes2Play
    {
        public string Name;
        public List<TextureEffect> TextureEffectList;
        public FormBoxes2Play(string name, List<TextureEffect> tel)
        {
            Name = name;
            TextureEffectList = tel;
        }
    }
}