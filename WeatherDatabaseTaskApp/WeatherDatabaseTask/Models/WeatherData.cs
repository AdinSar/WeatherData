using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherDatabaseTask.Models
{
    class WeatherData
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string InOrOut { get; set; }

        public double Temperature { get; set; }

        public int Humidity { get; set; }
    }
}
