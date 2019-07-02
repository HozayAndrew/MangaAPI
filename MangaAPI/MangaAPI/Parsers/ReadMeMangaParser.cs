using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace MangaAPI.Parsers
{
    class ReadMeMangaParser : HtmlDownloader, IParser
    {
        public List<Manga> GetMangeList(string baseSiteUrl, string listUrl)
        {
            List<Manga> mangaList = new List<Manga>();

            var document = GetHtmlDocument(listUrl);
            var mangaRootNode = document.DocumentNode.SelectSingleNode("html");
            var body = mangaRootNode.SelectSingleNode("body");
            var content = body.SelectSingleNode("//div[@class='tiles row']");

            foreach (var rootNode in content.ChildNodes)
            {
                var descriptionNode = rootNode.SelectSingleNode("div[@class='desc']");
                var imgNode = rootNode.SelectSingleNode("//div[@class='img']");

                if (descriptionNode != null && imgNode != null)
                {
                    Manga manga = new Manga();

                    var titleName = descriptionNode.SelectSingleNode("h3");
                    var link = titleName.SelectSingleNode("a");
                    manga.Name = link.Attributes["title"].Value;
                    manga.Url = baseSiteUrl + link.Attributes["href"].Value;

                    var ratingRoot = descriptionNode.SelectSingleNode("div");
                    var rating = ratingRoot.SelectSingleNode("div");
                    manga.Rating = rating.Attributes["title"].Value;

                    var imgLink = imgNode.SelectSingleNode("a");
                    var img = imgLink.SelectSingleNode("img");
                    manga.PreviewImageUrl = img.Attributes["data-original"].Value;

                    mangaList.Add(manga);
                }
            }

            return mangaList;
        }
    }
}
