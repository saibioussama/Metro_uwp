using Metro_UWP.Models;
using Metro_UWP.Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Metro_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimesPage : Page
    {
        Station station;
        Station.Directions direction;
        List<DateTime?> Times;

        public static event Action HideSearchBox;
        public static event Action ShowSearchBox;

        public TimesPage()
        {
            this.InitializeComponent();
            Times = new List<DateTime?>();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Times = await LinesRepos.GetTimesOfStation(direction, station.Id);
            TimesGridView.ItemsSource = Times;
            StationNameTB.Text = station.NameAR;
            DirectionTB.Text = direction == Station.Directions.SM ? "Sousse to Mahdia" : "Mahdia to Sousse";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Tuple<Station,Station.Directions> tuple = e.Parameter as Tuple<Station, Station.Directions>;
            station = tuple.Item1;
            direction = tuple.Item2;
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
            HideSearchBox?.Invoke();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ShowSearchBox?.Invoke();
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
