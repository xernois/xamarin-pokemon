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

        public List<Pokemon> PokemonsList = new List<Pokemon>();

        private static PokemonViewModel _instance = new PokemonViewModel();
        public static PokemonViewModel Instance { get { return _instance; } }


        public async void deletePokemon(Pokemon pokemon)
        {
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;

            await pokemonDB.DeletePokemonAsync(pokemon);
            this.PokemonsList = this.PokemonsList.FindAll(poke => poke.ID != pokemon.ID);
            this.fillPokemonList();
        }

        public void fillPokemonList(List<Pokemon> list)
        {
            Items.Clear();
            foreach (Pokemon pokemon in list)
            {
                Items.Add(pokemon);
            }
        }
        public void fillPokemonList()
        {
            Items.Clear();
            foreach (Pokemon pokemon in this.PokemonsList)
            {
                Items.Add(pokemon);
            }
        }

        public async void initPokemonList()
        {
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;
            foreach (Pokemon pokemon in pokemonDB.GetPokemonsAsync().Result)
            {
                Items.Add(pokemon);
                this.PokemonsList.Add(pokemon);
            }
        }
        public async void initPokemonDatabaseAndList()
        {
            PokeApiClient pokeClient = new PokeApiClient();
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;

            for (int i = 1; i <= 50; i++)
            {
                //recuperation du pokemon
                PokeApiNet.Pokemon pokemon = await Task.Run(() => pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(i));
                //recuperation de son espece
                PokeApiNet.PokemonSpecies species = Task.Run(() => pokeClient.GetResourceAsync(pokemon.Species)).Result;

                // instanciation du nouveau pokmeon
                Pokemon nouveauPokemon = new Pokemon();

                nouveauPokemon.ID = pokemon.Id;
                nouveauPokemon.Name = species.Names.Find(name => name.Language.Name.Equals("fr")).Name;
                nouveauPokemon.ImgSrc = pokemon.Sprites.FrontShiny;
                nouveauPokemon.TypeColor = Constants.TypeInfos[pokemon.Types[0].Type.Name.ToLower()].Item1;
                nouveauPokemon.Type = Constants.TypeInfos[pokemon.Types[0].Type.Name.ToLower()].Item2;
                nouveauPokemon.Type2Color = pokemon.Types.Count >= 2 ? Constants.TypeInfos[pokemon.Types[1].Type.Name.ToLower()].Item1 : "#000";
                nouveauPokemon.Type2 = pokemon.Types.Count >= 2 ? Constants.TypeInfos[pokemon.Types[1].Type.Name.ToLower()].Item2 : null;
                nouveauPokemon.Description = species.FlavorTextEntries.FindLast(flavor => flavor.Language.Name.Equals("fr")).FlavorText.Replace("\n", " ");
                nouveauPokemon.HP = pokemon.Stats[0].BaseStat;
                nouveauPokemon.ATK = pokemon.Stats[1].BaseStat;
                nouveauPokemon.DEF = pokemon.Stats[2].BaseStat;
                nouveauPokemon.SATK = pokemon.Stats[3].BaseStat;
                nouveauPokemon.SDEF = pokemon.Stats[4].BaseStat;
                nouveauPokemon.SPD = pokemon.Stats[5].BaseStat;
                nouveauPokemon.Height = (pokemon.Height / 10.0);
                nouveauPokemon.Weight = (pokemon.Weight / 10.0);

                //ajout dans la base
                this.addPokemonToBase(nouveauPokemon);
                // ajout dans la liste et dans la collection view
                this.addPokemonToList(nouveauPokemon);

            }
        }


        public async void addPokemonToBase(Pokemon pokemon)
        {
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;

            if(await pokemonDB.GetPokemonByIdAsync(pokemon.ID) != null) { 
                await pokemonDB.UpdatePokemonAsync(pokemon);
            }else
            {
                await pokemonDB.AddPokemonAsync(pokemon);
            }
        }

        public void addPokemonToList(Pokemon pokemon)
        {
            this.PokemonsList.Add(pokemon);
            Items.Add(pokemon);
        }


        public void replacePokemonInList(Pokemon pokemon)
        {
            int index = PokemonsList.FindIndex(poke => poke.ID == pokemon.ID);

            if (index != -1)
            {
                PokemonsList[index] = pokemon;
                fillPokemonList();
            }
        }

        public async void init()
        {
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;
            if (pokemonDB.isPokemonDatabaseEmptyAsync().Result == true)
            {
                this.initPokemonDatabaseAndList();
            }
            else
            {
                this.initPokemonList();
            }
        }

        public PokemonViewModel()
        {
            Items = new ObservableCollection<Pokemon>();
            this.init();
        }
    }
}