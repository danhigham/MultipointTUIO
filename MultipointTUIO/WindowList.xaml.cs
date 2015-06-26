using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MTMultiMouse
{
    /// <summary>
    /// Interaction logic for WindowList.xaml
    /// </summary>
    public partial class WindowList
    {
        

        public WindowList()
        {
            InitializeComponent();

            // Get a window list...
            DataContext = User32Helper.ListWindows();
            
        }

        private void _CaptionButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowSelector.SelectedIndex > -1)
            {
                WindowSpec spec = this.WindowSelector.SelectedItem as WindowSpec;

                TargetWindow targetWin = new TargetWindow();
                targetWin.Port = int.Parse(this.PortInput.Text);
                targetWin.TCP = (bool)this.UseTCP.IsChecked;

                User32Helper.tagWINDOWINFO info = spec.Info;
                User32Helper.GetWindowInfo(spec.Hwnd, ref info);

                long _windowWidth = targetWin._windowWidth = info.rcWindow.right - info.rcWindow.left;
                long _windowHeight = targetWin._windowHeight = info.rcWindow.bottom - info.rcWindow.top;

                targetWin.MTContainer.Width = _windowWidth;
                targetWin.MTContainer.Height = _windowHeight;

                long _windowLeft = targetWin._windowLeft = info.rcWindow.left + (info.cxWindowBorders / 2);
                long _windowTop = targetWin._windowTop = info.rcWindow.top + (info.cyWindowBorders);

                Canvas.SetLeft(targetWin.MTContainer, (double)_windowLeft);
                Canvas.SetTop(targetWin.MTContainer, (double)_windowTop);

                targetWin.Show();
                this.Hide();
            }
        }

        private void UseTCP_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)((CheckBox)sender).IsChecked)
            {
                this.PortInput.Text = "3000";
            }
            else
            {
                this.PortInput.Text = "3333";
            }
        }
    }
}
