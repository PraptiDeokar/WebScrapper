using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    public class abcd
    {
        private readonly AppDbContext _db;

        public abcd(AppDbContext db)
        {
            _db = db;
        }


        private static async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(fullUrl);
            return response;
        }
        public static void Main(String[] args)
        {
            Console.WriteLine("Enter URL");
            string input = Console.ReadLine();

            // string url = "https://en.wikipedia.org/wiki/List_of_programmers";
            string url = input;
            var response = CallUrl(url).Result;

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);

            //  var programmerLinks = htmlDoc.DocumentNode.SelectNodes("//li[not(contains(@class, 'tocsection'))]");
            // var programmerLinks = htmlDoc.DocumentNode.SelectNodes("//a[@class='name clamp-2']/h2");
            var modelName = htmlDoc.DocumentNode.SelectNodes("//a[@class='name clamp-2']/h2");
            
            var price = htmlDoc.DocumentNode.SelectNodes("//span[@class='price']");
           // Console.WriteLine(modelName + " " + price);

            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();


            foreach (var name in modelName)
            {
                list1.Add(name.InnerText);
            }

            foreach (var name in price)
            {
                string p = name.InnerText.ToString();
                string s = (p as string).Replace("₹", String.Empty);
                list2.Add(s);  
            }

            foreach (var name in list1)
            {
                Console.WriteLine(name);
            }

            foreach (var name in list2)
            {
                Console.WriteLine(name);
            }

            StringBuilder sb = new StringBuilder();
            //foreach (var link in modelName)
            //{
            //    sb.AppendLine(link);
            //}


            for(int i = 0; i < list1.Count; i++)
            {
                sb.AppendLine(list1[i] + " "+ (list2[i].ToString()));
            }




            //StringBuilder sb = new StringBuilder();
            //for (int i=0; i<list1.Count; i++)
            //{
            //    if (i >= list2.Count) break;
            //    var rec1 = list1[i];
            //    var rec2 = list2[i];
            //    rec1.NewColumn = rec2.ColumnToAdd;
            //}

            System.IO.File.WriteAllText("links.csv", sb.ToString());

        }
    }
}
