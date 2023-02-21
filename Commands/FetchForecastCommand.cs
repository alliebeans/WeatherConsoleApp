using System;

namespace WeatherApp.Commands
{
    public class FetchForecastCommand : ICommand
    {
        public string Name() => "hämta";
        public void Execute(App app) => app.ContentController.MoveNext(Content.FetchForecast);
    }
}