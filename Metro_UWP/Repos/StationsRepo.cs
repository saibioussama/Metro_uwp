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
        public static List<Station> GetStations(Station.Directions direction)
        {
            if (ApplicationData.Current.RoamingSettings.Values[direction == 0 ? StorageRepos.stations_sm : StorageRepos.stations_ms] != null)
            {
                string stations_json = ApplicationData.Current.RoamingSettings.Values[direction == 0 ? StorageRepos.stations_sm : StorageRepos.stations_ms].ToString();
                return JsonConvert.DeserializeObject<List<Station>>(stations_json);
            }
            return null;
        }

        public static Station GetStation(Station.Directions direction,int StationId)
        {
            return GetStations(direction).SingleOrDefault(s => s.Id == StationId);
        }
    }
}
