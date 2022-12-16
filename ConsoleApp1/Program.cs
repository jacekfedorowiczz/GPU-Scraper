// See https://aka.ms/new-console-template for more information
using GPU_Scraper.Entities;
using HtmlAgilityPack;

ScrapGPUs();

void ScrapGPUs()
{
    string XKomBaseURL = "https://www.x-kom.pl/g-5/c/345-karty-graficzne.html?f1702-uklad-graficzny=24826-amd-radeon&f1702-uklad-graficzny=24827-nvidia-geforce&f1702-uklad-graficzny=262522-intel-arc";
    var web = new HtmlWeb();
    var document = web.Load(XKomBaseURL);

    var products = document.QuerySelectorAll(".sc-3g60u5-0.dNgqYV > a.sc-1h16fat-0.dNrrmO");

    foreach (var product in products)
    {
        var productHref = product.QuerySelector("a").Attributes["href"].Value;
        Console.WriteLine($"{productHref.ToString()}");
    }
}