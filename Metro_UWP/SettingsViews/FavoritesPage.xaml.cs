using Metro_UWP.Models;
using Metro_UWP.Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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

namespace Metro_UWP.SettingsViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FavoritesPage : Page
    {
        Station SelectedStation;
        List<Station> stations_ms, stations_sm;
        int Fav_sm = 0;
        int Fav_ms = 0;
        public FavoritesPage()
        {
            this.InitializeComponent();
            try
            {
                Fav_sm = Convert.ToInt16(ApplicationData.Current.RoamingSettings.Values[StorageRepos.fav_sm].ToString());
                Fav_ms = Convert.ToInt16(ApplicationData.Current.RoamingSettings.Values[StorageRepos.fav_ms].ToString());
            }
            catch
            {
            }
        }

        private void MyPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MyListView_ms.SelectedIndex = Fav_ms;
                MyListView_ms.ScrollIntoView(MyListView_ms.Items[Fav_ms]);
                MyListView_sm.SelectedIndex = Fav_sm;
                MyListView_sm.ScrollIntoView(MyListView_sm.Items[Fav_sm]);

                if (MyPivot.SelectedIndex == 0)
                {
                    StationName.Text = stations_sm[Fav_sm].NameAR;
                }
                else
                {
                    StationName.Text = stations_ms[Fav_ms].NameAR;
                }
            }
            catch
            {

            }
        }

        private async void MyListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            SelectedStation = e.ClickedItem as Station;
            StationName.Text = SelectedStation.NameAR;
            string message = "";
            switch (MyPivot.SelectedIndex)
            {
                case 0:
                    ApplicationData.Current.RoamingSettings.Values[StorageRepos.fav_sm] = (SelectedStation.Id - 1).ToString();
                    message = $"'{SelectedStation.NameFR}' selected as your default station from sousse to mahdia.";
                    break;
                case 1:
                    ApplicationData.Current.RoamingSettings.Values[StorageRepos.fav_ms] = (SelectedStation.Id - 1).ToString();
                    message = $"'{SelectedStation.NameFR}' selected as your default station from mahdia to sousse.";
                    break;
                default:
                    message = "something went wrong !";
                    break;
            }

            try
            {
                Fav_sm = Convert.ToInt16(ApplicationData.Current.RoamingSettings.Values[StorageRepos.fav_sm].ToString());
                Fav_ms = Convert.ToInt16(ApplicationData.Current.RoamingSettings.Values[StorageRepos.fav_ms].ToString());
            }
            catch
            {
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                stations_sm = await StationsRepo.GetStations(Station.Directions.SM);
                MyListView_sm.ItemsSource = stations_sm;
                stations_ms = await StationsRepo.GetStations(Station.Directions.MS);
                MyListView_ms.ItemsSource = stations_ms;
                MyListView_ms.SelectedIndex = Fav_ms;
                MyListView_ms.ScrollIntoView(MyListView_ms.Items[Fav_ms]);
                MyListView_sm.SelectedIndex = Fav_sm;
                MyListView_sm.ScrollIntoView(MyListView_sm.Items[Fav_sm]);

                if (MyPivot.SelectedIndex == 0)
                {
                    StationName.Text = stations_sm[Fav_sm].NameAR;
                }
                else
                {
                    StationName.Text = stations_ms[Fav_ms].NameAR;
                }

                MainPage.OnSearchBoxTextChanged += MainPage_OnSearchBoxTextChanged;
            }
            catch (Exception)
            {

            }
        }

        private void MainPage_OnSearchBoxTextChanged(string QueryText)
        {
            if (QueryText.Length > 0)
            {
                switch (MyPivot.SelectedIndex)
                {
                    case 0:
                        MyListView_sm.ItemsSource = stations_sm.Where(s => s.NameAR.ToLower().Contains(QueryText.ToLower()) || s.NameFR.ToLower().Contains(QueryText.ToLower()));
                        break;
                    case 1:
                        MyListView_ms.ItemsSource = stations_ms.Where(s => s.NameAR.ToLower().Contains(QueryText.ToLower()) || s.NameFR.ToLower().Contains(QueryText.ToLower()));
                        break;
                    default: break;
                }
            }
            else
            {
                MyListView_sm.ItemsSource = stations_sm;
                MyListView_ms.ItemsSource = stations_ms;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
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
