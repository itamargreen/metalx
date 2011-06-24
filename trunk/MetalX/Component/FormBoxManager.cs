using System;
using System.Collections.Generic;
using System.Text;

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
                if (fb.BGTextureIndex > -1)
                {
                    game.DrawMetalXTexture(game.Textures[fb.BGTextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), fb.Size), fb.Location, fb.Size, fb.BGTextureFliterColor);
                }
                else
                {
                    int j = game.Textures.GetIndex(fb.BGTextureName);
                    game.DrawMetalXTexture(game.Textures[j], new System.Drawing.Rectangle(new System.Drawing.Point(), fb.Size), fb.Location, fb.Size, fb.BGTextureFliterColor);
                } //if (fb.Visible)
                {
                    foreach (ControlBox cb in fb.ControlBoxes)
                    {
                        if (cb is TextBox)
                        {
                            if (((TextBox)cb).OneByOne)
                            {
                                game.DrawText(((TextBox)cb).SubText, ((TextBox)cb).Location, ((TextBox)cb).TextColor);
                            }
                            else
                            {
                                game.DrawText(((TextBox)cb).Text, ((TextBox)cb).Location, ((TextBox)cb).TextColor);
                            }
                        }
                        else if (cb is TextureBox)
                        {
                            if (((TextureBox)cb).TextureIndex > -1)
                            {
                                game.DrawMetalXTexture(game.Textures[((TextureBox)cb).TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), ((TextureBox)cb).Size), ((TextureBox)cb).Location, ((TextureBox)cb).Size, ((TextureBox)cb).TextureFliterColor);
                            }
                            else
                            {
                                int j = game.Textures.GetIndex(((TextureBox)cb).BGTextureName);
                                game.DrawMetalXTexture(game.Textures[j], new System.Drawing.Rectangle(new System.Drawing.Point(), ((TextureBox)cb).Size), ((TextureBox)cb).Location, ((TextureBox)cb).Size, ((TextureBox)cb).TextureFliterColor);
                            }
                        }
                        else if (cb is ButtonBox)
                        {
                            if (((ButtonBox)cb).ButtonBoxState == ButtonBoxState.Down)
                            {
                                game.DrawMetalXTexture(game.Textures[((ButtonBox)cb).DownTextureBox.TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), ((ButtonBox)cb).Size), ((ButtonBox)cb).Location, ((ButtonBox)cb).DownTextureBox.TextureFliterColor);
                                game.DrawText(((ButtonBox)cb).DownTextBox.Text, ((ButtonBox)cb).Location, ((ButtonBox)cb).DownTextBox.TextColor);
                            }
                            else if (((ButtonBox)cb).ButtonBoxState == ButtonBoxState.Focus)
                            {
                                game.DrawMetalXTexture(game.Textures[((ButtonBox)cb).FocusTextureBox.TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), ((ButtonBox)cb).Size), ((ButtonBox)cb).Location, ((ButtonBox)cb).FocusTextureBox.TextureFliterColor);
                                game.DrawText(((ButtonBox)cb).FocusTextBox.Text, ((ButtonBox)cb).Location, ((ButtonBox)cb).FocusTextBox.TextColor);
                            }
                            else if (((ButtonBox)cb).ButtonBoxState == ButtonBoxState.Up)
                            {
                                game.DrawMetalXTexture(game.Textures[((ButtonBox)cb).UpTextureBox.TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), ((ButtonBox)cb).Size), ((ButtonBox)cb).Location, ((ButtonBox)cb).UpTextureBox.TextureFliterColor);
                                game.DrawText(((ButtonBox)cb).UpTextBox.Text, ((ButtonBox)cb).Location, ((ButtonBox)cb).UpTextBox.TextColor);
                            }
                            else if (((ButtonBox)cb).ButtonBoxState == ButtonBoxState.Wait)
                            {
                                game.DrawMetalXTexture(game.Textures[((ButtonBox)cb).WaitTextureBox.TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), ((ButtonBox)cb).Size), ((ButtonBox)cb).Location, ((ButtonBox)cb).WaitTextureBox.TextureFliterColor);
                                game.DrawText(((ButtonBox)cb).WaitTextBox.Text, ((ButtonBox)cb).Location, ((ButtonBox)cb).WaitTextBox.TextColor);
                            }
                        }
                    }
                }
            }
        }

        public void Appear(int i)
        {
            for (int j = 0; j < AppearingFormBoxIndex.Count; j++)
            {
                if (AppearingFormBoxIndex[i] == j)
                {
                    return;
                }
            }
            AppearingFormBoxIndex.Add(i);
            game.FormBoxes[i].Appear();
        }
        public void Disappear(int i)
        {
            try
            {
                AppearingFormBoxIndex.RemoveAt(i);
                game.FormBoxes[i].Disappear();
            }
            catch { }
        }

        public override void OnKeyboardUpCode(int key)
        {
            //base.OnKeyboardUpCode(key);
            if (key == (int)Key.J)
            {
                Appear(0);
            }
            else if (key == (int)Key.K)
            {
                Disappear(0);
            }
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
