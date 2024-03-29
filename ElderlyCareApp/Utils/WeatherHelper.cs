﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Svg;
using System.Net;
using System.Windows.Media;

namespace ElderlyCareApp.Utils
{
    public class WeatherHelper : INotifyPropertyChanged
    {
        private const string _webpage = "https://www.msn.cn/zh-cn/weather/";
        private HtmlWeb _web;

        public string? City
        {
            get => _city;
            private set => SetField(ref _city, value);
        }

        public string? Weather
        {
            get => _weather;
            private set => SetField(ref _weather, value);
        }

        public string? Temperature
        {
            get => _temperature;
            private set => SetField(ref _temperature, value);
        }

        public string TemperatureMask => "温度 " + Temperature;

        public string? Wind
        {
            get => _wind;
            private set => SetField(ref _wind, value);
        }

        public string? Sd
        {
            get => _sd;
            private set => SetField(ref _sd, value);
        }

        public string SdMask => "空气湿度 " + Temperature;

        public string? AirQuality
        {
            get => _airQuality;
            private set => SetField(ref _airQuality, value);
        }

        public string? AirQualityText
        {
            //get => "空气质量 " + _airQuality + " " + _airQualityText;
            get => _airQualityText;
            private set => SetField(ref _airQualityText, value);
        }

        public string? AirQualityMask
        {
            get => "空气质量 " + _airQuality + " " + _airQualityText;
        }

        public string? WeatherIconUrl
        {
            get => _weatherIconUrl;
            private set => SetField(ref _weatherIconUrl, value);
        }

        public string? WeatherBrief
        {
            get => _weatherBrief;
            set => SetField(ref _weatherBrief, value);
        }

        public ImageSource WeatherIcon
        {
            get => _weatherIcon;
            set => SetField(ref _weatherIcon, value);
        }

        public SolidColorBrush DotFill
        {
            get => _dotFill;
            set => SetField(ref _dotFill, value);
        }

        private string? _detailedUrl;
        private string? _city;
        private string? _weather;
        private string? _temperature;
        private string? _wind;
        private string? _sd;
        private string? _airQuality;
        private string? _airQualityText;
        private string? _weatherIconUrl;
        private string? _weatherBrief;
        private ImageSource _weatherIcon;
        private SolidColorBrush _dotFill;

        public WeatherHelper()
        {
            _web = new HtmlWeb();
            Fetch();
        }

        public void Fetch()
        {
            var _document = _web.Load(_webpage);

            FetchInfo(_document);
        }

        public async Task FetchAsync()
        {
            var _document = await _web.LoadFromWebAsync(_webpage);
            FetchInfo(_document);
        }

        private void FetchInfo(HtmlDocument document)
        {
            City = GetNodeAttribute(document, "//*[@id=\"WeatherOverviewLocationName\"]/a", "title");

            Temperature = GetNodeAttribute(document, "//*[@id=\"OverviewCurrentTemperature\"]/a", "title");

            Wind = GetNodeText(document, "//*[@id=\"WeatherOverviewCurrentSection\"]/div[2]/div/div[3]/div/div[2]/a/div[2]");

            AirQuality = GetNodeText(document, "//*[@id=\"WeatherOverviewCurrentSection\"]/div[2]/div/div[3]/div/div[1]/a/div[2]");

            Weather = GetNodeText(document, "//*[@id=\"CurrentWeatherFeedback\"]/div");

            Sd = GetNodeText(document, "//*[@id=\"CurrentDetailLineHumidityValue\"]/span");

            WeatherIconUrl = GetNodeAttribute(document, "//*[@id=\"OverviewCurrentTemperature\"]/img", "src");

            WeatherBrief = GetNodeAttribute(document, "//*[@id=\"weatherMiniMapContainer\"]/a", "aria-label");


            // 将SvgDocument渲染到Bitmap
            WeatherIcon = IconUtil.DownloadSvgImage(_weatherIconUrl);

            if (AirQuality != null)
                ConvertAirQuality(int.Parse(AirQuality));
        }

        private void ConvertAirQuality(int quality)
        {
            string textQuality;
            Color dotColor;
            switch(quality)
            {
                case <= 50:
                    textQuality = "优";
                    dotColor = Color.FromRgb(0, 255, 0);
                    break;
                case > 50 and <= 100:
                    textQuality = "良";
                    dotColor = Color.FromRgb(255, 255, 0);
                    break;
                case > 100 and <= 150:
                    textQuality = "轻度污染";
                    dotColor = Color.FromRgb(255, 150, 0);
                    break;
                case > 150 and <= 200:
                    textQuality = "中度污染";
                    dotColor = Color.FromRgb(255, 0, 0);
                    break;
                case > 200 and <= 300:
                    textQuality = "重度污染";
                    dotColor = Color.FromRgb(153, 0, 76);
                    break;
                case > 300:
                    textQuality = "严重污染";
                    dotColor = Color.FromRgb(126, 0, 35);
                    break;
                default:
                    textQuality = "数据无效";
                    dotColor = Color.FromArgb(0, 255, 255, 255);
                    break;
            }

            AirQualityText = textQuality;
            DotFill = new SolidColorBrush(dotColor);
        }

        private string? GetNodeText(HtmlDocument document, string xpath)
        {
            var node = document.DocumentNode.SelectSingleNode(xpath);
            if (node == null)
                return null;

            return node.InnerText;
        }

        private string? GetNodeAttribute(HtmlDocument document, string xpath, string attrName)
        {
            var node = document.DocumentNode.SelectSingleNode(xpath);
            if (node == null)
                return null;

            return node.GetAttributeValue(attrName, null);
        }

        private HtmlNode? GetNode(HtmlDocument document, string xpath)
        {
            return document.DocumentNode.SelectSingleNode(xpath);
        }

        public override string ToString()
        {
            return $"城市：{City} 温度：{Temperature} 湿度：{Sd} 风向：{Wind} 空气质量：{AirQuality} {AirQualityText}";
        }

        public void GotoDetail()
        {
            Process process = new()
            {
                StartInfo = new()
                {
                    FileName = _webpage,
                    UseShellExecute = true
                }
            };
            process.Start();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
