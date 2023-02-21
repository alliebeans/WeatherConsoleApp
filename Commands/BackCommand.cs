using System;

namespace WeatherApp.Commands
{
    public class BackCommand : ICommand
    {
        public string Name() => "tillbaka";
        public void Execute(App app) => app.ContentController.MoveNext(Content.Back);
    }
}