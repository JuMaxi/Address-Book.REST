using System;

namespace Address_Book.REST
{
    public class Location
    {
        public string State { get; set; }
        public string City { get; set; }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }
        public Location ForecastPlace { get; set; }
        public int[] MinMaxAvg { get; set; }
    }
}
