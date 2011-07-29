using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
namespace MetalX.File
{
    [Serializable]
    public class MetalXModel : IDisposable
    {
        public string Name;
        public string Path;
        public DateTime CreateTime;
        public string Version;

        public byte[] MeshData;

        public byte[][] TexturesData;

        [NonSerialized]
        public Microsoft.DirectX.Direct3D.Texture[] MEMTextures;
        [NonSerialized]
        public Material[] MEMMaterials;
        [NonSerialized]
        public Mesh MEMMesh;
        [NonSerialized]
        public int MEMCount;

        public MetalXModel()
        {
            //Index = -1;
            CreateTime = DateTime.Now;
        }
        public void Dispose()
        {
            //MEMMesh.Dispose();
        }
    }

}
