using System;
using System.Collections.Generic;
using System.Text;

namespace MetalX.Define
{
    [Serializable]
    public class MemoryIndexer
    {
        string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                index = -1;
                name = value;
            }
        }
        int index;
        public int Index
        {
            get
            {
                return index;
            }
        }
        public MemoryIndexer(string name)
        {
            this.name = name;
        }
        public MemoryIndexer()
        {
        }
    }
}
