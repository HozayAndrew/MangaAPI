using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Threading.Tasks;

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

            foreach (var rootNode in content.SelectNodes("div"))
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

        public ParsedManga GetParsedManga(string baseSiteUrl, string mangaUrl)
        {
            var parsedMange = new ParsedManga();

            var document = GetHtmlDocument(mangaUrl);
            var mangaRootNode = document.DocumentNode.SelectSingleNode("html");
            var body = mangaRootNode.SelectSingleNode("body");
            var leftContent = body.SelectSingleNode("//div[@class='leftContent']");

            var description = leftContent.SelectSingleNode("//meta[@itemprop='description']");
            parsedMange.Description = description.Attributes["content"].Value;

            var expandable = leftContent.SelectSingleNode("div[@class='expandable']");
            var flexRow = expandable.SelectSingleNode("div[@class='flex-row']");
            var hrefRoot = flexRow.SelectSingleNode("div[@class='subject-actions col-sm-7']");

            var link = hrefRoot.SelectSingleNode("a");
            var mangaReaderLink = baseSiteUrl + link.Attributes["href"].Value;

            parsedMange.Chapters = GetChapters(baseSiteUrl, mangaReaderLink);

            return parsedMange;
        }

        private List<Chapter> GetChapters(string baseSiteUrl, string readerUrl)
        {
            var chapters = new List<Chapter>();

            var document = GetHtmlDocument(readerUrl);
            var mangaRootNode = document.DocumentNode.SelectSingleNode("html");
            var body = mangaRootNode.SelectSingleNode("body");

            var content = body.SelectSingleNode("//div[@class='container pageBlock top-block']");
            var topControl = content.SelectSingleNode("//div[@class='topControl row']");
            var chaptersControl = topControl.SelectSingleNode("//div[@class='col-sm-5 col-xs-6']");
            var selectRoot = chaptersControl.SelectSingleNode("//div[@class='near-input-group']");

            var select = selectRoot.SelectSingleNode("select");

            foreach (var option in select.SelectNodes("option"))
            {
                var chapter = new Chapter();
                chapter.Name = option.InnerText;
                chapter.ChapterUrl = baseSiteUrl + option.Attributes["value"].Value;
                chapters.Add(chapter);
            }

            chapters.Reverse();

            return chapters;
        }

        public List<MangaPage> GetMangaPages(string chapterUrl)
        {
            var str = GetDocument(chapterUrl);

            var reg = Regex.Match(str, @"m_h.init\(([^)]*)\)").Groups[1];
            var listReg = Regex.Match(reg.Value, @"\[([^)]*)\]");
            var list = JsonConvert.DeserializeObject<List<List<string>>>(listReg.Value);

            var mangaPages = new List<MangaPage>();
            foreach (var item in list)
            {
                mangaPages.Add(new MangaPage
                {
                    Widht = int.Parse(item[3]),
                    Height = int.Parse(item[4]),
                    PageUrl = item[1] + item[2]
                });
            }

            return mangaPages;
        }
    }
}
