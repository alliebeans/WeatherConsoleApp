using System;
using WeatherApp.Weather;

namespace WeatherApp.Contents
{
    public class AddLocationContent : IContent
    {
        private bool _isIgnoreNextCommand = false;
        public bool IsIgnoreNextCommand() => _isIgnoreNextCommand;
        public void SetIgnoreNextCommand() => _isIgnoreNextCommand = true;
        public string GetTitle(App app) => "Lägg till en ny plats";
        public string GetStatus(App app) => "";
        public void PrintTitle(string title) => Extensions.PrintTitle(title);
        public void PrintStatus(string status) { throw new NotImplementedException(); }
        #pragma warning disable 1998
        public async Task Print(App app)
        {
            Console.WriteLine("Observera att du endast kan lägga till platser inom SMHI:s väderupptagningsområde.", Console.ForegroundColor = ConsoleColor.Gray);
            Console.WriteLine("För att skapa en plats behöver du dess ungefärliga koordinater, exempelvis: \n\"Norrköping\", latitud: 58.58921750315413, longitud: 16.191519013002157", Console.ForegroundColor = ConsoleColor.Gray);
            Console.ResetColor();
            Console.WriteLine();

            Console.Write("Ange latitud: ");
            var latitude = Console.ReadLine()!;
            Console.Write("Ange longitud: ");
            var longitude = Console.ReadLine()!;
            Console.Write("Ange namn: ");
            var name = Console.ReadLine()!;

            Console.WriteLine();

            ForecastLocation location;

            try 
            {
                var lon = Convert.ToDouble(longitude);
                var lat = Convert.ToDouble(latitude);

                location = app.ForecastVirtualProxy.CreateForecastLocation(name, lon, lat);
                app.ForecastVirtualProxy.AddForecastLocation(location);

                Console.WriteLine($"Platsen är tillagd!", Console.ForegroundColor = ConsoleColor.Green);
                Thread.Sleep(1500);
                Console.ResetColor();
                
                app.CommandController.CurrentCommand = app.CommandController.ForecastInitCommand;
                SetIgnoreNextCommand();
            }
            catch 
            {
                Console.WriteLine("Det gick inte att lägga till platsen.", Console.ForegroundColor = ConsoleColor.Yellow);
                Console.ResetColor();
                Console.WriteLine();
                Extensions.PrintConfirmation(app, "Vill du prova igen?", app.CommandController.AddLocationCommand, app.CommandController.ForecastInitCommand);
                return;
            }
        }
        #pragma warning restore 1998
    }
}