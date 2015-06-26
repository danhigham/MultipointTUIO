using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Microsoft.Multipoint.Sdk;
    
namespace MTMultiMouse
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        static public MultipointSdk MultiPointObject = MultipointSdk.Instance;
        static public WindowList WindowListPicker = new WindowList();

        public App (){
            WindowListPicker.Show();
        }
    }
}
