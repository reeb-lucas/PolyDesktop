﻿#pragma checksum "..\..\PolyBay.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3C942FBE42ACCCC43003439026AACF4DC171D5616AC169EA9C9A8570D1C54393"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    /// PolyBay
    /// </summary>
    public partial class PolyBay : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 95 "..\..\PolyBay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel BasicPanel;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\PolyBay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchBox;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\PolyBay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView SearchListBox;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\PolyBay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sendFileButton;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\PolyBay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar sendProgress;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\PolyBay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer fileScroller;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\PolyBay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbReceivedFiles;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\PolyBay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scroller;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\PolyBay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock logBox;
        
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
            System.Uri resourceLocater = new System.Uri("/PolyDesktopGUI-WPF;component/polybay.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PolyBay.xaml"
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
            case 3:
            this.BasicPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.SearchBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 97 "..\..\PolyBay.xaml"
            this.SearchBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.search_QueryChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SearchListBox = ((System.Windows.Controls.ListView)(target));
            
            #line 98 "..\..\PolyBay.xaml"
            this.SearchListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SearchListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.sendFileButton = ((System.Windows.Controls.Button)(target));
            
            #line 112 "..\..\PolyBay.xaml"
            this.sendFileButton.Click += new System.Windows.RoutedEventHandler(this.sendFileButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.sendProgress = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 8:
            this.fileScroller = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 9:
            this.lbReceivedFiles = ((System.Windows.Controls.ListBox)(target));
            return;
            case 10:
            this.scroller = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 11:
            this.logBox = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 54 "..\..\PolyBay.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteFile_Clicked);
            
            #line default
            #line hidden
            break;
            case 2:
            
            #line 62 "..\..\PolyBay.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveFile_Clicked);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

