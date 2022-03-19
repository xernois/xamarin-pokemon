using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace pokeInfo
{
    public static class Constants
    {

        public static readonly Dictionary<string, (string, string)> TypeInfos = new Dictionary<string, (string, string)>
        {
            {"normal", ("#AAA67F", "Normal")},
            {"fighting", ("#C12239", "Combat")},
            {"flying", ("#A891EC", "Vol")},
            {"poison", ("#A43E9E", "Poison")},
            {"ground", ("#DEC16B", "Sol")},
            {"rock", ("#B69E31", "Roche") },
            {"ghost", ("#70559B", "Fantome")},
            {"steel", ("#B7B9D0", "Acier")},
            {"water", ("#6493EB", "Eau")},
            {"grass", ("#74CB48", "Herbe" )},
            {"psychic", ("#FB5584", "Psy")},
            {"ice", ("#9AD6DF", "Glace")},
            {"dark", ("#75574C", "Ténèbres")},
            {"fairy", ("#E69EAC", "Fée")},
            {"bug", ("#A7B723", "Insecte")},
            {"fire", ("#F57D31", "Feu")},
            {"electric", ("#F9CF30", "Electric")},
            {"dragon", ("#7037FF", "Dragon")}
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
