using System;
using System.Collections.Generic;
using System.Drawing;
using MetalX.Data;

namespace MetalX.Resource
{
    public class FormBoxes
    {
        List<FormBox> items = new List<FormBox>();
        public FormBoxes()
        { 
        }
        public FormBox this[int i]
        {
            get
            {
                if (i < 0)
                {
                    return null;
                }
                return items[i];
            }
        }
        public FormBox this[string name]
        {
            get
            {
                return this[GetIndex(name)];
            }
        }
        public int GetIndex(string tname)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name == tname)
                {
                    return i;
                }
            }
            return -1;
        }
        public int Count
        {
            get
            {
                return items.Count;
            }
        }
        public void Add(FormBox texture)
        {
            foreach (FormBox mxt in items)
            {
                if (mxt.Name == texture.Name)
                {
                    return;
                }
            }
            items.Add(texture);
        }
        public void Del(int i)
        {
            items.RemoveAt(i);
        }
        public void LoadDotMXFormBox(FormBox fb)
        {
            Add(fb);
        }
    }
    class MessageBox : FormBox
    {
        TextBox tb1;
        public MessageBox(Game g)
            : base(g)
        {
            Name = "MessageBox";
            Location = new Point(0, 360);
            //Size = new Size(640, 120);
            //BGTextureName = "dialog-bgtexture";

            tb1 = new TextBox(g);
            tb1.Location = new Point(16, 16);
            tb1.OneByOne = true;

            ControlBoxes.Add(tb1);
        }
        public override void OnFormBoxAppearCode(object arg)
        {
            if (arg is TextBox)
            {
                tb1.Text = ((TextBox)arg).Text;
            }
        }
        //public void Close()
        //{
        //    //game.AppendAndExecuteScript("gui disappear " + Name);
        //}
        //public void Show(string text, string fontName, int fontSize, int interval, bool obo)
        //{
        //    tb1.Text = text;
        //    tb1.FontName = fontName;
        //    tb1.FontSize = fontSize;
        //    tb1.Interval = interval;
        //    tb1.OneByOne = obo;
        //    //game.AppendAndExecuteScript("gui appear " + Name);
        //}
        //public void Show(string text, bool obo)
        //{
        //    Show(text, tb1.FontName, tb1.FontSize, tb1.Interval, obo);
        //}
        //public void Show(string text)
        //{
        //    Show(text, tb1.FontName, tb1.FontSize, tb1.Interval, tb1.OneByOne);
        //}
    }
}
