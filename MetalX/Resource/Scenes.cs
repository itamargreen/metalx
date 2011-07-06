using System;
using System.Collections.Generic;
using System.Drawing;
using MetalX.Data;

namespace MetalX.Resource
{
    public class Scenes
    {
        List<FileLink> items = new List<FileLink>();
        public FileLink this[int i]
        {
            get
            {
                return items[i];
            }
        }
        public FileLink this[string name]
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
        public void Add(FileLink fl)
        {
            foreach (FileLink fileLink in items)
            {
                if (fileLink.Name == fl.Name)
                {
                    return;
                }
            }
            items.Add(fl);
        }
        public void Del(string name)
        {
            int i = GetIndex(name);
            Del(i);
        }
        public void Del(int i)
        {
            items.RemoveAt(i);
        }
    }
}
