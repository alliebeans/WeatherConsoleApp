using System;

namespace WeatherApp.Contents
{
    public class ForecastInitContent : IContent
    {
        private bool _isIgnoreNextCommand = false;
        public bool IsIgnoreNextCommand() => _isIgnoreNextCommand;
        public void SetIgnoreNextCommand() => _isIgnoreNextCommand = true;
        public string GetTitle(App app) => app.LoginFacade.GetUsername()!;
        public string GetStatus(App app) => "";
        public void PrintTitle(string title) => Extensions.PrintAppTitle(title);
        public void PrintStatus(string status) { throw new NotImplementedException(); }
        #pragma warning disable 1998
        public async Task Print(App app)
        {
            await app.ForecastVirtualProxy.PrintForecast();
            Console.WriteLine();
        }
        #pragma warning restore 1998
    }
}