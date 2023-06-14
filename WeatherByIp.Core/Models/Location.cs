﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherByIp.Core.Models
{
    public class Location: Entity
    {
        public string Ip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
