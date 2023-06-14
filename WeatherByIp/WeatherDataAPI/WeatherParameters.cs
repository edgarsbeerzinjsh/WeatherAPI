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

        public static string GetWeatherDescription(int code)
        {
            var codeDescription = new Dictionary<int, string>()
            {
                { 0, "Clear sky" },
                { 1, "Mainly clear" },
                { 2, "Partly cloudy" },
                { 3, "Overcast" },
                { 45, "Fog and depositing rime fog" },
                { 48, "Fog and depositing rime fog" },
                { 51, "Drizzle: Light intensity" },
                { 53, "Drizzle: Moderate intensity" },
                { 55, "Drizzle: Dense intensity" },
                { 56, "Freezing Drizzle: Light intensity" },
                { 57, "Freezing Drizzle: Dense intensity" },
                { 61, "Rain: Slight intensity" },
                { 63, "Rain: Moderate intensity" },
                { 65, "Rain: Heavy intensity" },
                { 66, "Freezing Rain: Light intensity" },
                { 67, "Freezing Rain: Heavy intensity" },
                { 71, "Snow fall: Slight intensity" },
                { 73, "Snow fall: Moderate intensity" },
                { 75, "Snow fall: Heavy intensity" },
                { 77, "Snow grains" },
                { 80, "Rain showers: Slight intensity" },
                { 81, "Rain showers: Moderate intensity" },
                { 82, "Rain showers: Violent intensity" },
                { 85, "Snow showers: Slight intensity" },
                { 86, "Snow showers: Heavy intensity" }
            };

            return codeDescription.TryGetValue(code, out string value) ? value : "Unknown weather code";
        }
    }
}
