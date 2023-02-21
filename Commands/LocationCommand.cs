using System;

namespace WeatherApp.Commands
{
    public class LocationCommand : ICommand
    {
        public string Name() => "plats";
        public void Execute(App app) => app.ContentController.MoveNext(Content.Location);
    }
}