using System;
using WeatherApp.Weather;

namespace WeatherApp.Contents
{
    public class FetchForecastContent : IContent
    {
        private bool _isIgnoreNextCommand = false;
        public bool IsIgnoreNextCommand() => _isIgnoreNextCommand;
        public void SetIgnoreNextCommand() => _isIgnoreNextCommand = true;
        public string GetTitle(App app) => "Hämta väderdata från plats";
        public string GetStatus(App app) => "";
        public void PrintTitle(string title) => Extensions.PrintTitle(title);
        public void PrintStatus(string status) { throw new NotImplementedException(); }
        #pragma warning disable 1998
        public async Task Print(App app)
        {
            Console.WriteLine("Välj en plats att hämta väderdata från:");
            Console.WriteLine();

            var nextLocation = (ForecastLocation?)app.ForecastVirtualProxy.UserGetForecastLocation()!;

            if (nextLocation != null)
            {   
                var _nextLocation = (ForecastLocation)nextLocation;
                app.ForecastVirtualProxy.SetForecastLocation(app.ForecastVirtualProxy.GetLocationFromStorage(_nextLocation.Name));
                await app.ForecastVirtualProxy.GetNextForecastData();
                
                app.CommandController.CurrentCommand = app.CommandController.ForecastInitCommand;
                SetIgnoreNextCommand();
                Console.WriteLine();
                return;

            }
            Console.WriteLine("Hittade inga sparade platser.", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.ResetColor();
            Console.WriteLine();
            Extensions.PrintConfirmation(app, "Vill du lägga till en ny plats?", app.CommandController.LocationCommand, app.CommandController.ForecastInitCommand);
            return;
        }
        #pragma warning restore 1998
    }
}