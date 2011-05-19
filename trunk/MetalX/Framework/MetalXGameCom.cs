using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX
{
    public class MetalXGameCom
    {
        protected MetalXGame metalXGame;

        bool enable = false;
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }
        bool visible = false;
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }
        bool controllable = false;
        public bool Controllable
        {
            get
            {
                return controllable;
            }
            set
            {
                controllable = value;
            }
        }

        public void EnableAll()
        {
            controllable = enable = visible = true;
        }
        public void DisableAll()
        {
            controllable = enable = visible = false;
        }

        Color colorFilter;
        public Color ColorFilter
        {
            get
            {
                return colorFilter;
            }
            set
            {
                colorFilter = value;
            }
        }

        public MetalXGameCom(MetalXGame metalxgame)
        {
            metalXGame = metalxgame;
            EnableAll();
        }

        public virtual void Code()
        {

        }
        public virtual void Draw()
        {

        }
    }

}
