using System;
using WeatherApp.Commands;

namespace WeatherApp
{
    public class CommandController
    {
        public ICommand? CurrentCommand;
        public ICommand? GetCommand(ICommand command) { return Commands.Find(x => x.Equals(command)); }
        public List<ICommand> Available = new List<ICommand>();
        public Dictionary<ICommand, ICommand[]> AvailableCommandsMap;

        #region Commands
        private List<ICommand> Commands;
        public ICommand InitialiseCommand { get; private set; }
        public ICommand AccountCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand HelpCommand { get; private set; }
        public ICommand CreateNewAccountCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }
        public ICommand ForecastInitCommand { get; private set; }
        public ICommand AddLocationCommand { get; private set; }
        public ICommand LocationCommand { get; private set; }
        public ICommand FetchForecastCommand { get; private set; }
        public ICommand ChooseForecastCommand { get; private set; }
        public ICommand FetchWeatherCommand { get; private set; }
        public ICommand WeatherInitCommand { get; private set; }
        public ICommand QuitCommand { get; private set; }
        #endregion

        public CommandController()
        {
            InitialiseCommand = new InitialiseCommand();
            AccountCommand = new AccountCommand();
            BackCommand = new BackCommand();
            HelpCommand = new HelpCommand();
            QuitCommand = new QuitCommand();
            CreateNewAccountCommand = new CreateNewAccountCommand();
            LoginCommand = new LoginCommand();
            LogoutCommand = new LogoutCommand();
            ForecastInitCommand = new ForecastInitCommand();
            AddLocationCommand = new AddLocationCommand();
            LocationCommand = new LocationCommand();
            FetchForecastCommand = new FetchForecastCommand();
            ChooseForecastCommand = new ChooseForecastCommand();
            FetchWeatherCommand = new FetchWeatherCommand();
            WeatherInitCommand = new WeatherInitCommand();

            Commands = new List<ICommand>();
            Commands.AddRange(new[] 
            {
                InitialiseCommand,
                AccountCommand,
                BackCommand,
                HelpCommand,
                QuitCommand,
                CreateNewAccountCommand,
                LoginCommand,
                LogoutCommand,
                ForecastInitCommand,
                AddLocationCommand,
                LocationCommand,
                FetchForecastCommand,
                ChooseForecastCommand,
                FetchWeatherCommand,
                WeatherInitCommand
            });

            AvailableCommandsMap = new Dictionary<ICommand, ICommand[]> 
            {
                {InitialiseCommand, new[] { AccountCommand, HelpCommand, QuitCommand }},
                {AccountCommand, new[] { LoginCommand, CreateNewAccountCommand, BackCommand }},
                {ForecastInitCommand, new[] { LocationCommand, FetchWeatherCommand, HelpCommand, LogoutCommand, QuitCommand }},
                {WeatherInitCommand, new[] { LocationCommand, FetchWeatherCommand, HelpCommand, LogoutCommand, QuitCommand }},
                {LocationCommand, new[] { FetchForecastCommand, ChooseForecastCommand, AddLocationCommand, BackCommand }},
                {HelpCommand, new[] { BackCommand, QuitCommand }},
            };

            Available.Add(InitialiseCommand);
            CurrentCommand = Available[0];
        }

        public ICommand[]? GetAvailable(ICommand current) 
        {
            ICommand[] available;
            if (!AvailableCommandsMap.TryGetValue(current, out available!))
                return null;
            return available;
        }

        public bool NextAvailableHasBackCommand() 
        {
            if (GetAvailable(CurrentCommand!) == null)
                return false;

            foreach(ICommand command in GetAvailable(CurrentCommand!)!)
                if (GetAvailable(command) != null && GetAvailable(command)!.Contains(BackCommand))
                    return true;
            return false;
        }

        public void GetNextCommand(ICommand[] available)
        {
            Available.Clear();
            Available.AddRange(available);

            foreach(ICommand command in Available)
                Console.WriteLine(command.Name().ToLower());
            Console.WriteLine();

            string input = "";

            do 
            {
                Console.Write("Ange kommando: ");
                input = Console.ReadLine()!;

                if (Available.Select(x => x.Name()).Contains(input!.ToLower()))
                {
                    CurrentCommand = Available.Find(x => x.Name().ToLower() == input.ToLower())!;
                    return;
                }
                else 
                {
                    Extensions.OverwritePreviousLine();
                    input = "";
                }
            } while (String.IsNullOrEmpty(input));
            throw new InvalidOperationException();
        }
    }
}