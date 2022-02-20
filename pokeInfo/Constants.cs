using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace pokeInfo
{
    public static class Constants
    {

        public static readonly Dictionary<string, string> TypeColor = new Dictionary<string, string>
        {
            {"rock", "#B69E31" },
            {"ghost", "#70559B"},
            {"steel", "#B7B9D0"},
            {"water", "#6493EB"},
            {"grass", "#74CB48"},
            {"psychic", "#FB5584"},
            {"ice", "#9AD6DF"},
            {"dark", "#75574C"},
            {"fairy", "#E69EAC"},
            {"normal", "#AAA67F"},
            {"fighting", "#C12239"},
            {"flying", "#A891EC"},
            {"poison", "#A43E9E"},
            {"ground", "#DEC16B"},
            {"bug", "#A7B723"},
            {"fire", "#F57D31"},
            {"electric", "#F9CF30"},
            {"dragon", "#7037FF"}
        };

        public const string DatabaseFilename = "PokemonSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
