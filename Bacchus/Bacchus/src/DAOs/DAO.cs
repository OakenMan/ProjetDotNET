using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.src.DAOs
{
    class DAO
    {
        protected const string DatabasePath = "Data Source = Bacchus.SQLite;";
        protected SQLiteConnection Connection;

        /// <summary>
        /// Constructeur du DAO, initialise la connexion avec la BDD SQLite.
        /// </summary>
        public DAO()
        {
            try
            {
                Connection = new SQLiteConnection(DatabasePath);
                Connection.Open();
            }
            catch(SQLiteException Error)
            {
                Console.WriteLine(Error.Message);
            }
        }
        
        /// <summary>
        /// Nettoie le contenu de chaque table de la BDD et réinitialise les indexs d'ID à 0.
        /// </summary>
        public void CleanDatabase()
        {
            //On récupère le nom de chaque table
            string Cmd = "SELECT name FROM sqlite_master WHERE type = 'table' AND name IS NOT 'sqlite_sequence' ORDER BY name;";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);
            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                while (Reader.Read())
                {
                    //On supprime le contenu
                    Cmd = "DELETE FROM '" + Reader["name"] + "'";
                    Command = new SQLiteCommand(Cmd, Connection);
                    Command.ExecuteReader();
                }
            }

            //On réinitialise les indexs
            Cmd = "VACUUM";
            Command = new SQLiteCommand(Cmd, Connection);
            Command.ExecuteNonQuery();
        } 
    }
}
