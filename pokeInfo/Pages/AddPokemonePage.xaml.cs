using Plugin.Media;
using Plugin.Media.Abstractions;
using pokeInfo.Models;
using pokeInfo.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pokeInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPokemonePage : ContentPage
    {

        private MediaFile Image { get; set; }
        private bool IsEdit = false;

        private Pokemon currentPokemon;

        // constructeur de la page
        public AddPokemonePage()
        {
            InitializeComponent();
            this.clearInputs();
            this.currentPokemon = new Pokemon();

        }
        public AddPokemonePage(Pokemon pokemon)
        {
            IsEdit = true;
            InitializeComponent();
            boutonValidation.Text = "Modifier";
            pokemonNum.IsEnabled = false;
            this.currentPokemon = pokemon;
            this.setPokemon(pokemon);
        }













        private async void OnPickImageClick(object sender, EventArgs args)
        {
            this.Image = await CrossMedia.Current.PickPhotoAsync();

            if (this.Image == null) return;

            imagePicker.Source = ImageSource.FromStream(() => this.Image.GetStream());
        }


        //Méthode qui vérifie si le formulaire est valide
        private bool IsFormValid()
        {
            bool isFormValid = true;

            if (pokemonName.Text == String.Empty)
            {
                frameName.BorderColor = Color.Red;
                isFormValid = false;
            }
            else { frameName.BorderColor = Color.FromHex("#00FBC1BC"); }

            if (pokemonNum.Text == String.Empty)
            {
                isFormValid = false;
                frameNum.BorderColor = Color.Red;
            }
            else { frameNum.BorderColor = Color.FromHex("#00FBC1BC"); }

            if (pickerType.SelectedItem == null)
            {
                isFormValid = false;
                pickerType.TextColor = Color.Red;
            }
            else { pickerType.TextColor = Color.FromHex("#666666"); }

            if (pokemonWeight.Text == String.Empty)
            {
                frameWeight.BorderColor = Color.Red;
                isFormValid = false;
            }
            else { frameWeight.BorderColor = Color.FromHex("#00FBC1BC"); }

            if (pokemonHeight.Text == String.Empty)
            {
                frameHeight.BorderColor = Color.Red;
                isFormValid = false;
            }
            else { frameHeight.BorderColor = Color.FromHex("#00FBC1BC"); }

            return isFormValid;
        }


        private async void onCancel(object obj, EventArgs e)
        {
            if (IsEdit)
            {
                await Navigation.PopAsync();
            }
            else
            {
                this.clearInputs();
            }
        }

        private void setPokemon(Pokemon pokemon)
        {
            imagePicker.Source = pokemon.ImgSrc;
            pokemonName.Text = pokemon.Name;
            pokemonNum.Text = "" + pokemon.ID;
            pickerType.SelectedIndex = pickerType.Items.IndexOf(pokemon.Type);
            pickerType2.SelectedIndex = pickerType2.Items.IndexOf(pokemon.Type2);
            description.Text = pokemon.Description;
            HPSlider.Value = pokemon.HP;
            ATKSlider.Value = pokemon.ATK;
            DEFSlider.Value = pokemon.DEF;
            SATKSlider.Value = pokemon.SATK;
            SDEFSlider.Value = pokemon.SDEF;
            SPDSlider.Value = pokemon.SPD;
            pokemonHeight.Text = "" + pokemon.Height;
            pokemonWeight.Text = "" + pokemon.Weight;
        }

        private void clearInputs()
        {
            imagePicker.Source = "image.png";
            pokemonName.Text = String.Empty;
            pokemonNum.Text = String.Empty;
            pickerType.SelectedItem = null;
            pickerType2.SelectedItem = null;
            description.Text = String.Empty;
            HPSlider.Value = 0;
            ATKSlider.Value = 0;
            DEFSlider.Value = 0;
            SATKSlider.Value = 0;
            SDEFSlider.Value = 0;
            SPDSlider.Value = 0;
            pokemonHeight.Text = String.Empty;
            pokemonWeight.Text = String.Empty;
        }

        private void clearTypes(object o, EventArgs e)
        {
            pickerType.SelectedItem = null;
            pickerType2.SelectedItem = null;
        }


        private string getTypeKeyFromValue(string value)
        {
            if (value == null) return "normal";

            foreach (var typeinfo in Constants.TypeInfos)
            {
                if (typeinfo.Value.Item2.ToLower() == value.ToLower())
                {
                    return typeinfo.Key;
                }
            }

            return "normal";
        }


        public async void OnButtonClicked(object sender, EventArgs args)
        {
            if (IsFormValid())
            {
                Models.Pokemon pokemon = new Models.Pokemon();
                pokemon.Name = pokemonName.Text;
                pokemon.ID = Int32.Parse(pokemonNum.Text);
                pokemon.ImgSrc = this.Image == null ? currentPokemon.ImgSrc : this.Image.Path;

                pokemon.Type = pickerType.SelectedItem != null ? pickerType.SelectedItem.ToString() :
                        pickerType2.SelectedItem != null ? pickerType.SelectedItem.ToString() : null;
                pokemon.TypeColor = Constants.TypeInfos[getTypeKeyFromValue(pokemon.Type)].Item1;

                pokemon.Type2 = pickerType.SelectedItem != null && pickerType2.SelectedItem != null ? pickerType2.SelectedItem.ToString() : null;
                pokemon.Type2Color = Constants.TypeInfos[getTypeKeyFromValue(pokemon.Type2)].Item1;

                pokemon.Description = description.Text;
                pokemon.HP = (int)HPSlider.Value;
                pokemon.ATK = (int)ATKSlider.Value;
                pokemon.DEF = (int)DEFSlider.Value;
                pokemon.SATK = (int)SATKSlider.Value;
                pokemon.SDEF = (int)SDEFSlider.Value;
                pokemon.SPD = (int)SPDSlider.Value;
                pokemon.Weight = double.Parse(pokemonWeight.Text);
                pokemon.Height = double.Parse(pokemonHeight.Text);

                var vm = PokemonViewModel.Instance;
                PokemonDatabase pokemonDB = await PokemonDatabase.Instance;

                

                if (IsEdit)
                {
                    vm.replacePokemonInList(pokemon);
                    await Navigation.PopAsync();
                }
                else
                {

                    if (vm.PokemonsList.FindIndex(poke => poke.ID == pokemon.ID) != -1)
                    {
                        var result = await DisplayAlert("Attention", "Un pokemon avec le meme identifiant existe deja, voulez vous l'ecrabouiller ?", "Oui, ☠️", "Non, 😇");
                        Console.WriteLine(result);
                        if (result)
                        {
                            this.clearInputs();
                            vm.replacePokemonInList(pokemon);
                            await Shell.Current.GoToAsync($"//List", true);
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        this.clearInputs();
                        vm.addPokemonToList(pokemon);
                        await Shell.Current.GoToAsync($"//List", true);
                    }
                }

                vm.addPokemonToBase(pokemon);
            }
        }
    }
}

