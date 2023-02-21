using System;

namespace WeatherApp.Commands
{
    public class LoginCommand : ICommand
    {
        public string Name() => "inloggning";
        public void Execute(App app) => app.ContentController.MoveNext(Content.Login);
    }
}