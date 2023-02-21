using System;

namespace WeatherApp.Weather
{
    public class ForecastStorage
    {
        public Dictionary<ForecastLocation, ForecastData> StoredForecasts;
        public ForecastStorage()
        {
            StoredForecasts = new Dictionary<ForecastLocation, ForecastData>();
        }

        public void AddToForecastStorage(ForecastLocation forecastLocation, ForecastData forecastData) 
        {
            if (StoredForecasts.ContainsKey(forecastLocation))
                StoredForecasts.Remove(forecastLocation);
            StoredForecasts.Add(forecastLocation, forecastData);
        }
        public ForecastData GetForecastFromStorage(string name) 
        {
            ForecastLocation _forecastLocation;
            ForecastData? _forecastData;

            foreach(ForecastLocation location in StoredForecasts.Keys)
                if (location.Name.ToLower() == name.ToLower())
                {
                    _forecastLocation = location;
                    if (!StoredForecasts.TryGetValue(_forecastLocation, out _forecastData!))
                        throw new ArgumentNullException();
                    return _forecastData;
                }
            throw new InvalidOperationException();
        }

        public ForecastLocation[] GetLocationsHasData() => StoredForecasts.Keys.ToArray<ForecastLocation>();
    }
}