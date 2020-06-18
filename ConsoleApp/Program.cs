using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ASPNETCoreMVA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            PrepareTorrentsList();
        }

        private static void PrepareTorrentsList()
        {
            List<string> locations = File.ReadAllLines(@"D:\Git\sapatelbaps\ASPNETCoreMVA\ConsoleApp\Locations.txt").ToList();
            string outputFile = @"D:\Git\sapatelbaps\ASPNETCoreMVA\ConsoleApp\AllTorrents.txt";
            StringBuilder content = new StringBuilder();

            foreach (string path in locations)
            {
                List<string> items = new List<string>();
                items.AddRange(Directory.GetDirectories(path).Select(f => path + "~" + Path.GetFileName(f)).ToList());
                items.AddRange(Directory.GetFiles(path).Select(f => path + "~" + Path.GetFileName(f)).ToList());

                content.AppendLine(path);
                items.ForEach(i => content.AppendLine(i));
            }

            File.WriteAllText(outputFile, content.ToString());
        }
    }
}
