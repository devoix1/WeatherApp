using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherAPI {
    class Program {
        public static double KtoCConverter(double K) { // Static method to convert Kelvin to Celsius
            double result = K - 273.15;
            return result;
        }
        public static string Ip() {
            string url = "http://icanhazip.com/"; // To get your IP
            var client = new WebClient();
            var ip = client.DownloadString(url);
            return ip;
        }
        static void Main() {
            WebClient geoinfo = new WebClient();
            string url = "http://api.ipstack.com/"+Ip()+"?access_key=YOUR API KEY"; // Get your api key in http://api.ipstack.com
            string res = geoinfo.DownloadString(url);
            var georesult = System.Text.Json.JsonSerializer.Deserialize<GeoInfo>(res);
            Console.WriteLine("\t\t\t\t\t\t___YOUR GEOLOCATION INFO___\n");
            Console.WriteLine($"Continent: {georesult.continent_name}" + $"\t\t\tLatitude: {georesult.latitude}");
            Console.WriteLine($"Country:{georesult.country_name}" + $"\t\tLongitude: {georesult.longitude}");
            Console.WriteLine($"Region: {georesult.region_name}" + $"\t\t\tLocation: {georesult.location.capital}");
            Console.WriteLine($"City: {georesult.region_name}" + $"\t\t\tTime zone: {georesult.time_zone}");

            WebClient weatherinfo = new WebClient();
            var apikey = File.ReadAllText("apikey"); // Create a file and add your api key into this file
            //To get wheather api visit this website openweathermap.org or find a best one
            string weatherurl = $"https://api.openweathermap.org/data/2.5/weather?lat={georesult.latitude}&lon={georesult.longitude}&appid={apikey}";
            string weatherres = weatherinfo.DownloadString(weatherurl);
            var weatherresult = JsonSerializer.Deserialize<WeatherInfo>(weatherres);
            Console.WriteLine("\n\n\t\t\t\t\t\t___YOUR WEATHER INFO___\n");
            Console.WriteLine($"Exact Location: {weatherresult.name}");
            Console.WriteLine($"Temprature: {Math.Round(KtoCConverter(weatherresult.main.temp))}°C");
            Console.WriteLine($"Speed of wind: {weatherresult.wind.speed}m/s");
            Console.WriteLine($"Humidity: {weatherresult.main.humidity}%");
            Console.WriteLine($"Pressure: {weatherresult.main.pressure}Pa");

            Console.ReadLine();
        }
    }
}
