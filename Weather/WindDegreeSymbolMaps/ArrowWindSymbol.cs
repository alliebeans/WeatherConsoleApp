using System;

namespace WeatherApp.Weather.WindDegreeSymbolMaps
{
    public class ArrowWindSymbol : IWindSymbolMap
    {
        private Dictionary<int, string> windDegreeMap;
        public Dictionary<int, string> GetWindDegreeMap() => windDegreeMap != null ? windDegreeMap : throw new InvalidOperationException();
        public ArrowWindSymbol()
        {
            windDegreeMap = SetWindDegreeMap();
        }
        public Dictionary<int, string> SetWindDegreeMap()
        {
            return new Dictionary<int, string> 
            {
                {1, "🡑"},
                {2, "🡕"},
                {3, "🡒"},
                {4, "🡖"},
                {5, "🡓"},
                {6, "🡗"},
                {7, "🡐"},
                {8, "🡔"}
            };
        }
        public string GetWindSymbol(int windDegree)
        {
            int _cardinalDirection = GetCardinalDirection(windDegree);
            string _windDegree;
            if (!windDegreeMap.TryGetValue(_cardinalDirection, out _windDegree!))
                throw new InvalidOperationException();
            return _windDegree;
        }
        public int GetCardinalDirection(int windDegree)
        {
            if (windDegree > 360 || windDegree < 0)
                throw new InvalidOperationException();

            if (windDegree >= 0 && windDegree <= 22 ||
                windDegree >= 338 && windDegree <= 360)
                return 1;
            else if (windDegree >= 23 && windDegree <= 67)
                return 2;
            else if (windDegree >= 68 && windDegree <= 112)
                return 3;
            else if (windDegree >= 113 && windDegree <= 157)
                return 4;
            else if (windDegree >= 158 && windDegree <= 202)
                return 5;
            else if (windDegree >= 203 && windDegree <= 247)
                return 6;
            else if (windDegree >= 248 && windDegree <= 292)
                return 7;
            else if (windDegree >= 293 && windDegree <= 337)
                return 8;
            else
                throw new InvalidOperationException();
        }
    }
}