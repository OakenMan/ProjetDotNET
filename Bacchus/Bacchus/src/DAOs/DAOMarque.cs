using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Bacchus.src.DAOs
{
    class DAOMarque
    {
        protected const string DatabasePath = "Data Source = Bacchus.SQLite;";

        /// <summary>
        /// Essaye de récupérer la Ref de la marque passée en paramètre.
        /// Si cette marque n'existe pas dans la BDD, elle est ajoutée et la nouvelle Ref est renvoyée.
        /// </summary>
        /// <param name="NomMarque"></param>
        /// <returns></returns>
        public int GetRefMarque(string NomMarque)
        {
            string Cmd = "SELECT RefMarque FROM Marques WHERE Nom = '" + NomMarque + "'";

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
                    using (SQLiteDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            return Reader.GetInt16(0);
                        }
                        else
                        {
                            Console.WriteLine("La marque [{0}] n'existe pas, on l'ajoute", NomMarque);
                            return AddMarque(NomMarque);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Renvoie le nom de la marque correspondant à RefMarque, ou null si non-trouvé
        /// </summary>
        /// <param name="RefMarque"></param>
        /// <returns></returns>
        public string GetNomMarque(int RefMarque)
        {
            string Cmd = "SELECT Nom FROM Marques WHERE RefMarque = " + RefMarque;

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
                    using (SQLiteDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            return Reader.GetString(0);
                        }
                    }

                    return null;
                }
            }
        }

        /// <summary>
        /// Ajoute une marque à la BDD et renvoie sa Ref
        /// </summary>
        /// <param name="NomMarque"></param>
        /// <returns></returns>
        public int AddMarque(string NomMarque)
        {
            string Cmd = "INSERT INTO Marques(Nom) VALUES('" + NomMarque + "')";

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
                    Command.ExecuteNonQuery();
                }
            }

            Cmd = "SELECT RefMarque FROM Marques WHERE Nom = '" + NomMarque + "'";

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
                    using (SQLiteDataReader Reader = Command.ExecuteReader())
                    {
                        Reader.Read();
                        return Reader.GetInt16(0);
                    }
                }
            }
        }

        public int DeleteMarque(int RefMarque)
        {
            string Cmd = "DELETE FROM Marques WHERE RefMarque = " + RefMarque;
            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
                    return Command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Renvoie une liste contenant le nom de toutes les marques
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllMarques()
        {
            List<string> ListeMarques = new List<string>();

            string Cmd = "SELECT Nom FROM Marques";

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
                    // On récupère la liste des noms de marques
                    using (SQLiteDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            ListeMarques.Add(Reader.GetString(0));
                        }
                    }

                    return ListeMarques;
                }
            }
        }
    }
}
