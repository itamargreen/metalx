using System;
using System.Collections.Generic;
using System.Text;

using MetalX.Data;

namespace MetalX.Component
{
    public class UIManager : GameCom
    {
        Stack<int> AppearingFormBoxIndex = new Stack<int>();

        public UIManager(Game g)
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
                if (fb.Visible)
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
                            game.DrawMetalXTexture(game.Textures[((TextureBox)cb).TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), ((TextureBox)cb).Size), ((TextureBox)cb).Location, ((TextureBox)cb).TextureColor);
                        }
                        else if (cb is ButtonBox)
                        {
                            if (((ButtonBox)cb).ButtonBoxState == ButtonBoxState.Down)
                            {
                                game.DrawMetalXTexture(game.Textures[((ButtonBox)cb).DownTextureBox.TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), ((ButtonBox)cb).Size), ((ButtonBox)cb).Location, ((ButtonBox)cb).DownTextureBox.TextureColor);
                                game.DrawText(((ButtonBox)cb).DownTextBox.Text, ((ButtonBox)cb).Location, ((ButtonBox)cb).DownTextBox.TextColor);
                            }
                            else if (((ButtonBox)cb).ButtonBoxState == ButtonBoxState.Focus)
                            {
                                game.DrawMetalXTexture(game.Textures[((ButtonBox)cb).FocusTextureBox.TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), ((ButtonBox)cb).Size), ((ButtonBox)cb).Location, ((ButtonBox)cb).FocusTextureBox.TextureColor);
                                game.DrawText(((ButtonBox)cb).FocusTextBox.Text, ((ButtonBox)cb).Location, ((ButtonBox)cb).FocusTextBox.TextColor);
                            }
                            else if (((ButtonBox)cb).ButtonBoxState == ButtonBoxState.Up)
                            {
                                game.DrawMetalXTexture(game.Textures[((ButtonBox)cb).UpTextureBox.TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), ((ButtonBox)cb).Size), ((ButtonBox)cb).Location, ((ButtonBox)cb).UpTextureBox.TextureColor);
                                game.DrawText(((ButtonBox)cb).UpTextBox.Text, ((ButtonBox)cb).Location, ((ButtonBox)cb).UpTextBox.TextColor);
                            }
                            else if (((ButtonBox)cb).ButtonBoxState == ButtonBoxState.Wait)
                            {
                                game.DrawMetalXTexture(game.Textures[((ButtonBox)cb).WaitTextureBox.TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), ((ButtonBox)cb).Size), ((ButtonBox)cb).Location, ((ButtonBox)cb).WaitTextureBox.TextureColor);
                                game.DrawText(((ButtonBox)cb).WaitTextBox.Text, ((ButtonBox)cb).Location, ((ButtonBox)cb).WaitTextBox.TextColor);
                            }
                        }
                    }
                }
            }
        }
    }
}
