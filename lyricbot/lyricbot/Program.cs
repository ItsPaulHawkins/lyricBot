using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;   

namespace lyricbot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Name a song, any song.");
            string song = Console.ReadLine();
            string converted = song.Replace(" ", "+");
            Console.WriteLine(converted);
            Console.ReadLine();
            string webdata = new WebClient().DownloadString(new Uri("https://genius.com/search?q=" + converted));
            Console.WriteLine(webdata);
            int pos = webdata.IndexOf("song_link");
            var lineNumber = webdata.Substring(0, pos).Count(c => c == '\n') + 1;

            Console.WriteLine(lineNumber);
            var array = webdata.Split('\n');
            string link = array[lineNumber - 1];
            string[] splitLink = link.Split();
            int i = 0;
            int ii = 1;
            string ay = strip(link);
            Console.WriteLine(ay);

            Console.WriteLine(i);
            Console.WriteLine(ii);
            Console.ReadLine();
        }
        public static string strip(string input)
        {
            Int32 startIdx = input.IndexOf("href=\"");
            if (startIdx < 0) return null;
            Int32 endIdx = input.IndexOf("\"", startIdx);
            if (endIdx < 0) return null;
            return input.Substring(startIdx, endIdx - startIdx);
        }
    }
}
