using System;
using System.Globalization;

namespace WeatherApp
{
    public class Extensions
    {
        #region Date parsing methods
        private static CultureInfo Culture = new CultureInfo("sv-SE");
        public static string GetAbbreviatedDay(string smhiDateFormat)
        {
            var day = Culture.DateTimeFormat.GetDayName(DateTime.Parse(smhiDateFormat).DayOfWeek);
            return $"{day[..3].ToUpper()}";
        }

        public static string GetDateAndMonth(string smhiDateFormat)
        {
            var date = DateTime.Parse(smhiDateFormat).Day;
            var month = DateTime.Parse(smhiDateFormat).Month;
            return $"{date}/{month}";
        }

        public static string GetToday()
        {
            var date = DateTime.Now.ToString("dddd dd MMMM", Culture);
            return $"{String.Concat(date.ToUpper().ToString()[0], date.Substring(1))}";
        }
        #endregion
        
        public static void OverwritePreviousLine()
        {
            var cursorPosition = Console.CursorTop;
            Console.SetCursorPosition(0, cursorPosition-1);
            Console.Write("".PadRight(Console.WindowWidth));
            Console.SetCursorPosition(0, cursorPosition-1);
        }

        public static void Printer(Action<string> printer, string content)
        {
            printer.Invoke(content);
        }

        public static void PrintAppTitle(string username)
        {
            var AppName = "Väderprognos";
            var space1 = 1;
            var usernameSpace = 10 - username.Length;
            var space2 = 3;
            var today = Extensions.GetToday();
            var padLength = AppName.Length + space1 + username.Length + usernameSpace + space2 + today.Length;

            if (username.Length > 0)
                usernameSpace -= 2;

            Console.WriteLine($"{AppName}{"".PadRight(space1)}{PrintIfName(username)}{"".PadRight(usernameSpace)}{"".PadRight(space2)}{today}");
            Console.WriteLine("".PadRight(padLength, '='));
        }

        private static string PrintIfName(string username)
        {
            if (username.Length > 0)
                return $"({username})";
            return "";
        }

        public static void PrintLicenseInformation() 
        {
            Console.WriteLine("Användande av väderdata från SMHI i enlighet med Creative Commons BY 4.0 SE:\nhttps://www.smhi.se/data/oppna-data/information-om-oppna-data/villkor-for-anvandning-1.30622");
        }

        public static void PrintTitle(string title)
        {
            Console.WriteLine(title);
            Console.WriteLine("".PadRight(title.Length, '='));
            Console.WriteLine();
        }

        public static void PrintConfirmation(App app, string message, ICommand affirmation, ICommand negation)
        {
            Console.WriteLine($"{message} (j/n)");
            var input = Console.ReadKey(true);
            if (input.KeyChar == 'j')
            {
                app.CommandController.CurrentCommand = app.CommandController.GetCommand(affirmation);
                app.ContentController.CurrentContent!.SetIgnoreNextCommand();
                return;
            }
            else
            {
                app.CommandController.CurrentCommand = app.CommandController.GetCommand(negation);
                app.ContentController.CurrentContent!.SetIgnoreNextCommand();
                return;
            }
        }
    }
}