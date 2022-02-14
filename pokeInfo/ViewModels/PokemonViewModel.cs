using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using PokeApiNet;

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



        public async void initPokemon()
        {
            PokeApiClient pokeClient = new PokeApiClient();

            var random = new Random();

            for (int i = 1; i < 20; i++)
            {
                Items.Add(await Task.Run(() => pokeClient.GetResourceAsync<Pokemon>(random.Next(1, 722))));
            }

        }

        public void addPokemon(Pokemon pokemon)
        {
            Items.Add(pokemon);
        }

        public PokemonViewModel()
        {

            Items = new ObservableCollection<Pokemon>();
            this.initPokemon();

        }
    }
}