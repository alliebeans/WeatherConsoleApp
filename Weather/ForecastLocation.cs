using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace WeatherApp.Weather
{
    public struct ForecastLocation : IEqualityComparer<ForecastLocation>, IComparable<ForecastLocation>
    {
        private string name;
        private double longitude;
        private double latitude;
        public Coordinate Coordinate { get; private set; }
        public ForecastLocation(string name, double longitude, double latitude)
        {
            if (longitude > 24.955109 ||
                longitude < 3.862167 ||
                latitude > 69.473256 ||
                latitude < 54.658264)
                throw new InvalidOperationException("This location is not supported.");

            this.name = name;
            this.longitude = longitude;
            this.latitude = latitude;
            Coordinate = new Coordinate(longitude, latitude);
        }
        public string Name => name;
        public string GetLongitude()
        {
            if (longitude.ToString(CultureInfo.InvariantCulture).Split(".", 2)[1].Length > 6)
                return String.Concat(longitude.ToString(CultureInfo.InvariantCulture).Split(".", 2)[0], ".", longitude.ToString(CultureInfo.InvariantCulture).Split(".", 2)[1].Remove(6));

            return longitude.ToString();
        }
        public string GetLatitude()
        { 
            if (latitude.ToString(CultureInfo.InvariantCulture).Split(".", 2)[1].Length > 6)
                return String.Concat(latitude.ToString(CultureInfo.InvariantCulture).Split(".", 2)[0], ".", latitude.ToString(CultureInfo.InvariantCulture).Split(".", 2)[1].Remove(6));

            return latitude.ToString();
        }

        public bool Equals(ForecastLocation x, ForecastLocation y)
        {
            return (x.GetLatitude(), x.GetLongitude()) 
                == (y.GetLatitude(), y.GetLongitude());
        }

        public int GetHashCode([DisallowNull] ForecastLocation obj)
        {
            return 7 + 15 * obj.GetLatitude().GetHashCode() 
                + 7 * obj.GetLongitude().GetHashCode();
        }
        public string GetUri() => $"https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/{GetLongitude()}/lat/{GetLatitude()}/data.json";

        public int CompareTo(ForecastLocation other)
        {
            if (String.IsNullOrEmpty(other.name))
                return 1;
            
            return this.name.CompareTo(other.name);
        }
    }
}