using System;

namespace WeatherApp.Commands
{
    public class AccountCommand : ICommand
    {
        public string Name() => "konton";
        public void Execute(App app) => app.ContentController.MoveNext(Content.Account);
    }
}