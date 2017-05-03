using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace lyricbot
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {

                try {
                    Maximize();
                    Console.WriteLine("Name a song, or say quit to exit.");
                    string song = Console.ReadLine();
                    if (song == "quit" || song == "Quit")
                    {
                        Environment.Exit(0);
                    }
                    string converted = song.Replace(" ", "+"); //replaces the spaces with +'s
                    string webdata = new WebClient().DownloadString(new Uri("https://genius.com/search?q=" + converted)); //searches for the song, chooses the first option.
                    int pos = webdata.IndexOf("song_link");
                    var lineNumber = webdata.Substring(0, pos).Count(c => c == '\n') + 1;
                    var array = webdata.Split('\n');
                    string link = array[lineNumber - 1];
                    string[] splitLink = link.Split();              //not sure what I did here
                    Int32 startIdx = link.IndexOf("https://");
                    Int32 endIdx = link.IndexOf("\"", startIdx);
                    string strippedLink = link.Substring(startIdx, endIdx - startIdx); //strips the link to a regular one. uses the variable link
                    string lyricdata = new WebClient().DownloadString(new Uri("" + strippedLink));
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(lyricdata);
                    string result = htmlDoc.DocumentNode.InnerText;
                    result = WebUtility.HtmlDecode(result);
                    Int32 startIndex = result.IndexOf("<!--sse-->");
                    Int32 EndIndex = result.IndexOf("<!--/sse-->");
                    string finalLyrics = result.Substring(startIndex, EndIndex - startIndex);
                    finalLyrics = finalLyrics.Replace("<!--sse-->", " ");
                    finalLyrics = finalLyrics.Replace("â?T", "'");
                    Console.WriteLine(finalLyrics);

                }
                catch
                {
                    Console.WriteLine("Error, please try again");
                }
                }
        }
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        private static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }
    }
}
