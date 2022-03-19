using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pokeInfo.Models;
using pokeInfo.ViewModels;
using pokeInfo.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pokeInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonListPage : ContentPage
    {
        public PokemonListPage()
        {
            InitializeComponent();
            BindingContext = PokemonViewModel.Instance;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Console.WriteLine("test");
            Navigation.PopAsync();
            Console.WriteLine("test2");
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pokemon current = (e.CurrentSelection.FirstOrDefault() as Pokemon);
            if (current == null)
            {
                return;
            }
            (sender as CollectionView).SelectedItem = null;
            await Navigation.PushAsync(new PokemonDetailPage(current));
        }

        void onTextChange(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;

            PokemonViewModel.Instance.fillPokemonList(PokemonViewModel.Instance.PokemonsList.ToList().Where(pokemon => pokemon.Name.ToUpper().Contains(e.NewTextValue.ToUpper())).ToList());
        }
       
    }
    
}