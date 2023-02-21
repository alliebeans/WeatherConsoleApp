using System;

namespace WeatherApp.Contents
{
    public class LoginContent : IContent
    {
        private bool _isIgnoreNextCommand = false;
        public bool IsIgnoreNextCommand() => _isIgnoreNextCommand;
        public void SetIgnoreNextCommand() => _isIgnoreNextCommand = true;
        public string GetTitle(App app) => "Logga in";
        public string GetStatus(App app) => "";
        public void PrintTitle(string title) => Extensions.PrintTitle(title);
        public void PrintStatus(string status) { throw new NotImplementedException(); }
        #pragma warning disable 1998
        public async Task Print(App app)
        {
            if (app.UserLoginNameStorage.StoredUserLoginNames.Count == 0)
            {
                Console.WriteLine("Inget konto associerat med denna sessionen hittades...", Console.ForegroundColor = ConsoleColor.Yellow);
                Console.ResetColor();
                Console.WriteLine();
                Extensions.PrintConfirmation(app, "Vill du skapa ett nytt konto?", app.CommandController.CreateNewAccountCommand, app.CommandController.InitialiseCommand);
                return;
            }

            app.LoginFacade.Login();
            while (!app.LoginFacade.IsSuccess)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Inloggningen misslyckades, användarnamnet eller lösenordet är felaktigt.", Console.ForegroundColor = ConsoleColor.Yellow);
                Console.ResetColor();
                Console.WriteLine();
                Extensions.PrintConfirmation(app, "Vill du prova igen?", app.CommandController.LoginCommand, app.CommandController.InitialiseCommand);
                return;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Välkommen {app.LoginFacade.GetUsername()}!", Console.ForegroundColor = ConsoleColor.Green);
            Thread.Sleep(1500);
            Console.ResetColor();
            
            app.CommandController.CurrentCommand = app.CommandController.ForecastInitCommand;
            SetIgnoreNextCommand();
            return;
        }
        #pragma warning restore 1998
    }
}