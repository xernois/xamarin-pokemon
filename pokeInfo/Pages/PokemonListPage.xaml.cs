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
        //Constructeur de la page
        //Entrées : 
        //Sorties :
        public PokemonListPage()
        {
            InitializeComponent();
            BindingContext = PokemonViewModel.Instance;
        }

        //Méthode qui retourne à la page précédente lorsque celle-ci disparait 
        //Entrées : 
        //Sorties :
        protected override void OnDisappearing()
        {
            //methode de base que l'on override 
            base.OnDisappearing();
            //retourne en arriere dans la pile de vue
            Navigation.PopAsync();
        }

        //methode executé quand un element de la liste est selectionné
        //Entrées : l'element selectionné et les arguments de l'event
        //Sorties :
        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //recuperation du pokemon selectionné
            Pokemon current = (e.CurrentSelection.FirstOrDefault() as Pokemon);
            // ne rien faire si le pokemon selectione est null
            if (current == null)
            {
                return;
            }
            // remet l'element selectionne a null pour pouvoir le reselectionner aussi tot
            (sender as CollectionView).SelectedItem = null;
            //affichage de la page de detail
            await Navigation.PushAsync(new PokemonDetailPage(current));
        }

        //methode executé pour modifier la la liste
        //Entrées : la searchBar et les arguments de l'event
        //Sorties :
        void onTextChange(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            // modification de la liste
            PokemonViewModel.Instance.fillPokemonList(PokemonViewModel.Instance.PokemonsList.ToList().Where(pokemon => pokemon.Name.ToUpper().Contains(e.NewTextValue.ToUpper())).ToList());
        }
       
    }
    
}