using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Bacchus.src.DAOs
{
    class DAOSousFamille
    {
        protected const string DatabasePath = "Data Source = Bacchus.SQLite;";

        /// <summary>
        /// Renvoie une liste contenant le nom de toutes les sous-familles d'une famille de ref "RefFamille"
        /// </summary>
        /// /// <param name="RefFamille"></param>
        /// <returns></returns>
        public List<string> GetAllSousFamilles(int RefFamille)
        {
            List<string> ListeSousFamilles = new List<string>();

            string Cmd = "SELECT Nom FROM SousFamilles WHERE RefFamille = " + RefFamille;

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
                    // On récupère la liste des noms de sous-familles appartenant à la famille RefFamille
                    using (SQLiteDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            ListeSousFamilles.Add(Reader.GetString(0));
                        }
                    }

                    return ListeSousFamilles;
                }
            }
        }

        /// <summary>
        /// Essaye de récupérer la Ref de la sous-famille passée en paramètre.
        /// Si cette sous-famille n'existe pas dans la BDD, elle est ajoutée et la nouvelle Ref est renvoyée. 
        /// </summary>
        /// <param name="NomFamille"></param>
        /// <returns></returns>
        public int GetRefSousFamille(int RefFamille, string NomSousFamille)
        {
            string Cmd = "SELECT RefSousFamille FROM SousFamilles WHERE RefFamille = " + RefFamille + " AND Nom = '" + NomSousFamille + "'";
            Console.WriteLine(Cmd);
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
                            return -1;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Renvoie le nom de la sous-famille correspondant à RefSousFamille, ou null si non-trouvé
        /// </summary>
        /// <param name="RefSousFamille"></param>
        /// <returns></returns>
        public string GetNomSousFamille(int RefSousFamille)
        {
            string Cmd = "SELECT Nom FROM SousFamilles WHERE RefSousFamille = " + RefSousFamille;

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
        /// Ajoute une sous-famille à la BDD et renvoie sa Ref
        /// </summary>
        /// <param name="NomMarque"></param>
        /// <returns></returns>
        public int AddSousFamille(int RefFamille, string NomSousFamille)
        {
            string Cmd = "INSERT INTO SousFamilles(RefFamille, Nom) VALUES(@RefFamille, @NomSousFamille)";

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
                    SQLiteParameter RefFamilleParam = new SQLiteParameter("@RefFamille", DbType.Int16) { Value = RefFamille };
                    SQLiteParameter NomSousFamilleParam = new SQLiteParameter("@NomSousFamille", DbType.String) { Value = NomSousFamille };

                    Command.Parameters.Add(RefFamilleParam);
                    Command.Parameters.Add(NomSousFamilleParam);

                    int result = Command.ExecuteNonQuery();
                }
            }

            Cmd = "SELECT RefSousFamille FROM SousFamilles WHERE RefFamille = " + RefFamille + " AND Nom = '" + NomSousFamille + "'";

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

        public int DeleteSousFamille(int RefSousFamille)
        {
            string Cmd = "DELETE FROM SousFamilles WHERE RefSousFamille = " + RefSousFamille;

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
                    return Command.ExecuteNonQuery();
                }
            }
        }

        public int UpdateSousFamille(int RefSousFamille, string Nom)
        {
            string Cmd = "UPDATE SousFamilles " +
                        "SET Nom = '" + Nom + "' " +
                        "WHERE RefSousFamille = " + RefSousFamille;

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
                    return Command.ExecuteNonQuery();
                }
            }
        }
    }
}
