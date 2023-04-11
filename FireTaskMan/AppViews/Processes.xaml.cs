// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

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
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using FireTaskMan.Helpers;
using System.Collections.ObjectModel;
using FireTaskMan.Controls;
using Windows.UI.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FireTaskMan.AppViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Processes : Page
    {
     
        private PerformanceCounter cpuUsageCounter;
        public Processes()
        {
            this.InitializeComponent();
            GpuNames();
          
        }

        public async void rundelay()
        {
            await Task.Delay(1);
            PopulateProcessList();
        }
      
        public void RunStartDelay()
        {
            cpuUsageCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            UpdateCpuUsageAsync();
        }
        private async Task UpdateCpuUsageAsync()
        {
            while (true)
            {
                var cpuUsage = cpuUsageCounter.NextValue();
                CpuUsage.Text = $"CPU Usage: {cpuUsage:0.00}%";
                await Task.Delay(500);
            }
        }

    
        private async void CpuUsage_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1);
            RunStartDelay();
            // Update the UI on the UI thread
      
                // Perform some work that takes a long time, but doesn't block the UI thread
                rundelay();
           
        }

        private async void PopulateProcessList()
        {
            ProcessesListView = (ListView)FindName("ProcessesListView");

            // Get a list of all running processes with their process name, ID, and base priority
            var processes = Process.GetProcesses()
                                   .Select(p => new ProcessInfo
                                   {
                                       Name = p.ProcessName,
                                       Id = p.Id,
                                       Priority = p.BasePriority.ToString()
                                   })
                                   .ToList();

            // Set the ItemsSource of the ListView to the list of processes
            ProcessesListView.ItemsSource = processes;

        }


        public async Task GpuNames()
        {
            string gpuBrandNames = await SysInfo.GetGpuBrandNamesAsync();
            GpUsage.Text = gpuBrandNames;
        }
    }
}
