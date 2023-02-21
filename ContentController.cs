using System;
using WeatherApp.Contents;

namespace WeatherApp
{
    public class ContentController
    {
        public IContent? NextContent;
        public IContent? CurrentContent;
        private Dictionary<Content, IContent> AppContent;
        public ContentController() 
        {
            AppContent = new Dictionary<Content, IContent> 
            {
                {Content.Initialise, new InitialiseContent()},
                {Content.Account, new AccountContent()},
                {Content.Back, new BackContent()},
                {Content.Help, new HelpContent()},
                {Content.CreateNewAccount, new CreateNewAccountContent()},
                {Content.Login, new LoginContent()},
                {Content.Logout, new LogoutContent()},
                {Content.ForecastInit, new ForecastInitContent()},
                {Content.Location, new LocationContent()},
                {Content.AddLocation, new AddLocationContent()},
                {Content.FetchForecast, new FetchForecastContent()},
                {Content.ChooseForecast, new ChooseForecastContent()},
                {Content.FetchWeather, new FetchWeatherContent()},
                {Content.WeatherInit, new WeatherInitContent()},
            };

            CurrentContent = null;
        }
        private IContent GetNext(Content content)
        {
            IContent _next;
            if (!AppContent.TryGetValue(content, out _next!))
                throw new KeyNotFoundException();
            return _next;
        }

        public IContent MoveNext(Content content)
        {
            CurrentContent = GetNext(content);
            return CurrentContent;
        }
    }
}