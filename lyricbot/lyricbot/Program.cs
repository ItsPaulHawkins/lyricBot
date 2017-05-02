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


namespace lyricbot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Name a song, any song.");
            string song = Console.ReadLine();
            string converted = song.Replace(" ", "+");
            Console.ReadLine();
            string webdata = new WebClient().DownloadString(new Uri("https://genius.com/search?q=" + converted));
            int pos = webdata.IndexOf("song_link");
            var lineNumber = webdata.Substring(0, pos).Count(c => c == '\n') + 1;

            Console.WriteLine(lineNumber);
            var array = webdata.Split('\n');
            string link = array[lineNumber - 1];
            string[] splitLink = link.Split();
            Int32 startIdx = link.IndexOf("https://");
            Int32 endIdx = link.IndexOf("\"", startIdx);
            string strippedLink = link.Substring(startIdx, endIdx - startIdx); //strips the link to a regular one. uses the variable link
            string lyricdata = new WebClient().DownloadString(new Uri(""+ strippedLink));
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(lyricdata);
            string result = htmlDoc.DocumentNode.InnerText;
            result = WebUtility.HtmlDecode(result);
            Int32 startIndex = result.IndexOf("<!--sse-->");
            Int32 EndIndex = result.IndexOf("<!--/sse-->");
            string finalLyrics = result.Substring(startIndex, EndIndex - startIndex);
            finalLyrics = finalLyrics.Replace("<!--sse-->", " ");
            finalLyrics = finalLyrics.Replace("â?T", "");
            Console.WriteLine(finalLyrics);
            Console.ReadLine();


        }

    }
}
