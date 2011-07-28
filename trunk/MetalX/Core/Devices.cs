using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX.Direct3D;
namespace MetalX
{
    public class GameWindow : Form
    {
        public GameWindow(Game g)
        {
            Text = g.Name;
            StartPosition = FormStartPosition.CenterScreen;
            Size = g.Options.WindowSizePixel;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.Opaque, true);
        }
    }
    public class Devices : IDisposable
    {
        Game game;
        public GameWindow GameWindow;
        public Control GameControl;

        public Microsoft.DirectX.Direct3D.Device D3DDev;
        public Microsoft.DirectX.DirectSound.Device DSoundDev;
        public Microsoft.DirectX.DirectInput.Device DKeyboardDev;
        public Microsoft.DirectX.DirectInput.Device DMouseDev;
        public Microsoft.DirectX.Direct3D.VertexBuffer VertexBuffer;

        public Microsoft.DirectX.Direct3D.Font Font;
        public Microsoft.DirectX.Direct3D.Sprite Sprite;

        public SizeF D3DDevSizePixel
        {
            get
            {
                return new SizeF(D3DDev.PresentationParameters.BackBufferWidth, D3DDev.PresentationParameters.BackBufferHeight);
            }
        }
        void init()
        {
            Microsoft.DirectX.Direct3D.PresentParameters pps = new Microsoft.DirectX.Direct3D.PresentParameters();
            pps.SwapEffect = Microsoft.DirectX.Direct3D.SwapEffect.Discard;
            pps.Windowed = true;
            pps.BackBufferCount = 2;
            pps.PresentationInterval = PresentInterval.One;
            pps.PresentFlag = PresentFlag.LockableBackBuffer;

            if (GameWindow == null)
            {
                //pps.BackBufferWidth = GameControl.Width;
                //pps.BackBufferHeight = GameControl.Height;
                D3DDev = new Microsoft.DirectX.Direct3D.Device(0, Microsoft.DirectX.Direct3D.DeviceType.Hardware, GameControl, Microsoft.DirectX.Direct3D.CreateFlags.SoftwareVertexProcessing, pps);
            }
            else
            {
                //pps.BackBufferWidth = GameWindow.Width;
                //pps.BackBufferHeight = GameWindow.Height;
                D3DDev = new Microsoft.DirectX.Direct3D.Device(0, Microsoft.DirectX.Direct3D.DeviceType.Hardware, GameWindow, Microsoft.DirectX.Direct3D.CreateFlags.SoftwareVertexProcessing, pps);
            }
            D3DDev.VertexFormat = CustomVertex.PositionColoredTextured.Format;


            //D3DDev.ShowCursor(false);

            DSoundDev = new Microsoft.DirectX.DirectSound.Device();
            if (GameWindow == null)
            {
                DSoundDev.SetCooperativeLevel(GameControl, Microsoft.DirectX.DirectSound.CooperativeLevel.Normal);
            }
            else
            {
                DSoundDev.SetCooperativeLevel(GameWindow, Microsoft.DirectX.DirectSound.CooperativeLevel.Normal);
            }
            
            DKeyboardDev = new Microsoft.DirectX.DirectInput.Device(Microsoft.DirectX.DirectInput.SystemGuid.Keyboard);
            DKeyboardDev.Acquire();

            DMouseDev = new Microsoft.DirectX.DirectInput.Device(Microsoft.DirectX.DirectInput.SystemGuid.Mouse);
            DMouseDev.Acquire();

            VertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionColoredTextured), 6, D3DDev, Usage.None, CustomVertex.PositionColoredTextured.Format, Pool.Managed);

            D3DDev.SetStreamSource(0, VertexBuffer, 0);
            D3DDev.TextureState[0].AlphaOperation = TextureOperation.Modulate;

            Sprite = new Sprite(D3DDev);

            FontDescription fd = new FontDescription();
            fd.FaceName = "新宋体";
            fd.Height = -12;
            Font = new Microsoft.DirectX.Direct3D.Font(D3DDev, fd);
        }
        public Devices(Game g)
        {            
            game = g;
            GameWindow = new MetalX.GameWindow(g);
            init();
        }
        public Devices(Game g, Control c)
        {
            game = g;
            GameControl = c;
            init();
        }
        public void Dispose()
        {
            Font.Dispose();
            Sprite.Dispose();
            D3DDev.Dispose();
            DSoundDev.Dispose();
            DKeyboardDev.Dispose();
            DMouseDev.Dispose();
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
