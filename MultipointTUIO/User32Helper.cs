using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Drawing;

namespace MTMultiMouse
{
    public delegate bool CallBackPtr(int hwnd, int lParam);

    public class WindowSpec
    {
       
        public String Caption { get; set; }
        public IntPtr Hwnd { get; set; }
        public User32Helper.tagWINDOWINFO Info { get; set; }
        public ImageSource Preview { get; set; }
    }

    public static class User32Helper
    {
        public static List<WindowSpec> WindowNames;

        #region User32 Imports

        [DllImport("user32.dll")]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref tagWINDOWINFO pwi);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);

        // For Windows Mobile, replace user32.dll with coredll.dll
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.
        // Also consider whether you're being lazy or not.
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsIconic(IntPtr hWnd);

        [DllImport ("User32.dll")]
        public extern static bool PrintWindow (System.IntPtr hWnd, System.IntPtr dc, uint reservedFlag);

        [DllImport("user32.dll")]
        private static extern int EnumWindows(CallBackPtr callPtr, int lPar);

        #endregion

        #region structs

        [StructLayout(LayoutKind.Sequential)]
        public struct tagRECT
        {
            /// LONG->int
            public int left;

            /// LONG->int
            public int top;

            /// LONG->int
            public int right;

            /// LONG->int
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct tagWINDOWINFO
        {
            /// DWORD->unsigned int
            public uint cbSize;

            /// RECT->tagRECT
            public tagRECT rcWindow;

            /// RECT->tagRECT
            public tagRECT rcClient;

            /// DWORD->unsigned int
            public uint dwStyle;

            /// DWORD->unsigned int
            public uint dwExStyle;

            /// DWORD->unsigned int
            public uint dwWindowStatus;

            /// UINT->unsigned int
            public uint cxWindowBorders;

            /// UINT->unsigned int
            public uint cyWindowBorders;

            /// ATOM->WORD->unsigned short
            public ushort atomWindowType;

            /// WORD->unsigned short
            public ushort wCreatorVersion;
        }

        #endregion

        public static string GetText(IntPtr hWnd)
        {
            // Allocate correct string length first
            int length = GetWindowTextLength(hWnd);
            StringBuilder sb = new StringBuilder(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        static Bitmap GetWindowPreview(WindowSpec spec)
        {
            
            Rectangle rc;
            GetWindowRect(spec.Hwnd, out rc);

            if ((rc.Width > 0) && (rc.Height > 0))
            {
                Bitmap bmp = new Bitmap(rc.Width, rc.Height);
                Graphics gfxBmp = Graphics.FromImage(bmp);

                IntPtr hdcBitmap = gfxBmp.GetHdc();

                PrintWindow(spec.Hwnd, hdcBitmap, 0);

                gfxBmp.ReleaseHdc(hdcBitmap);
                gfxBmp.Dispose();

                return bmp;
            }
            else
            {
                return new Bitmap(1, 1);
            }
            /*
            int height = spec.Info.rcWindow.bottom - spec.Info.rcWindow.top;
            int width = spec.Info.rcWindow.right - spec.Info.rcWindow.left;

            Bitmap screen = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(screen);
            
            g.CopyFromScreen(spec.Info.rcWindow.left, spec.Info.rcWindow.top, 0, 0, new System.Drawing.Size(width, height));

            g.Dispose();

            return screen;
            */
        }


        private static BitmapImage ConvertBitmapToBitmapImage(System.Drawing.Bitmap b)
        {
            BitmapImage bmpimg = new BitmapImage();
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            b.Save(memStream, System.Drawing.Imaging.ImageFormat.Bmp);
            bmpimg.BeginInit();
            b.MakeTransparent(System.Drawing.Color.White);
            //b.Save(memStream, System.Drawing.Imaging.ImageFormat.Bmp);
            bmpimg.StreamSource = memStream;
            bmpimg.EndInit();
            return bmpimg;
        }


        private static bool Report(int hwnd, int lParam)
        {
            IntPtr ptr = new IntPtr(hwnd);
            tagWINDOWINFO info = new tagWINDOWINFO();
            GetWindowInfo(ptr, ref info);

            if (IsWindowVisible(ptr) && !IsIconic(ptr) && info.cxWindowBorders > 0 && info.cyWindowBorders > 0)
            {
                string title = GetText(new IntPtr(hwnd));

                if (title != "")
                {
                    WindowSpec spec = new WindowSpec();
                    spec.Caption = title;
                    spec.Hwnd = ptr;
                    spec.Info = info;

                    Bitmap bm = GetWindowPreview(spec);
                    BitmapImage bi = ConvertBitmapToBitmapImage(bm);
                    TransformedBitmap tb = new TransformedBitmap();
                    tb.BeginInit();
                    tb.Source = bi;

                    double ratio = bi.Height / bi.Width;
                    double newHeight = 150 * ratio;
                    double scaleX = 150 / bi.Width;
                    double scaleY =  newHeight / bi.Height;

                    ScaleTransform transform = new ScaleTransform(scaleX, scaleY);
                    tb.Transform = transform;
                    tb.EndInit();
                    spec.Preview = tb;
                    WindowNames.Add(spec);
                }
            }
                
            return true;
        }

        public static List<WindowSpec> ListWindows()
        {
            WindowNames = new List<WindowSpec>();

            CallBackPtr callBackPtr;
            
            callBackPtr = new CallBackPtr(Report);  
            EnumWindows(callBackPtr, 0);

            return WindowNames;
        }

    }

   
}
