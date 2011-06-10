using System;
using System.Collections.ObjectModel;
using System.Text;

namespace MetalX.Resource
{
    public class Audios
    {
        Collection<MetalXAudio> items = new Collection<MetalXAudio>();
        public MetalXAudio this[int i]
        {
            get
            {
                return items[i];
            }
        }
        public MetalXAudio this[string name]
        {
            get
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Name == name)
                    {
                        return items[i];
                    }
                }
                return null;
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
        public void Add(MetalXAudio audio)
        {
            foreach (MetalXAudio mxt in items)
            {
                if (mxt.Name == audio.Name)
                {
                    return;
                }
            }
            //if (items.Contains(audio))
            //{
            //    return;
            //}
            items.Add(audio);
        }
        public void Del(MetalXAudio audio)
        {
            items.Remove(audio);
        }
        public void Del(int i)
        {
            items.RemoveAt(i);
        }
    }
}
