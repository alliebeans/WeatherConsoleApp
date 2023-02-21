using System;

namespace WeatherApp.Weather
{
    public interface IWindSymbolMap
    {
        Dictionary<int, string> SetWindDegreeMap();
        Dictionary<int, string> GetWindDegreeMap();
        int GetCardinalDirection(int windDegree);
        string GetWindSymbol(int windDegree);
    }
}