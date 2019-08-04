using System;
using System.Collections.Generic;
using System.Text;

namespace MangaAPI
{
    interface IMangaListsFinder
    {
        List<Manga> GetTopMangaList();
        List<Manga> GetMangaList(int mangaCount, Category category);
    }
}
