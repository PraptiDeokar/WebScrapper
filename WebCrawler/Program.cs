
using System.Diagnostics;
using HtmlAgilityPack;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;

namespace WebCrawler
{
    public class Program
    {
        
        private static async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(fullUrl);
            return response;
        }
        public static void main3(String[] args)   
        {
            Console.WriteLine("Enter URL");
            string input = Console.ReadLine();

            // string url = "https://en.wikipedia.org/wiki/List_of_programmers";
            string url = input;
            var response = CallUrl(url).Result;

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);

            //  var programmerLinks = htmlDoc.DocumentNode.SelectNodes("//li[not(contains(@class, 'tocsection'))]");
            var programmerLinks = htmlDoc.DocumentNode.Descendants("li")
               .Where(node => !node.GetAttributeValue("class", "").Contains("tocsection"))
               .ToList();

            List<string> wikiLink = new List<string>();

            foreach (var link in programmerLinks)
            {
                if (link.FirstChild.Attributes.Count > 0)
                    wikiLink.Add(url + link.FirstChild.Attributes[0].Value);
            }

            StringBuilder sb = new StringBuilder();
            foreach (var link in wikiLink)
            {
                sb.AppendLine(link);
            }

            System.IO.File.WriteAllText("links.csv", sb.ToString());

        }
    }
}