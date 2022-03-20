using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using pokeInfo.Models;

namespace pokeInfo { 


    public class AsyncLazy<T>
    {
        readonly Lazy<Task<T>> instance;

        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public AsyncLazy(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }
    }
    
    public class PokemonDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<PokemonDatabase> Instance = new AsyncLazy<PokemonDatabase>(async () =>
        {
            var instance = new PokemonDatabase();
            CreateTableResult result = await Database.CreateTableAsync<Pokemon>();
            return instance;
        });

        //Constructeur
        //Entrées :
        //Sorties :
        public PokemonDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        //Fonction qui cherche l'ensemble des pokemons
        //Entrées :
        //Sorties : List contenant l'ensemble des pokemons
        public Task<List<Pokemon>> GetPokemonsAsync()
        {
            return Database.Table<Pokemon>().ToListAsync();
        }

        //Fonction qui cherche un pokemon dnas la BDD
        //Entrées : un entier representant l'id d'un pokemon
        //Sorties : un pokemon ou null
        public Task<Pokemon> GetPokemonByIdAsync(int id)
        {
            return Database.Table<Pokemon>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        //Fonction qui enregistre un pokemon
        //Entrées : le pokemon a enregistrer
        //Sorties : un entier representant le nombre de ligne ajoutées
        public Task<int> AddPokemonAsync(Pokemon pokemon)
        {
            return Database.InsertAsync(pokemon);
        }
        //Fonction qui supprime un pokemon
        //Entrées : le pokemon a supprimer
        //Sorties : un entier representant le nombre de ligne supprimée
        public Task<int> DeletePokemonAsync(Pokemon pokemon)
        {
            return Database.DeleteAsync(pokemon);
        }
        //Fonction qui retourne si la BDD est vide ou non
        //Entrées : 
        //Sorties : un booleen qui est vrai si la base est vide sinon faux
        public Task<bool> isPokemonDatabaseEmptyAsync()
        {
            return Task.Run(() => 0 == Database.Table<Pokemon>().ToListAsync().Result.Count);
        }
        //Fonction qui modifie un pokemon
        //Entrées : le pokemon a modifier
        //Sorties : un entier representant le nombre de ligne modifiée
        public Task<int> UpdatePokemonAsync(Pokemon pokemon)
        {
            return Database.UpdateAsync(pokemon);
        }
    }
}
