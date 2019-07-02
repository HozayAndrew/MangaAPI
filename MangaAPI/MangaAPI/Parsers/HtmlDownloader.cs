using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace MangaAPI.Parsers
{
    internal class HtmlDownloader
    {
        public HtmlDocument GetHtmlDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load(url);
        }
    }
}
