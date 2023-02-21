using System;
using System.Globalization;

namespace WeatherApp.Weather
{
    public class ForecastFacade : IForecastReciever
    {
        #region Component fields
        public ForecastLocationStorage ForecastLocationStorage;
        public ForecastStorage ForecastStorage;
        public WeatherDataDeserializer WeatherDataDeserializer;
        public WeatherDescriptionCreator WeatherDescriptionCreator;
        public WindSymbolCreator WindSymbolCreator;
        #endregion

        #region State fields
        private ForecastData? CurrentForecastData;
        private ForecastLocation? CurrentForecastLocation;
        private string? CurrentMatchWeather = null;

        public void SetForecastLocation(ForecastLocation forecastLocation) => CurrentForecastLocation = forecastLocation;
        public void SetForecastData(ForecastData forecastData) => CurrentForecastData = forecastData;
        public void SetCurrentMatchWeather(string weather) => CurrentMatchWeather = weather;

        public ForecastLocation? GetCurrentForecastLocation() => CurrentForecastLocation;
        public ForecastData? GetCurrentForecastData() => CurrentForecastData;
        public string? GetCurrentMatchWeather() => CurrentMatchWeather;
        #endregion  

        public ForecastFacade(ForecastLocationStorage forecastLocationStorage, ForecastStorage forecastStorage, WeatherDataDeserializer weatherDataDeserializer, WeatherDescriptionCreator weatherDescriptionCreator, WindSymbolCreator windSymbolCreator)
        {
            ForecastLocationStorage = forecastLocationStorage;
            ForecastStorage = forecastStorage;
            WeatherDataDeserializer = weatherDataDeserializer;
            WeatherDescriptionCreator = weatherDescriptionCreator;
            WindSymbolCreator = windSymbolCreator;

            ForecastLocationStorage.Add(new ForecastLocation("Stockholm", 18.067386578974183, 59.32904856076159));
            ForecastLocationStorage.Add(new ForecastLocation("Göteborg", 11.986528670748033, 57.70061996686596));    
            ForecastLocationStorage.Add(new ForecastLocation("Malmö", 13.004984923845846, 55.60623707097732));
            ForecastLocationStorage.Add(new ForecastLocation("Jukkasjärvi", 20.601098797045648, 67.85126547832208));
        }

        public async Task GetNextForecastData()
        {
            if (CurrentForecastLocation == null)
                throw new InvalidCastException();
            var _currentForecastLocation = (ForecastLocation)CurrentForecastLocation!;
            WeatherDataDeserializer.SetUri(_currentForecastLocation.GetUri());
            await WeatherDataDeserializer.FetchWeatherData(this);

            ForecastStorage.AddToForecastStorage((ForecastLocation)CurrentForecastLocation!, CurrentForecastData!);
        }

        #pragma warning disable 1998
        public async Task PrintForecast()
        {
            if (CurrentForecastData != null)
            {
                PrintCurrentLocation();
                Console.WriteLine();

                int smhiDescriptionMaxLength = CurrentForecastData!.Max(x => WeatherDescriptionCreator.GetDescription(WeatherDescriptionCreator.SMHIMap, x.weatherDescription).Length);
                for(int i = 0; i < CurrentForecastData.Count; i++)
                    Console.WriteLine($"{Extensions.GetAbbreviatedDay(CurrentForecastData[i].validTime)}: {WeatherDescriptionCreator.GetDescription(WeatherDescriptionCreator.IconMap, CurrentForecastData[i].weatherDescription)}  {GetTemperatureCelsius(CurrentForecastData[i].temperatureCelsius).PadRight(CurrentForecastData.Max(x => GetTemperatureCelsius(x.temperatureCelsius).Length)+2)}{WeatherDescriptionCreator.GetDescription(WeatherDescriptionCreator.SMHIMap, CurrentForecastData[i].weatherDescription).PadRight(smhiDescriptionMaxLength+2)}{WindSymbolCreator.GetWindSymbol(WindSymbolCreator.ArrowSymbolMap, CurrentForecastData[i].windDegree)} {CurrentForecastData[i].windStrength.ToString("F1", CultureInfo.InvariantCulture)} m/s  {PrintIfToday(i)}{PrintIfTomorrow(i)}");
            }
        }
        #pragma warning restore 1998

        #pragma warning disable 1998
        public async Task PrintAllMatchingWeather(string veryBasicWeatherDescription)
        {
            Console.WriteLine($"Valt väder: {veryBasicWeatherDescription}");
            Console.WriteLine();

            var _count = 0;

            foreach(ForecastLocation location in ForecastStorage.StoredForecasts.Keys)
            {
                CurrentForecastData = ForecastStorage.GetForecastFromStorage(location.Name);

                if (ContainsWeather(veryBasicWeatherDescription))
                {
                    Console.WriteLine($"{location.Name}");

                    int smhiDescriptionMaxLength = CurrentForecastData!.Max(x => WeatherDescriptionCreator.GetDescription(WeatherDescriptionCreator.SMHIMap, x.weatherDescription).Length);
                    for(int i = 0; i < CurrentForecastData.Count; i++)
                        if (WeatherDescriptionCreator.GetDescription(WeatherDescriptionCreator.VeryBasicWeatherDescription, CurrentForecastData[i].weatherDescription).ToLower() == veryBasicWeatherDescription.ToLower())
                            Console.WriteLine($"{Extensions.GetAbbreviatedDay(CurrentForecastData[i].validTime)}: {WeatherDescriptionCreator.GetDescription(WeatherDescriptionCreator.IconMap, CurrentForecastData[i].weatherDescription)}  {GetTemperatureCelsius(CurrentForecastData[i].temperatureCelsius).PadRight(CurrentForecastData.Max(x => GetTemperatureCelsius(x.temperatureCelsius).Length)+2)}{WeatherDescriptionCreator.GetDescription(WeatherDescriptionCreator.SMHIMap, CurrentForecastData[i].weatherDescription).PadRight(smhiDescriptionMaxLength+2)}{WindSymbolCreator.GetWindSymbol(WindSymbolCreator.ArrowSymbolMap, CurrentForecastData[i].windDegree)} {CurrentForecastData[i].windStrength.ToString("F1", CultureInfo.InvariantCulture)} m/s ({Extensions.GetDateAndMonth(CurrentForecastData[i].validTime)})");
                    
                    Console.WriteLine();
                    _count++; 
                }
            }
            if (_count == 0)
            {
                Console.WriteLine($"Ingen prognos för det valda vädret hittades.");
                Console.WriteLine();   
            }
        }
        #pragma warning restore 1998

        public ForecastLocation? UserGetForecastLocation() 
        {
            if (ForecastLocationStorage.StoredForecastLocations.Count() == 0) 
                return null;
            
            var _chosen = 0;
            PrintStoredLocations();

            Console.WriteLine();
            var input = "";
            
            do 
            {
                Console.Write("Ange nummer: ");
                input = Console.ReadLine()!;

                try 
                {
                    _chosen = Convert.ToInt32(input);

                    if (_chosen > 0 && _chosen <= ForecastLocationStorage.StoredForecastLocations.Count())
                    {
                        input = _chosen.ToString();
                        return ForecastLocationStorage.StoredForecastLocations[--_chosen];
                    }
                    else 
                    {
                        Extensions.OverwritePreviousLine();
                        input = "";
                    }
                }
                catch
                {
                    Extensions.OverwritePreviousLine();
                    input = "";
                }
            } while (String.IsNullOrEmpty(input));
            throw new InvalidOperationException();
        }

        public ForecastLocation? UserGetForecastLocationHasData() 
        {
            if (ForecastStorage.StoredForecasts.Count() == 0) 
                return null;
            
            var _chosen = 0;
            PrintStoredLocationHasData();

            Console.WriteLine();
            var input = "";
            
            do 
            {
                Console.Write("Ange nummer: ");
                input = Console.ReadLine()!;

                try 
                {
                    _chosen = Convert.ToInt32(input);

                    if (_chosen > 0 && _chosen <= GetStoredLocationsHasData().Count())
                    {
                        input = _chosen.ToString();
                        return GetStoredLocationsHasData()[--_chosen];
                    }
                    else 
                    {
                        Extensions.OverwritePreviousLine();
                        input = "";
                    }
                }
                catch
                {
                    Extensions.OverwritePreviousLine();
                    input = "";
                }
            } while (String.IsNullOrEmpty(input));
            throw new InvalidOperationException();
        }
        
        public ForecastLocation CreateForecastLocation(string name, double longitude, double latitude) => new ForecastLocation(name, longitude, latitude);
        public void AddForecastLocation(ForecastLocation location) => ForecastLocationStorage.StoredForecastLocations.Add(location);
        public ForecastLocation GetLocationFromStorage(string name) => ForecastLocationStorage.GetLocationFromStorage(name);
        public ForecastLocation[] GetStoredLocationsHasData() => ForecastStorage.GetLocationsHasData();
        public ForecastData GetForecastFromStorage(string name) => ForecastStorage.GetForecastFromStorage(name);

        #region Helper methods
        private string GetTemperatureCelsius(double temperatureCelsius)
        {
            if (temperatureCelsius >= 0)
                return $"+{temperatureCelsius.ToString("F1", CultureInfo.InvariantCulture)}°C";

            return $"{temperatureCelsius.ToString("F1", CultureInfo.InvariantCulture)}°C";
        }

        private string PrintIfToday(int i)
        {
            if (i != 0)
                return "";
            return "(just nu)";
        }

        private string PrintIfTomorrow(int i)
        {
            if (i != 1)
                return "";
            return "(imorgon)";
        }

        private void PrintCurrentLocation()
        {
            var _currentForecastLocation = (ForecastLocation)CurrentForecastLocation!;
            Console.WriteLine($"Vald plats: {_currentForecastLocation.Name}");
        }

        private void PrintStoredLocations()
        {
            if (ForecastLocationStorage.StoredForecastLocations.Count() == 0)
                return;
            
            var _count = 1;
            foreach(ForecastLocation location in ForecastLocationStorage.StoredForecastLocations)
            {
                Console.Write($"{_count++}. ");
                Console.Write(location.Name);
                Console.WriteLine();
            }
        }

        private void PrintStoredLocationHasData()
        {
            if (ForecastStorage.StoredForecasts.Count() == 0)
                return;
            
            var _count = 1;
            foreach(ForecastLocation location in GetStoredLocationsHasData())
            {
                Console.Write($"{_count++}. ");
                Console.Write(location.Name);
                Console.WriteLine();
            }
        }

        private bool ContainsWeather(string veryBasicWeatherDescription)
        {
            for(int i = 0; i < CurrentForecastData!.Count; i++)
                    if (WeatherDescriptionCreator.GetDescription(WeatherDescriptionCreator.VeryBasicWeatherDescription, CurrentForecastData[i].weatherDescription).ToLower() == veryBasicWeatherDescription.ToLower())
                        return true;
            return false;
        }
        #endregion
    }
}