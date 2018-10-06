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
        public List<DateTime?> GetTimesOfStation(Station.Directions direction, int stationId)
        {
            List<DateTime?> times = new List<DateTime?>();
            if (ApplicationData.Current.RoamingSettings.Values[direction == 0 ? StorageRepos.times_sm : StorageRepos.times_ms] != null)
            {
                string lines_json = ApplicationData.Current.RoamingSettings.Values[direction == 0 ? StorageRepos.times_sm : StorageRepos.times_ms].ToString();
                var lines = JsonConvert.DeserializeObject<List<Line>>(lines_json);
                foreach (var line in lines)
                    if (line.Times[stationId - 1] != null)
                        times.Add(line.Times[stationId - 1]);
            }
            return times;
        }

        public List<DateTime?> GetAvailableTimesOfStation(Station.Directions direction, int stationId)
        {
            List<DateTime?> times = new List<DateTime?>();
            if (ApplicationData.Current.RoamingSettings.Values[direction == 0 ? StorageRepos.times_sm : StorageRepos.times_ms] != null)
            {
                string lines_json = ApplicationData.Current.RoamingSettings.Values[direction == 0 ? StorageRepos.times_sm : StorageRepos.times_ms].ToString();
                var lines = JsonConvert.DeserializeObject<List<Line>>(lines_json);
                foreach (var line in lines)
                    if (line.Times[stationId - 1] != null && line.Times[stationId - 1] > DateTime.Now)
                        times.Add(line.Times[stationId - 1]);
                if(times.Count == 0)
                    foreach (var line in lines)
                        if (line.Times[stationId - 1] != null && line.Times[stationId - 1] < DateTime.Now)
                            times.Add(line.Times[stationId - 1]);
            }
            return times;
        }
    }
}
