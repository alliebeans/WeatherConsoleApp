using System;
using System.Text;

namespace WeatherApp.UserAccount
{
    public class LoginFacade
    {
        #region Component fields
        private LoginVerifier loginVerifier;
        public UserAccountCreator UserAccountCreator;
        #endregion

        #region State region
        private string Username;
        public string? GetUsername() => Username;
        public void SetNoUsername() => Username = String.Empty;
        public bool IsSuccess { get; private set; }
        public void SetSuccess(bool state) => IsSuccess = state;
        #endregion

        public LoginFacade(LoginVerifier loginVerifier, UserAccountCreator userAccountCreator)
        {
            this.loginVerifier = loginVerifier;
            this.UserAccountCreator = userAccountCreator;
            IsSuccess = false;
            Username = String.Empty;
        }

        public void Login()
        {
            var username = InputUsername(new StringBuilder());
            Console.WriteLine();
            var password = InputPasswordHash(new StringBuilder());
            return;
        }

        private string InputUsername(StringBuilder stringBuilder)
        {
            while (true)
            {
                Console.Write($"Användarnamn: {stringBuilder.ToString()}");
                var input = Console.ReadKey(true);

                if (input.Key == ConsoleKey.Backspace)
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Remove(stringBuilder.Length-1, 1);
                        Console.WriteLine();
                        Extensions.OverwritePreviousLine();
                        InputUsername(stringBuilder);
                        return "";
                    }

                if (input.Key == ConsoleKey.Enter && stringBuilder.Length > 0)
                {
                    Username = stringBuilder.ToString();
                    return "";
                }
                
                else
                {
                    stringBuilder.Append(input.KeyChar);
                    Console.WriteLine();
                    Extensions.OverwritePreviousLine();
                    InputUsername(stringBuilder);
                    return "";
                }
            }
            throw new InvalidOperationException();
        }

        private string InputPasswordHash(StringBuilder stringBuilder)
        {
            while (stringBuilder.Length <= 8) 
            {
                Console.Write($"Lösenord: {"".PadRight(stringBuilder.Length, '*')}");
                var input = Console.ReadKey(true);

                if (input.Key == ConsoleKey.Backspace)
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Remove(stringBuilder.Length-1, 1);
                        Console.WriteLine();
                        Extensions.OverwritePreviousLine();
                        InputPasswordHash(stringBuilder);
                        return "";
                    }

                if (input.Key == ConsoleKey.Enter && stringBuilder.Length > 0)
                {
                    if (!loginVerifier.VerifyLogin(Username, stringBuilder.ToString()))
                        SetSuccess(false);
                    else
                        SetSuccess(true);

                    return "";
                }
                
                else
                {
                    stringBuilder.Append(input.KeyChar);
                    Console.WriteLine();
                    Extensions.OverwritePreviousLine();
                    InputPasswordHash(stringBuilder);
                    return "";
                }
            }
            throw new InvalidOperationException();
        }
    }
}