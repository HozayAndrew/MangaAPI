using System;
using System.Collections.Generic;
using System.Text;

namespace MangaAPI
{
    public class MangaFinder : IMangaFinder, IMangaListsFinder
    {
        private MangaSite _site;

        public MangaFinder(MangaSite site)
        {
            _site = site;
        }

        public void FindManga(string name)
        {

        }

        public List<Manga> GetMangaList(int mangaCount, Category category)
        {
            return _site.Parser.GetMangeList(_site.BaseUrl, _site.MangaListUrl);
        }

        public List<Manga> GetMangaList(int mangaCount)
        {
            return _site.Parser.GetMangeList(_site.BaseUrl, _site.MangaListUrl);
        }

        public void RanodmManga()
        {

        }
    }
}
