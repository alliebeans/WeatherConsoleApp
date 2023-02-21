using System;

namespace WeatherApp.Weather.WeatherDescriptionMaps
{
    public class BasicWeatherDescription : IWeatherDescriptionMap
    {
        private Dictionary<int, string> weatherDescriptionMap;
        public Dictionary<int, string> GetWeatherDescriptionMap() => weatherDescriptionMap != null ? weatherDescriptionMap : throw new InvalidOperationException();
        public BasicWeatherDescription()
        {
            weatherDescriptionMap = SetWeatherDescriptionMap();
        }
        public Dictionary<int, string> SetWeatherDescriptionMap()
        {
            return new Dictionary<int, string> 
            {
                {1, "Klart"},
                {2, "Lätt molnighet"},
                {3, "Halvklart"},
                {4, "Molnigt"},
                {5, "Molnigt"},
                {6, "Mulet"},
                {7, "Dimma"},
                {8, "Lätt regn"},
                {9, "Regn"},
                {10, "Regn"},
                {11, "Åska"},
                {12, "Lätt snöblandat regn"},
                {13, "Snöblandat regn"},
                {14, "Snöblandat regn"},
                {15, "Lätt snö"},
                {16, "Snö"},
                {17, "Snö"},
                {18, "Lätt regn"},
                {19, "Regn"},
                {20, "Regn"},
                {21, "Åska"},
                {22, "Lätt snöblandat regn"},
                {23, "Snöblandat regn"},
                {24, "Snöblandat regn"},
                {25, "Lätt snö"},
                {26, "Snö"},
                {27, "Snö"},
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