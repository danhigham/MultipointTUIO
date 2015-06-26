﻿#pragma checksum "..\..\WindowList.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8DE21738EF52D0190694482E18F90E3D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MTMultiMouse {
    
    
    /// <summary>
    /// WindowList
    /// </summary>
    public partial class WindowList : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 255 "..\..\WindowList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel _NavigationRoot;
        
        #line default
        #line hidden
        
        
        #line 257 "..\..\WindowList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid _Caption;
        
        #line default
        #line hidden
        
        
        #line 266 "..\..\WindowList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button _CloseButton;
        
        #line default
        #line hidden
        
        
        #line 295 "..\..\WindowList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView WindowSelector;
        
        #line default
        #line hidden
        
        
        #line 322 "..\..\WindowList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox UseTCP;
        
        #line default
        #line hidden
        
        
        #line 328 "..\..\WindowList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PortInput;
        
        #line default
        #line hidden
        
        
        #line 332 "..\..\WindowList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GoButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MTMultiMouse;component/windowlist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\WindowList.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this._NavigationRoot = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 2:
            this._Caption = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this._CloseButton = ((System.Windows.Controls.Button)(target));
            
            #line 266 "..\..\WindowList.xaml"
            this._CloseButton.Click += new System.Windows.RoutedEventHandler(this._CaptionButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.WindowSelector = ((System.Windows.Controls.ListView)(target));
            return;
            case 5:
            this.UseTCP = ((System.Windows.Controls.CheckBox)(target));
            
            #line 322 "..\..\WindowList.xaml"
            this.UseTCP.Click += new System.Windows.RoutedEventHandler(this.UseTCP_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.PortInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.GoButton = ((System.Windows.Controls.Button)(target));
            
            #line 332 "..\..\WindowList.xaml"
            this.GoButton.Click += new System.Windows.RoutedEventHandler(this.GoButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

