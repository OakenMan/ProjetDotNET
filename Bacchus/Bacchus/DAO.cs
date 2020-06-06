using System;
using System.Collections.Generic;
using System.Data;
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
        
        //~DAO()
        //{
        //    try
        //    {
        //        Connection.Close();
        //    }
        //    catch (SQLiteException Error)
        //    {
        //        Console.WriteLine(Error.Message);
        //    }
        //}

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

        /// <summary>
        /// Ajoute le nouvel article à la BDD. 
        /// Si il est déjà présent (= RefArticle identique), les autres champs sont mis à jour.
        /// </summary>
        /// <param name="NewArticle"></param>
        public void AddOrUpdateArticle(Article article)
        {
            string Cmd = "SELECT * FROM Articles WHERE RefArticle = '" + article.RefArticle + "'";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                // Si l'article existe déjà, on le met à jour
                if (Reader.Read())
                {
                    Console.WriteLine("L'article {0} existe déjà, on le met à jour", article.RefArticle);
                    UpdateArticle(article);
                }
                // Si l'article n'existe pas dans la BDD, on l'ajoute
                else
                {
                    Console.WriteLine("Ajout de l'article {0}", article.RefArticle);
                    AddArticle(article);
                }
            }
        }

        /// <summary>
        /// Ajoute un article à la base de données.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public int AddArticle(Article article)
        {
            int RefMarque = GetRefMarque(article.Marque);
            int RefFamille = GetRefFamille(article.Famille);
            int RefSousFamille = GetRefSousFamille(RefFamille, article.SousFamille);

            string Cmd = "INSERT INTO Articles (RefArticle, Description, RefSousFamille, RefMarque, PrixHT, Quantite) " +
                "VALUES(@RefArticle, @Description, @RefSousFamille, @RefMarque, @PrixHT, @Quantite);";

            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            SQLiteParameter RefArticleParam = new SQLiteParameter("@RefArticle", DbType.String) { Value = article.RefArticle };
            SQLiteParameter DescriptionParam = new SQLiteParameter("@Description", DbType.String) { Value = article.Description };
            SQLiteParameter RefSousFamilleParam = new SQLiteParameter("@RefSousFamille", DbType.Int16) { Value = RefSousFamille };
            SQLiteParameter RefMarqueParam = new SQLiteParameter("@RefMarque", DbType.Int16) { Value = RefMarque };
            SQLiteParameter PrixHTParam = new SQLiteParameter("@PrixHT", DbType.Decimal) { Value = article.PrixHT };
            SQLiteParameter QuantiteParam = new SQLiteParameter("@Quantite", DbType.Int16) { Value = article.Quantite };

            Command.Parameters.Add(RefArticleParam);
            Command.Parameters.Add(DescriptionParam);
            Command.Parameters.Add(RefSousFamilleParam);
            Command.Parameters.Add(RefMarqueParam);
            Command.Parameters.Add(PrixHTParam);
            Command.Parameters.Add(QuantiteParam);

            return Command.ExecuteNonQuery();
        }

        /// <summary>
        /// Met à jour un article déjà existant dans la base de données.
        /// </summary>
        /// <param name="ArticleUpdated"></param>
        public int UpdateArticle(Article ArticleUpdated)
        {
            int RefMarqueUpdated = GetRefMarque(ArticleUpdated.Marque);
            int RefFamilleUpdated = GetRefFamille(ArticleUpdated.Famille);
            int RefSousFamilleUpdated = GetRefSousFamille(RefFamilleUpdated, ArticleUpdated.SousFamille);

            string Cmd = "UPDATE Articles " +
                        "SET Description = '@Description', RefSousFamille = @RefSousFamille, RefMarque = @RefMarque, PrixHT = @PrixHT, Quantite = @Quantite " +
                        "WHERE RefArticle = '@RefArticle';";

            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            SQLiteParameter RefArticleParam = new SQLiteParameter("@RefArticle", DbType.String) { Value = ArticleUpdated.RefArticle };
            SQLiteParameter DescriptionParam = new SQLiteParameter("@Description", DbType.String) { Value = ArticleUpdated.Description };
            SQLiteParameter RefSousFamilleParam = new SQLiteParameter("@RefSousFamille", DbType.Int16) { Value = RefSousFamilleUpdated };
            SQLiteParameter RefMarqueParam = new SQLiteParameter("@RefMarque", DbType.Int16) { Value = RefMarqueUpdated };
            SQLiteParameter PrixHTParam = new SQLiteParameter("@PrixHT", DbType.Decimal) { Value = ArticleUpdated.PrixHT };
            SQLiteParameter QuantiteParam = new SQLiteParameter("@Quantite", DbType.Int16) { Value = ArticleUpdated.Quantite };

            Command.Parameters.Add(RefArticleParam);
            Command.Parameters.Add(DescriptionParam);
            Command.Parameters.Add(RefSousFamilleParam);
            Command.Parameters.Add(RefMarqueParam);
            Command.Parameters.Add(PrixHTParam);
            Command.Parameters.Add(QuantiteParam);

            return Command.ExecuteNonQuery();
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

                    Article NewArticle = new Article
                    {
                        // Champs récupérables directement depuis la table SQL
                        RefArticle = Reader.GetString(0),
                        Description = Reader.GetString(1),
                        RefSousFamille = Reader.GetInt16(2),
                        RefMarque = Reader.GetInt16(3),
                        PrixHT = Reader.GetFloat(4),
                        Quantite = Reader.GetInt16(5),

                        // Champs récupérables depuis d'autres tables
                        Marque = GetNomMarque(Reader.GetInt16(3)),
                        RefFamille = GetRefFamille(Reader.GetInt16(2)),
                        Famille = GetNomFamille(GetRefFamille(Reader.GetInt16(2))),
                        SousFamille = GetNomSousFamille(Reader.GetInt16(2))
                    };

                    return NewArticle;
                }
            }

            return null;
        }

        /// <summary>
        /// Renvoie une liste avec tous les articles de la BDD
        /// </summary>
        /// <returns></returns>
        public List<Article> GetAllArticles()
        {
            List<Article> ListeArticles = new List<Article>();

            string Cmd = "SELECT RefArticle FROM Articles";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            // On récupère la liste des RefArticle
            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                while (Reader.Read())
                {
                    // Pour chaque RefArticle, on récupère l'article correspondant et on l'ajoute à la liste
                    Article NewArticle = GetArticle(Reader.GetString(0));
                    ListeArticles.Add(NewArticle);
                }
            }

            return ListeArticles;
        }

        /// <summary>
        /// Renvoie une liste contenant tous les RefArticles de la BDD
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllRefArticles()
        {
            List<string> ListeRefArticles = new List<string>();

            string Cmd = "SELECT RefArticle FROM Articles";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            // On récupère la liste des RefArticle
            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                while (Reader.Read())
                {
                    ListeRefArticles.Add(Reader.GetString(0));
                }
            }

            return ListeRefArticles;
        }

        /// <summary>
        /// Renvoie une liste contenant le nom de toutes les familles
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllFamilles()
        {
            List<string> ListeFamilles = new List<string>();

            string Cmd = "SELECT Nom FROM Familles";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            // On récupère la liste des noms de familles
            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                while (Reader.Read())
                {
                    ListeFamilles.Add(Reader.GetString(0));
                }
            }

            return ListeFamilles;
        }

        /// <summary>
        /// Renvoie une liste contenant le nom de toutes les sous-familles d'une famille de ref "RefFamille"
        /// </summary>
        /// /// <param name="RefFamille"></param>
        /// <returns></returns>
        public List<string> GetAllSousFamilles(int RefFamille)
        {
            List<string> ListeSousFamilles = new List<string>();

            string Cmd = "SELECT Nom FROM SousFamilles WHERE RefFamille = " + RefFamille;
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

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

        /// <summary>
        /// Renvoie une liste contenant le nom de toutes les marques
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllMarques()
        {
            List<string> ListeMarques = new List<string>();

            string Cmd = "SELECT Nom FROM Marques";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

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
                    Console.WriteLine("La marque [{0}] n'existe pas, on l'ajoute", NomMarque);
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
            }

            return null;
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
                    Console.WriteLine("La famille [{0}] n'existe pas, on l'ajoute", NomFamille);
                    return AddFamille(NomFamille);
                }
            }
        }

        /// <summary>
        /// Renvoie la RefFamille de la sous-famille passée en paramètre, ou -1 sinon
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

            return -1;
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
            string Cmd = "SELECT RefSousFamille FROM SousFamilles WHERE RefFamille = " + RefFamille + " AND Nom = '" + NomSousFamille + "'";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                if (Reader.Read())
                {
                    return Reader.GetInt16(0);
                }
                else {
                    Console.WriteLine("La sous-famille [{0}] n'existe pas, on l'ajoute", NomSousFamille);
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
            string Cmd = "INSERT INTO SousFamilles(RefFamille, Nom) VALUES(@RefFamille, @NomSousFamille)";
            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

            SQLiteParameter RefFamilleParam = new SQLiteParameter("@RefFamille", DbType.Int16) { Value = RefFamille };
            SQLiteParameter NomSousFamilleParam = new SQLiteParameter("@NomSousFamille", DbType.String) { Value = NomSousFamille };

            Command.Parameters.Add(RefFamilleParam);
            Command.Parameters.Add(NomSousFamilleParam);

            int result = Command.ExecuteNonQuery();

            Cmd = "SELECT RefSousFamille FROM SousFamilles WHERE RefFamille = " + RefFamille + " AND Nom = '" + NomSousFamille + "'";
            Command = new SQLiteCommand(Cmd, Connection);

            using (SQLiteDataReader Reader = Command.ExecuteReader())
            {
                Reader.Read();
                return Reader.GetInt16(0);
            }
        }
    }

}
