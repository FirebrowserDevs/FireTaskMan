// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using FireTaskMan.Helpers;
using Microsoft.UI;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.UI;
using System.Runtime.InteropServices;
using WinRT.Interop;
using WinRT;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Composition;
using System.Collections.ObjectModel;
using FireTaskMan.AppViews;
using System.Linq;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Navigation;
using Windows.UI.ApplicationSettings;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FireTaskMan
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
    
        private AppWindow m_AppWindow;
        public static MainWindow Current;
        //this are values dont need to by exactly the same
        WindowsSystemDispatcherQeueuHelper m_wsqdHelper;
        MicaController m_backdropController;
        SystemBackdropConfiguration m_configurationSource;
        public bool isCostumizationSupported { get; set; } = true;
        public MainWindow()
        {
            this.InitializeComponent();
         
            this.Title = "FireTaskMan";

            Current = this;

            isCostumizationSupported = AppWindowTitleBar.IsCustomizationSupported();

            Color cls = Colors.Transparent;

            m_AppWindow = GetAppWindowForCurrentWindow();
            // Check to see if customization is supported.
            // Currently only supported on Windows 11.
            if (isCostumizationSupported)
            {
                m_AppWindow = GetAppWindowForCurrentWindow();
                m_AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
                m_AppWindow.Title = "FireTaskMan";           
                m_AppWindow.TitleBar.BackgroundColor = cls;
                m_AppWindow.TitleBar.ButtonBackgroundColor = cls;
                m_AppWindow.TitleBar.InactiveBackgroundColor = cls;
                m_AppWindow.TitleBar.ButtonInactiveBackgroundColor = cls;
                m_AppWindow.TitleBar.ButtonInactiveForegroundColor = cls;
                m_AppWindow.TitleBar.ButtonHoverBackgroundColor = cls;
                TrySetSystemBackrop();
            }

            m_AppWindow.SetIcon("Assets/download.ico");

            // Get caption button occlusion information.
            int CaptionButtonOcclusionWidthRight = m_AppWindow.TitleBar.RightInset;
            int CaptionButtonOcclusionWidthLeft = m_AppWindow.TitleBar.LeftInset;

            // Set the width of padding columns in the UI.
            RightPaddingColumn.Width = new GridLength(CaptionButtonOcclusionWidthRight);
            LeftPaddingColumn.Width = new GridLength(CaptionButtonOcclusionWidthLeft);
        }


        #region appwindow

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(wndId);
            GetScaleAdjustments();
        }

        [DllImport("Shcore.dll", SetLastError = true)]
        internal static extern int GetDpiForMonitor(IntPtr hmonitor, Monitor_Dpi_Type dpi_Type, out uint dpiX, out uint dpiY);

        internal enum Monitor_Dpi_Type : int
        {
            MDT_Effective_DPI = 0,
            MDT_Angular_DPI = 1,
            MDT_Raw_DPI = 2,
            MDT_Default = MDT_Effective_DPI
        }

        private double GetScaleAdjustments()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            DisplayArea displayArea = DisplayArea.GetFromWindowId(wndId, DisplayAreaFallback.Primary);
            IntPtr hMonitor = Win32Interop.GetMonitorFromDisplayId(displayArea.DisplayId);

            int result = GetDpiForMonitor(hMonitor, Monitor_Dpi_Type.MDT_Default, out uint dpiX, out uint dpiY);
            if (result != 0)
            {
                throw new Exception("Could Not Get Dpi");
            }
            uint scaleFactoreProcent = (uint)(((long)dpiX * 100 + (96 >> 1)) / 96);
            return scaleFactoreProcent / 100.0;
        }

        #endregion

        #region micaalt

        bool TrySetSystemBackrop()
        {
            if (Microsoft.UI.Composition.SystemBackdrops.MicaController.IsSupported())
            {
                //this is the windows class from earlier
                m_wsqdHelper = new WindowsSystemDispatcherQeueuHelper();
                m_wsqdHelper.EnsureDispatchQeuerController();

                m_configurationSource = new SystemBackdropConfiguration();
                //important handlers
                this.Activated += Window_Activated;
                this.Closed += Window_Closed;
                //handle the themes
                ((FrameworkElement)this.AppTitleBar).ActualThemeChanged += Window_ThemeChanged;

                m_backdropController = new Microsoft.UI.Composition.SystemBackdrops.MicaController();
                //set your mica kind here
                m_backdropController.Kind = MicaKind.BaseAlt;
                //end mica kind


                m_backdropController.AddSystemBackdropTarget(this.As<ICompositionSupportsSystemBackdrop>());
                m_backdropController.SetSystemBackdropConfiguration(m_configurationSource);

                return true; // this means success
            }
            //return false if not supported
            return false;
        }

        private void Window_ThemeChanged(FrameworkElement sender, object args)
        {
            if (m_configurationSource != null)
            {
                //set the theme if = empty
                SetConfigurationTheme();
            }
        }

        private void SetConfigurationTheme()
        {
            switch (((FrameworkElement)this.Content).ActualTheme)
            {
                case ElementTheme.Dark: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Dark; break;
                case ElementTheme.Light: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Light; break;
                case ElementTheme.Default: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Default; break;
            }
        }
        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {

        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            //dipose if closing
            if (m_backdropController != null)
            {
                m_backdropController.Dispose();
                m_backdropController = null;
            }

            //change state
            this.Activated += Window_Activated;
            m_configurationSource = null;
        }

        #endregion    

        private void Nav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            switch (args.InvokedItem)
            {
                case "Processes":
                    // Navigate to the Home page
                    ContentFrame.Navigate(typeof(Processes));
                    break;
              
            }
        }      
    }
}