using System;

namespace WeatherApp.Commands
{
    public class InitialiseCommand : ICommand
    {
        public string Name() => "_init";
        public void Execute(App app) => app.ContentController.MoveNext(Content.Initialise);
    }
}