using System;

namespace WeatherApp
{
    public interface IContent
    {
        void SetIgnoreNextCommand();
        bool IsIgnoreNextCommand();
        string GetTitle(App app);
        string GetStatus(App app);
        void PrintTitle(string title);
        //void PrintStatus(string status);
        Task Print(App app);
    }
}