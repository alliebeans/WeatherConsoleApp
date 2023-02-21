using System;

namespace WeatherApp.Weather
{
    public struct Line 
    {
        public Coordinate coordinate1 { get; private set; }
        public Coordinate coordinate2 { get; private set; }
        public Line(Coordinate coordinate1, Coordinate coordinate2)
        {
            this.coordinate1 = coordinate1;
            this.coordinate2 = coordinate2;
        }
    }
}