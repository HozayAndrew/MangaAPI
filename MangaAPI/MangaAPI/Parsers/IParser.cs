using System;
using System.Collections.Generic;
using System.Text;

namespace MangaAPI.Parsers
{
    internal interface IParser
    {
        List<Manga> GetMangeList(string baseUrl, string listUrl);
        ParsedManga GetParsedManga(string baseSiteUrl, string mangaUrl);
        List<MangaPage> GetMangaPages(string chapterUrl);
    }
}
