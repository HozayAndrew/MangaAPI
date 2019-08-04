using System;
using System.Collections.Generic;
using System.Text;

namespace MangaAPI
{
    public class Manga
    {
        public string Name { get; internal set; }
        public string Url { get; internal set; }
        public string PreviewImageUrl { get; internal set; }
        public string Rating { get; internal set; }
        public string State { get; internal set; }
        public List<Category> Categories { get; internal set; }
    }

    public enum Category
    {
        Shoujo,
        Art
    }

}
