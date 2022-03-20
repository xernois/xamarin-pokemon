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
        private bool IsEdit = false;

        //Constructeur de la page
        //Entrées : un Pokemon
        //Sorties :
        public PokemonDetailPage(Pokemon poke)
        {
            pokemon = poke;

            InitializeComponent();
            BindingContext = pokemon;
            // masque la deuxieme frame pour ne pas que le deuxieme type soit visible quand le pokemon n'en a qu'un
            if (pokemon.Type2 == null)
            {
                    secondType.IsVisible = false;
            }
        }

        //Méthode qui retourne à la page précédente quand on quite la page plus si IsEdit vrai
        //Entrées :
        //Sorties :
        protected override void OnDisappearing()
        {
            if (!IsEdit)
            {

                //methode de base que l'on override 
                base.OnDisappearing();
                //retourne en arriere dans la pile de vue
                Navigation.PopAsync();
            }
        }

        //Méthode qui met IsEdit à faux à l'apparition de la page
        //Entrées :
        //Sorties :
        protected override void OnAppearing()
        {
            //methode de base que l'on overrid
            base.OnAppearing();
            IsEdit = false;
        }

        //Méthode qui retourne à la page liste
        //Entrées : élément qui trigger l'événement, arguments de l'événement
        //Sorties :
        public async void BackToList(object sender, EventArgs args)
        {
            //retour en arriere dans la pile de vue
            await Navigation.PopAsync();
        }

        //Méthode qui supprime un pokemon et reviens à la page précédente
        //Entrées : élément qui trigger l'événement, arguments de l'événement
        //Sorties :
        public void supprimerPokemon(object sender, EventArgs args)
        {
            Navigation.PopAsync();
            // suppression d'un pokemon
            PokemonViewModel.Instance.deletePokemon(pokemon);
        }

        //Méthode qui met IsEdit à vrai et ouvre une page pour le modifier 
        //Entrées : élément qui trigger l'événement, arguments de l'événement
        //Sorties :
        public void modifierPokemon(object sender, EventArgs args)
        {
            IsEdit = true;
            // affiche la page d'ajout mais avec un pokemon en parametre
            Navigation.PushAsync(new AddPokemonePage(pokemon));
        }

    }
}