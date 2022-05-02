﻿#pragma checksum "..\..\VncPageGroup.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5C5829330908F5F46D032C75F7E20770A25F6DBA551BAC4E9EAA65873B716C61"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro;
using MahApps.Metro.Accessibility;
using MahApps.Metro.Actions;
using MahApps.Metro.Automation.Peers;
using MahApps.Metro.Behaviors;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Converters;
using MahApps.Metro.Markup;
using MahApps.Metro.Theming;
using MahApps.Metro.ValueBoxes;
using PollRobots.OmotVnc.Controls;
using PolyDesktopGUI_WPF;
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
using System.Windows.Shell;


namespace PolyDesktopGUI_WPF {
    
    
    /// <summary>
    /// VncPageGroup
    /// </summary>
    public partial class VncPageGroup : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\VncPageGroup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PollRobots.OmotVnc.Controls.VncHost VncHost;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\VncPageGroup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ComputerPanel;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\VncPageGroup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.ToggleSwitch AdvancedSwitch;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\VncPageGroup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel BasicPanel;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\VncPageGroup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\VncPageGroup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView SearchListBox;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\VncPageGroup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel AdvancedPanel;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\VncPageGroup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ServerNameBox;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\VncPageGroup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PortBox;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\VncPageGroup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PWBox;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\VncPageGroup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ConnectButton;
        
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
            System.Uri resourceLocater = new System.Uri("/PolyDesktopGUI-WPF;component/vncpagegroup.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\VncPageGroup.xaml"
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
            this.VncHost = ((PollRobots.OmotVnc.Controls.VncHost)(target));
            return;
            case 2:
            this.ComputerPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.AdvancedSwitch = ((MahApps.Metro.Controls.ToggleSwitch)(target));
            
            #line 33 "..\..\VncPageGroup.xaml"
            this.AdvancedSwitch.Toggled += new System.Windows.RoutedEventHandler(this.AdvancedSwitch_Toggled);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BasicPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.SearchBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 36 "..\..\VncPageGroup.xaml"
            this.SearchBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.search_QueryChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SearchListBox = ((System.Windows.Controls.ListView)(target));
            
            #line 37 "..\..\VncPageGroup.xaml"
            this.SearchListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SearchListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.AdvancedPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 8:
            this.ServerNameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.PortBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.PWBox = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 11:
            this.ConnectButton = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\VncPageGroup.xaml"
            this.ConnectButton.Click += new System.Windows.RoutedEventHandler(this.ConnectButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

