using System;
using System.Collections.ObjectModel;
using System.IO;


namespace MetalX.Resource
{
    public class Models
    {
        Collection<MetalXModel> items = new Collection<MetalXModel>();
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
            //items = new Collection<Model>();
        }
    }

}
