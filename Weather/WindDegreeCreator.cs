using System;
using Maps = WeatherApp.Weather.WindDegreeSymbolMaps;

namespace WeatherApp.Weather
{
    public class WindSymbolCreator
    {
        #region WindSymbolMaps
        public IWindSymbolMap ArrowSymbolMap;
        #endregion

        public WindSymbolCreator()
        {
            ArrowSymbolMap = new Maps::ArrowWindSymbol();
        }

        public string GetWindSymbol(IWindSymbolMap map, int windDegree) => map.GetWindSymbol(windDegree);
    }
}