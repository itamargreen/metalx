using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX.DirectInput;
using MetalX.Define;
using MetalX.File;

namespace MetalX.Component
{
    public class FormBoxManager : GameCom
    {
        List<int> AppearingFormBoxIndex = new List<int>();
        FormBox TopFormBox
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
        {
            FallOutAlpha = true;
        }
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
                game.DrawMetalXTexture(game.Textures[tb.TextureIndex], new System.Drawing.Rectangle(new System.Drawing.Point(), game.Textures[tb.TextureIndex].Size), basepos, tb.Size,0, fColor);
            }
            else
            {
                int j = game.Textures.GetIndex(tb.TextureName);
                if (j < 0)
                {
                    return;
                }
                tb.TextureIndex = j;
                game.DrawMetalXTexture(game.Textures[j], new System.Drawing.Rectangle(new System.Drawing.Point(), game.Textures[tb.TextureIndex].Size), basepos, tb.Size,0, fColor);
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
                    if (game.FormBoxes[i].Name == "MessageBox")
                    {
                        game.FormBoxes[i].Appear(arg);
                        AppearingFormBoxIndex.Add(i);
                        Disappear(AppearingFormBoxIndex[j]);
                    }
                    return;
                }
            }
            
            game.FormBoxes[i].Appear(arg);
            AppearingFormBoxIndex.Add(i);
            game.SceneManager.Controllable = false;
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
            if (i < 0)
            {
                return;
            }
            AppearingFormBoxIndex.Remove(i);
            game.FormBoxes[i].Disappear();
            if (AppearingFormBoxIndex.Count == 0)
            {
                game.SceneManager.Controllable = true;
            }
            //game.FormBoxes[i].OnFormBoxDisappearCode();
        }
        public void DisappearAll()
        {
            while (AppearingFormBoxIndex.Count > 0)
            {
                Disappear(AppearingFormBoxIndex[0]);
            }
        }
        //void KeyEventSendToAllForm(int key, KeyState ks)
        //{
        //    foreach (int i in AppearingFormBoxIndex)
        //    {
        //        game.FormBoxes[i].SetKeyboardEvent(key, ks);
        //    }
        //}
        void KeyEventSendToTopForm(int key, KeyState ks)
        {
            if (TopFormBox != null)
            {
                TopFormBox.SetKeyboardEvent(key, ks);
            }
        }
        public override void OnKeyDownCode(object sender, int key)
        {
            KeyEventSendToTopForm(key, KeyState.Down);
            if (AppearingFormBoxIndex.Count < 1)
            {
                return;
            }

            Key k = (Key)key;
            if (k == game.Options.KeyYES)
            {
                TopFormBox.DownNowButtonBox();
            }
        }
        public override void OnKeyUpCode(object sender, int key)
        {
            KeyEventSendToTopForm(key, KeyState.Up);
            if (AppearingFormBoxIndex.Count < 1)
            {
                return;
            }

            Key k = (Key)key;
            if (k == game.Options.KeyYES)
            {
                TopFormBox.UpNowButtonBox();
                TopFormBox.FocusNowButtonBox();
            }
            else if (k == game.Options.KeyNO)
            {
                //Disappear();
                //if (AppearingFormBoxIndex.Count == 0)
                //{
                //    game.SceneManager.Controllable = true;
                //}
            }
            else if (k == game.Options.KeyUP)
            {
                TopFormBox.FocusLastButtonBox();
            }
            else if (k == game.Options.KeyDOWN)
            {
                TopFormBox.FocusNextButtonBox();
            }
            else if (k == game.Options.KeyLEFT)
            {
                for (int i = 0; i < TopFormBox.BigStep; i++)
                {
                    TopFormBox.FocusLastButtonBox();
                }
            }
            else if (k == game.Options.KeyRIGHT)
            {
                for (int i = 0; i < TopFormBox.BigStep; i++)
                {
                    TopFormBox.FocusNextButtonBox();
                }
            }
        }
    }
}
