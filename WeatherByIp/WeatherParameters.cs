namespace WeatherByIp
{
    public class WeatherParameters
    {
        public decimal temperature { get; set; }
        public decimal windspeed { get; set; }
        public decimal winddirection { get; set; }
        public int weathercode { get; set; }
        public int is_day { get; set;}
        public DateTime time { get; set; }
    }
}
