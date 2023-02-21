using System;
using System.Text.Json;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace WeatherApp.Weather
{
    public class WeatherDataDeserializer
    {
        private Uri? Uri = null;
        public void SetUri(string uri) => Uri = new Uri(uri);
        private static Regex Time = new Regex(@"T{1}\d{2}");

        public async Task FetchWeatherData(IForecastReciever reciever)
        {
            using HttpClient client = new()
            {
                BaseAddress = Uri
            };

            var smhiData = await client.GetFromJsonAsync<JsonDocument>(client.BaseAddress);

            if (smhiData != null)
            {
                var validTimes = GetValidTimeIndexes(smhiData);
                var forecastData = SetForecastData(smhiData, validTimes);
                reciever.SetForecastData(forecastData);
            }
        }

        /// <summary>
        /// Matches the fetched SMHI "validTime" data against regular expression in order to 
        /// find the indexes where weather forecast data at 12:00 UTC for the next 7 days.
        /// </summary>
        /// <param name="smhiData"></param>
        /// <returns></returns>
        public List<int>? GetValidTimeIndexes(JsonDocument? smhiData)
        {
            var validTimes = new List<int>();

            if (smhiData != null)
            {
                var smhiTimeSeries = smhiData!.RootElement.GetProperty("timeSeries");
                var timeSeriesLength = smhiTimeSeries.GetArrayLength();
                int tomorrowIndex = 0;

                for (int i = 1; i < timeSeriesLength; i++)
                    if (DateTime.Now.Date == DateTime.Parse(smhiTimeSeries[i].GetProperty("validTime").GetString()!).Date)
                        tomorrowIndex = i;

                for (int i = tomorrowIndex; i < timeSeriesLength; i++)
                    if (Time.Match((smhiTimeSeries[i].GetProperty("validTime").GetString()!)).Value == "T12")
                        if (validTimes.Count < 7)
                            validTimes.Add(i);
                return validTimes;
            }
            return null;
        }

        /// <summary>
        /// Creates new WeatherData objects from todays current weather forecast data and for the next 7 days.
        /// </summary>
        /// <param name="smhiData"></param>
        /// <returns></returns>
        public ForecastData SetForecastData(JsonDocument? smhiData, List<int>? validTimes)
        {
            ForecastData _forecastData = new ForecastData();

            if (smhiData != null)
            {
                var smhiTimeSeries = smhiData.RootElement.GetProperty("timeSeries");

                var date = smhiTimeSeries[0].GetProperty("validTime");
                var temperatureCelsius = smhiTimeSeries[0].GetProperty("parameters")[11].GetProperty("values")[0];
                if (temperatureCelsius.GetDouble() > 55)
                    temperatureCelsius = smhiTimeSeries[0].GetProperty("parameters")[10].GetProperty("values")[0];
                var windDegree = smhiTimeSeries[0].GetProperty("parameters")[13].GetProperty("values")[0];
                var windStrength = smhiTimeSeries[0].GetProperty("parameters")[14].GetProperty("values")[0];
                var weatherSymbol = smhiTimeSeries[0].GetProperty("parameters")[18].GetProperty("values")[0];

                _forecastData.Add(new WeatherData(date.GetString()!, temperatureCelsius.GetDouble(), windDegree.GetInt32(), windStrength.GetDouble(), weatherSymbol.GetInt32()));

                foreach(int validTime in validTimes!)
                {
                    date = smhiTimeSeries[validTime].GetProperty("validTime");
                    temperatureCelsius = smhiTimeSeries[validTime].GetProperty("parameters")[1].GetProperty("values")[0];
                    windDegree = smhiTimeSeries[validTime].GetProperty("parameters")[3].GetProperty("values")[0];
                    windStrength = smhiTimeSeries[validTime].GetProperty("parameters")[4].GetProperty("values")[0];
                    weatherSymbol = smhiTimeSeries[validTime].GetProperty("parameters")[18].GetProperty("values")[0];

                    _forecastData.Add(new WeatherData(date.GetString()!, temperatureCelsius.GetDouble(), windDegree.GetInt32(), windStrength.GetDouble(), weatherSymbol.GetInt32()));                    
                }

                return _forecastData;
            }
            throw new ArgumentNullException();
        }
    }
}