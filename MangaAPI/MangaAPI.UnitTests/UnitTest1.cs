using MangaAPI;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private MangaSite MangaSite;
        private MangaFinder MangaFinder;

        [SetUp]
        public void Setup()
        {
            MangaSite = new MangaSite( MangaSites.ReadManga);
            MangaFinder = new MangaFinder(MangaSite);
        }

        [Test]
        public void Test1()
        {
            var list = MangaFinder.GetMangaList(10);
        }
    }
}