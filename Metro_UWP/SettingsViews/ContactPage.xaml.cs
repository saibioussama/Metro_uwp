using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ContactPage : Page
    {
        List<Tuple<string, string, string, string>> DevelopedBy;
        public ContactPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;

            DevelopedBy = new List<Tuple<string, string, string, string>>()
            {
                new Tuple<string, string,string,string>("Oussama SAIBI", "saibioussama@outlook.fr","https://www.facebook.com/saibioussama","+216 52304328"),
                new Tuple<string, string,string,string>("Majdi SAIBI", "saibimajdi@outlook.fr","https://www.facebook.com/saibimajdi"," - - "),
                //new Tuple<string, string>("Mejdi RADHOUANI", "--"),
                //new Tuple<string, string>("Emina DRIRA", "--"),
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DevelopedByListView.ItemsSource = DevelopedBy;
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
