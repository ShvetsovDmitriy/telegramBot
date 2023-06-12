using System;
using HtmlAgilityPack;

namespace TelegramBot
{
    public class ParsingSiteSpartak
    {
        private string result;
        
        public ParsingSiteSpartak()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://spartak.com/");                             
                                        
            HtmlNode additionalNode = doc.DocumentNode.SelectSingleNode("//div[@class='sc-aa5feb5c-6 fxWDXc']");
            HtmlNode additionalNode2 = doc.DocumentNode.SelectSingleNode("//div[@class='sc-aa5feb5c-2 lftVyk'][1]");
            HtmlNode additionalNode3 = doc.DocumentNode.SelectSingleNode("//div[@class='sc-aa5feb5c-2 lftVyk'][2]");
            
            if (additionalNode != null && additionalNode2 != null )
            {
                result = additionalNode2.InnerText + " : " + additionalNode3.InnerText
                 + Environment.NewLine
                 + additionalNode.InnerText;
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










