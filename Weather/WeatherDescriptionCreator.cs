using System;
using Maps = WeatherApp.Weather.WeatherDescriptionMaps;

namespace WeatherApp.Weather
{
    public class WeatherDescriptionCreator
    {
        #region WeatherDescriptionMaps
        public IWeatherDescriptionMap SMHIMap;
        public IWeatherDescriptionMap IconMap;
        public IWeatherDescriptionMap VeryBasicWeatherDescription;
        #endregion

        public WeatherDescriptionCreator()
        {
            SMHIMap = new Maps::SMHIWeatherDescription();
            IconMap = new Maps::IconWeatherDescription();
            VeryBasicWeatherDescription = new Maps::VeryBasicWeatherDescription();
        }

        public string GetDescription(IWeatherDescriptionMap map, int Wsymb2) => map.GetWeatherDescription(Wsymb2);
    }
}