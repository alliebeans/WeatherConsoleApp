using System;

namespace WeatherApp.Contents
{
    public class BackContent : IContent
    {
        private bool _isIgnoreNextCommand = false;
        public bool IsIgnoreNextCommand() => _isIgnoreNextCommand;
        public void SetIgnoreNextCommand() => _isIgnoreNextCommand = true;
        public string GetTitle(App app) => "";
        public string GetStatus(App app) => "";
        public void PrintTitle(string title) => Extensions.PrintTitle(title);
        public void PrintStatus(string status) { throw new NotImplementedException(); }
        #pragma warning disable 1998
        public async Task Print(App app)
        {
            if (app.CommandHistory.Count == 0)
                throw new InvalidOperationException();
            
            app.CommandController.CurrentCommand = app.CommandHistory.Pop();
            SetIgnoreNextCommand();
            return;
        }
        #pragma warning restore 1998
    }
}

