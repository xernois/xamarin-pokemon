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

        public ObservableCollection<Pokemon> Items
        {
            get { return GetValue<ObservableCollection<Pokemon>>(); }
            set { SetValue(value); }
        }

        public List<Pokemon> PokemonsList;

        private static PokemonViewModel _instance = new PokemonViewModel();
        public static PokemonViewModel Instance { get { return _instance; } }
        

        public async void deletePokemon(Pokemon pokemon)
        {
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;
            await pokemonDB.DeletePokemonAsync(pokemon);
            PokemonsList = PokemonsList.FindAll(poke => poke.ID != pokemon.ID);
            Items.Clear();
            foreach(var poke in PokemonsList)
            {
                Items.Add(poke);
            }
        }

        public void addPokemon(Pokemon pokemon)
        {
            Items.Add(pokemon);
        }

        public void fillPokemonList(List<Pokemon> list)
        {
            Items.Clear();
            foreach (Pokemon pokemon in list)
            {
                Items.Add(pokemon);
            }
        }
        public async void fillPokemonList()
        {
            Items.Clear();
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;
            PokemonsList = pokemonDB.GetPokemonsAsync().Result;
            foreach (Pokemon pokemon in PokemonsList)
            {
                Items.Add(pokemon);
            }
        }
        public async void fillPokemonDatabase()
        {
            PokeApiClient pokeClient = new PokeApiClient();
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;

            var random = new Random();

            for (int i = 1; i <= 10; i++)
            {
                PokeApiNet.Pokemon pokemon = await Task.Run(() => pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(random.Next(1, 722)));
                PokeApiNet.PokemonSpecies species = Task.Run(() => pokeClient.GetResourceAsync(pokemon.Species)).Result;

                await pokemonDB.AddPokemonAsync(new Pokemon
                {
                    ID = pokemon.Id,
                    Name = species.Names.Find(name => name.Language.Name.Equals("fr")).Name,
                    ImgSrc = pokemon.Sprites.FrontShiny,
                    TypeColor = Constants.TypeInfos[pokemon.Types[0].Type.Name.ToLower()].Item1,
                    Type =  Constants.TypeInfos[pokemon.Types[0].Type.Name.ToLower()].Item2,
                    Type2Color = pokemon.Types.Count >= 2 ? Constants.TypeInfos[pokemon.Types[1].Type.Name.ToLower()].Item1 : "#000",
                    Type2 = pokemon.Types.Count >= 2 ? Constants.TypeInfos[pokemon.Types[1].Type.Name.ToLower()].Item2 : null,
                    Description = species.FlavorTextEntries.FindLast(flavor => flavor.Language.Name.Equals("fr")).FlavorText.Replace("\n", " "),
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

        public async void init()
        {
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;
            if (pokemonDB.isPokemonDatabaseEmptyAsync().Result == true)
            {
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