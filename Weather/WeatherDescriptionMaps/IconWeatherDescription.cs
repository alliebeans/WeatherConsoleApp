using System;

namespace WeatherApp.Weather.WeatherDescriptionMaps
{
    public class IconWeatherDescription : IWeatherDescriptionMap
    {
        private Dictionary<int, string> weatherDescriptionMap;
        public Dictionary<int, string> GetWeatherDescriptionMap() => weatherDescriptionMap != null ? weatherDescriptionMap : throw new InvalidOperationException();
        public IconWeatherDescription()
        {
            weatherDescriptionMap = SetWeatherDescriptionMap();
        }
        public Dictionary<int, string> SetWeatherDescriptionMap()
        {
            return new Dictionary<int, string> 
            {
                {1, "☼"},
                {2, "☁"},
                {3, "☼"},
                {4, "☁"},
                {5, "☁"},
                {6, "☁"},
                {7, "☁"},
                {8, "☂"},
                {9, "☂"},
                {10, "☂"},
                {11, "ϟ"},
                {12, "☂"},
                {13, "☂"},
                {14, "☂"},
                {15, "❄"},
                {16, "❄"},
                {17, "❄"},
                {18, "☂"},
                {19, "☂"},
                {20, "☂"},
                {21, "ϟ"},
                {22, "☂"},
                {23, "☂"},
                {24, "☂"},
                {25, "❄"},
                {26, "❄"},
                {27, "❄"},
            };
        }
        public string GetWeatherDescription(int Wsymb2)
        {
            string _weatherDescription;
            if (!weatherDescriptionMap.TryGetValue(Wsymb2, out _weatherDescription!))
                throw new InvalidOperationException();
            return _weatherDescription;
        }
    }
}