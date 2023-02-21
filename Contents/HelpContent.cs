using System;

namespace WeatherApp.Contents
{
    public class HelpContent : IContent
    {
        private bool _isIgnoreNextCommand = false;
        public bool IsIgnoreNextCommand() => _isIgnoreNextCommand;
        public void SetIgnoreNextCommand() => _isIgnoreNextCommand = true;
        public string GetTitle(App app) => "Hjälp";
        public string GetStatus(App app) => "";
        public void PrintTitle(string title) => Extensions.PrintTitle(title);
        public void PrintStatus(string status) { throw new NotImplementedException(); }
        #pragma warning disable 1998
        public async Task Print(App app)
        {
            Extensions.PrintTitle("Konto");
            Console.WriteLine("Du behöver ett konto för att hämta väderdata. Du skapar ett nytt konto genom", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("att från denna sidan köra kommandon: tillbaka > konton > nytt", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine();
            Console.ResetColor();
            Extensions.PrintTitle("Väderprognoser");
            Console.WriteLine("Väderdata skrivs ut enligt följande format:", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("[Veckodag]: [väderlek] [°C] [väderbeskrivning] [vindriktning] [vindstyrka]", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine();
            Console.WriteLine("Symboler för väderlek tolkas enligt följande:", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("☼ = sol, ☁ = moln, ☂ = regn, ❄ = snö, ϟ = åska", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine();
            Console.WriteLine("Du behöver vara uppkopplad mot Internet för att hämta väderdata.", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("Väderprognoser hämtas från SMHI:s servrar.", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine();
            Console.ResetColor();
            Extensions.PrintTitle("Kommandon");
            Console.WriteLine("Såhär använder du programmet: skriv namnet på det kommando", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("du vill köra. Tillgängliga kommandon visas i en lista.", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine();
            Console.ResetColor();
        }
        #pragma warning restore 1998
    }
}