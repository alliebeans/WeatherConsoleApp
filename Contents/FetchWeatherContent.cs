using System;
using WeatherApp.Weather;

namespace WeatherApp.Contents
{
    public class FetchWeatherContent : IContent
    {
        private bool _isIgnoreNextCommand = false;
        public bool IsIgnoreNextCommand() => _isIgnoreNextCommand;
        public void SetIgnoreNextCommand() => _isIgnoreNextCommand = true;
        public string GetTitle(App app) => "Sök efter specifik väderlek";
        public string GetStatus(App app) => "";
        public void PrintTitle(string title) => Extensions.PrintTitle(title);
        public void PrintStatus(string status) { throw new NotImplementedException(); }
        #pragma warning disable 1998
        public async Task Print(App app)
        {
            Console.WriteLine("Observera att sökningen sker lokalt. Du måste därför hämta", Console.ForegroundColor = ConsoleColor.Gray);
            Console.WriteLine("väderdata för att en plats ska ingå i sökningen. Detta", Console.ForegroundColor = ConsoleColor.Gray);
            Console.WriteLine("för att undvika masshämtning av väderdata.", Console.ForegroundColor = ConsoleColor.Gray);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Välj en väderlek att söka efter:");
            Console.WriteLine();
            Console.WriteLine("sol");
            Console.WriteLine("moln");
            Console.WriteLine("regn");
            Console.WriteLine("snö");
            Console.WriteLine("åska");
            Console.WriteLine();

            var input = "";
            do 
            {
                Console.Write("Ange väderlek: ");
                input = Console.ReadLine()!;

                
                if (app.WeatherDescriptionCreator.VeryBasicWeatherDescription.GetWeatherDescriptionMap().ContainsValue(input.ToLower()))
                {   
                    app.ForecastVirtualProxy.SetCurrentMatchWeather(input.ToLower());
                    app.CommandController.CurrentCommand = app.CommandController.WeatherInitCommand;
                    SetIgnoreNextCommand();
                    Console.WriteLine();
                    return;
                }
                else 
                {
                    Extensions.OverwritePreviousLine();
                    input = "";
                }
            } while (String.IsNullOrEmpty(input));
        }
        #pragma warning restore 1998
    }
}