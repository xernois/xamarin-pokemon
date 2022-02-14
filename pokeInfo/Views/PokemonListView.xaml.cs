using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pokeInfo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pokeInfo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonListView : ContentView
    {
        public PokemonListView()
        {
            InitializeComponent();
            BindingContext = PokemonViewModel.Instance;
        }
    }
}