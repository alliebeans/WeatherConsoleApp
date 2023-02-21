using System;

namespace WeatherApp.Weather.WeatherDescriptionMaps
{
    public class VeryBasicWeatherDescription : IWeatherDescriptionMap
    {
        private Dictionary<int, string> weatherDescriptionMap;
        public Dictionary<int, string> GetWeatherDescriptionMap() => weatherDescriptionMap != null ? weatherDescriptionMap : throw new InvalidOperationException();
        public VeryBasicWeatherDescription()
        {
            weatherDescriptionMap = SetWeatherDescriptionMap();
        }
        public Dictionary<int, string> SetWeatherDescriptionMap()
        {
            return new Dictionary<int, string> 
            {
                {1, "sol"},
                {2, "moln"},
                {3, "sol"},
                {4, "moln"},
                {5, "moln"},
                {6, "moln"},
                {7, "moln"},
                {8, "regn"},
                {9, "regn"},
                {10, "regn"},
                {11, "åska"},
                {12, "regn"},
                {13, "regn"},
                {14, "regn"},
                {15, "snö"},
                {16, "snö"},
                {17, "snö"},
                {18, "regn"},
                {19, "regn"},
                {20, "regn"},
                {21, "åska"},
                {22, "regn"},
                {23, "regn"},
                {24, "regn"},
                {25, "snö"},
                {26, "snö"},
                {27, "snö"},
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