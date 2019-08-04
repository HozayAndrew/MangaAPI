using MangaAPI.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MangaAPI
{
    public class Chapter
    {
        public string ChapterUrl { get; set; }
        public string Name { get; set; }

        public List<MangaPage> GetMangaPages(MangaSite site)
        {
            return site.Parser.GetMangaPages(ChapterUrl);
        }
    }
}
