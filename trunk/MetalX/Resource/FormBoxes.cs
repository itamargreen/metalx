using System;
using System.Collections.Generic;
using System.Drawing;
using MetalX.Define;

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
            set
            {
                if (i < 0)
                {
                    return;
                }
                items[i] = value;
            }
        }
        public FormBox this[string name]
        {
            get
            {
                return this[GetIndex(name)];
            }
            set
            {
                this[GetIndex(name)] = value;
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
    }
    public class ASKboolBox : FormBox
    {
        protected ButtonBox BB1, BB2;
        protected TextBox TextBox;
        public ASKboolBox(Game g)
            : base(g)
        {
            Name = "ASKboolBox";
            Location = new Point(0, 360);

            TextBox = new TextBox(g);
            TextBox.Location = new Point(16, 16);
            TextBox.OneByOne = true;

            BB1 = new ButtonBox(g);
            BB1.Location = new Point(300, 80);
            BB1.UpTextBox.Text = BB1.DownTextBox.Text = BB1.FocusTextBox.Text = BB1.WaitTextBox.Text = "是";
            BB1.FocusTextBox.FontColor = Color.Blue;
            BB1.OnButtonDown += new ButtonBoxEvent(BB1_OnButtonDown);

            BB2 = new ButtonBox(g);
            BB2.Location = new Point(400, 80);
            BB2.UpTextBox.Text = BB2.DownTextBox.Text = BB2.FocusTextBox.Text = BB2.WaitTextBox.Text = "否";
            BB2.FocusTextBox.FontColor = Color.Blue;
            BB2.OnButtonDown += new ButtonBoxEvent(BB2_OnButtonDown);

            ControlBoxes.Add(TextBox); 
            ControlBoxes.Add(BB1);
            ControlBoxes.Add(BB2);
        }
        public string Text
        {
            get
            {
                return TextBox.Text;
            }
            set
            {
                TextBox.Text = value;
            }
        }
        void BB1_OnButtonDown(object sender, object arg)
        {
            //game.AppendScript("return true");
            //game.ExecuteScript();
            game.ReturnScript(true);
        }

        void BB2_OnButtonDown(object sender, object arg)
        {
            //game.AppendScript("return false");
            //game.ExecuteScript();
            game.ReturnScript(false);
        }
        public override void OnFormBoxAppearCode(object sender, object arg)
        {
            if (arg is string)
            {
                ((ASKboolBox)sender).Text = arg.ToString();
            }
        }
    }
    public class MSGBox : FormBox
    {
        public TextBox TextBox;
        public MSGBox(Game g)
            : base(g)
        {
            Name = "MessageBox";
            Location = new Point(64, 480 - 128 - 8);

            TextBox = new TextBox(g);
            TextBox.Location = new Point(20, 20);
            TextBox.OneByOne = true;

            ControlBoxes.Add(TextBox);
        }
        public string Text
        {
            get
            {
                return TextBox.Text;
            }
            set
            {
                TextBox.Text = value;
            }
        }
        public override void OnFormBoxAppearCode(object sender, object arg)
        {
            if (arg is string)
            {
                ((MSGBox)sender).Text = arg.ToString();
            }
        }
    }
}
