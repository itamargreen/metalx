using System;
using System.Collections.Generic;
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
}
