using Metro_UWP.Models;
using Metro_UWP.Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
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
    public sealed partial class MapPage : Page
    {
        List<Station> Stations;
        Station station;
        Geopoint CurrentPosition = null;

        public MapPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Stations = await StationsRepo.GetStations(Station.Directions.SM);
            MyListView_sm.ItemsSource = Stations;
            MyListView_sm.SelectedIndex = 0;
            station = Stations.First();
            MainPage.OnSearchBoxTextChanged += MainPage_OnSearchBoxTextChanged;
            InitMap();
            MyMap.Center = new Geopoint(new BasicGeoposition()
            {
                Latitude = 35.670235,
                Longitude = 10.882897
            });
            MyMap.ZoomLevel = 10;
        }

        private void MainPage_OnSearchBoxTextChanged(string QueryText)
        {
            if (QueryText.Length > 0)
                MyListView_sm.ItemsSource = Stations.Where(s => s.NameAR.ToLower().Contains(QueryText.ToLower()) || s.NameFR.ToLower().Contains(QueryText.ToLower()));
            else
                MyListView_sm.ItemsSource = Stations;
        }

        private async void MyListView_sm_ItemClick(object sender, ItemClickEventArgs e)
        {
            station = e.ClickedItem as Station;
            MyMap.Center = new Geopoint(new BasicGeoposition()
            {
                Latitude = station.Lat,
                Longitude = station.Long
            });

            MapScene mp = MapScene.CreateFromLocationAndRadius(new Geopoint(new BasicGeoposition() { Latitude = station.Lat, Longitude = station.Long }), 800);
            await MyMap.TrySetSceneAsync(mp);
        }

        private void SetPoint(double Lat, double Long, string Name)
        {
            // Specify a known location.
            BasicGeoposition snPosition = new BasicGeoposition() { Latitude = Lat, Longitude = Long };
            Geopoint snPoint = new Geopoint(snPosition);

            // Create a MapIcon.
            MapIcon mapIcon1 = new MapIcon();
            mapIcon1.Location = snPoint;
            mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon1.Title = Name;

            mapIcon1.ZIndex = 0;

            // Add the MapIcon to the map.
            MyMap.MapElements.Add(mapIcon1);

            // Center the map over the POI.
            MyMap.Center = snPoint;
            MyMap.ZoomLevel = 16;
        }

        private void InitMap()
        {
            foreach (Station x in Stations)
            {
                SetPoint(x.Lat, x.Long, x.NameAR);
            }
        }

        private async Task GetCurrentLocation()
        {
            MyProgressRing.IsActive = true;
            MyMap.Opacity = 0.5;
            try
            {
                if (MyMap.MapElements.Count > Stations.Count)
                    MyMap.MapElements.RemoveAt(MyMap.MapElements.Count - 1);
                var geoLocator = new Geolocator();
                var position = await geoLocator.GetGeopositionAsync();
                var mapLocation = await MapLocationFinder.FindLocationsAtAsync(position.Coordinate.Point);
                if (mapLocation.Status == MapLocationFinderStatus.Success)
                {
                    BasicGeoposition basicGeoposition = new BasicGeoposition();
                    basicGeoposition.Latitude = position.Coordinate.Point.Position.Latitude;
                    basicGeoposition.Longitude = position.Coordinate.Point.Position.Longitude;
                    Geopoint point = new Geopoint(basicGeoposition);
                    CurrentPosition = point;
                    MapIcon mapIcon = new MapIcon()
                    {
                        Location = point,
                        Title = "Me"
                    };
                    MyMap.MapElements.Add(mapIcon);
                    MyMap.Center = point;
                    MapScene mp = MapScene.CreateFromLocationAndRadius(new Geopoint(new BasicGeoposition() { Latitude = point.Position.Latitude, Longitude = point.Position.Longitude }), 800);
                    await MyMap.TrySetSceneAsync(mp);
                }
            }
            catch
            {
                MessageDialog m = new MessageDialog("failed to get your location.\nTurn on your location and try again.");
                await m.ShowAsync();
            }
            MyMap.Opacity = 1;
            MyProgressRing.IsActive = false;
        }

        private async void LocationBtn_Click(object sender, RoutedEventArgs e)
        {
            await GetCurrentLocation();
        }

        private void ThemeBtn_Click(object sender, RoutedEventArgs e)
        {
            //dark icon
            if (MyMap.ColorScheme == MapColorScheme.Light)
            {
                MyMap.ColorScheme = MapColorScheme.Dark;
                BtnContainer.RequestedTheme = ElementTheme.Dark;
            }
            else
            {
                MyMap.ColorScheme = MapColorScheme.Light;
                BtnContainer.RequestedTheme = ElementTheme.Light;
            }

            ThemeBtn.Content = ThemeBtn.Content.ToString() == "" ? "" : "";
        }

        private async void GetDirection(Geopoint startPoint, Geopoint endPoint)
        {
            MapRouteFinderResult routeResult = await MapRouteFinder.GetDrivingRouteAsync(startPoint, endPoint, MapRouteOptimization.Time, MapRouteRestrictions.None);
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Salmon;
                viewOfRoute.OutlineColor = Colors.OrangeRed;
                MyMap.Routes.Add(viewOfRoute);
                await MyMap.TrySetViewBoundsAsync(routeResult.Route.BoundingBox, null, MapAnimationKind.Bow);
            }
        }

        private async void DirectionBtn_Click(object sender, RoutedEventArgs e)
        {
            await GetCurrentLocation();
            if (CurrentPosition != null)
            {
                MyMap.Routes.Clear();
                GetDirection(CurrentPosition, new Geopoint(new BasicGeoposition()
                {
                    Latitude = station.Lat,
                    Longitude = station.Long
                }));
            }
        }

        private void ClearMapBtn_Click(object sender, RoutedEventArgs e)
        {
            MyMap.Routes.Clear();
            MyMap.Center = new Geopoint(new BasicGeoposition()
            {
                Latitude = 35.670235,
                Longitude = 10.882897
            });
            MyMap.ZoomLevel = 10;
        }
    }
}
