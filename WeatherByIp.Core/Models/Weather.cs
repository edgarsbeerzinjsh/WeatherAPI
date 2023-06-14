using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherByIp.Core.Models
{
    public class Weather: Entity
    {
        public decimal Temperature { get; set; }
        public decimal Windspeed { get; set; }
        public decimal WindDirection { get; set; }
        public string WeatherState { get; set; }
    }
}
