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

        public override void Draw()
        {
            base.Draw();
            foreach (int i in AppearingFormBoxIndex)
            {
                FormBox fb = game.FormBoxes[i];
                {
                    Point pos = Util.PointAddPoint(fb.Location, ScreenOffsetPoint);
                    Color fColor = Util.MixColor(ColorFilter, fb.BGTextureFliterColor);
                    if (fb.BGTextureIndex > -1)
                    {
                        game.DrawMetalXTexture(game.Textures[fb.BGTextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), fb.Size), pos, fb.Size, fColor);
                    }
                    else
                    {
                        int j = game.Textures.GetIndex(fb.BGTextureName);
                        game.DrawMetalXTexture(game.Textures[j], new System.Drawing.Rectangle(new System.Drawing.Point(), fb.Size), pos, fb.Size, fColor);
                    }
                }
                foreach (ControlBox cb in fb.ControlBoxes)
                {
                    Point pos = Util.PointAddPoint(fb.Location, cb.Location);
                    pos = Util.PointAddPoint(pos, ScreenOffsetPoint);
                    Color fColor = ColorFilter;
                    if (cb is TextBox)
                    {
                        fColor = Util.MixColor(fColor, ((TextBox)cb).TextColor);
                        if (((TextBox)cb).OneByOne)
                        {
                            game.DrawText(((TextBox)cb).SubText, pos, fColor);
                        }
                        else
                        {
                            game.DrawText(((TextBox)cb).Text, pos, fColor);
                        }
                    }
                    else if (cb is TextureBox)
                    {
                        fColor = Util.MixColor(fColor, ((TextureBox)cb).TextureFliterColor);
                        if (((TextureBox)cb).TextureIndex > -1)
                        {
                            game.DrawMetalXTexture(game.Textures[((TextureBox)cb).TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), ((TextureBox)cb).Size), pos, ((TextureBox)cb).Size, fColor);
                        }
                        else
                        {
                            int j = game.Textures.GetIndex(((TextureBox)cb).TextureName);
                            game.DrawMetalXTexture(game.Textures[j], new System.Drawing.Rectangle(new System.Drawing.Point(), ((TextureBox)cb).Size), pos, ((TextureBox)cb).Size, fColor);
                        }
                    }
                    else if (cb is ButtonBox)
                    {
                        if (((ButtonBox)cb).ButtonBoxState == ButtonBoxState.Down)
                        {
                            pos = Util.PointAddPoint(pos, ((ButtonBox)cb).DownTextureBox.Location);
                            fColor = Util.MixColor(fColor, ((ButtonBox)cb).DownTextureBox.TextureFliterColor);
                            game.DrawMetalXTexture(game.Textures[((ButtonBox)cb).DownTextureBox.TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), ((ButtonBox)cb).Size), pos, fColor);

                            pos = Util.PointAddPoint(pos, ((ButtonBox)cb).DownTextBox.Location);
                            fColor = Util.MixColor(fColor, ((ButtonBox)cb).DownTextBox.TextColor);
                            game.DrawText(((ButtonBox)cb).DownTextBox.Text, ((ButtonBox)cb).Location, fColor);
                        }
                        else if (((ButtonBox)cb).ButtonBoxState == ButtonBoxState.Focus)
                        {
                            pos = Util.PointAddPoint(pos, ((ButtonBox)cb).FocusTextureBox.Location);
                            fColor = Util.MixColor(fColor, ((ButtonBox)cb).FocusTextureBox.TextureFliterColor);
                            game.DrawMetalXTexture(game.Textures[((ButtonBox)cb).FocusTextureBox.TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), ((ButtonBox)cb).Size), pos, ((ButtonBox)cb).FocusTextureBox.TextureFliterColor);

                            pos = Util.PointAddPoint(pos, ((ButtonBox)cb).FocusTextBox.Location);
                            fColor = Util.MixColor(fColor, ((ButtonBox)cb).FocusTextBox.TextColor);
                            game.DrawText(((ButtonBox)cb).FocusTextBox.Text, ((ButtonBox)cb).Location, ((ButtonBox)cb).FocusTextBox.TextColor);
                        }
                        else if (((ButtonBox)cb).ButtonBoxState == ButtonBoxState.Up)
                        {
                            pos = Util.PointAddPoint(pos, ((ButtonBox)cb).UpTextureBox.Location);
                            fColor = Util.MixColor(fColor, ((ButtonBox)cb).UpTextureBox.TextureFliterColor);
                            game.DrawMetalXTexture(game.Textures[((ButtonBox)cb).UpTextureBox.TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), ((ButtonBox)cb).Size), pos, ((ButtonBox)cb).UpTextureBox.TextureFliterColor);

                            pos = Util.PointAddPoint(pos, ((ButtonBox)cb).UpTextBox.Location);
                            fColor = Util.MixColor(fColor, ((ButtonBox)cb).UpTextBox.TextColor);
                            game.DrawText(((ButtonBox)cb).UpTextBox.Text, ((ButtonBox)cb).Location, ((ButtonBox)cb).UpTextBox.TextColor);
                        }
                        else if (((ButtonBox)cb).ButtonBoxState == ButtonBoxState.Wait)
                        {
                            pos = Util.PointAddPoint(pos, ((ButtonBox)cb).WaitTextureBox.Location);
                            fColor = Util.MixColor(fColor, ((ButtonBox)cb).WaitTextureBox.TextureFliterColor);
                            game.DrawMetalXTexture(game.Textures[((ButtonBox)cb).WaitTextureBox.TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), ((ButtonBox)cb).Size), pos, ((ButtonBox)cb).WaitTextureBox.TextureFliterColor);

                            pos = Util.PointAddPoint(pos, ((ButtonBox)cb).WaitTextBox.Location);
                            fColor = Util.MixColor(fColor, ((ButtonBox)cb).WaitTextBox.TextColor);
                            game.DrawText(((ButtonBox)cb).WaitTextBox.Text, ((ButtonBox)cb).Location, ((ButtonBox)cb).WaitTextBox.TextColor);
                        }
                    }
                }
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
            Disappear(AppearingFormBoxIndex[0]);
        }
        public void Disappear(string name)
        {
            int i = game.FormBoxes.GetIndex(name);
            Disappear(i);
        }
        public void Disappear(int i)
        {
            try
            {
                game.FormBoxes[i].Disappear();
                game.FormBoxes[i].OnFormBoxDisappearCode();
                AppearingFormBoxIndex.RemoveAt(i);
            }
            catch { }
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
