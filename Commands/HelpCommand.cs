using System;

namespace WeatherApp.Commands
{
    public class HelpCommand : ICommand
    {
        public string Name() => "hjälp";
        public void Execute(App app) => app.ContentController.MoveNext(Content.Help);
    }
}