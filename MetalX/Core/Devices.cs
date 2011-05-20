using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace MetalX
{
    public class Devices
    {
        MetalXGame metalXGame;
        public Form Window;

        //public Microsoft.DirectX.Direct3D.Font Font;
        //public Microsoft.DirectX.Direct3D.Sprite Sprite;
        public Microsoft.DirectX.Direct3D.Device D3DDev;
        public Microsoft.DirectX.DirectSound.Device DSoundDev;
        public Microsoft.DirectX.DirectInput.Device DKeyboardDev;
        public Microsoft.DirectX.DirectInput.Device DMouseDev;
        public Devices(MetalXGame metalxgame)
        {
            Window = new Form();
            Window.Text = metalxgame.Name;
            Window.StartPosition = FormStartPosition.CenterScreen;
            Window.Size = metalxgame.Settings.WindowSize;

            metalXGame = metalxgame;

            Microsoft.DirectX.Direct3D.PresentParameters pps = new Microsoft.DirectX.Direct3D.PresentParameters();
            pps.SwapEffect = Microsoft.DirectX.Direct3D.SwapEffect.Discard;
            pps.Windowed = true;

            D3DDev = new Microsoft.DirectX.Direct3D.Device(0, Microsoft.DirectX.Direct3D.DeviceType.Hardware, Window, Microsoft.DirectX.Direct3D.CreateFlags.SoftwareVertexProcessing, pps);

            DSoundDev = new Microsoft.DirectX.DirectSound.Device();
            DSoundDev.SetCooperativeLevel(Window, Microsoft.DirectX.DirectSound.CooperativeLevel.Normal);

            DKeyboardDev = new Microsoft.DirectX.DirectInput.Device(Microsoft.DirectX.DirectInput.SystemGuid.Keyboard);
            DKeyboardDev.Acquire();

            DMouseDev = new Microsoft.DirectX.DirectInput.Device(Microsoft.DirectX.DirectInput.SystemGuid.Mouse);
            DMouseDev.Acquire();

            //Sprite = new Microsoft.DirectX.Direct3D.Sprite(D3DDev);
            //Font = new Microsoft.DirectX.Direct3D.Font(D3DDev, new Font("微软雅黑", 12));

        }
        public Devices(Control control, MetalXGame metalxgame)
        {
            metalXGame = metalxgame;

            Microsoft.DirectX.Direct3D.PresentParameters pps = new Microsoft.DirectX.Direct3D.PresentParameters();
            pps.SwapEffect = Microsoft.DirectX.Direct3D.SwapEffect.Discard;
            pps.Windowed = true;
            //pps.MultiSample=Microsoft.DirectX.Direct3D.MultiSampleType.

            D3DDev = new Microsoft.DirectX.Direct3D.Device(0, Microsoft.DirectX.Direct3D.DeviceType.Hardware, control, Microsoft.DirectX.Direct3D.CreateFlags.SoftwareVertexProcessing, pps);

            DSoundDev = new Microsoft.DirectX.DirectSound.Device();
            DSoundDev.SetCooperativeLevel(control, Microsoft.DirectX.DirectSound.CooperativeLevel.Normal);

            DKeyboardDev = new Microsoft.DirectX.DirectInput.Device(Microsoft.DirectX.DirectInput.SystemGuid.Keyboard);
            DKeyboardDev.Acquire();

            DMouseDev = new Microsoft.DirectX.DirectInput.Device(Microsoft.DirectX.DirectInput.SystemGuid.Mouse);
            DMouseDev.Acquire();

            //Sprite = new Microsoft.DirectX.Direct3D.Sprite(D3DDev);
            //Font = new Microsoft.DirectX.Direct3D.Font(D3DDev, new Font("微软雅黑", 12));
        }

        #region metal2d
        //Microsoft.DirectX.Direct3D.Sprite _Sprite;
        //Microsoft.DirectX.Direct3D.Device _D3DDev;
        //Microsoft.DirectX.DirectSound.Device _DSoundDev;
        //Microsoft.DirectX.DirectInput.Device _DKeyboardDev;
        //Microsoft.DirectX.DirectInput.Device _DMouseDev;

        //public Form Window
        //{
        //    get
        //    {
        //        return _Window;
        //    }
        //}
        //public Microsoft.DirectX.Direct3D.Sprite Sprite
        //{
        //    get
        //    {
        //        return _Sprite;
        //    }
        //}
        //public Microsoft.DirectX.Direct3D.Device D3DDev
        //{
        //    get
        //    {
        //        return _D3DDev;
        //    }
        //    set
        //    {
        //        _D3DDev = value;
        //    }
        //}
        //public Microsoft.DirectX.DirectSound.Device DSoundDev
        //{
        //    get
        //    {
        //        return _DSoundDev;
        //    }
        //}
        //public Microsoft.DirectX.DirectInput.Device DKeyboardDev
        //{
        //    get { return _DKeyboardDev; }
        //}
        //public Microsoft.DirectX.DirectInput.Device DMouseDev
        //{
        //    get
        //    {
        //        return _DMouseDev;
        //    }
        //}

        //public Devices(Game game)
        //{
        //    _Window = new Form();
        //    _Window.Text = game.Name;
        //    _Window.StartPosition = FormStartPosition.CenterScreen;
        //    _Window.Size = game.Settings.WindowSize;

        //    this.game = game;

        //    Microsoft.DirectX.Direct3D.PresentParameters pps = new Microsoft.DirectX.Direct3D.PresentParameters();
        //    pps.SwapEffect = Microsoft.DirectX.Direct3D.SwapEffect.Discard;
        //    pps.Windowed = true;

        //    _D3DDev = new Microsoft.DirectX.Direct3D.Device(0, Microsoft.DirectX.Direct3D.DeviceType.Hardware, _Window, Microsoft.DirectX.Direct3D.CreateFlags.SoftwareVertexProcessing, pps);

        //    _DSoundDev = new Microsoft.DirectX.DirectSound.Device();
        //    _DSoundDev.SetCooperativeLevel(_Window, Microsoft.DirectX.DirectSound.CooperativeLevel.Normal);

        //    _DKeyboardDev = new Microsoft.DirectX.DirectInput.Device(Microsoft.DirectX.DirectInput.SystemGuid.Keyboard);
        //    _DKeyboardDev.Acquire();

        //    _DMouseDev = new Microsoft.DirectX.DirectInput.Device(Microsoft.DirectX.DirectInput.SystemGuid.Mouse);
        //    _DMouseDev.Acquire();

        //    _Sprite = new Microsoft.DirectX.Direct3D.Sprite(_D3DDev);

        //}
        //public Devices(Control control, Game game)
        //{
        //    this.game = game;

        //    Microsoft.DirectX.Direct3D.PresentParameters pps = new Microsoft.DirectX.Direct3D.PresentParameters();
        //    pps.SwapEffect = Microsoft.DirectX.Direct3D.SwapEffect.Discard;
        //    pps.Windowed = true;

        //    _D3DDev = new Microsoft.DirectX.Direct3D.Device(0, Microsoft.DirectX.Direct3D.DeviceType.Hardware, control, Microsoft.DirectX.Direct3D.CreateFlags.SoftwareVertexProcessing, pps);

        //    _DSoundDev = new Microsoft.DirectX.DirectSound.Device();
        //    _DSoundDev.SetCooperativeLevel(control, Microsoft.DirectX.DirectSound.CooperativeLevel.Normal);

        //    _DKeyboardDev = new Microsoft.DirectX.DirectInput.Device(Microsoft.DirectX.DirectInput.SystemGuid.Keyboard);
        //    _DKeyboardDev.Acquire();

        //    _DMouseDev = new Microsoft.DirectX.DirectInput.Device(Microsoft.DirectX.DirectInput.SystemGuid.Mouse);
        //    _DMouseDev.Acquire();

        //    _Sprite = new Microsoft.DirectX.Direct3D.Sprite(_D3DDev); 
        //}
        #endregion
    }
}
