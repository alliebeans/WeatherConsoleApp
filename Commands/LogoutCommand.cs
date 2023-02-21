using System;

namespace WeatherApp.Commands
{
    public class LogoutCommand : ICommand
    {
        public string Name() => "utloggning";
        public void Execute(App app) => app.ContentController.MoveNext(Content.Logout);
    }
}