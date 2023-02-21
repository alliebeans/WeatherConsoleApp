using System;

namespace WeatherApp.Commands
{
    public class FetchWeatherCommand : ICommand
    {
        public string Name() => "väder";
        public void Execute(App app) => app.ContentController.MoveNext(Content.FetchWeather);
    }
}