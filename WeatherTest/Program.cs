// See https://aka.ms/new-console-template for more information

using ElderlyCareApp.Utils;

WeatherHelper weatherHelper = new WeatherHelper();
weatherHelper.Fetch();

Console.WriteLine(weatherHelper.ToString());
