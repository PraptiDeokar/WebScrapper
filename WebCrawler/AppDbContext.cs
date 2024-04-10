using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    public class AppDbContext: DbContext
    {
        public DbSet<Crawled> crawlData {  get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-AAJRE23\SQLEXPRESS;Database=WebScraper;Trusted_Connection=True;MultipleActiveResultSets=true; TrustServerCertificate=True");
        }
    }
}
