using System;
using System.Collections.Generic;
using System.Text;

namespace MetalX.Data
{
    [Serializable]
    public class FileIndexer
    {
        public FileIndexer()
        { 
        }
        public FileIndexer(string fileName)
        {
            FileName = fileName;
        }
        public string Name
        {
            get
            {
                return System.IO.Path.GetFileNameWithoutExtension(FileName);
            }
        }
        public string ExtName
        {
            get
            {
                return System.IO.Path.GetExtension(FileName);
            }
        }
        public string FileName;
    }
}
