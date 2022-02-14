using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokeApiNet;
using pokeInfo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pokeInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPokemonePage : ContentPage
    {
        public AddPokemonePage()
        {
            
            InitializeComponent();
        }

        public void OnHpSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            int value = (int)args.NewValue;
            Slider slider = (Slider)sender;
            hp_label.Text = value.ToString();
        }

        public void OnAtkSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            int value = (int)args.NewValue;
            Slider slider = (Slider)sender;
            atk_label.Text = value.ToString();
        }

        public void OnDefSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            int value = (int)args.NewValue;
            Slider slider = (Slider)sender;
            def_label.Text = value.ToString();
        }

        public void OnSatkSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            int value = (int)args.NewValue;
            Slider slider = (Slider)sender;
            satk_label.Text = value.ToString();
        }

        public void OnSdefSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            int value = (int)args.NewValue;
            Slider slider = (Slider)sender;
            sdef_label.Text = value.ToString();
        }

        public void OnSpdSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            int value = (int)args.NewValue;
            Slider slider = (Slider)sender;
            spd_label.Text = value.ToString();
        }

        public void OnButtonClicked(object sender, EventArgs args)
        {
            //tentative d'ajout d'un pokemon vide pour tester
            Pokemon pokemon = new Pokemon();
            var vm = new PokemonViewModel();
        }
    }
}