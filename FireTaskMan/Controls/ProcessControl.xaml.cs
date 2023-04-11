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
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            ProcessInfo pr = new ProcessInfo();
            pr.Priority = "Normal";        
        }

        public List<ComboBoxItem> PriorityItems => new List<ComboBoxItem>
        {
              new ComboBoxItem { Content = "Normal" },
              new ComboBoxItem { Content = "Low" },
              new ComboBoxItem { Content = "High" }
        };

        private static readonly Dictionary<string, int> PriorityIndices = new Dictionary<string, int>
        {
          { "Normal", 0 },
          { "Low", 1 },
          { "High", 2 }
        };

        // ...

    

        private void PriorityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Kill_Click(object sender, RoutedEventArgs e)
        {

        }
    
    }
}
