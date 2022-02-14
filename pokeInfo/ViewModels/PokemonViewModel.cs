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

        public event PropertyChangedEventHandler PropertyChanged; // gestionnaire d'event

        private ObservableCollection<Pokemon> items = new ObservableCollection<Pokemon>(); // la liste de nos pokemon

        private Dictionary<string, object> properties = new Dictionary<string, object>(); // dictionnaire pour faire correspondre les valeurs contenu dans la page d'ajout
       /* protected void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (!properties.ContainsKey(propertyName))
            {
                properties.Add(propertyName, default(T));
            }

            var oldValue = GetValue<T>(propertyName);
            if (!EqualityComparer<T>.Default.Equals(oldValue, value))
            {
                properties[propertyName] = value;
                OnPropertyChanged(propertyName);
            }
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            if (!properties.ContainsKey(propertyName))
            {
                return default(T);
            }
            else
            {
                return (T)properties[propertyName];
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
       */

        //list contenant la liste de nos pokemon dans lapp
        public ObservableCollection<Pokemon> Items
        {
            get { return items; }
            set
            {
                items = value;
            }
        }   

        // recuperation des pokemons depuis l'api
        public async void initPokemon()
        {
           PokeApiClient pokeClient = new PokeApiClient();

            var random = new Random();

            for (int i = 1; i < 20; i++)
            { 
                Items.Add(await Task.Run(() => pokeClient.GetResourceAsync<Pokemon>(random.Next(1,722))));
            }

        }

        // construteur de notre view model
        public PokemonViewModel()
        {
            this.initPokemon();

        }
    }
}
