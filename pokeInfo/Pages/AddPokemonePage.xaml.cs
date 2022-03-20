using Plugin.Media;
using Plugin.Media.Abstractions;
using pokeInfo.Models;
using pokeInfo.Pages;
using pokeInfo.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pokeInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPokemonePage : ContentPage
    {

        // l'image du pokemon
        private MediaFile Image { get; set; }
        // un booleen correspondant a l'etat de la page (ajout/modification)
        private bool IsEdit = false;
        // le pokemon actuel
        private Pokemon currentPokemon;
        //Constructeur de la page si c'est un ajout
        //Entrées : 
        //Sorties :
        public AddPokemonePage()
        {
            InitializeComponent();
            this.clearInputs();
            this.currentPokemon = new Pokemon();
        }

        //Constructeur de la page si c'est une modification. 
        //Entrées : Le pokemon a modifier
        //Sorties : 
        public AddPokemonePage(Pokemon pokemon)
        {
            IsEdit = true;
            InitializeComponent();
            // modification du bouton de validation pour la page d'edition
            boutonValidation.Text = "Modifier";
            pokemonNum.IsEnabled = false;
            this.currentPokemon = pokemon;
            // remplis les champs avec les valeurs du pokemon passer en parametre
            this.setPokemon(pokemon);
        }

        //Méthode qui permet la sélection d'une image 
        //Entrées : l'élément qui trigger l'événement, arguments de l'événement
        //Sorties : 
        private async void OnPickImageClick(object sender, EventArgs args)
        {
            // ouverture de la gallerie
            this.Image = await CrossMedia.Current.PickPhotoAsync();

            // si aucune image est selectionnée, quitter la methode
            if (this.Image == null) return;

            //met la source de notre imagePicker à l'image selectionnée
            imagePicker.Source = ImageSource.FromStream(() => this.Image.GetStream());
        }

        //Fonction correspondant a la validitée du formulaire
        //Entrées : 
        //Sorties : un booleen vrai si le formulaire est valide
        private async Task<bool> IsFormValid()
        {
            bool isFormValid = true;

            if (isFormValid  && pokemonName.Text == String.Empty)
            {
                await DisplayAlert("Attention", "Le nom n'est pas ou est mal renseigné", "Ok");
                isFormValid = false;
            }

            if (isFormValid && pokemonNum.Text == String.Empty)
            {
                await DisplayAlert("Attention", "Le numéro n'est pas ou est mal renseigné", "Ok");
                isFormValid = false;
            }

            if (isFormValid && pokemonWeight.Text == String.Empty)
            {
                await DisplayAlert("Attention", "La taille n'est pas ou est mal renseignée", "Ok");
                isFormValid = false;
            }

            if (isFormValid && pokemonHeight.Text == String.Empty)
            {
                await DisplayAlert("Attention", "Le poids n'est pas ou est mal renseigné", "Ok");
                isFormValid = false;
            }

            if (isFormValid && pickerType.SelectedItem == null && pickerType2.SelectedItem == null)
            {
                await DisplayAlert("Attention", "Le premier type n'est pas ou est mal renseigné", "Ok");
                isFormValid = false;
            }

            return isFormValid;
        }
        //Méthode pour annuler l'ajout / modification 
        //Entrées : l'élément qui trigger l'événement, arguments de l'événement
        //Sorties :
        private async void onCancel(object obj, EventArgs e)
        {
            if (IsEdit)
            {
                // retourne a la page de detail en mode modification
                await Navigation.PopAsync();
            }
            else
            {
                //vide tous les inputs du formulaire
                this.clearInputs();
            }
        }

        //Méthode qui remplis les champs du formulaire a partir des valeurs d'un pokemon 
        //Entrées : Le Pokemon à modifier
        //Sorties :
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

        //Méthode qui vide tous les champs du formulaire
        //Entrées :
        //Sorties :
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
        //Méthode qui vide les deux champs des types dans le formulaire
        //Entrées : l'élément qui trigger l'événement, arguments de l'événement
        //Sorties :
        private void clearTypes(object o, EventArgs e)
        {
            pickerType.SelectedItem = null;
            pickerType2.SelectedItem = null;
        }
        //Fonction qui retourne la clef a partir d'une valeur dans le dictionnaire de type (Constants)
        //Entrées : la valeur (string)
        //Sorties : la clef (string)
        private string getTypeKeyFromValue(string value)
        {
            // si la valeur est null, retourne un type par defaut
            if (value == null) return "normal";

            foreach (var typeinfo in Constants.TypeInfos)
            {
                if (typeinfo.Value.Item2.ToLower() == value.ToLower())
                {
                    // si une clef correspond a la valeur passée en parametre, la retourner
                    return typeinfo.Key;
                }
            }
            // si aucun resultat, retourne un type par defaut (normal)
            return "normal";
        }

        //Méthode pour modifier ou ajouter un pokemon en base
        //Entrées : élément qui trigger l'événement, arguments de l'événement
        //Sorties :
        public async void OnButtonClicked(object sender, EventArgs args)
        {
            //Si la validation du formulaire retourne vrai alors créé un nouveau pokemon avec les valeurs présentent dans le formulaire
            if (await IsFormValid())
            {
                // creation du nouveau pokemon
                Models.Pokemon pokemon = new Models.Pokemon();
                pokemon.Name = pokemonName.Text;
                pokemon.ID = Int32.Parse(pokemonNum.Text);
                pokemon.ImgSrc = this.Image == null ? currentPokemon.ImgSrc : this.Image.Path;

                // definission du premier type a partir du picker 1 et 2, si le 1 est vide le picker 2 sert pour le type 1
                pokemon.Type = pickerType.SelectedItem != null ? pickerType.SelectedItem.ToString() :
                        pickerType2.SelectedItem != null ? pickerType.SelectedItem.ToString() : null;
                pokemon.TypeColor = Constants.TypeInfos[getTypeKeyFromValue(pokemon.Type)].Item1;
                // definission du type 2 si les deux picker ne sont pas null
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


                //si IsEdit est vrai (mode édition) on le replace dans la liste et dans la base
                if (IsEdit)
                {
                    // remplace le pokemon en base et dans la liste
                    vm.replacePokemonInList(pokemon);
                    vm.addPokemonToBase(pokemon);
                    //retour a la page de detail
                    await Navigation.PopToRootAsync();
                }
                //sinon nous sommes en mode ajout donc le nouveau pokemon est ajouter en BDD et dans la liste
                else
                {

                    if (vm.PokemonsList.FindIndex(poke => poke.ID == pokemon.ID) != -1)
                    {
                        var result = await DisplayAlert("Attention", "Un pokemon avec le meme identifiant existe deja, voulez vous l'ecrabouiller ?", "Oui, ☠️", "Non, 😇");
                        if (result)
                        {
                            // vide les inputs
                            this.clearInputs();
                            // remplace le pokemon en base et dans la liste
                            vm.replacePokemonInList(pokemon);
                            vm.addPokemonToBase(pokemon);
                            // retour a la liste
                            await Shell.Current.GoToAsync($"//List", true);
                        }
                    }
                    else
                    {
                        // on vide les inputs
                        this.clearInputs();
                        // ajout du pokemon en base et dans la liste
                        vm.addPokemonToList(pokemon);
                        vm.addPokemonToBase(pokemon);
                        // retour a la liste
                        await Shell.Current.GoToAsync($"//List", true);
                    }
                }
            }
        }
    }
}

