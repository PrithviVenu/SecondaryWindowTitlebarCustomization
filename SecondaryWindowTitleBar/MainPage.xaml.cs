using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SecondaryWindowTitleBar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public static Dictionary<string, AppWindow> AppWindows { get; set; }
        = new Dictionary<string, AppWindow>();

        // ...

        private async void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Create a new window.
            AppWindow appWindow = await AppWindow.TryCreateAsync();

            // Create a Frame and navigate to the Page you want to show in the new window.
            // Add the new page to the Dictionary using the UIContext as the Key.
            Frame appWindowContentFrame = new Frame();
            AppWindows.Add("100", appWindow);
            appWindow.Title = "App Window " + AppWindows.Count.ToString();
            appWindowContentFrame.Navigate(typeof(MeetingPage));

            // Attach the XAML content to the window.
            ElementCompositionPreview.SetAppWindowContent(appWindow, appWindowContentFrame);



            // When the window is closed, be sure to release
            // XAML resources and the reference to the window.
            appWindow.Closed += delegate
            {
                MainPage.AppWindows.Remove("100");
                appWindowContentFrame.Content = null;
                appWindow = null;
            };

            // Show the window.
            await appWindow.TryShowAsync();
        }

    }
}
