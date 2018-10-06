using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Metro_UWP.Repos
{
    public class StorageRepos
    {
        public static string times_ms = nameof(times_ms);
        public static string times_sm = nameof(times_sm);
        public static string stations_ms = nameof(stations_ms);
        public static string stations_sm = nameof(stations_sm);


        public static bool IsDataExist() => false;

        public static async Task GetData()
        {
            try
            {
                HttpClient client = new HttpClient();
                var times_ms = await client.GetStringAsync("http://metroapps.azurewebsites.net/data/times_ms.json");
                var times_sm = await client.GetStringAsync("http://metroapps.azurewebsites.net/data/times_sm.json");
                var stations_ms = await client.GetStringAsync("http://metroapps.azurewebsites.net/data/stations_ms.json");
                var stations_sm = await client.GetStringAsync("http://metroapps.azurewebsites.net/data/stations_sm.json");

                Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;


                StorageFile sampleFile_times_ms = await localFolder.CreateFileAsync(StorageRepos.times_ms,
                    CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(sampleFile_times_ms, times_ms);

                StorageFile sampleFile_times_sm = await localFolder.CreateFileAsync(StorageRepos.times_sm,
                    CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(sampleFile_times_sm, times_sm);

                StorageFile sampleFile_stations_ms = await localFolder.CreateFileAsync(StorageRepos.stations_ms,
                    CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(sampleFile_stations_ms, stations_ms);

                StorageFile sampleFile_stations_sm = await localFolder.CreateFileAsync(StorageRepos.stations_sm,
                    CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(sampleFile_stations_sm, stations_sm);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
