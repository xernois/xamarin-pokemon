using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokeApiNet;
using pokeInfo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using pokeInfo.Models;

namespace pokeInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPokemonePage : ContentPage
    {
        public AddPokemonePage()
        {
            InitializeComponent();
            pokemonNum.Text = String.Empty;
            pokemonName.Text = String.Empty;
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

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            if (pokemonName.Text == String.Empty)
            {
                frameName.BorderColor = Color.Red;
                Console.WriteLine("nom non !");
            }
            else
            {
                frameName.BorderColor = Color.FromHex("#00FBC1BC");
            }

            if (pokemonNum.Text == String.Empty)
            {
                frameNum.BorderColor = Color.Red;
                Console.WriteLine("numéro non !");
            }
            else
            {
                frameNum.BorderColor = Color.FromHex("#00FBC1BC");
            }

            if (pokemonName.Text != String.Empty && pokemonNum.Text != String.Empty)
            {
                var vm = PokemonViewModel.Instance;
                PokemonDatabase pokemonDB = await PokemonDatabase.Instance;

                Models.Pokemon pokemon = new Models.Pokemon
                {
                    Name = pokemonName.Text,
                    ID = Int32.Parse(pokemonNum.Text),
                    ImgSrc = "",
                    TypeColor = Constants.TypeColor[pickerType.SelectedItem.ToString().ToLower()],
                    Description = description.Text,
                    HP = 
                };

                vm.addPokemon(pokemon);
                await pokemonDB.AddPokemonAsync(pokemon);
                
                pokemonName.Text = String.Empty;
                pokemonNum.Text = String.Empty;
                pickerType.SelectedItem = null;

                await Shell.Current.GoToAsync($"//List", true);

                Console.WriteLine("ajout !");
            } 
        }
    }
}