using Metro_UWP.Models;
using Newtonsoft.Json;
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
        public static readonly string times_ms = nameof(times_ms);
        public static readonly string times_sm = nameof(times_sm);
        public static readonly string stations_ms = nameof(stations_ms);
        public static readonly string stations_sm = nameof(stations_sm);
        public static readonly string LastUpdateAt = nameof(LastUpdateAt);

        public static async Task GetData()
        {

            HttpClient client = new HttpClient();
            var times_ms_json = await client.GetStringAsync("http://metroapps.azurewebsites.net/data/times_ms.json");
            var times_sm_json = await client.GetStringAsync("http://metroapps.azurewebsites.net/data/times_sm.json");
            var stations_ms_json = await client.GetStringAsync("http://metroapps.azurewebsites.net/data/stations_ms.json");
            var stations_sm_json = await client.GetStringAsync("http://metroapps.azurewebsites.net/data/stations_sm.json");

            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            StorageFile sampleFile_times_ms = await localFolder.CreateFileAsync(StorageRepos.times_ms,
                CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile_times_ms, times_ms_json);

            StorageFile sampleFile_times_sm = await localFolder.CreateFileAsync(StorageRepos.times_sm,
                CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile_times_sm, times_sm_json);

            StorageFile sampleFile_stations_ms = await localFolder.CreateFileAsync(StorageRepos.stations_ms,
                CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile_stations_ms, stations_ms_json);

            StorageFile sampleFile_stations_sm = await localFolder.CreateFileAsync(StorageRepos.stations_sm,
                CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile_stations_sm, stations_sm_json);

            ApplicationData.Current.RoamingSettings.Values[StorageRepos.LastUpdateAt] = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        public static async Task ClearData()
        {

            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            StorageFile sampleFile_times_ms = await localFolder.CreateFileAsync(StorageRepos.times_ms,
                CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile_times_ms, "");

            StorageFile sampleFile_times_sm = await localFolder.CreateFileAsync(StorageRepos.times_sm,
                CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile_times_sm, "");

            StorageFile sampleFile_stations_ms = await localFolder.CreateFileAsync(StorageRepos.stations_ms,
                CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile_stations_ms, "");

            StorageFile sampleFile_stations_sm = await localFolder.CreateFileAsync(StorageRepos.stations_sm,
                CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile_stations_sm, "");

            //ApplicationData.Current.RoamingSettings.Values[StorageRepos.LastUpdateAt] = DateTime.Now;
        }

    }
}
