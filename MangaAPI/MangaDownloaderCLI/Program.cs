using MangaAPI;
using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace MangaDownloaderCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                MangaSite site = null;
                MangaFinder mangaFinder = null;

                Console.WriteLine("ChooseSite MintManga - 1, ReadManga - 2");
                var key = Console.ReadKey();

                if (key.KeyChar == '1')
                {
                    site = new MangaSite(MangaSites.MintManga);
                    mangaFinder = new MangaFinder(site);
                }
                else
                {
                    site = new MangaSite(MangaSites.ReadManga);
                    mangaFinder = new MangaFinder(site);
                }

                var mangaName = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "mangaName.txt"));

                Console.WriteLine(mangaName);

                var list = mangaFinder.GetTopMangaList();
                var manga = list.FirstOrDefault(i => i.Name == mangaName);

                if (manga != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MangaFinded");
                    Console.ForegroundColor = ConsoleColor.Black;

                    var parsedManga = ParsedManga.GetManga(site, manga.Url);

                    var mangaDirectory = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), list[11].Name));

                    int indexChapter = 0;
                    while (indexChapter < parsedManga.Chapters.Count)
                    {
                        try
                        {
                            using (var client = new WebClient())
                            {
                                var chapter = parsedManga.Chapters[indexChapter];
                                var chapterDirectory = Directory.CreateDirectory(Path.Combine(mangaDirectory.FullName, indexChapter.ToString()));

                                foreach (var image in chapter.GetMangaPages(site))
                                {
                                    client.DownloadFile(image.PageUrl, Path.Combine(chapterDirectory.FullName, image.PageUrl.Split('/').Last()));
                                }
                            }

                            indexChapter++;
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"Downloaded {indexChapter}");
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Exception {ex}");
                            Console.ForegroundColor = ConsoleColor.Black;
                            Task.Delay(TimeSpan.FromMinutes(10)).Wait();
                        }
                    }

                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine("-----------------------------------DOwnloaded-------------------------------------------");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Manga had not finded");
                    Console.ForegroundColor = ConsoleColor.Black;
                }
            }
        }
    }
}
