using System;
using System.Data.SQLite;

namespace Bacchus.src.DAOs
{
    class DAODatabase
    {
        protected const string DatabasePath = "Data Source = Bacchus.SQLite;";
        
        /// <summary>
        /// Nettoie le contenu de chaque table de la BDD et réinitialise les indexs d'ID à 0.
        /// </summary>
        public void CleanDatabase()
        {
            //On récupère le nom de chaque table
            string Cmd = "SELECT name FROM sqlite_master WHERE type = 'table' AND name IS NOT 'sqlite_sequence' ORDER BY name;";

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
                    using (SQLiteDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            //On supprime le contenu
                            Cmd = "DELETE FROM '" + Reader["name"] + "'";
                            using (SQLiteCommand Command2 = new SQLiteCommand(Cmd, Connection))
                            {
                                Command2.ExecuteNonQuery();
                            }
                        }
                    }
                }

                //  On réinitialise les indexs
                Cmd = "VACUUM";
                using (SQLiteCommand Command3 = new SQLiteCommand(Cmd, Connection))
                {
                    Command3.ExecuteNonQuery();
                }
            }
        } 
    }
}
