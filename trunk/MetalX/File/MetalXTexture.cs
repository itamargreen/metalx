using System;
using System.Collections.Generic;
using System.Drawing;
using MetalX.Define;
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
        public System.IO.MemoryStream TextureDataStream
        {
            get
            {
                return new System.IO.MemoryStream(TextureData);
            }
        }
        public Size Size;
        public Size Size2X
        {
            get
            {
                return new Size(Size.Width * 2, Size.Height * 2);
            }
        }
        public Size TileSize;
        public Size TileSize2X
        {
            get
            {
                return new Size(TileSize.Width * 2, TileSize.Height * 2);
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
        public MetalXTexture(string fullName)
            : base()
        {
            //SizePixel = sz;
            FileIndexer = new FileIndexer(fullName);
            Name = FileIndexer.FileName;
            TextureData = System.IO.File.ReadAllBytes(FileIndexer.FullName);
        }
        public void Init(Microsoft.DirectX.Direct3D.Device dev)
        {
            MEMTexture = Microsoft.DirectX.Direct3D.TextureLoader.FromStream(dev, new System.IO.MemoryStream(TextureData), Size.Width, Size.Height, 0, Microsoft.DirectX.Direct3D.Usage.None, Microsoft.DirectX.Direct3D.Format.A8R8G8B8, Microsoft.DirectX.Direct3D.Pool.Managed, Microsoft.DirectX.Direct3D.Filter.Point, Microsoft.DirectX.Direct3D.Filter.Point, Color.Pink.ToArgb());
            Image img = Image.FromStream(new System.IO.MemoryStream(TextureData));

            Bitmap bmp = new Bitmap(img);
            Bitmap bmp2x = new Bitmap(bmp.Size.Width * 2, bmp.Size.Height * 2);
            MEMTexture2X = Microsoft.DirectX.Direct3D.TextureLoader.FromStream(dev, new System.IO.MemoryStream(TextureData), bmp2x.Width, bmp2x.Height, 0, Microsoft.DirectX.Direct3D.Usage.None, Microsoft.DirectX.Direct3D.Format.A8R8G8B8, Microsoft.DirectX.Direct3D.Pool.Managed, Microsoft.DirectX.Direct3D.Filter.Point, Microsoft.DirectX.Direct3D.Filter.Point, Color.Pink.ToArgb());

            img.Dispose();
            bmp.Dispose();
            bmp2x.Dispose();
        }

        public void Dispose()
        {
            MEMTexture.Dispose();
            MEMTexture2X.Dispose();
            TextureData = null;
        }
    }
}
