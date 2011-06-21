using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

using Microsoft.DirectX.Direct3D;

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
        }        /// <summary>
        /// 加载.PNG文件
        /// </summary>
        /// <param name="fileName">文件路径+文件名</param>
        /// <returns>MetalX纹理</returns>
        public MetalXTexture LoadDotPNG(string fileName,Game g)
        {
            MetalXTexture texture = new MetalXTexture();
            texture.Name = Path.GetFileNameWithoutExtension(fileName);
            texture.TextureData = File.ReadAllBytes(fileName);
            Bitmap bmp = new Bitmap(new MemoryStream(texture.TextureData));            
            texture.SizePixel = bmp.Size;
            texture.TileSizePixel = new Size(24, 24);
            //texture.MEMTexture = TextureLoader.FromFile(g.Devices.D3DDev, fileName, texture.SizePixel.Width+1, texture.SizePixel.Height+1, 0, Usage.None, Microsoft.DirectX.Direct3D.Format.A8R8G8B8, Pool.Managed, Filter.Point, Filter.Point, Color.Pink.ToArgb());
            texture.MEMTexture = new Texture(g.Devices.D3DDev, bmp, Usage.None, Pool.Managed);
            return texture;
        }
        /// <summary>
        /// 加载.MXT文件
        /// </summary>
        /// <param name="fileName">文件路径+文件名</param>
        /// <returns>MetalX纹理</returns>
        public MetalXTexture LoadDotMXT(string fileName, Game g)
        {
            MetalXTexture texture = new MetalXTexture();
            texture = (MetalXTexture)Util.LoadObject(fileName);
            texture.MEMTexture = TextureLoader.FromStream(g.Devices.D3DDev, new MemoryStream(texture.TextureData), texture.SizePixel.Width, texture.SizePixel.Height, 0, Usage.None, Microsoft.DirectX.Direct3D.Format.X8R8G8B8, Pool.Managed, Filter.Point, Filter.Point, Color.Pink.ToArgb());
            return texture;
        }
    }
}
