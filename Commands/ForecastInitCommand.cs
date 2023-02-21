using System;

namespace WeatherApp.Commands
{
    public class ForecastInitCommand : ICommand
    {
        public string Name() => "_forecastInit";
        public void Execute(App app) => app.ContentController.MoveNext(Content.ForecastInit);
    }
}