using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Bacchus.src.DAOs
{
    class DAOArticle
    {
        protected const string DatabasePath = "Data Source = Bacchus.SQLite;";

        /// <summary>
        /// Ajoute le nouvel article à la BDD. 
        /// Si il est déjà présent (= RefArticle identique), les autres champs sont mis à jour.
        /// </summary>
        /// <param name="NewArticle"></param>
        public void AddOrUpdateArticle(Article article)
        {
            bool Update = false;
            string Cmd = "SELECT * FROM Articles WHERE RefArticle = '" + article.RefArticle + "'";

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Console.WriteLine("On est connecté à la DB");
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
                    Console.WriteLine("La commande est créer");
                    using (SQLiteDataReader Reader = Command.ExecuteReader())
                    {
                        // Si l'article existe déjà, on le met à jour
                        if (Reader.Read())
                        {
                            Console.WriteLine("L'article {0} existe déjà, on le met à jour", article.RefArticle);
                            Update = true; 
                        }
                        //Si l'article n'existe pas dans la BDD, on l'ajoute
                        else
                        {
                            Console.WriteLine("Ajout de l'article {0}", article.RefArticle);
                            Update = false;
                        }
                    }
                }
            }

            if (Update)
            {
                UpdateArticle(article);
            }
            else
            {
                AddArticle(article);
            }
        }

        /// <summary>
        /// Ajoute un article à la base de données.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public int AddArticle(Article article)
        {
            DAOMarque daoMarque = new DAOMarque();
            DAOFamille daoFamille = new DAOFamille();
            DAOSousFamille daoSousFamille = new DAOSousFamille();

            int RefMarque = daoMarque.GetRefMarque(article.Marque);
            int RefFamille = daoFamille.GetRefFamille(article.Famille);
            int RefSousFamille = daoSousFamille.GetRefSousFamille(RefFamille, article.SousFamille);

            string Cmd = "INSERT INTO Articles (RefArticle, Description, RefSousFamille, RefMarque, PrixHT, Quantite) " +
                "VALUES(@RefArticle, @Description, @RefSousFamille, @RefMarque, @PrixHT, @Quantite);";

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
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
            }
        }

        /// <summary>
        /// Met à jour un article déjà existant dans la base de données.
        /// </summary>
        /// <param name="ArticleUpdated"></param>
        public int UpdateArticle(Article ArticleUpdated)
        {
            DAOMarque daoMarque = new DAOMarque();
            DAOFamille daoFamille = new DAOFamille();
            DAOSousFamille daoSousFamille = new DAOSousFamille();

            int RefMarque = daoMarque.GetRefMarque(ArticleUpdated.Marque);
            int RefFamille = daoFamille.GetRefFamille(ArticleUpdated.Famille);
            int RefSousFamille = daoSousFamille.GetRefSousFamille(RefFamille, ArticleUpdated.SousFamille);

            string Cmd = "UPDATE Articles " +
                        "SET Description = '@Description', RefSousFamille = @RefSousFamille, RefMarque = @RefMarque, PrixHT = @PrixHT, Quantite = @Quantite " +
                        "WHERE RefArticle = '@RefArticle';";

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
                    SQLiteParameter RefArticleParam = new SQLiteParameter("@RefArticle", DbType.String) { Value = ArticleUpdated.RefArticle };
                    SQLiteParameter DescriptionParam = new SQLiteParameter("@Description", DbType.String) { Value = ArticleUpdated.Description };
                    SQLiteParameter RefSousFamilleParam = new SQLiteParameter("@RefSousFamille", DbType.Int16) { Value = RefSousFamille };
                    SQLiteParameter RefMarqueParam = new SQLiteParameter("@RefMarque", DbType.Int16) { Value = RefMarque };
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
            }
        }

        public int DeleteArticle(string RefArticle)
        {
            string Cmd = "DELETE FROM Articles WHERE RefArticle = '" + RefArticle + "'";

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
        /// Renvoie l'article correspondant à RefArticle, ou null si l'article n'a pas été trouvé
        /// </summary>
        /// <param name="RefArticle"></param>
        /// <returns></returns>
        public Article GetArticle(string RefArticle)
        {
            string Cmd = "SELECT * FROM Articles WHERE RefArticle = '" + RefArticle + "'";

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
                    using (SQLiteDataReader Reader = Command.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            DAOMarque daoMarque = new DAOMarque();
                            DAOFamille daoFamille = new DAOFamille();
                            DAOSousFamille daoSousFamille = new DAOSousFamille();

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
                                Marque = daoMarque.GetNomMarque(Reader.GetInt16(3)),
                                RefFamille = daoFamille.GetRefFamille(Reader.GetInt16(2)),
                                Famille = daoFamille.GetNomFamille(daoFamille.GetRefFamille(Reader.GetInt16(2))),
                                SousFamille = daoSousFamille.GetNomSousFamille(Reader.GetInt16(2))
                            };

                            return NewArticle;
                        }
                    }
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

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
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
            }
        }

        /// <summary>
        /// Renvoie une liste contenant tous les RefArticles de la BDD
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllRefArticles()
        {
            List<string> ListeRefArticles = new List<string>();

            string Cmd = "SELECT RefArticle FROM Articles";

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
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
            }
        }

        /// <summary>
        /// Renvoie la liste de tous les articles de la marque "Marque"
        /// </summary>
        /// <param name="Marque"></param>
        /// <returns></returns>
        public List<Article> GetArticlesWhereMarque(string Marque)
        {
            List<Article> ListeArticles = new List<Article>();

            DAOMarque daoMarque = new DAOMarque();
            int RefMarque = daoMarque.GetRefMarque(Marque);

            string Cmd = "SELECT RefArticle FROM Articles WHERE RefMarque = " + RefMarque;

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
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
            }
        }

        /// <summary>
        /// Renvoie la liste de tous les articles dans la famille "Famille" et la sous-famille "SousFamille"
        /// </summary>
        /// <param name="Famille"></param>
        /// <param name="SousFamille"></param>
        /// <returns></returns>
        public List<Article> GetArticlesWhereSousFamille(string Famille, string SousFamille)
        {
            List<Article> ListeArticles = new List<Article>();

            DAOSousFamille daoSousFamille = new DAOSousFamille();
            DAOFamille daoFamille = new DAOFamille();

            int RefFamille = daoFamille.GetRefFamille(Famille);
            int RefSousFamille = daoSousFamille.GetRefSousFamille(RefFamille, SousFamille);

            string Cmd = "SELECT RefArticle FROM Articles WHERE RefSousFamille = " + RefSousFamille;

            using (SQLiteConnection Connection = new SQLiteConnection(DatabasePath))
            {
                Connection.Open();
                using (SQLiteCommand Command = new SQLiteCommand(Cmd, Connection))
                {
                    // On récupère la liste des articles
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
            }
        }
    }
}
