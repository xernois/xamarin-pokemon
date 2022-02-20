using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pokeInfo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace pokeInfo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonDetailPage : ContentPage
    {
        public PokemonDetailPage(Pokemon pokemon)
        {
            InitializeComponent();
            BindingContext = pokemon;
        }
        public void BackToList(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
    }
}