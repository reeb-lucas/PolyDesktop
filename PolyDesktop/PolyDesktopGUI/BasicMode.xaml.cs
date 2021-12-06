using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
/**************************************************************
 * Copyright (c) 2021
 * Author: Tyler Lucas
 * Filename: BasicMode.xaml.cs
 * Date Created: 11/30/2021
 * Modifications: 11/30/2021 - Created Basic Mode, WIP label waiting on remoting capabilities
 **************************************************************/
/**************************************************************
 * Overview:
 *      This page displays one desktop for the user along with the computer id
 **************************************************************/

namespace PolyDesktopGUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BasicMode : Page
    {
        public BasicMode()
        {
            this.InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
