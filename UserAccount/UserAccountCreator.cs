using System;
using System.Text;
using System.Security.Cryptography;
using Isopoh.Cryptography.Argon2;

namespace WeatherApp.UserAccount
{
    public class UserAccountCreator
    {
        REMOTE_DATABASE.UserLoginNameStorage names;
        REMOTE_DATABASE.PasswordHashStorage passwords;
        
        public UserAccountCreator(REMOTE_DATABASE.UserLoginNameStorage names, REMOTE_DATABASE.PasswordHashStorage passwords)
        {
            this.names = names;
            this.passwords = passwords;
        }
        public void CreateAccount()
        {
            GetLoginData();
        }

        private void GetLoginData()
        {
            var userAccountID = String.Concat(names.StoredUserLoginNames.Count, RandomNumberGenerator.GetBytes(17-names.Count).ToString());
            var userName = GetLoginName(new StringBuilder(), userAccountID);
            Console.WriteLine();
            var password = GetPasswordHash(new StringBuilder(), userAccountID);

            return;
        }

        private string GetLoginName(StringBuilder stringBuilder, string? userAccountID)
        {
            while (stringBuilder.Length <= 8) 
            {
                Console.Write($"Användarnamn: {stringBuilder.ToString()}");
                var input = Console.ReadKey(true);

                if (input.Key == ConsoleKey.Backspace)
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Remove(stringBuilder.Length-1, 1);
                        Console.WriteLine();
                        Extensions.OverwritePreviousLine();
                        GetLoginName(stringBuilder, userAccountID);
                        return "";
                    }

                if (input.Key == ConsoleKey.Enter && stringBuilder.Length > 0)
                {
                    names.StoredUserLoginNames.Add(stringBuilder.ToString(), userAccountID!);
                    return stringBuilder.ToString();
                }
                
                if (InputIsValid(stringBuilder, input, "Användarnamnet"))
                {
                    Console.WriteLine();
                    Extensions.OverwritePreviousLine();
                    GetLoginName(stringBuilder, userAccountID);
                    return "";
                }
            }
            throw new InvalidOperationException();
        }

        private string GetPasswordHash(StringBuilder stringBuilder, string? userAccountID)
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
                        GetPasswordHash(stringBuilder, userAccountID);
                        return "";
                    }

                if (input.Key == ConsoleKey.Enter && stringBuilder.Length > 0)
                {
                    var hash = Argon2.Hash(stringBuilder.ToString());
                    passwords.StoredPasswordHashes.Add(userAccountID!, hash);
                    return "";
                }
                
                if (InputIsValid(stringBuilder, input, "Lösenordet"))
                {
                    Console.WriteLine();
                    Extensions.OverwritePreviousLine();
                    GetPasswordHash(stringBuilder, userAccountID);
                    return "";
                }
            }
            throw new InvalidOperationException();
        }

        #region Helper methods
        private bool InputIsValid(StringBuilder stringBuilder, ConsoleKeyInfo input, string inputType)
        {
            if (!Char.IsAsciiLetterOrDigit(input.KeyChar))
            {
                PrintInvalidInput(InvalidInputException.NotAsciiLetterOrDigit, inputType);
                stringBuilder.Clear();
                return false;
            }
            else 
            {
                stringBuilder.Append(input.KeyChar);

                if (stringBuilder.Length > 8)
                {
                    PrintInvalidInput(InvalidInputException.ExceedsMaxLength, inputType);
                    stringBuilder.Clear();
                    return false;
                }
                return true;
            }
        }

        private void PrintInvalidInput(InvalidInputException exception, string inputType)
        {
            var message = exception switch 
            {
                InvalidInputException.NotAsciiLetterOrDigit => $" {inputType} får endast bestå av bokstäver eller siffror.",
                InvalidInputException.ExceedsMaxLength => $" {inputType} får max bestå av 8 tecken.",
                _ => throw new InvalidOperationException(),
            };
            Console.WriteLine(message, Console.ForegroundColor = ConsoleColor.Red);
            Console.ResetColor();
            Thread.Sleep(1500);
            Extensions.OverwritePreviousLine();
            return;
        }
        #endregion
    }
}