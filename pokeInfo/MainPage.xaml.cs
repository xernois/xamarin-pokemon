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
        public MainPage()
        {
            InitializeComponent();
            InitPageHome();
        }

        private void InitPageHome()
        {
            main_tab_bar.CurrentItem = main_tab_bar_home;
        }
    }
}
