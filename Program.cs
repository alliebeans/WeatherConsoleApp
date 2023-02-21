using System;
using WeatherApp.Weather;
using WeatherApp.UserAccount;
using WeatherApp.REMOTE_DATABASE;

namespace WeatherApp
{
    class Program
    {       
        public static async Task Main()
        {
            UserLoginNameStorage UserLoginNameStorage = new UserLoginNameStorage();
            PasswordHashStorage PasswordHashStorage = new PasswordHashStorage();

            App app = new App
            (
                UserLoginNameStorage,
                PasswordHashStorage,
                
                new LoginVerifier(UserLoginNameStorage, PasswordHashStorage),
                new UserAccountCreator(UserLoginNameStorage, PasswordHashStorage),

                new ForecastLocationStorage(),
                new ForecastStorage(),
                new WeatherDataDeserializer(), 
                new WeatherDescriptionCreator(), 
                new WindSymbolCreator(),

                new ContentController()
            );

            await app.Run();
        }
    }
}