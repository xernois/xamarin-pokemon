using pokeInfo.Models;
using System.Collections.Generic;

namespace pokeInfo.Models
{
    public class Pokemon
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImgSrc { get; set; }
        public string TypeColor { get; set; }
        public string Type { get; set; }
        public string Type2Color { get; set; }
        public string Type2 { get; set; }
        public string Description { get; set; }
        public int HP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int SATK { get; set; }
        public int SDEF { get; set; }
        public int SPD { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
    }
}
