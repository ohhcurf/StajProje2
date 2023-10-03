using StajProje2.Classes;
using StajProje2.Classes.StajProje2.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace StajProje2
{
    public class Paths
    {
        public static string MainPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public string ConsPath = Path.Combine(MainPath, "Maps\\consumables.txt");
        public string ScoreboardPath = Path.Combine(MainPath, "Maps\\scoreboard.txt");
        public string MapsPath = Path.Combine(MainPath, "Maps\\maps.txt");
        public string FolderPath = Path.Combine(MainPath, "Maps");

        public int lineNum;

        // Map verilerini oku
        public List<MapClass> ReadData_Map()
        {
            List<MapClass> maps = new List<MapClass> { };

            using (StreamReader sr = new StreamReader(MapsPath))
            {
                string line;
                List<string> lines = new List<string>();

                while ((line = sr.ReadLine()) != null)
                {
                    string[] cut = line.Split(';');

                    MapClass map = new MapClass()
                    {
                        Name = cut[0],
                    };
                    maps.Add(map);

                    lines.Add(line);
                }
                return maps;
            }
        }




        // Yem verilerini oku
        public List<ConsumableClass> ReadData_Consumables()
        {
            List<ConsumableClass> consumables = new List<ConsumableClass> { };

            using (StreamReader sr = new StreamReader(ConsPath))
            {
                string line;
                List<string> lines = new List<string>();

                while ((line = sr.ReadLine()) != null)
                {
                    string[] cut = line.Split(';');

                    ConsumableClass consumable = new ConsumableClass()
                    {
                        Name = cut[0],
                        Lifetime = float.Parse(cut[1]),
                        SpawnRate = float.Parse(cut[2]),
                        Point = float.Parse(cut[3]),
                        Color = ParseColorString(cut[4]),
                        Description = cut[5],
                        Spawned = false,
                        Expand = int.Parse(cut[6]),
                        SpeedDown = int.Parse(cut[7]),
                        SpeedUp = int.Parse(cut[8]),
                    };
                    consumables.Add(consumable);

                    lines.Add(line);
                }
                return consumables;
            }
        }





        // Skor tablosu verilerini oku
        public List<ScoreClass> ReadData_Scoreboard()
        {
            List<ScoreClass> scores = new List<ScoreClass> { };

            using (StreamReader sr = new StreamReader(ScoreboardPath))
            {
                string line;
                List<string> lines = new List<string>();

                while ((line = sr.ReadLine()) != null)
                {
                    string[] cut = line.Split(';');

                    ScoreClass score = new ScoreClass()
                    {
                        Username = cut[0],
                        Map = cut[1],
                        score = int.Parse(cut[2]),
                    };
                    scores.Add(score);

                    lines.Add(line);
                }
                return scores;
            }
        }




        // String'e çevrilmiş renk verisini okuyarak tekrardan renk oluşturma
        public static Color ParseColorString(string colorString)
        {
            Regex regex = new Regex(@"\[(A=\d+), (R=\d+), (G=\d+), (B=\d+)\]");
            Match match = regex.Match(colorString);

            if (match.Success)
            {
                int alpha = int.Parse(match.Groups[1].Value.Split('=')[1]);
                int red = int.Parse(match.Groups[2].Value.Split('=')[1]);
                int green = int.Parse(match.Groups[3].Value.Split('=')[1]);
                int blue = int.Parse(match.Groups[4].Value.Split('=')[1]);

                return Color.FromArgb(alpha, red, green, blue);
            }
            else
            {
                throw new FormatException("Geçersiz format.");
            }
        }




        // map.txt dosyasından mevcut mapleri okur
        public MapClass ReadData_Map(int amount, int rule)
        {
            MapClass newMap = new MapClass { };
            if (rule == 2) lineNum = 0;
            string maps = string.Empty;
            int maxLine = amount + lineNum;
            if (rule == 1) maxLine = int.MaxValue;

            using (StreamReader sr = new StreamReader(MapsPath))
            {
                string line;
                List<string> lines = new List<string>();

                for (int i = 0; i < lineNum; i++)
                {
                    sr.ReadLine();
                }

                while ((line = sr.ReadLine()) != null)
                {
                    string[] cut = line.Split(';');

                    newMap.Name = cut[0];
                    newMap.Image = cut[1];
                    if (cut.Length > 2) newMap.Obstacles = cut[2];

                    lines.Add(line);
                    lineNum++;

                    if (lineNum >= maxLine)
                    {
                        lines.Clear();
                        return newMap;
                    }
                }
                if (rule == 1)
                {
                    return newMap;
                }
                return null;
            }
        }
    }
}