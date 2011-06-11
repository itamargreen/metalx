using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX;

namespace MetalX.Data
{
    [Serializable]
    public class CHR
    {
        public string Name;
        
        public int TextureIndex;
        public string TextureFileName;
        public Size TileSizePixel;
       
        public float MoveSpeed;
        public Direction Direction;

        public Vector3 PreLocation;
        public Vector3 Location;
        public Vector3 NextLocation;

        public string AtSceneName;
        public int AtSceneIndex;

        public bool CanMove;
        public bool CanTurn;

        public int Gold;

        public int Level;

        public int MLevel;
        public int ELevel;
        public int BLevel;

        public int HP;
        public int HPMax;
        public int MP;
        public int MPMax;

        public List<Item> Bag = new List<Item>();

        public void BagIn(Item item)
        {
            Bag.Add(item);
        }
        public void BagOut(int i)
        {
            Bag.RemoveAt(i);
        }
    }
    [Serializable]
    public class PC : CHR
    { }
    [Serializable]
    public class NPC : CHR
    { }
}
