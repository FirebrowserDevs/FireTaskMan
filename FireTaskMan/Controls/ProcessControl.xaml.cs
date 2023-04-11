// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using FireTaskMan.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FireTaskMan.Controls
{
    public sealed partial class ProcessControl : UserControl
    {
        public ProcessControl()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty ProcessProperty =
                  DependencyProperty.Register("Process", typeof(ProcessInfo), typeof(ProcessControl), null);

        public ProcessInfo Process
        {
            get { return (ProcessInfo)GetValue(ProcessProperty); }
            set { SetValue(ProcessProperty, value); }
        }

        private void PriorityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
