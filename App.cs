using System;
using WeatherApp.Weather;
using WeatherApp.UserAccount;
using WeatherApp.REMOTE_DATABASE;

namespace WeatherApp
{
    public class App
    {
        #region Components - UserAccount, Weather
        /// <summary>
        /// UserAccount components 
        /// </summary>
        public UserLoginNameStorage UserLoginNameStorage { get; private set; }
        public PasswordHashStorage PasswordHashStorage { get; private set; }
        public LoginVerifier LoginVerifier { get; private set; }
        public UserAccountCreator UserAccountCreator { get; private set; }
        public LoginFacade LoginFacade { get; private set; }

        /// <summary>
        /// Weather components 
        /// </summary>
        public ForecastLocationStorage ForecastLocationStorage { get; private set; }
        public ForecastStorage ForecastStorage { get; private set; }
        public WeatherDataDeserializer WeatherDataDeserializer { get; private set; }
        public WeatherDescriptionCreator WeatherDescriptionCreator { get; private set; }
        public WindSymbolCreator WindSymbolCreator { get; private set; }
        public IForecastReciever ForecastFacade { get; private set; }
        public IForecastReciever ForecastVirtualProxy { get; private set; }
        #endregion

        #region Components - UI
        public Stack<ICommand> CommandHistory { get; private set; }
        public CommandController CommandController { get; private set; }
        public ContentController ContentController { get; private set; }
        #endregion

        #region State fields
        private bool IsQuit = false; 
        public void RequestQuit() => IsQuit = true;
        #endregion

        public App
        (
            UserLoginNameStorage userLoginNameStorage, 
            PasswordHashStorage passwordHashStorage, 
            LoginVerifier loginVerifier, 
            UserAccountCreator userAccountCreator,
            ForecastLocationStorage forecastLocationStorage,
            ForecastStorage forecastStorage,
            WeatherDataDeserializer weatherDataDeserializer,
            WeatherDescriptionCreator weatherDescriptionCreator,
            WindSymbolCreator windSymbolCreator,
            ContentController contentController)
        {
            UserLoginNameStorage = userLoginNameStorage; 
            PasswordHashStorage = passwordHashStorage;

            LoginVerifier = loginVerifier; 
            UserAccountCreator = userAccountCreator;

            ForecastLocationStorage = forecastLocationStorage;
            ForecastStorage = forecastStorage;
            WeatherDataDeserializer = weatherDataDeserializer;
            WeatherDescriptionCreator = weatherDescriptionCreator;
            WindSymbolCreator = windSymbolCreator;

            ContentController = contentController;

            LoginFacade = new LoginFacade(LoginVerifier, UserAccountCreator);
            ForecastFacade = new ForecastFacade
            (
                ForecastLocationStorage, 
                ForecastStorage, 
                WeatherDataDeserializer, 
                WeatherDescriptionCreator, 
                WindSymbolCreator
            );
            ForecastVirtualProxy = new ForecastVirtualProxy(ForecastFacade);
            CommandHistory = new Stack<ICommand>();
            CommandController = new CommandController();
        }

        public async Task Run()
        {
            CommandController.CurrentCommand!.Execute(this);
            if (CommandController.NextAvailableHasBackCommand())
                    CommandHistory.Push(CommandController.CurrentCommand);

            while (!IsQuit)
            {
                Console.WriteLine();
                var title = ContentController.CurrentContent!.GetTitle(this);
                ContentController.CurrentContent!.PrintTitle(title);
                await ContentController.CurrentContent!.Print(this);
                var available = CommandController.GetAvailable(CommandController.CurrentCommand!);
                if (!ContentController.CurrentContent.IsIgnoreNextCommand())
                    CommandController.GetNextCommand(available!);
                if (CommandController.NextAvailableHasBackCommand())
                    CommandHistory.Push(CommandController.CurrentCommand!);
                CommandController.CurrentCommand!.Execute(this);
            }
            return;
        }
    }
}