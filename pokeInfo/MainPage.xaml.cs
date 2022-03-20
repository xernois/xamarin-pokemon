using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace pokeInfo
{
    public partial class MainPage : Shell
    {
        //Contructeur de la page principale l'app
        //Entrées :
        //Sorties :
        public MainPage()
        {
            InitializeComponent();
            InitPageHome();
        }

        //Fonction pour initialiser la main page et definissant la page par defaut
        //Entrées :
        //Sorties : 
        private void InitPageHome()
        {
            main_tab_bar.CurrentItem = main_tab_bar_home;
        }
    }
}
