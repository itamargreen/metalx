using System;
using System.Collections.Generic;
using System.IO;

namespace MetalX.Resource
{
    public class Audios
    {
        List<MetalXAudio> items = new List<MetalXAudio>();
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
        }        /// <summary>
        /// 加载.MP3文件
        /// </summary>
        /// <param name="fileName">文件路径+文件名</param>
        /// <returns>MetalX音频</returns>
        public MetalXAudio LoadDotMP3(string fileName)
        {
            MetalXAudio mxa = new MetalXAudio();
            mxa.Name = Path.GetFileNameWithoutExtension(fileName);
            mxa.AudioData = File.ReadAllBytes(fileName);
            Add(mxa);
            return mxa;
        }
        /// <summary>
        /// 加载.MXA文件
        /// </summary>
        /// <param name="fileName">文件路径+文件名</param>
        /// <returns>MetalX音频</returns>
        public MetalXAudio LoadDotMXA(string fileName)
        {
            return (MetalXAudio)Util.LoadObject(fileName);
        }
    }
}
