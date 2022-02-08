using System;

namespace pokeInfo.Models
{
    public class Pokemon
    {
        public int PokemonID { get; set; }
        public string[] Type { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
    }
}
