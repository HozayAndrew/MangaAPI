using System;
using System.Collections.Generic;
using System.Text;

namespace MangaAPI
{
    interface IMangaListsFinder
    {
        List<Manga> GetMangaList(int mangaCount);
        List<Manga> GetMangaList(int mangaCount, Category category);
    }
}
