using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using pokeInfo.Models;

namespace pokeInfo.ViewModels
{
    public class PokemonViewModel : INotifyPropertyChanged
    {

        //TODO: pk le type private l:14(private) & l:18(public) 

        private ObservableCollection<Pokemon> items; // la liste de nos pokemon

        public event PropertyChangedEventHandler PropertyChanged; // gestionnaire d'event

        public ObservableCollection<Pokemon> Items
        {
            get { return items; }
            set
            {
                items = value;
            }
        }









        public PokemonViewModel()
        {
            Items = new ObservableCollection<Pokemon>() {
                new Pokemon()
                {
                    PokemonID = 1,
                    Type = new string[] {"type1", "type 2" },
                    Name = "Bulbizzare",
                    ImageName = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/1.png"
                },
                new Pokemon()
                {
                    PokemonID = 2,
                    Type = new string[] {"type1", "type 2" },
                    Name = "Herbizzare",
                    ImageName = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/2.png"
                },
                new Pokemon()
                {
                    PokemonID = 3,
                    Type = new string[] {"type1", "type 2" },
                    Name = "Florizarre",
                    ImageName = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/3.png"
                },
                new Pokemon()
                {
                    PokemonID = 4,
                    Type = new string[] {"type1", "type 2" },
                    Name = "Salamèche",
                    ImageName = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/4.png"
                },
                new Pokemon()
                {
                    PokemonID = 5,
                    Type = new string[] {"type1", "type 2" },
                    Name = "Reptincel",
                    ImageName = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/5.png"
                },
                new Pokemon()
                {
                    PokemonID = 6,
                    Type = new string[] {"type1", "type 2" },
                    Name = "Dracaufeu",
                    ImageName = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/6.png"
                },
                new Pokemon()
                {
                    PokemonID = 7,
                    Type = new string[] {"type1", "type 2" },
                    Name = "Carapuce",
                    ImageName = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/7.png"
                },
                new Pokemon()
                {
                    PokemonID = 8,
                    Type = new string[] {"type1", "type 2" },
                    Name = "Carabaffe",
                    ImageName = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/8.png"
                },
                new Pokemon()
                {
                    PokemonID = 9,
                    Type = new string[] {"type1", "type 2" },
                    Name = "Tortank",
                    ImageName = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/9.png"
                },
                new Pokemon()
                {
                    PokemonID = 10,
                    Type = new string[] {"type1", "type 2" },
                    Name = "Chenipan",
                    ImageName = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/10.png"
                },


            };

        }
    }
}
