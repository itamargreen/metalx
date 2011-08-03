using System;
using System.Collections.Generic;
using System.Text;
using MetalX.Define;
namespace MetalX.Resource
{
    public class Items
    {
        List<Item> items = new List<Item>();
        public Items()
        { 
        }
        public Item this[int i]
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
        public Item this[string name]
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
        public void Add(Item texture)
        {
            foreach (Item mxt in items)
            {
                if (mxt.Name == texture.Name)
                {
                    return;
                }
            }
            //if (items.Contains(texture))
            //{
            //    return;
            //}
            items.Add(texture);
        }
        public void Add(Item texture, string name)
        {
            texture.Name = name;
            items.Add(texture);
        }
        public void Del(Item texture)
        {
            items.Remove(texture);
        }
        public void Del(int i)
        {
            items.RemoveAt(i);
        }

    }
}
