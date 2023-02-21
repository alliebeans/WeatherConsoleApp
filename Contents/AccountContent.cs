using System;

namespace WeatherApp.Contents
{
    public class AccountContent : IContent
    {
        private bool _isIgnoreNextCommand = false;
        public bool IsIgnoreNextCommand() => _isIgnoreNextCommand;
        public void SetIgnoreNextCommand() => _isIgnoreNextCommand = true;
        public string GetTitle(App app) => "Konton";
        public string GetStatus(App app) => "";
        public void PrintTitle(string title) => Extensions.PrintTitle(title);
        public void PrintStatus(string status) { throw new NotImplementedException(); }
        #pragma warning disable 1998
        public async Task Print(App app) {}
        #pragma warning restore 1998
    }
}