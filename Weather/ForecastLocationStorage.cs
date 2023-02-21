using System;

namespace WeatherApp.Weather
{
    public class ForecastLocationStorage
    {
        public List<ForecastLocation> StoredForecastLocations;

        public ForecastLocationStorage()
        {
            StoredForecastLocations = new List<ForecastLocation>();
        }

        public void Add(ForecastLocation forecastLocation) 
        {
            StoredForecastLocations.Add(forecastLocation);
            StoredForecastLocations.Sort();
        }

        public ForecastLocation GetLocationFromStorage(string name) => StoredForecastLocations.Find(x => x.Name.ToLower() == name.ToLower());
    }
}