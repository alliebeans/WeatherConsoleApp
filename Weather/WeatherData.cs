using System;
using System.Diagnostics.CodeAnalysis;

namespace WeatherApp.Weather
{
    public struct WeatherData : IEqualityComparer<WeatherData>, IComparable<WeatherData>
    {
        public string validTime { get; private set; }
        public double temperatureCelsius { get; private set; }
        public int windDegree { get; private set; }
        public double windStrength { get; private set; }
        public int weatherDescription { get; private set; }

        public WeatherData(string validTime, double temperatureCelsius, int windDegree, double windStrength, int weatherDescription)
        {
            this.validTime = validTime;
            this.temperatureCelsius = temperatureCelsius;
            this.windDegree = windDegree;
            this.windStrength = windStrength;
            this.weatherDescription = weatherDescription;
        }

        public bool Equals(WeatherData x, WeatherData y)
        {
            return (x.validTime, x.temperatureCelsius, x.weatherDescription) 
                == (y.validTime, y.temperatureCelsius, y.weatherDescription);
        }

        public int GetHashCode([DisallowNull] WeatherData obj)
        {
            return 7 + 15 * obj.validTime.GetHashCode() 
                + 7 * obj.temperatureCelsius.GetHashCode() 
                + 15 * obj.weatherDescription.GetHashCode();
        }

        public int CompareTo(WeatherData other)
        {
            if (String.IsNullOrEmpty(other.validTime))
                return 1;
            
            return DateTime.Parse(this.validTime).CompareTo(DateTime.Parse(other.validTime));
        }
    }
}