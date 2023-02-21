using System;

namespace WeatherApp.Commands
{
    public class WeatherInitCommand : ICommand
    {
        public string Name() => "_weatherInit";
        public void Execute(App app) => app.ContentController.MoveNext(Content.WeatherInit);
    }
}