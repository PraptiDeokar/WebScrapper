using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        private static async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(fullUrl);
            return response;
        }
        public static void Main(String[] args)
        {

            AppDbContext db = new AppDbContext();

            Console.WriteLine("Enter URL");
            string input = Console.ReadLine();
            var response = CallUrl(input).Result;
            
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
            
            var modelName = htmlDoc.DocumentNode.SelectNodes("//a[@class='name clamp-2']/h2");
            var price = htmlDoc.DocumentNode.SelectNodes("//span[@class='price']");

            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();

            foreach (var name in modelName)
            {
                list1.Add(name.InnerText);
            }

            foreach (var name in price)
            {
                string p = name.InnerText.ToString();
                string x = (p as string).Replace("₹", String.Empty);
                string s = (x as string).Replace(",", String.Empty);
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

            //for saving to db
            for (int i = 0; i < list1.Count; i++)
            {
                Crawled obj = new Crawled();

                obj.MobileName = list1[i].ToString();
                obj.Price = Convert.ToInt32(list2[i].ToString());
                db.crawlData.Add(obj);
                db.SaveChanges();

            }


            //to save data in csv

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < list1.Count; i++)
            {
                sb.AppendLine(list1[i] + " " + (list2[i].ToString()));
            }
            System.IO.File.WriteAllText("links.csv", sb.ToString());

        }
    }
}
