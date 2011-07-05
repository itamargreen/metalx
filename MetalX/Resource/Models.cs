using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace MetalX.Resource
{
    public class Models
    {
        List<MetalXModel> items = new List<MetalXModel>();
        public MetalXModel this[int i]
        {
            get
            {
                return items[i];
            }
        }
        public MetalXModel this[string name]
        {
            get
            {
                for (int i = 0; i < Count; i++)
                {
                    if (items[i].Name == name)
                    {
                        return items[i];
                    }
                }
                return null;
            }
        }
        public void Add(MetalXModel model)
        {
            items.Add(model);
        }
        public void Del(MetalXModel model)
        {
            items.Remove(model);
        }
        public void Del(int i)
        {
            items.RemoveAt(i);
        }

        public int Count
        {
            get
            {
                return  items.Count;
            }
        }
        public Models()
        {
            //if (_Items == null)
            //{
            //}
            //items = new List<Model>();
        }
        /// <summary>
        /// 加载.X文件
        /// </summary>
        /// <param name="fileName">文件路径+文件名</param>
        /// <returns>MetalX模型</returns>
        public MetalXModel LoadDotX(Game g,string fileName)
        {
            GraphicsStream adj;
            ExtendedMaterial[] extendedMaterials;
            EffectInstance[] effectInstances;

            Mesh mesh = Mesh.FromFile(fileName, MeshFlags.Managed, g.Devices.D3DDev,
                out adj, out extendedMaterials, out effectInstances);

            MetalXModel model = new MetalXModel();
            MemoryStream tms = new MemoryStream();
            mesh.Save(tms, adj, extendedMaterials, effectInstances, XFileFormat.Compressed);
            //model.MeshData.Position = 0;
            model.MeshData = tms.ToArray();

            model.MEMMesh = mesh;
            model.MEMTextures = new Microsoft.DirectX.Direct3D.Texture[extendedMaterials.Length];
            model.MEMMaterials = new Material[extendedMaterials.Length];
            model.TexturesData = new byte[extendedMaterials.Length][];

            for (int i = 0; i < extendedMaterials.Length; i++)
            {
                if (extendedMaterials[i].TextureFilename != null && extendedMaterials[i].TextureFilename != string.Empty)
                {
                    model.MEMTextures[i] = TextureLoader.FromFile(g.Devices.D3DDev, extendedMaterials[i].TextureFilename);
                    FileStream fs = File.OpenRead(extendedMaterials[i].TextureFilename);
                    byte[] buf = new byte[(int)fs.Length];
                    fs.Read(buf, 0, buf.Length);
                    model.TexturesData[i] = buf;
                    model.MEMMaterials[i] = extendedMaterials[i].Material3D;
                }
            }
            model.MEMCount = extendedMaterials.Length;

            return model;
        }
        /// <summary>
        /// 加载.MXM文件
        /// </summary>
        /// <param name="fileName">文件路径+文件名</param>
        /// <returns>MetalX模型</returns>
        public MetalXModel LoadDotMXM(Game g,string fileName)
        {
            MetalXModel model = (MetalXModel)Util.LoadObject(fileName);

            ExtendedMaterial[] extendedMaterials;

            Mesh mesh = Mesh.FromStream(new MemoryStream(model.MeshData), MeshFlags.Managed, g.Devices.D3DDev,
                   out extendedMaterials);

            model.MEMMesh = mesh;
            model.MEMTextures = new Microsoft.DirectX.Direct3D.Texture[extendedMaterials.Length];
            model.MEMMaterials = new Material[extendedMaterials.Length];

            for (int i = 0; i < extendedMaterials.Length; i++)
            {
                if (extendedMaterials[i].TextureFilename != null && extendedMaterials[i].TextureFilename != string.Empty)
                {
                    model.MEMTextures[i] = TextureLoader.FromStream(g.Devices.D3DDev, new MemoryStream(model.TexturesData[i]));
                    model.MEMMaterials[i] = extendedMaterials[i].Material3D;
                }
            }
            model.MEMCount = extendedMaterials.Length;

            Add(model);

            return model;
        }
    }

}
