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
            //Point pos = Util.PointAddPoint(fb.Location, ScreenOffsetPoint);
            //Color fColor = Util.MixColor(ColorFilter, fb.BGTextureFliterColor);
            //if (fb.BGTextureIndex > -1)
            //{
            //    game.DrawMetalXTexture(game.Textures[fb.BGTextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), fb.Size), pos, fb.Size, fColor);
            //}
            //else
            //{
            //    int j = game.Textures.GetIndex(fb.BGTextureName);
            //    game.DrawMetalXTexture(game.Textures[j], new System.Drawing.Rectangle(new System.Drawing.Point(), fb.Size), pos, fb.Size, fColor);
            //}
        }
        void drawTextureBox(TextureBox tb,Point basepos)
        {
            basepos = Util.PointAddPoint(basepos, tb.Location);
            basepos = Util.PointAddPoint(basepos, ScreenOffsetPoint);
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
            basepos = Util.PointAddPoint(basepos, ScreenOffsetPoint);
            Color fColor = Util.MixColor(ColorFilter, tb.TextColor);
            if (tb.OneByOne)
            {
                game.DrawText(tb.SubText, basepos, tb.TextFont, tb.TextFontSize, fColor);
            }
            else
            {
                game.DrawText(tb.Text, basepos, tb.TextFont, tb.TextFontSize, fColor);
            }
        }
        void drawButtonBox(ButtonBox bb, Point basepos)
        {
            basepos = Util.PointAddPoint(basepos, bb.Location);
            basepos = Util.PointAddPoint(basepos, ScreenOffsetPoint);
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
            Appear(i);
        }
        public void Appear(int i)
        {
            for (int j = 0; j < AppearingFormBoxIndex.Count; j++)
            {
                if (AppearingFormBoxIndex[j] == i)
                {
                    return;
                }
            }
            AppearingFormBoxIndex.Add(i);
            game.FormBoxes[i].Appear();
            game.FormBoxes[i].OnFormBoxAppearCode();
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
            //try
            //{
                AppearingFormBoxIndex.Remove(i);
                game.FormBoxes[i].Disappear();
                game.FormBoxes[i].OnFormBoxDisappearCode();
            //}
            //catch { }
        }

        public override void OnKeyboardUpCode(int key)
        {
            //base.OnKeyboardUpCode(key);
            //if (key == (int)Key.J)
            //{
            //    Appear(0);
            //}
            //else if (key == (int)Key.K)
            //{
            //    Disappear(0);
            //}
            //else if (key == (int)Key.Space)
            //{
            //    game.Options.TextureDrawMode = TextureDrawMode.Direct2D;
            //}
            //else if (key == (int)Key.L)
            //{
            //    FileLoader.Load(@"e:\[幻彩壁纸第一辑].Fantasy.Wallpapers.1920×1200.400P.rar");
            //}
        }
    }
}
