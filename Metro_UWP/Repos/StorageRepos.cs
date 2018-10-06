using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Metro_UWP.Repos
{
    public class StorageRepos
    {
        public static string times_ms = nameof(times_ms);
        public static string times_sm = nameof(times_sm);
        public static string stations_ms = nameof(stations_ms);
        public static string stations_sm = nameof(stations_sm);

        public static bool IsDataExist() => !(Windows.Storage.ApplicationData.Current.RoamingSettings.Values[nameof(StorageRepos.times_ms)] == null ||
                Windows.Storage.ApplicationData.Current.RoamingSettings.Values[nameof(StorageRepos.times_sm)] == null ||
                Windows.Storage.ApplicationData.Current.RoamingSettings.Values[nameof(StorageRepos.stations_ms)] == null ||
                Windows.Storage.ApplicationData.Current.RoamingSettings.Values[nameof(StorageRepos.stations_sm)] == null);

        public static async Task GetData()
        {
            try
            {
                HttpClient client = new HttpClient();
                var times_ms = await client.GetStringAsync("http://metroapps.azurewebsites.net/data/times_ms.json");
                var times_sm = await client.GetStringAsync("http://metroapps.azurewebsites.net/data/times_sm.json");
                var stations_ms = await client.GetStringAsync("http://metroapps.azurewebsites.net/data/stations_ms.json");
                var stations_sm = await client.GetStringAsync("http://metroapps.azurewebsites.net/data/stations_sm.json");
                Windows.Storage.ApplicationData.Current.RoamingSettings.Values[nameof(StorageRepos.times_ms)] = times_ms;
                Windows.Storage.ApplicationData.Current.RoamingSettings.Values[nameof(StorageRepos.times_sm)] = times_sm;
                Windows.Storage.ApplicationData.Current.RoamingSettings.Values[nameof(StorageRepos.stations_ms)] = stations_ms;
                Windows.Storage.ApplicationData.Current.RoamingSettings.Values[nameof(StorageRepos.stations_sm)] = stations_sm;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
