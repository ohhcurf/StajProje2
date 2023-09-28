using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace StajProje2.Classes
{
    public class ConsumableClass
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Lifetime { get; set; }
        public float SpawnRate { get; set; }
        public float Point { get; set; }
        public Color Color { get; set; }
        public bool Spawned { get; set; }
        public int Expand { get; set; }
        public int SpeedUp { get; set; }
        public int SpeedDown { get; set; }
    }
}
