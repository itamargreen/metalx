using System;
using System.Collections.Generic;
using MetalX.Data;

namespace MetalX.Resource
{
    public class Scenes
    {
        List<Scene> items = new List<Scene>();
        public Scenes()
        { 
        }
        public Scene this[int i]
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
        public void Add(Scene s)
        {
            foreach (Scene mxt in items)
            {
                if (mxt.Name == s.Name)
                {
                    return;
                }
            }
            items.Add(s);
        }
        public void Del(int i)
        {
            items.RemoveAt(i);
        }
        public Scene LoadDotMXScene(Game g,string pathName)
        {
            Scene scene  = (Scene)Util.LoadObject(pathName);

            for (int i = 0; i < scene.TileLayers.Count; i++)
            {
                for (int j = 0; j < scene.TileLayers[i].Tiles.Count; j++)
                {
                    for (int k = 0; k < scene.TileLayers[i].Tiles[j].Frames.Count; k++)
                    {
                        string tfname = scene.TileLayers[i].Tiles[j].Frames[k].TextureFileName;
                        scene.TileLayers[i].Tiles[j].Frames[k].TextureIndex = g.Textures.GetIndex(tfname);
                    }
                }
            }
            //Add(scene);
            return scene;
        }
    }
}
