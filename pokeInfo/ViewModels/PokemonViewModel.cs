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

        //Création de l'observable 
        public ObservableCollection<Pokemon> Items
        {
            get { return GetValue<ObservableCollection<Pokemon>>(); }
            set { SetValue(value); }
        }

        // notre liste de reference pour la recherche notamment
        public List<Pokemon> PokemonsList = new List<Pokemon>();

        private static PokemonViewModel _instance = new PokemonViewModel();
        public static PokemonViewModel Instance { get { return _instance; } }

        //Méthode qui supprime un pokemon de la BDD et de la liste 
        //Entrées : le pokemon a supprimer
        //Sorties :
        public async void deletePokemon(Pokemon pokemon)
        {
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;

            //Suppression du pokemon dans la base
            await pokemonDB.DeletePokemonAsync(pokemon);
            //Modification de la liste de reference
            this.PokemonsList = this.PokemonsList.FindAll(poke => poke.ID != pokemon.ID);
            //Mise a jour de l'observable a partir de la liste de reference
            this.fillPokemonList();
        }
        //Méthode qui definie le contenu de l'observable
        //Entrées : une liste de pokemon
        //Sorties :
        public void fillPokemonList(List<Pokemon> list)
        {
            Items.Clear();
            foreach (Pokemon pokemon in list)
            {
                Items.Add(pokemon);
            }
        }
        //Méthode qui definie le contenu de l'observable a partir de la liste de reference
        //Entrées :
        //Sorties :
        public void fillPokemonList()
        {
            Items.Clear();
            foreach (Pokemon pokemon in this.PokemonsList)
            {
                Items.Add(pokemon);
            }
        }
        //Méthode qui initiallise la liste 
        //Entrées :
        //Sorties :
        public async void initPokemonList()
        {
            //Récuperation des pokemon dans la base 
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;
            foreach (Pokemon pokemon in pokemonDB.GetPokemonsAsync().Result)
            {
                // ajout du pokemon dans les listes
                Items.Add(pokemon);
                this.PokemonsList.Add(pokemon);
            }
        }
        //Methode qui ajoute remplis la base et la liste
        //Entrées :
        //Sorties :
        public async void initPokemonDatabaseAndList()
        {
            PokeApiClient pokeClient = new PokeApiClient();
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;

            for (int i = 1; i <= 50; i++)
            {
                //Recuperation du pokemon
                PokeApiNet.Pokemon pokemon = await Task.Run(() => pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(i));
                //Recuperation de son espece
                PokeApiNet.PokemonSpecies species = Task.Run(() => pokeClient.GetResourceAsync(pokemon.Species)).Result;

                //Instanciation du nouveau pokmeon
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

                //Ajout dans la base
                this.addPokemonToBase(nouveauPokemon);
                //Ajout dans la liste et dans la collection view
                this.addPokemonToList(nouveauPokemon);

            }
        }
        //Méthode qui ajoute un pokemon dans la BDD
        //Entrées : le pokemon a ajouter
        //Sorties :
        public async void addPokemonToBase(Pokemon pokemon)
        {
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;

            if(await pokemonDB.GetPokemonByIdAsync(pokemon.ID) != null) { 
                //Si le pokemon existe deja en base
                await pokemonDB.UpdatePokemonAsync(pokemon);
            }else
            {
                // si le pokemon n'existe pas en base
                await pokemonDB.AddPokemonAsync(pokemon);
            }
        }
        //Méthode qui dajoute un pokemon dans la liste
        //Entrées : le pokemon a ajouter
        //Sorties :
        public void addPokemonToList(Pokemon pokemon)
        {
            this.PokemonsList.Add(pokemon);
            Items.Add(pokemon);
        }
        //Méthode qui update le pokemon dans la liste
        //Entrées : le pokemon modifier
        //Sorties :
        public void replacePokemonInList(Pokemon pokemon)
        {
            int index = PokemonsList.FindIndex(poke => poke.ID == pokemon.ID);

            //Si un pokemon avec le meme id existe
            if (index != -1)
            {
                PokemonsList[index] = pokemon;
                fillPokemonList();
            }
        }
        //Méthode qui determine si la BDD doit etre replis ou non et remplis la liste
        //Entrées :
        //Sorties :
        public async void init()
        {
            PokemonDatabase pokemonDB = await PokemonDatabase.Instance;
            if (pokemonDB.isPokemonDatabaseEmptyAsync().Result == true) 
            {
                //Si la BDD est vide
                this.initPokemonDatabaseAndList();
            }
            else
            {
                //Si la BDD est pas vide
                this.initPokemonList();
            }
        }
        //Constructeur du ViewModel
        //Entrées :
        //Sorties :
        public PokemonViewModel()
        {
            Items = new ObservableCollection<Pokemon>();
            this.init();
        }
    }
}