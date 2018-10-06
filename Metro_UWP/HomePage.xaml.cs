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
        public HomePage()
        {
            this.InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, object e)
        {
            if (AvailableTimesOfStation.First() != null)
                RemainingTimeTB.Text = (AvailableTimesOfStation.First() - DateTime.Now).ToString();
            else
                RemainingTimeTB.Text = "--:--:--";
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var stations_sm = await StationsRepo.GetStations(Models.Station.Directions.SM);
            MyListView_sm.ItemsSource = stations_sm;
            var stations_ms = await StationsRepo.GetStations(Models.Station.Directions.MS);
            MyListView_ms.ItemsSource = stations_ms;
            SelectedStation = stations_sm.First();
            AvailableTimesOfStation = await GetAvailableTimesOfStation(MyPivot.SelectedIndex == 0 ? Station.Directions.SM : Station.Directions.MS, SelectedStation.Id);
            StationName.Text = SelectedStation.NameAR;
            TimesGridView.ItemsSource = AvailableTimesOfStation;
            timer.Start();
        }

        private async void MyListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            SelectedStation = e.ClickedItem as Station;
            AvailableTimesOfStation = await GetAvailableTimesOfStation(MyPivot.SelectedIndex == 0 ? Station.Directions.SM : Station.Directions.MS, SelectedStation.Id);
            StationName.Text = SelectedStation.NameAR;
        }

        public async Task<List<DateTime?>> GetAvailableTimesOfStation(Station.Directions direction,int stationId)
        {
            var times = await LinesRepos.GetAvailableTimesOfStation(direction, stationId);
            return Task.FromResult(times).Result;
        }
    }
}
