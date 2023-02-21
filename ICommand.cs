using System;

namespace WeatherApp
{
    public interface ICommand
    {
        string Name();
        void Execute(App app);
    }
}