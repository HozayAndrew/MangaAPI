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
            throw new NotImplementedException();
        }

        public List<Manga> GetMangaList(int mangaCount, Category category)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// get 1400 top manga
        /// </summary>
        /// <returns></returns>
        public List<Manga> GetTopMangaList()
        {
            List<Manga> mangaList = new List<Manga>();

            for (int i = 0; i < 10; i++)
            {
                var currentPageManga = _site.Parser.GetMangeList(_site.BaseUrl, _site.MangaListUrl + $"?offset={i * 70}");
                mangaList.AddRange(currentPageManga);
            }

            return mangaList;
        }

        public void RandomManga()
        {
            throw new NotImplementedException();
        }
    }
}
