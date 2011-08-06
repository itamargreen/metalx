using System;
using System.Collections.Generic;
using System.IO;

using MetalX.Define;
using MetalX.File;

namespace MetalX.Resource
{
    public class FileLib
    {
        List<FileIndexer> items = new List<FileIndexer>();
        public FileIndexer this[int i]
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
        public FileIndexer this[string name]
        {
            get
            {
                return this[GetIndex(name)];
            }
        }
        public int GetIndex(string tname)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].FileName == tname)
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
        public void Add(FileIndexer fl)
        {
            foreach (FileIndexer fileLink in items)
            {
                if (fileLink.FileName == fl.FileName)
                {
                    return;
                }
            }
            items.Add(fl);
        }
        public void Del(string name)
        {
            int i = GetIndex(name);
            Del(i);
        }
        public void Del(int i)
        {
            items.RemoveAt(i);
        }       
    }
}
