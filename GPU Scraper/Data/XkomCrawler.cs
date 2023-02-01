using GPU_Scraper.Entities;
using GPUScraper.Models.Models;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;
using System.Globalization;
using Newtonsoft.Json;
using GPU_Scraper.Data.Contracts;
using System.Security.Policy;

namespace GPU_Scraper.Data
{
    public class XkomCrawler : ICrawler
    {
        const string XKomBaseURL = "https://www.x-kom.pl/g-5/c/345-karty-graficzne.html?f1702-uklad-graficzny=24826-amd-radeon&f1702-uklad-graficzny=24827-nvidia-geforce&f1702-uklad-graficzny=262522-intel-arc";
        HtmlWeb Web = new HtmlWeb();
        NumberFormatInfo nfi = new CultureInfo("pl-PL", false).NumberFormat;

        public async Task<List<Product>> CrawlProducts()
        {
            var urls = GetUrls();
            var products = GetProducts(urls);

            if (products == null)
            {
                throw new Exception();
            }

            return products;

        }

        private List<Product> GetProducts(List<string> urls)
        {
            var gpusList = new List<Product>();
            NumberFormatInfo nfi = new CultureInfo("pl-PL", false).NumberFormat;

            foreach (var url in urls)
            {
                var page = Web.Load(url);
                var products = page.QuerySelectorAll(".gyHdpL");

                foreach (var product in products)
                {
                    var productName = String.Join(' ', product.QuerySelector("a > h3").Attributes["title"].Value.ToString().Split(' ').Skip(3)).Trim();
                    var productPrice = decimal.Parse(product.QuerySelector(".gAlJbD > span.guFePW").InnerText.ToString(nfi).Replace("zł", "").Replace("od", ""));

                    gpusList.Add(
                        new Product
                        {
                            Name = productName,
                            Price = productPrice,
                            Shop = "X-Kom"
                        });
                }
            }
            return gpusList;
        }

        private List<string> GetUrls()
        {
            var document = Web.Load(XKomBaseURL);
            var urls = new List<string>() { XKomBaseURL };

            if (document.QuerySelector("a.kGuktN").Attributes["href"].Value != null)
            {
                var nextPageUrl = "https://x-kom.pl/" + document.QuerySelector("a.kGuktN").Attributes["href"].Value.ToString();
                while (nextPageUrl != null)
                {
                    urls.Add(nextPageUrl);
                    var newDocument = Web.Load(nextPageUrl);
                    if (newDocument.QuerySelector(".ieNAbN") != null)
                    {
                        break;
                    }
                    else
                    {
                        nextPageUrl = "https://x-kom.pl/" + newDocument.QuerySelector("a.kGuktN").Attributes["href"].Value.ToString();
                    }
                }
            }

            return urls;
        }
    }
}
