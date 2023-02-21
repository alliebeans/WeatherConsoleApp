using System;

namespace WeatherApp.Weather
{
    public class CheckForecastBoundaries
    {
        public List<Coordinate> SMHISupportedForecastArea = new List<Coordinate>
        {
            new Coordinate(2.250475, 52.500440),
            new Coordinate(27.348870, 52.547483),
            new Coordinate(37.848053, 70.740996),
            new Coordinate(-8.541278, 70.655722)
        };

        public bool ForecastLocationIsSupported(Coordinate coordinate)
        {
            throw new NotImplementedException();
        }

        #region Helper methods
        

        #endregion
    }
}