using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Drawing;
using OSC.NET;

using Microsoft.Multipoint.Sdk;

namespace MTMultiMouse
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TargetWindow : Window
    {

        private Dictionary<string, Cursor> _cursors;
        private OSCTransmitter _oscTransmitter;
        private int _cursorSessionCounter;

        private int _clientCount;

        public long _windowWidth, _windowHeight, _windowLeft, _windowTop;
        public int _messageCounter = 0;

        public bool TCP;
        public int Port;

        public TargetWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _oscTransmitter = new OSCTransmitter("127.0.0.1", Port, TCP);
            _oscTransmitter.TCPClientConnected += _oscTransmitter_TCPClientConnected;
            _cursorSessionCounter = 0;

            _cursors = new Dictionary<string, Cursor>();

            this.WindowState = WindowState.Maximized;

            
            MultipointSdk.Instance.Register(this);
            App.MultiPointObject.CurrentWindow = this;

            foreach (DeviceInfo d in App.MultiPointObject.MouseDeviceList)
            {
                MultipointMouseDevice mpMouseDevice = d.DeviceVisual;
                Bitmap cursorBitmap = GetCursorImage(MultipointSdk.Instance.MouseDeviceList.Count);
                mpMouseDevice.CursorImage = ConvertBitmapToBitmapImage(cursorBitmap);
            }
            App.MultiPointObject.DeviceArrivalEvent += MultiPointObject_DeviceArrivalEvent;

            KeyDown += Window1_KeyDown;

            MTContainer.MultipointMouseDownEvent += MTContainer_MultiPointMouseDownEvent;
            MTContainer.MultipointMouseMoveEvent += MTContainer_MultiPointMouseMoveEvent;
            MTContainer.MultipointMouseUpEvent += MTContainer_MultiPointMouseUpEvent;

        }

        void _oscTransmitter_TCPClientConnected(object sender)
        {
            _clientCount++;

            Dispatcher.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            {
                MTContainer.ConnectedCount.Visibility = Visibility.Visible;

                MTContainer.ConnectedCount.Text = "";
            });

        }

        void MTContainer_MultiPointMouseUpEvent(object sender, RoutedEventArgs e)
        {
            //Send OSC message to remove cursor

            MultipointMouseEventArgs args = (MultipointMouseEventArgs)e;
            _cursors.Remove(args.DeviceInfo.DeviceId);

            SendStatusUpdate();
        }

        void MTContainer_MultiPointMouseDownEvent(object sender, RoutedEventArgs e)
        {
            //Add Cursor Object and send cursor add an OSC message

            MultipointMouseEventArgs args = (MultipointMouseEventArgs)e;
            MultipointMouseDevice mouse = args.DeviceInfo.DeviceVisual as MultipointMouseDevice;

            System.Windows.Point position = new System.Windows.Point();

            if (mouse != null)
                position = mouse.Position;

            Cursor _newCursor = new Cursor(position, 0, _cursorSessionCounter);
            if (!_cursors.Keys.Contains(args.DeviceInfo.DeviceId))
            {
                _cursors.Add(args.DeviceInfo.DeviceId, _newCursor);
                _cursorSessionCounter++;
            }

            SendStatusUpdate();
        }

        void MTContainer_MultiPointMouseMoveEvent(object sender, RoutedEventArgs e)
        {
            MultipointMouseEventArgs args = (MultipointMouseEventArgs)e;
            MultipointMouseDevice mouse = args.DeviceInfo.DeviceVisual;

            System.Windows.Point position = new System.Windows.Point();

            if (mouse.LeftButton == MultipointMouseButtonState.Pressed)
            {
                if (mouse != null)
                    position = mouse.Position;

                if (_cursors.Keys.Contains(args.DeviceInfo.DeviceId))
                {
                    Cursor c = _cursors[args.DeviceInfo.DeviceId];
                    c.Position = position;

                    SendStatusUpdate();
                }
            }
        }

        void Window1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                _oscTransmitter.Close();
                Close();
            }
        }

        void MultiPointObject_DeviceArrivalEvent(object sender, DeviceNotifyEventArgs e)
        {
            DeviceInfo mouseObject = e.DeviceInfo;
            MultipointMouseDevice mpMouseDevice = mouseObject.DeviceVisual;
            Bitmap cursorBitmap = GetCursorImage(MultipointSdk.Instance.MouseDeviceList.Count);
            mpMouseDevice.CursorImage = ConvertBitmapToBitmapImage(cursorBitmap);
        }

        public static BitmapImage ConvertBitmapToBitmapImage(System.Drawing.Bitmap b)
        {
            BitmapImage bmpimg = new BitmapImage();
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            bmpimg.BeginInit();
            b.MakeTransparent(System.Drawing.Color.White);
            b.Save(memStream, System.Drawing.Imaging.ImageFormat.Png);
            bmpimg.StreamSource = memStream;
            bmpimg.EndInit();
            return bmpimg;
        }


        private void SendStatusUpdate()
        {
            OSCBundle bundle = new OSCBundle();       

            OSCMessage message = new OSCMessage("/tuio/2Dcur");
            message.Append("source");
            message.Append("MultiPoint TUIO");

            bundle.Append(message);
            //_oscTransmitter.Send(message);

            message = new OSCMessage("/tuio/2Dcur");
            message.Append("alive");

            foreach (Cursor c in _cursors.Values)
            {
                message.Append(c.SessionID);
            }

            bundle.Append(message);
            //_oscTransmitter.Send(message);

            foreach (Cursor c in _cursors.Values)
            {
                float xPos = (float)((1 / (double)_windowWidth) * (c.Position.X - _windowLeft));
                float yPos = (float)((1 / (double)_windowHeight) * (c.Position.Y - _windowTop));

                message = new OSCMessage("/tuio/2Dcur");
                message.Append("set");
                message.Append(c.SessionID);
                message.Append(xPos);
                message.Append(yPos);
                message.Append(0.0f);
                message.Append(0.0f);
                message.Append(0.0f);

                bundle.Append(message as OSCPacket);
            }

            message = new OSCMessage("/tuio/2Dcur");
            message.Append("fseq");
            message.Append(_messageCounter);
            _messageCounter++;

            bundle.Append(message as OSCPacket);

            _oscTransmitter.Send(bundle);
        }

        private Bitmap GetCursorImage(int id)
        {
            switch (id)
            {
                case 0: return Properties.Resources.finger;
                case 1: return Properties.Resources.finger;
                case 2: return Properties.Resources.finger;
                case 3: return Properties.Resources.finger;
                default: return Properties.Resources.finger;
            }
        }


    }
}
