using System;

namespace WeatherApp.Weather.WeatherDescriptionMaps
{
    public class SMHIWeatherDescription : IWeatherDescriptionMap
    {
        private Dictionary<int, string> weatherDescriptionMap;
        public Dictionary<int, string> GetWeatherDescriptionMap() => weatherDescriptionMap != null ? weatherDescriptionMap : throw new InvalidOperationException();
        public SMHIWeatherDescription()
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
                {5, "Mycket moln"},
                {6, "Mulet"},
                {7, "Dimma"},
                {8, "Lätt regnskur"},
                {9, "Regnskur"},
                {10, "Kraftig regnskur"},
                {11, "Åskskur"},
                {12, "Lätt by av regn och snö"},
                {13, "By av regn och snö"},
                {14, "Kraftig by av regn och snö"},
                {15, "Lätt snöby"},
                {16, "Snöby"},
                {17, "Kraftig snöby"},
                {18, "Lätt regn"},
                {19, "Regn"},
                {20, "Kraftigt regn"},
                {21, "Åska"},
                {22, "Lätt snöblandat regn"},
                {23, "Snöblandat regn"},
                {24, "Kraftigt snöblandat regn"},
                {25, "Lätt snöfall"},
                {26, "Snöfall"},
                {27, "Ymnigt snöfall"},
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