using System;

namespace WeatherApp.Weather
{
    public class ForecastVirtualProxy : IForecastReciever
    {
        ForecastFacade forecastFacade;

        #region Facade methods
        public ForecastLocation GetLocationFromStorage(string name) => forecastFacade.GetLocationFromStorage(name);
        public void SetForecastLocation(ForecastLocation forecastLocation) => forecastFacade.SetForecastLocation(forecastLocation);
        public async Task GetNextForecastData() => await forecastFacade.GetNextForecastData();
        public void SetForecastData(ForecastData forecastData) => forecastFacade.SetForecastData(forecastData);
        public ForecastLocation? UserGetForecastLocation() => forecastFacade.UserGetForecastLocation();
        public ForecastLocation? GetCurrentForecastLocation() => forecastFacade.GetCurrentForecastLocation();
        public ForecastData? GetCurrentForecastData() => forecastFacade.GetCurrentForecastData();
        public void SetCurrentMatchWeather(string weather) => forecastFacade.SetCurrentMatchWeather(weather);
        public string? GetCurrentMatchWeather() => forecastFacade.GetCurrentMatchWeather();
        public ForecastData GetForecastFromStorage(string name) => forecastFacade.GetForecastFromStorage(name);
        public ForecastLocation[] GetStoredLocationsHasData() => forecastFacade.GetStoredLocationsHasData();
        public ForecastLocation? UserGetForecastLocationHasData() => forecastFacade.UserGetForecastLocationHasData();
        public ForecastLocation CreateForecastLocation(string name, double longitude, double latitude) => forecastFacade.CreateForecastLocation(name, longitude, latitude);
        public void AddForecastLocation(ForecastLocation location) => forecastFacade.AddForecastLocation(location);
        #endregion

        public ForecastVirtualProxy(IForecastReciever forecastFacade)
        {
            this.forecastFacade = (ForecastFacade)forecastFacade;
        }

        public async Task PrintForecast() 
        {
            if (forecastFacade.GetCurrentForecastLocation() == null)
            {
                PrintNoLocation();
                return;
            }
            PrintFetchingForecast();

            await GetNextForecastData();
            
            Extensions.OverwritePreviousLine();
            
            if (forecastFacade.GetCurrentForecastData() != null)
            {
                await forecastFacade.PrintForecast();
            }
        }

        public async Task PrintAllMatchingWeather(string veryBasicWeatherDescription) 
        {
            await forecastFacade.PrintAllMatchingWeather(veryBasicWeatherDescription);   
        }

        #region Helper methods
        private void PrintNoLocation()
        {
            Console.WriteLine($"Ingen plats vald.");
            return;
        }

        private void PrintFetchingForecast()
        {
            Console.WriteLine($"Hämtar data från SMHI...");
            return;
        }
        #endregion
    }
}