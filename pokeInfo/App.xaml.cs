using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pokeInfo
{
    public partial class App : Application
    {
        //Initialise l'app et met MainPage en page d'accueil
        //Entrées :
        //Sorties :
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        //Fonction qui ne font rien mais qui sont implementées
        protected override void OnStart(){}

        protected override void OnSleep(){}

        protected override void OnResume(){}
    }
}
