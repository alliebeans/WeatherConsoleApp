using System;

namespace WeatherApp.Weather
{
    public interface IWeatherDescriptionMap
    {
        Dictionary<int, string> SetWeatherDescriptionMap();
        Dictionary<int, string> GetWeatherDescriptionMap();
        string GetWeatherDescription(int Wsymb2);
    }
}