using System;

namespace WeatherApp.Commands
{
    public class AddLocationCommand : ICommand
    {
        public string Name() => "ny";
        public void Execute(App app) => app.ContentController.MoveNext(Content.AddLocation);
    }
}