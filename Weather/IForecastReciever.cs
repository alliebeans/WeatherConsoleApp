using System;

namespace WeatherApp.Weather
{
    public interface IForecastReciever
    {
        ForecastLocation GetLocationFromStorage(string name);
        Task GetNextForecastData();
        void SetForecastLocation(ForecastLocation forecastLocation);
        void SetForecastData(ForecastData forecastData);
        ForecastLocation? UserGetForecastLocation();
        ForecastLocation? GetCurrentForecastLocation();
        ForecastData? GetCurrentForecastData();
        Task PrintForecast();
        void SetCurrentMatchWeather(string weather);
        string? GetCurrentMatchWeather();
        Task PrintAllMatchingWeather(string veryBasicWeatherDescription);
        ForecastData GetForecastFromStorage(string name);
        ForecastLocation[] GetStoredLocationsHasData();
        ForecastLocation? UserGetForecastLocationHasData();
        ForecastLocation CreateForecastLocation(string name, double longitude, double latitude);
        void AddForecastLocation(ForecastLocation location);
    }
}