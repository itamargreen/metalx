﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;


namespace MetalX.Resource
{
    public class Textures
    {
        List<MetalXTexture> items = new List<MetalXTexture>();
        public Textures()
        { 
        }
        public MetalXTexture this[int i]
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
        //public MetalXTexture this[string name]
        //{
        //    get
        //    {
        //        for (int i = 0; i < items.Count; i++)
        //        {
        //            if (items[i].Name == name)
        //            {
        //                return items[i];
        //            }
        //        }
        //        return null;
        //    }
        //}
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
        public void Add(MetalXTexture texture)
        {
            foreach (MetalXTexture mxt in items)
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
        public void Del(MetalXTexture texture)
        {
            items.Remove(texture);
        }
        public void Del(int i)
        {
            items.RemoveAt(i);
        }
    }
}
