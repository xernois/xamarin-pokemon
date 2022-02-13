using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using PokeApiNet;

namespace pokeInfo.ViewModels
{
    public class PokemonViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Pokemon> items = new ObservableCollection<Pokemon>(); // la liste de nos pokemon

        public event PropertyChangedEventHandler PropertyChanged; // gestionnaire d'event

        public ObservableCollection<Pokemon> Items
        {
            get { return items; }
            set
            {
                items = value;
            }
        }   
        public async void initPokemon()
        {
            PokeApiClient pokeClient = new PokeApiClient();

            var random = new Random();

            for (int i = 1; i < 20; i++)
            {
                
                Items.Add(await Task.Run(() => pokeClient.GetResourceAsync<Pokemon>(random.Next(1,722))));

            }

        }


        public PokemonViewModel()
        {
            Console.WriteLine("----------------Début--------------");
            this.initPokemon();

        }
    }
}
