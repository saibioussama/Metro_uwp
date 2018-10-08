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
    public class LinesRepos
    {
        static StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public async static Task<List<DateTime?>> GetTimesOfStation(Station.Directions direction, int stationId)
        {
            List<DateTime?> times = new List<DateTime?>();
            try
            {
                StorageFile sampleFile = await localFolder.GetFileAsync(direction == 0 ? StorageRepos.times_sm : StorageRepos.times_ms);
                string lines_json = await FileIO.ReadTextAsync(sampleFile);
                var lines = JsonConvert.DeserializeObject<List<Line>>(lines_json);
                foreach (var line in lines)
                    if (line.Times[stationId - 1] != null)
                        times.Add(line.Times[stationId - 1]);
            }
            catch
            {

            }
            return  Task.FromResult(times).Result;
        }

        public async static Task<List<DateTime?>> GetAvailableTimesOfStation(Station.Directions direction, int stationId)
        {
            List<DateTime?> times = new List<DateTime?>();
            try
            {
                StorageFile sampleFile = await localFolder.GetFileAsync(direction == 0 ? StorageRepos.times_sm : StorageRepos.times_ms);
                string lines_json = await FileIO.ReadTextAsync(sampleFile);
                var lines = JsonConvert.DeserializeObject<List<Line>>(lines_json);
                foreach (var line in lines)
                    if (line.Times[stationId - 1] != null && line.Times[stationId - 1] > DateTime.Now)
                        times.Add(line.Times[stationId - 1]);
                if (times.Count == 0)
                    foreach (var line in lines)
                        if (line.Times[stationId - 1] != null && line.Times[stationId - 1] < DateTime.Now)
                            times.Add(line.Times[stationId - 1].Value.AddDays(1));
            }
            catch
            {

            }
            return  Task.FromResult(times).Result;
        }
    }
}
