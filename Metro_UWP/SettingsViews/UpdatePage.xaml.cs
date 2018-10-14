using Metro_UWP.Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Metro_UWP.SettingsViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdatePage : Page
    {
        public UpdatePage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LastChangeAtTextBlock.Text = Windows.Storage.ApplicationData.Current.RoamingSettings.Values[StorageRepos.LastUpdateAt].ToString();
            }
            catch (Exception ex)
            {
                
            }
        }

        private async void GetLastChangesBtn_Click(object sender, RoutedEventArgs e)
        {
            MyProgressRing.IsActive = true;
            GetLastChangesBtn.IsEnabled = false;
            try
            {
                await StorageRepos.GetData();
                LastChangeAtTextBlock.Text = Windows.Storage.ApplicationData.Current.RoamingSettings.Values[StorageRepos.LastUpdateAt].ToString();
                StateTextBlock.Text = "Up to date ";
                StateTextBlock.Foreground = new SolidColorBrush(Colors.DarkCyan);
            }
            catch(Exception ex)
            {
                StateTextBlock.Text = "failed to get update !";
                StateTextBlock.Foreground = new SolidColorBrush(Colors.Salmon);
            }
            MyProgressRing.IsActive = false;

            await Task.Delay(4000);
            StateTextBlock.Text = "";

            GetLastChangesBtn.IsEnabled = true;
        }

        public event EventHandler<BackRequestedEventArgs> OnBackRequested;
        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            OnBackRequested?.Invoke(this, e);
            if (!e.Handled)
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    e.Handled = true;
                }
            }
        }
    }
}
