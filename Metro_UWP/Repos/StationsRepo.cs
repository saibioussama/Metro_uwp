using Metro_UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Metro_UWP.Repos
{
    public class StationsRepo
    {
        static StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public async static Task<List<Station>> GetStations(Station.Directions direction)
        {
            try
            {
                StorageFile sampleFile = await localFolder.GetFileAsync(direction == Station.Directions.SM ? StorageRepos.stations_sm : StorageRepos.stations_ms);
                string stations_json = await FileIO.ReadTextAsync(sampleFile);
                return Task.FromResult(JsonConvert.DeserializeObject<List<Station>>(stations_json)).Result;
            }
            catch
            {
                return null;
            }
        }

        public static Station GetStation(Station.Directions direction, int StationId)
        {
            return  GetStations(direction).Result.SingleOrDefault(s => s.Id == StationId);
        }
    }
}
