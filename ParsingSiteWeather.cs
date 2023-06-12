using System;
using HtmlAgilityPack;

namespace TelegramBot
{
    public class ParsingSiteWeather
    {
        public HtmlNodeCollection nodes;
        private string result;
       
        public ParsingSiteWeather()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://pogoda.turtella.ru/montenegro/bar/sea_temperature");
            HtmlNode additionalNode = doc.DocumentNode.SelectSingleNode("//div[@class='seaTemp']");

            HtmlWeb web2 = new HtmlWeb();
            HtmlDocument doc2 = web.Load("https://world-weather.ru/pogoda/montenegro/bar/");
            HtmlNode additionalNode2 = doc2.DocumentNode.SelectSingleNode("//div[@id='weather-now-number']");


            if (additionalNode2 != null && additionalNode != null)
            {
                result = "Температура моря: " + additionalNode.InnerText + ", " + "Температура воздуха: " + additionalNode2.InnerText;
                result = result.Replace("&deg;C", "°");
            }
            else
            {
                result = "Не удалось найти данные на веб-странице.";
            }
        }
        public string Result
        {
            get
            {
                return result;
            }
        }
        
    }
}

