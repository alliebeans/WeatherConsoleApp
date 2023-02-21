using System;

namespace WeatherApp.Commands
{
    public class CreateNewAccountCommand : ICommand
    {
        public string Name() => "nytt";
        public void Execute(App app) => app.ContentController.MoveNext(Content.CreateNewAccount);
    }
}