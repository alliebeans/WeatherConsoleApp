using System;
using WeatherApp.Weather;

namespace WeatherApp.Contents
{
    public class ChooseForecastContent : IContent
    {
        private bool _isIgnoreNextCommand = false;
        public bool IsIgnoreNextCommand() => _isIgnoreNextCommand;
        public void SetIgnoreNextCommand() => _isIgnoreNextCommand = true;
        public string GetTitle(App app) => "Välj plats";
        public string GetStatus(App app) => "";
        public void PrintTitle(string title) => Extensions.PrintTitle(title);
        public void PrintStatus(string status) { throw new NotImplementedException(); }
        #pragma warning disable 1998
        public async Task Print(App app)
        {
            if (app.ForecastVirtualProxy.GetStoredLocationsHasData().Count() > 0) 
            {
                Console.WriteLine("Välj en plats att visa väderdata från:");
                Console.WriteLine();

                var nextLocation = (ForecastLocation?)app.ForecastVirtualProxy.UserGetForecastLocationHasData()!;

                if (nextLocation != null)
                {   
                    var _nextLocation = (ForecastLocation)nextLocation;
                    app.ForecastVirtualProxy.SetForecastLocation(app.ForecastVirtualProxy.GetLocationFromStorage(_nextLocation.Name));
                    app.ForecastVirtualProxy.SetForecastData(app.ForecastVirtualProxy.GetForecastFromStorage(_nextLocation.Name)!);
                    
                    app.CommandController.CurrentCommand = app.CommandController.ForecastInitCommand;
                    SetIgnoreNextCommand();
                    Console.WriteLine();
                    return;

                }
            }
            Console.WriteLine("Hittar ingen plats med hämtad väderdata.", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.ResetColor();
            Console.WriteLine();
            Extensions.PrintConfirmation(app, "Vill du hämta väderdata nu?", app.CommandController.FetchForecastCommand, app.CommandController.ForecastInitCommand);
            return;
        }
        #pragma warning restore 1998
    }
}