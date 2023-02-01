using GPU_Scraper.Data.Contracts;
using GPU_Scraper.Entities;
using GPUScraper.Models.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Globalization;
using System.Security.Policy;
using System.Text;

namespace GPU_Scraper.Data
{
    public class MoreleCrawler : ICrawler
    {
        const string MoreleBaseURL = "https://www.morele.net/kategoria/karty-graficzne-12/,,,,,,,,0,,,,8143O368064.1800730.1997842.470265.629277.1070067.976407.974080.1163157.955615.955614.1123985.1111434.1258915.2083200.1591770.1649860.1580461.1646742.1576222.1567369.1609819.1497277.1609833.1613601.1967000.1760879.1728675.1689348.1827217.1689340.1827334.1689334.2027819.2073326.2073322.2087937.2119318.2135123.2139481,8143O!2837.!2835,sprzedawca:m/1/?noi";
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


        private List<string> GetUrls()
        {
            var document = Web.Load(MoreleBaseURL);
            var urls = new List<string>() { MoreleBaseURL };
            var paginationSelector = "li.pagination-lg.next > link";

            if (document.QuerySelector(paginationSelector).Attributes["href"].Value != null)
            {
                var nextPageUrl = "https://www.morele.net" + document.QuerySelector(paginationSelector).Attributes["href"].Value.ToString();
                while (nextPageUrl != null)
                {
                    urls.Add(nextPageUrl);
                    var newDocument = Web.Load(nextPageUrl);
                    if (newDocument.QuerySelector(paginationSelector) != null)
                    {
                        nextPageUrl = "https://www.morele.net" + newDocument.QuerySelector(paginationSelector).Attributes["href"].Value.ToString();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return urls;
        }

        private List<Product> GetProducts(List<String> urls)
        {
            var gpusList = new List<Product>();

            foreach (var url in urls)
            {
                var page = Web.Load(url);
                var products = page.QuerySelectorAll(".cat-product.card");

                foreach (var product in products)
                {
                    var productName = string.Join(' ', product.QuerySelector(".cat-product-name > h2 > a")
                                                                .Attributes["title"]
                                                                .Value
                                                                .ToString()
                                                                .Split(' ')
                                                                .Skip(2));
                    var productPrice = decimal.Parse(product.QuerySelector(".price-new")
                                                                .InnerText
                                                                .ToString(nfi)
                                                                .Replace("zł", "")
                                                                .Replace("od", ""));

                    var character = "(";
                    if (productName.Contains(character))
                    {
                        productName = productName.Substring(0, productName.IndexOf(character)).Trim();
                    }

                    gpusList.Add(
                    new Product
                    {
                        Name = productName,
                        Price = productPrice,
                        Shop = "Morele"
                    });
                }
            }
            return gpusList;
        }
    }
}
