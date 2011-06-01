using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX.Format
{
    [Serializable]
    public class MetalXTexture : IDisposable
    {
        //public int Index;
        public string Name;
        public string Path;
        public DateTime CreateTime;
        public string Version;

        public byte[] TextureData;
        public Size SizePixel, TileSizePixel;

        [NonSerialized]
        public Microsoft.DirectX.Direct3D.Texture MEMTexture;

        public MetalXTexture()
        {
            //Index = -1;
            //Version = Path = Name = null;
            CreateTime = DateTime.Now;
        }

        public void Dispose()
        {
        }
    }
}
