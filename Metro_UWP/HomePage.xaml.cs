using Metro_UWP.Models;
using Metro_UWP.Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class HomePage : Page
    {
        Station SelectedStation;
        List<DateTime?> AvailableTimesOfStation;
        DispatcherTimer timer;
        List<Station> stations_ms, stations_sm;

        public HomePage()
        {
            this.InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, object e)
        {
            if (AvailableTimesOfStation != null && AvailableTimesOfStation.Count > 0)
                RemainingTimeTB.Text = (AvailableTimesOfStation.First() - DateTime.Now).Value.ToString("hh\\:mm\\:ss");
            else
                RemainingTimeTB.Text = "--:--:--";
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                stations_sm = await StationsRepo.GetStations(Models.Station.Directions.SM);
                MyListView_sm.ItemsSource = stations_sm;
                stations_ms = await StationsRepo.GetStations(Models.Station.Directions.MS);
                MyListView_ms.ItemsSource = stations_ms;
                SelectedStation = stations_sm?.First();
                AvailableTimesOfStation = await GetAvailableTimesOfStation(MyPivot.SelectedIndex == 0 ? Station.Directions.SM : Station.Directions.MS, SelectedStation.Id);
                UpdateInformation();
                MainPage.OnSearchBoxTextChanged += MainPage_OnSearchBoxTextChanged;
            }
            catch (Exception ex)
            {

            }
            timer.Start();
        }

        private async void MainPage_OnSearchBoxTextChanged(string QueryText)
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

        private async void MyListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            SelectedStation = e.ClickedItem as Station;
            AvailableTimesOfStation = await GetAvailableTimesOfStation(MyPivot.SelectedIndex == 0 ? Station.Directions.SM : Station.Directions.MS, SelectedStation.Id);
            UpdateInformation();
        }

        private async Task<List<DateTime?>> GetAvailableTimesOfStation(Station.Directions direction, int stationId)
        {
            var times = await LinesRepos.GetAvailableTimesOfStation(direction, stationId);
            return Task.FromResult(times).Result;
        }

        private void UpdateInformation()
        {
            StationName.Text = SelectedStation.NameAR;
            TimesGridView.ItemsSource = AvailableTimesOfStation;

        }

        private async void MyPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (stations_sm?.Count > 0 && stations_ms?.Count > 0)
            {
                var item = MyPivot.Items[MyPivot.SelectedIndex];

                switch (MyPivot.SelectedIndex)
                {
                    case 0:
                        SelectedStation = stations_sm.First();
                        MyListView_sm.ItemsSource = stations_sm;
                        AvailableTimesOfStation = await GetAvailableTimesOfStation(Station.Directions.SM, SelectedStation.Id);
                        break;
                    case 1:
                        SelectedStation = stations_ms.First();
                        MyListView_ms.ItemsSource = stations_ms;
                        AvailableTimesOfStation = await GetAvailableTimesOfStation(Station.Directions.MS, SelectedStation.Id);
                        break;
                    default: break;
                }
                UpdateInformation();
            }
        }
    }
}
