﻿#pragma checksum "C:\Users\nate2\Documents\GitHub\PolyDesktop\PolyDesktop\PolyDesktopGUI\TabMode.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "500B631FEB695963615EA07D9EFDC1A0947771186E087E6D774677553B5C7CD4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PolyDesktopGUI
{
    partial class TabMode : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // TabMode.xaml line 14
                {
                    this.BackButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.BackButton).Click += this.BackButton_Click;
                }
                break;
            case 3: // TabMode.xaml line 15
                {
                    global::Microsoft.UI.Xaml.Controls.TabView element3 = (global::Microsoft.UI.Xaml.Controls.TabView)(target);
                    ((global::Microsoft.UI.Xaml.Controls.TabView)element3).TabCloseRequested += this.Tabs_TabCloseRequested;
                    ((global::Microsoft.UI.Xaml.Controls.TabView)element3).AddTabButtonClick += this.TabView_AddTabButtonClick;
                }
                break;
            case 4: // TabMode.xaml line 16
                {
                    this.PolyBayTab = (global::Microsoft.UI.Xaml.Controls.TabViewItem)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

