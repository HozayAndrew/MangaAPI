using MangaAPI;
using NUnit.Framework;
using System.IO;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Diagnostics;

namespace Tests
{
    public class Tests
    {
        private MangaSite ReadMangaSite;
        private MangaSite MintMangaSite;

        private MangaFinder ReadMangaFinder;
        private MangaFinder MintMangaFinder;

        [SetUp]
        public void Setup()
        {
            MintMangaSite = new MangaSite(MangaSites.MintManga);
            ReadMangaSite = new MangaSite(MangaSites.ReadManga);
            ReadMangaFinder = new MangaFinder(ReadMangaSite);
            MintMangaFinder = new MangaFinder(MintMangaSite);
        }

        [Test]
        public void Test1()
        {
            var list = ReadMangaFinder.GetTopMangaList();
        }

        [Test]
        public void Test2()
        {
            var list = ReadMangaFinder.GetTopMangaList();
            var parsedManga = ParsedManga.GetManga(ReadMangaSite, list[0].Url);
            var chepterPages = parsedManga.Chapters[0].GetMangaPages(ReadMangaSite);
        }

        [Test]
        public void MintListTest()
        {
            var list = MintMangaFinder.GetTopMangaList();
        }

        [Test]
        public async void DownloadManga()
        {
            

        }
    }
}