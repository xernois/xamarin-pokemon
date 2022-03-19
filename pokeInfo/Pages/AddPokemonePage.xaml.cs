using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;
using pokeInfo.ViewModels;
using pokeInfo.Models;

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
            else { frameName.BorderColor = Color.FromHex("#00FBC1BC");}

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
            }else
            {
                this.clearInputs();
            }
        }

        private void setPokemon(Pokemon pokemon)
        {
            imagePicker.Source = pokemon.ImgSrc ;
            pokemonName.Text = pokemon.Name;
            pokemonNum.Text = ""+pokemon.ID;
            pickerType.SelectedIndex = pickerType.Items.IndexOf(pokemon.Type);
            description.Text = pokemon.Description;
            HPSlider.Value = pokemon.HP;
            ATKSlider.Value = pokemon.ATK;
            DEFSlider.Value = pokemon.DEF;
            SATKSlider.Value = pokemon.SATK;
            SDEFSlider.Value = pokemon.SDEF;
            SPDSlider.Value = pokemon.SPD;
            pokemonHeight.Text = ""+pokemon.Height;
            pokemonWeight.Text = ""+pokemon.Weight;
        }

        private void clearInputs()
        {
            imagePicker.Source = "image.png";
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
            pokemonHeight.Text = String.Empty;
            pokemonWeight.Text = String.Empty;
        }

        private void clearTypes(object o, EventArgs e)
        {
            pickerType.SelectedItem = null;
            pickerType2.SelectedItem = null;
        }

       

        public async void OnButtonClicked(object sender, EventArgs args)
        {
            if (IsFormValid())
            {

                string type = "";

                foreach (var typeinfo in Constants.TypeInfos)
                {
                    if (typeinfo.Value.Item2.ToLower() == pickerType.SelectedItem.ToString().ToLower())
                    {
                        type = typeinfo.Key;
                    }
                }

                Models.Pokemon pokemon = new Models.Pokemon
                {
                    Name = pokemonName.Text,
                    ID = Int32.Parse(pokemonNum.Text),
                    ImgSrc = this.Image == null ? currentPokemon.ImgSrc : this.Image.Path, 
                    TypeColor = Constants.TypeInfos[type].Item1,
                    Type = pickerType.SelectedItem.ToString(),
                    Description = description.Text,
                    HP = (int)HPSlider.Value,
                    ATK = (int)ATKSlider.Value,
                    DEF = (int)DEFSlider.Value,
                    SATK = (int)SATKSlider.Value,
                    SDEF = (int)SDEFSlider.Value,
                    SPD = (int)SPDSlider.Value,
                    Weight = double.Parse(pokemonWeight.Text),
                    Height = double.Parse(pokemonHeight.Text),
                };

                var vm = PokemonViewModel.Instance;
                PokemonDatabase pokemonDB = await PokemonDatabase.Instance;

                vm.addPokemonToBase(pokemon);

                if (IsEdit)
                {
                    Console.WriteLine("mes couilles sur ton front");
                    vm.replacePokemonInList(pokemon);
                }
                else
                {
                    vm.addPokemonToList(pokemon);
                }

                this.clearInputs();
                await Shell.Current.GoToAsync($"//List", true);
                }
            }
        }
    }
