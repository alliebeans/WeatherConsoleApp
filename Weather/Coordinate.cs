using System;

namespace WeatherApp.Weather
{
    public struct Coordinate
    {
        public double longitude { get; private set; }
        public double latitude { get; private set; }
        public Coordinate(double longitude, double latitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
        }
    }
}