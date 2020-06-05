using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus
{
    class DAO
    {
        private const string DatabasePath = "Data Source = Bacchus.SQLite;";
        private SQLiteConnection Connection;

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
        /// Ajoute "NewArticle" à la BDD. 
        /// Si il est déjà présent (= RefArticle identique), les autres champs sont mis à jour.
        /// </summary>
        /// <param name="NewArticle"></param>
        public void AddArticle(Article NewArticle)
        {
            string Cmd = "SELECT * FROM Articles WHERE RefArticle = '" + NewArticle.RefArticle + "'";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                // Si l'article existe déjà, on le met à jour
                if (Reader.Read())
                {
                    // Je ferais ça plus tard
                }
                // Si l'article n'existe pas dans la BDD, on l'ajoute
                else
                {
                    // Déso c'est crade j'essayerais d'utiliser des SQLiteParameters plus tard
                    Cmd = "INSERT INTO Articles (RefArticle, Description, RefSousFamille, RefMarque, PrixHT, Quantite) " +
                        "VALUES('" + NewArticle.RefArticle +
                        "', '" + NewArticle.Description +
                        "', " + NewArticle.RefSousFamille +
                        ", " + NewArticle.RefMarque +
                        ", " + NewArticle.PrixHT +
                        ", " + NewArticle.Quantite +
                        ");";
                    Command = new SQLiteCommand(Cmd, Connection);

                    int result = Command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Renvoie l'article correspondant à RefArticle, ou null si l'article n'a pas été trouvé
        /// </summary>
        /// <param name="RefArticle"></param>
        /// <returns></returns>
        public Article GetArticle(string RefArticle)
        {
            string Cmd = "SELECT * FROM Articles WHERE RefArticle = '" + RefArticle + "'";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                if (Reader.Read()) {

                    Article NewArticle = new Article();

                    // Champs récupérables directement depuis la table SQL
                    NewArticle.RefArticle = Reader.GetString(0);
                    NewArticle.Description = Reader.GetString(1);
                    NewArticle.RefSousFamille = Reader.GetInt16(2);
                    NewArticle.RefMarque = Reader.GetInt16(3);
                    NewArticle.PrixHT = Reader.GetFloat(4);
                    NewArticle.Quantite = Reader.GetInt16(5);

                    // Champs récupérables depuis d'autres tables
                    NewArticle.Marque = GetNomMarque(NewArticle.RefMarque);
                    NewArticle.RefFamille = GetRefFamille(NewArticle.RefSousFamille);
                    NewArticle.Famille = GetNomFamille(NewArticle.RefFamille);
                    NewArticle.SousFamille = GetNomSousFamille(NewArticle.RefSousFamille);

                    return NewArticle;
                }
            }

            return null;
        }

        /// <summary>
        /// Essaye de récupérer la Ref de la marque passée en paramètre.
        /// Si cette marque n'existe pas dans la BDD, elle est ajoutée et la nouvelle Ref est renvoyée.
        /// </summary>
        /// <param name="NomMarque"></param>
        /// <returns></returns>
        public int GetRefMarque(string NomMarque)
        {
            string Cmd = "SELECT RefMarque FROM Marques WHERE Nom = '" + NomMarque + "'";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                if(Reader.Read())
                {
                    return Reader.GetInt16(0);
                }
                else
                {
                    return AddMarque(NomMarque);
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
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                if (Reader.Read())
                {
                    return Reader.GetString(0);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Essaye de récupérer la Ref de la famille passée en paramètre.
        /// Si cette famille n'existe pas dans la BDD, elle est ajoutée et la nouvelle Ref est renvoyée. 
        /// </summary>
        /// <param name="NomFamille"></param>
        /// <returns></returns>
        public int GetRefFamille(string NomFamille)
        {
            string Cmd = "SELECT RefFamille FROM Familles WHERE Nom = '" + NomFamille + "'";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                if (Reader.Read())
                {
                    return Reader.GetInt16(0);
                }
                else
                {
                    return AddFamille(NomFamille);
                }
            }
        }

        /// <summary>
        /// Renvoie la RefFamille de la sous-famille passée en paramètre, ou null sinon
        /// </summary>
        /// <param name="NomFamille"></param>
        /// <returns></returns>
        public int GetRefFamille(int RefSousFamille)
        {
            string Cmd = "SELECT RefFamille FROM SousFamilles WHERE RefSousFamille = " + RefSousFamille;
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                if (Reader.Read())
                {
                    return Reader.GetInt16(0);
                }
            }

            return null;
        }

        /// <summary>
        /// Renvoie le nom de la famille correspondant à RefFamille, ou null si non-trouvé
        /// </summary>
        /// <param name="RefFamille"></param>
        /// <returns></returns>
        public string GetNomFamille(int RefFamille)
        {
            string Cmd = "SELECT Nom FROM Familles WHERE RefFamille = " + RefFamille;
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                if (Reader.Read())
                {
                    return Reader.GetString(0);
                }
            }

            return null;
        }

        /// <summary>
        /// Essaye de récupérer la Ref de la sous-famille passée en paramètre.
        /// Si cette sous-famille n'existe pas dans la BDD, elle est ajoutée et la nouvelle Ref est renvoyée. 
        /// </summary>
        /// <param name="NomFamille"></param>
        /// <returns></returns>
        public int GetRefSousFamille(int RefFamille, string NomSousFamille)
        {
            string Cmd = "SELECT RefSousFamille FROM Familles WHERE RefFamille = " + RefFamille + " AND Nom = '" + NomSousFamille + "'";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                if (Reader.Read())
                {
                    return Reader.GetInt16(0);
                }
                else {
                    return AddSousFamille(RefFamille, NomSousFamille);
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
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                if (Reader.Read())
                {
                    return Reader.GetString(0);
                }
            }

            return null;
        }

        /// <summary>
        /// Ajoute une marque à la BDD et renvoie sa Ref
        /// </summary>
        /// <param name="NomMarque"></param>
        /// <returns></returns>
        public int AddMarque(string NomMarque)
        {
            string Cmd = "INSERT INTO Marques(Nom) VALUES('"+ NomMarque +"')";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            int result = Command.ExecuteNonQuery();

            Cmd = "SELECT RefMarque FROM Marques WHERE Nom = '" + NomMarque + "'";
            Command = new SQLiteCommand(Cmd, Connection);

            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                Reader.Read();
                return Reader.GetInt16(0);   
            }
        }

        /// <summary>
        /// Ajoute une famille à la BDD et renvoie sa Ref
        /// </summary>
        /// <param name="NomMarque"></param>
        /// <returns></returns>
        public int AddFamille(string NomFamille)
        {
            string Cmd = "INSERT INTO Familles(Nom) VALUES('" + NomFamille + "')";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            int result = Command.ExecuteNonQuery();

            Cmd = "SELECT RefFamille FROM Familles WHERE Nom = '" + NomFamille + "'";
            Command = new SQLiteCommand(Cmd, Connection);

            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                Reader.Read();
                return Reader.GetInt16(0);
            }
        }

        /// <summary>
        /// Ajoute une sous-famille à la BDD et renvoie sa Ref
        /// </summary>
        /// <param name="NomMarque"></param>
        /// <returns></returns>
        public int AddSousFamille(int RefFamille, string NomSousFamille)
        {
            string Cmd = "INSERT INTO SousFamilles(RefFamille, Nom) VALUES(" + RefFamille + "'" + NomSousFamille + "')";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            int result = Command.ExecuteNonQuery();

            Cmd = "SELECT RefSousFamille FROM Familles WHERE RefFamille = " + RefFamille + " AND Nom = '" + NomSousFamille + "'";
            Command = new SQLiteCommand(Cmd, Connection);

            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                Reader.Read();
                return Reader.GetInt16(0);
            }
        }
    }

}
