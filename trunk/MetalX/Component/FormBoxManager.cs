using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX.DirectInput;
using MetalX.Data;

namespace MetalX.Component
{
    public class FormBoxManager : GameCom
    {
        List<int> AppearingFormBoxIndex = new List<int>();
        FormBox AppearingFormBox
        {
            get
            {
                if (AppearingFormBoxIndex.Count > 0)
                {
                    int i = AppearingFormBoxIndex[AppearingFormBoxIndex.Count - 1];
                    return game.FormBoxes[i];
                }
                return null;
            }
        }
        public FormBoxManager(Game g)
            : base(g)
        { }
        public override void Code()
        {
            base.Code();
        }
        void drawFormBox(FormBox fb)
        {
            foreach (ControlBox cb in fb.ControlBoxes)
            {
                if (cb is TextBox)
                {
                    drawTextBox((TextBox)cb, fb.Location);
                }
                else if (cb is TextureBox)
                {
                    drawTextureBox((TextureBox)cb, fb.Location);
                }
                else if (cb is ButtonBox)
                {
                    drawButtonBox((ButtonBox)cb, fb.Location);
                }
            }
        }
        void drawTextureBox(TextureBox tb,Point basepos)
        {
            basepos = Util.PointAddPoint(basepos, tb.Location);
            basepos = Util.PointAddPoint(basepos, ScreenOffsetPixelPoint);
            Color fColor = ColorFilter;
            
            fColor = Util.MixColor(fColor, tb.TextureFliterColor);
            if (tb.TextureIndex > -1)
            {
                game.DrawMetalXTexture(game.Textures[tb.TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), game.Textures[tb.TextureIndex].SizePixel), basepos, tb.Size, fColor);
            }
            else
            {
                int j = game.Textures.GetIndex(tb.TextureName);
                if (j < 0)
                {
                    return;
                }
                tb.TextureIndex = j;
                game.DrawMetalXTexture(game.Textures[j], new System.Drawing.Rectangle(new System.Drawing.Point(), game.Textures[tb.TextureIndex].SizePixel), basepos, tb.Size, fColor);
            }
        }
        void drawTextBox(TextBox tb, Point basepos)
        {
            basepos = Util.PointAddPoint(basepos, tb.Location);
            basepos = Util.PointAddPoint(basepos, ScreenOffsetPixelPoint);
            Color fColor = Util.MixColor(ColorFilter, tb.FontColor);
            if (tb.OneByOne)
            {
                game.DrawText(tb.SubText, basepos, tb.FontName, tb.FontSize, fColor);
            }
            else
            {
                game.DrawText(tb.Text, basepos, tb.FontName, tb.FontSize, fColor);
            }
        }
        void drawButtonBox(ButtonBox bb, Point basepos)
        {
            basepos = Util.PointAddPoint(basepos, bb.Location);
            basepos = Util.PointAddPoint(basepos, ScreenOffsetPixelPoint);
            if (bb.ButtonBoxState == ButtonBoxState.Down)
            {
                drawTextureBox(bb.DownTextureBox, basepos);
                drawTextBox(bb.DownTextBox, basepos);
            }
            else if (bb.ButtonBoxState == ButtonBoxState.Focus)
            {
                drawTextureBox(bb.FocusTextureBox, basepos);
                drawTextBox(bb.FocusTextBox, basepos);
            }
            else if (bb.ButtonBoxState == ButtonBoxState.Up)
            {
                drawTextureBox(bb.UpTextureBox, basepos);
                drawTextBox(bb.UpTextBox, basepos);
            }
            else if (bb.ButtonBoxState == ButtonBoxState.Wait)
            {
                drawTextureBox(bb.WaitTextureBox, basepos);
                drawTextBox(bb.WaitTextBox, basepos);
            }
        }
        public override void Draw()
        {
            base.Draw();
            for (int i = 0; i < AppearingFormBoxIndex.Count; i++)
            {
                FormBox fb = game.FormBoxes[AppearingFormBoxIndex[i]];
                drawFormBox(fb);
            }
        }

        public void Appear(string name)
        {
            int i = game.FormBoxes.GetIndex(name);
            Appear(i, null);
        }
        public void Appear(string name,object arg)
        {
            int i = game.FormBoxes.GetIndex(name);
            Appear(i, arg);
        }
        public void Appear(int i)
        {
            Appear(i, null);
        }
        public void Appear(int i,object arg)
        {
            for (int j = 0; j < AppearingFormBoxIndex.Count; j++)
            {
                if (AppearingFormBoxIndex[j] == i)
                {
                    return;
                }
            }
            AppearingFormBoxIndex.Add(i);
            game.FormBoxes[i].Appear(arg);
            //game.FormBoxes[i].OnFormBoxAppearCode();
        }
        public void Disappear()
        {
            Disappear(AppearingFormBoxIndex[AppearingFormBoxIndex.Count - 1]);
        }
        public void Disappear(string name)
        {
            int i = game.FormBoxes.GetIndex(name);
            Disappear(i);
        }
        public void Disappear(int i)
        {
            AppearingFormBoxIndex.Remove(i);
            game.FormBoxes[i].Disappear();
            //game.FormBoxes[i].OnFormBoxDisappearCode();
        }
        public void DisappearAll()
        {
            while (AppearingFormBoxIndex.Count > 0)
            {
                Disappear(AppearingFormBoxIndex[0]);
            }
        }

        public override void OnKeyboardDownCode(int key)
        {
            if (AppearingFormBoxIndex.Count < 1)
            {
                return;
            }

            Key k = (Key)key;
            if (k == Key.W || k == Key.A)
            {
                AppearingFormBox.FocusLastButtonBox();
            }
            else if (k == Key.S || k == Key.D)
            {
                AppearingFormBox.FocusNextButtonBox();
            }
            else if (k == Key.J)
            {
                AppearingFormBox.DownNowButtonBox();
            }
        }
        public override void OnKeyboardUpCode(int key)
        {
            if (AppearingFormBoxIndex.Count < 1)
            {
                return;
            } 
            
            Key k = (Key)key;
            if (k == Key.J)
            {
                AppearingFormBox.UpNowButtonBox();
                AppearingFormBox.FocusNowButtonBox();
            }
            else if (k == Key.K)
            {
                Disappear();
            }
        }
    }
}
