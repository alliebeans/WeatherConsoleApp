using System;

namespace WeatherApp.Commands
{
    public class ChooseForecastCommand : ICommand
    {
        public string Name() => "välj";
        public void Execute(App app) => app.ContentController.MoveNext(Content.ChooseForecast);
    }
}