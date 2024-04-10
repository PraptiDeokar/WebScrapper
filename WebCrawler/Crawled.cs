using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    public class Crawled
    {
        [Key]
        public int id { get; set; }

        public string MobileName { get; set; }
        public int Price { get; set; }
    }
}
