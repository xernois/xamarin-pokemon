using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pokeInfo.Models;
using pokeInfo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace pokeInfo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonDetailPage : ContentPage
    {

        Pokemon pokemon;

        public PokemonDetailPage(Pokemon poke)
        {

            pokemon = poke;

            InitializeComponent();
            BindingContext = pokemon;

            if (pokemon.Type2 == null)
            {
                    secondType.IsVisible = false;
            }

        }
        public async void BackToList(object sender, EventArgs args)
        {

            Navigation.PopAsync();
        }

        public void supprimerPokemon(object sender, EventArgs args)
        {
            Navigation.PopAsync();
            PokemonViewModel.Instance.deletePokemon(pokemon);
        }

        public void modifierPokemon(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AddPokemonePage(pokemon));
        }

    }
}