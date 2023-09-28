using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace StajProje2
{
    public class Paths
    {
        public static string MainPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public string ConsPath = Path.Combine(MainPath, "Maps\\consumables.txt");
        public string ScoreboardPath = Path.Combine(MainPath, "Maps\\scoreboard.txt");
        public string MapsPath = Path.Combine(MainPath, "Maps\\maps.txt");
        public string FolderPath = Path.Combine(MainPath, "Maps");
    }
}
