﻿#pragma checksum "S:\School\Fall2021\PolyDesktop\PolyDesktop\PolyDesktopGUI\MainPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E50E1F531679CA786F2DD70A8FD7A0FB747AD4017CFFD3A7EF7BA0F4E40155B4"
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
    partial class MainPage : 
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
            case 2: // MainPage.xaml line 13
                {
                    this.StartPDButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 3: // MainPage.xaml line 23
                {
                    this.DesktopPresetsButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.DesktopPresetsButton).Click += this.DesktopPresetsButton_Click;
                }
                break;
            case 4: // MainPage.xaml line 24
                {
                    this.DesktopPropertiesButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.DesktopPropertiesButton).Click += this.DesktopPropertiesButton_Click;
                }
                break;
            case 5: // MainPage.xaml line 25
                {
                    this.ScriptStatsButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.ScriptStatsButton).Click += this.ScriptStatsButton_Click;
                }
                break;
            case 6: // MainPage.xaml line 26
                {
                    this.TutorialButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 7: // MainPage.xaml line 27
                {
                    this.SettingsButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 8: // MainPage.xaml line 16
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element8 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element8).Click += this.BasicMode_Click;
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

