using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
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

        public PokemonDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        //---------------------------------------------------------------------------------------------------------

        public Task<List<Pokemon>> GetPokemonsAsync()
        {
            return Database.Table<Pokemon>().ToListAsync();
        }

        public Task<Pokemon> GetPokemonByIdAsync(int id)
        {
            return Database.Table<Pokemon>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> AddPokemonAsync(Pokemon pokemon)
        {
            return Database.InsertAsync(pokemon);
        }

        public Task<int> DeletePokemonAsync(Pokemon pokemon)
        {
            return Database.DeleteAsync(pokemon);
        }

        public Task<bool> isPokemonDatabaseEmptyAsync()
        {
            return Task.Run(() => 0 == Database.Table<Pokemon>().ToListAsync().Result.Count);
        }
        public Task<int> UpdatePokemonAsync(Pokemon pokemon)
        {
            return Database.UpdateAsync(pokemon);
        }
    }
}
