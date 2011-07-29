using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX.File
{
    [Serializable]
    public class MetalXTexture : IDisposable
    {
        public string Name;
        public FileIndexer FileIndexer;
        public DateTime CreateTime;
        public string Version;

        public byte[] TextureData;
        public Size SizePixel;
        public Size SizePixel2X
        {
            get
            {
                return new Size(SizePixel.Width * 2, SizePixel.Height * 2);
            }
        }
        public Size TileSizePixel;
        public Size TileSizePixel2X
        {
            get
            {
                return new Size(TileSizePixel.Width * 2, TileSizePixel.Height * 2);
            }
        }

        [NonSerialized]
        public Microsoft.DirectX.Direct3D.Texture MEMTexture;
        [NonSerialized]
        public Microsoft.DirectX.Direct3D.Texture MEMTexture2X;

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
