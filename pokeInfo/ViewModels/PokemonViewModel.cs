using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using pokeInfo.Models;
using System.Threading.Tasks;
using PokeApiNet;
using Pokemon = pokeInfo.Models.Pokemon;

namespace pokeInfo.ViewModels
{
    public class PokemonViewModel : BaseViewModel
    {

        //private Dictionary<string, object> properties = new Dictionary<string, object>(); // dictionnaire pour faire correspondre les valeurs contenu dans la page d'ajout

        public ObservableCollection<Pokemon> Items
        {
            get { return GetValue<ObservableCollection<Pokemon>>(); }
            set { SetValue(value); }
        }


        private static PokemonViewModel _instance = new PokemonViewModel();
        public static PokemonViewModel Instance { get { return _instance; } }
        
        public void addPokemon(Pokemon pokemon)
        {
            Items.Add(pokemon);
        }


        public async void fillPokemonDatabase()
        {
            PokeApiClient pokeClient = new PokeApiClient();
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;

            var random = new Random();

            for (int i = 1; i <= 10; i++)
            {
                PokeApiNet.Pokemon pokemon = await Task.Run(() => pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(random.Next(1, 722)));

                await pokemonDB.AddPokemonAsync(new Pokemon
                {
                    ID = pokemon.Id,
                    Name = pokemon.Name,
                    ImgSrc = pokemon.Sprites.FrontShiny,
                    TypeColor = Constants.TypeColor[pokemon.Types[0].Type.Name.ToLower()],
                    Type = pokemon.Types[0].Type.Name.ToLower(),
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis faucibus imperdiet leo vitae bibendum. Vestibulum fermentum nunc ac eros vulputate.",
                    HP = pokemon.Stats[0].BaseStat,
                    ATK = pokemon.Stats[1].BaseStat,
                    DEF = pokemon.Stats[2].BaseStat,
                    SATK = pokemon.Stats[3].BaseStat,
                    SDEF = pokemon.Stats[4].BaseStat,
                    SPD = pokemon.Stats[5].BaseStat,
                    Height = (pokemon.Height / 10.0),
                    Weight = (pokemon.Weight / 10.0)
                });
            }

            

            this.fillPokemonList();
        }

        public async void fillPokemonList()
        {
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;
            List<Pokemon> pokemonList = pokemonDB.GetPokemonsAsync().Result;

            foreach (Pokemon pokemon in pokemonList)
            {
                Items.Add(pokemon);
            }
        }

        public async void init()
        {
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;
            if (pokemonDB.isPokemonDatabaseEmptyAsync().Result == true)
            {
                Console.WriteLine("------------- bd Vide -------------");
                this.fillPokemonDatabase();
            }
            else
            {
                this.fillPokemonList();
            }



        }

        public PokemonViewModel()
        {

            Items = new ObservableCollection<Pokemon>();
            this.init();

        }
    }
}