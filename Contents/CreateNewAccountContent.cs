using System;

namespace WeatherApp.Contents
{
    public class CreateNewAccountContent : IContent
    {
        private bool _isIgnoreNextCommand = false;
        public bool IsIgnoreNextCommand() => _isIgnoreNextCommand;
        public void SetIgnoreNextCommand() => _isIgnoreNextCommand = true;
        public string GetTitle(App app) => "Skapa nytt konto";
        public string GetStatus(App app) => "";
        public void PrintTitle(string title) => Extensions.PrintTitle(title);
        public void PrintStatus(string status) { throw new NotImplementedException(); }
        #pragma warning disable 1998
        public async Task Print(App app)
        {
            Console.WriteLine("För att ta del av väderprognoserna behöver du skapa ett konto. Praktiskt!");
            Console.WriteLine();
            Console.WriteLine("Användarnamnet och lösenordet ska bestå av bokstäver (a-z)", Console.ForegroundColor = ConsoleColor.Gray);
            Console.WriteLine("eller siffror och får max innehålla 8 tecken.", Console.ForegroundColor = ConsoleColor.Gray);
            Console.WriteLine();
            Console.ResetColor();
            app.LoginFacade.UserAccountCreator.CreateAccount();
            Console.WriteLine();
            Console.WriteLine();
            Extensions.PrintConfirmation(app, "Ditt konto är skapat! Vill du logga in nu?", app.CommandController.LoginCommand, app.CommandController.AccountCommand);
            return;
        }
        #pragma warning restore 1998
    }
}