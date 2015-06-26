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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Multipoint.Sdk;
using Microsoft.Multipoint.Sdk.Controls;

namespace MTMultiMouse
{
    /// <summary>
    /// Interaction logic for MTTouchBox.xaml
    /// </summary>
    /// 

    public partial class MTTouchBox : UserControl, IMultipointMouseEvents, IMultipointGenericDeviceEvents
    {
        public MTTouchBox()
        {
            InitializeComponent();

        }

        public event RoutedEventHandler MultipointMouseDownEvent
        {
            add { MultipointMouseEvents.AddMultipointMouseDownHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointMouseDownHandler(this, value); }
        }

        public event RoutedEventHandler MultipointMouseLeftButtonDownEvent
        {
            add { MultipointMouseEvents.AddMultipointMouseLeftButtonDownHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointMouseLeftButtonDownHandler(this, value); }
        }

        public event RoutedEventHandler MultipointMouseLeftButtonUpEvent
        {
            add { MultipointMouseEvents.AddMultipointMouseLeftButtonUpHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointMouseLeftButtonUpHandler(this, value); }
        }

        public event RoutedEventHandler MultipointMouseRightButtonDownEvent
        {
            add { MultipointMouseEvents.AddMultipointMouseRightButtonDownHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointMouseRightButtonDownHandler(this, value); }
        }

        public event RoutedEventHandler MultipointMouseRightButtonUpEvent
        {
            add { MultipointMouseEvents.AddMultipointMouseRightButtonUpHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointMouseRightButtonUpHandler(this, value); }
        }

        public event RoutedEventHandler MultipointMouseEnterEvent
        {
            add { MultipointMouseEvents.AddMultipointMouseEnterHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointMouseEnterHandler(this, value); }
        }

        public event RoutedEventHandler MultipointMouseLeaveEvent
        {
            add { MultipointMouseEvents.AddMultipointMouseLeaveHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointMouseLeaveHandler(this, value); }
        }

        public event RoutedEventHandler MultipointMouseMoveEvent
        {
            add { MultipointMouseEvents.AddMultipointMouseMoveHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointMouseMoveHandler(this, value); }
        }

        public event RoutedEventHandler MultipointMouseWheelEvent
        {
            add { MultipointMouseEvents.AddMultipointMouseWheelHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointMouseWheelHandler(this, value); }
        }

        public event RoutedEventHandler MultipointPreviewMouseUpEvent
        {
            add { MultipointMouseEvents.AddMultipointPreviewMouseUpHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointPreviewMouseUpHandler(this, value); }
        }

        public event RoutedEventHandler MultipointPreviewMouseDownEvent
        {
            add { MultipointMouseEvents.AddMultipointPreviewMouseDownHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointPreviewMouseDownHandler(this, value); }
        }

        public event RoutedEventHandler MultipointPreviewMouseLeftButtonDownEvent
        {
            add { MultipointMouseEvents.AddMultipointPreviewMouseLeftButtonDownHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointPreviewMouseLeftButtonDownHandler(this, value); }
        }

        public event RoutedEventHandler MultipointPreviewMouseLeftButtonUpEvent
        {
            add { MultipointMouseEvents.AddMultipointPreviewMouseLeftButtonUpHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointPreviewMouseLeftButtonUpHandler(this, value); }
        }

        public event RoutedEventHandler MultipointPreviewMouseRightButtonDownEvent
        {
            add { MultipointMouseEvents.AddMultipointPreviewMouseRightButtonDownHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointPreviewMouseRightButtonDownHandler(this, value); }
        }

        public event RoutedEventHandler MultipointPreviewMouseRightButtonUpEvent
        {
            add { MultipointMouseEvents.AddMultipointPreviewMouseRightButtonUpHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointPreviewMouseRightButtonUpHandler(this, value); }
        }

        public event RoutedEventHandler MultipointPreviewMouseWheelEvent
        {
            add { MultipointMouseEvents.AddMultipointPreviewMouseWheelHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointPreviewMouseWheelHandler(this, value); }
        }

        public event RoutedEventHandler MultipointMouseUpEvent
        {
            add { MultipointMouseEvents.AddMultipointMouseUpHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointMouseUpHandler(this, value); }
        }

        public event RoutedEventHandler MultipointPreviewMouseMoveEvent
        {
            add { MultipointMouseEvents.AddMultipointPreviewMouseMoveHandler(this, value); }
            remove { MultipointMouseEvents.RemoveMultipointPreviewMouseMoveHandler(this, value); }
        }

        public event RoutedEventHandler MultipointDeviceDown
        {
            add { }
            remove { }
        }

        public event RoutedEventHandler MultipointDeviceUp
        {
            add { }
            remove { }
        }

        public event RoutedEventHandler MultipointDeviceMove
        {
            add { }
            remove { }
        }

    }
}
