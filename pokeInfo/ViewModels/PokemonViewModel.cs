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

        

        public async Task<ObservableCollection<Pokemon>> initPokemon()
        {
            PokeApiClient pokeClient = new PokeApiClient();

            var Items = new ObservableCollection<Pokemon>();

            for (int i = 1; i <=5; i++) {
                Items.Add(await Task.Run(() => pokeClient.GetResourceAsync<Pokemon>(i)));
            };

            return Items;
        }
     
        public PokemonViewModel()
        {

           Items = this.initPokemon().Result;

        }
    }
}
