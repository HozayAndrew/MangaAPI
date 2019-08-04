using MangaAPI.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MangaAPI
{
    public class ParsedManga
    {
        public static ParsedManga GetManga(MangaSite mangaSite, string mangaUrl)
        {
            return mangaSite.Parser.GetParsedManga(mangaSite.BaseUrl, mangaUrl);
        }

        public string Description { get; set; }
        public List<Chapter> Chapters { get; set; }
    }
}
