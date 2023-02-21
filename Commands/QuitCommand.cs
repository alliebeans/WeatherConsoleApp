using System;

namespace WeatherApp.Commands
{
    public class QuitCommand : ICommand
    {
        public string Name() => "avsluta";
        public void Execute(App app) => app.RequestQuit();
    }
}