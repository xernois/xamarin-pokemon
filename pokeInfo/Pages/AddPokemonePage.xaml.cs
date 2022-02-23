﻿using System;
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
            String str = "00" + value.ToString();
            hp_label.Text = str.Substring(str.Length - 3);
        }

        public void OnAtkSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            int value = (int)args.NewValue;
            Slider slider = (Slider)sender;
            String str = "00" + value.ToString();
            atk_label.Text = str.Substring(str.Length - 3);
        }

        public void OnDefSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            int value = (int)args.NewValue;
            Slider slider = (Slider)sender;
            String str = "00" + value.ToString();
            def_label.Text = str.Substring(str.Length - 3);
        }

        public void OnSatkSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            int value = (int)args.NewValue;
            Slider slider = (Slider)sender;
            String str = "00" + value.ToString();
            satk_label.Text = str.Substring(str.Length - 3);
        }

        public void OnSdefSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            int value = (int)args.NewValue;
            Slider slider = (Slider)sender;
            String str = "00" + value.ToString();
            sdef_label.Text = str.Substring(str.Length - 3);
        }

        public void OnSpdSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            int value = (int)args.NewValue;
            Slider slider = (Slider)sender;
            String str = "00" + value.ToString();
            spd_label.Text = str.Substring(str.Length - 3);
        }

        public async void OnButtonClicked(object sender, EventArgs args)
        {

            bool isFormValid = true;

            if (pokemonName.Text == String.Empty)
            {
                frameName.BorderColor = Color.Red;
                isFormValid = false;
            }
            else
            {
                frameName.BorderColor = Color.FromHex("#00FBC1BC");
            }
            
            if (pokemonNum.Text == String.Empty)
            {
                isFormValid = false;
                frameNum.BorderColor = Color.Red;
            }
            else
            {
                frameNum.BorderColor = Color.FromHex("#00FBC1BC");
            }

            if (pickerType.SelectedItem == null)
            {
                isFormValid = false;
                pickerType.TextColor = Color.Red;
            }
            else
            {
                pickerType.TextColor = Color.FromHex("#666666");
            }

            if (isFormValid)
            {
                var vm = PokemonViewModel.Instance;
                PokemonDatabase pokemonDB = await PokemonDatabase.Instance;

                Models.Pokemon pokemon = new Models.Pokemon
                {
                    Name = pokemonName.Text,
                    ID = Int32.Parse(pokemonNum.Text),
                    ImgSrc = "",
                    TypeColor = Constants.TypeColor[pickerType.SelectedItem.ToString().ToLower()],
                    Type = pickerType.SelectedItem.ToString().ToLower(),
                    Description = description.Text,
                    HP = (int)HPSlider.Value,
                    ATK = (int)ATKSlider.Value,
                    DEF = (int)DEFSlider.Value,
                    SATK = (int)SATKSlider.Value,
                    SDEF = (int)SDEFSlider.Value,
                    SPD = (int)SPDSlider.Value
                };

                vm.addPokemon(pokemon);
                await pokemonDB.AddPokemonAsync(pokemon);
                
                pokemonName.Text = String.Empty;
                pokemonNum.Text = String.Empty;
                pickerType.SelectedItem = null;
                description.Text = String.Empty;
                HPSlider.Value = 0;
                ATKSlider.Value = 0;
                DEFSlider.Value = 0;
                SATKSlider.Value = 0;
                SDEFSlider.Value = 0;
                SPDSlider.Value = 0;
                hp_label.Text = "000";
                atk_label.Text = "000";
                def_label.Text = "000";
                satk_label.Text = "000";
                sdef_label.Text = "000";
                spd_label.Text = "000";

                await Shell.Current.GoToAsync($"//List", true);
            } 
        }
    }
}