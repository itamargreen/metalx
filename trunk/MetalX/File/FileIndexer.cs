using System;
using System.Collections.Generic;
using System.Text;

namespace MetalX.File
{
    [Serializable]
    public class FileIndexer
    {
        public FileIndexer(string fullname)
        {
            FullName = fullname;
        }
        public string FileName
        {
            get
            {
                return System.IO.Path.GetFileNameWithoutExtension(FullName);
            }
        }
        public string ExtName
        {
            get
            {
                return System.IO.Path.GetExtension(FullName);
            }
        }
        public string FolderName
        {
            get
            {
                string[] str = System.IO.Path.GetDirectoryName(FullName).Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                return str[str.Length - 1];
            }
        }
        public string FullName;
    }
}
