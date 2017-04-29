using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
