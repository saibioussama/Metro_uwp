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

namespace Metro_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StationsPage : Page
    {
        Station SelectedStation;
        List<Station> stations_ms, stations_sm;
        

        public StationsPage()
        {
            this.InitializeComponent();
        }

        private void MyPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void MyListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            SelectedStation = e.ClickedItem as Station;
            Frame.Navigate(typeof(TimesPage), new Tuple<Station, Station.Directions>(SelectedStation, MyPivot.SelectedIndex == 0 ? Station.Directions.SM : Station.Directions.MS));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                stations_sm = await StationsRepo.GetStations(Models.Station.Directions.SM);
                MyListView_sm.ItemsSource = stations_sm;
                stations_ms = await StationsRepo.GetStations(Models.Station.Directions.MS);
                MyListView_ms.ItemsSource = stations_ms;
                MainPage.OnSearchBoxTextChanged += MainPage_OnSearchBoxTextChanged;
            }
            catch (Exception )
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
    }
}
