﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.src.DAOs
{
    class DAOArticle : DAO
    {
        public DAOArticle() : base()
        {
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
            DAOMarque daoMarque = new DAOMarque();
            DAOFamille daoFamille = new DAOFamille();
            DAOSousFamille daoSousFamille = new DAOSousFamille();

            int RefMarque = daoMarque.GetRefMarque(article.Marque);
            int RefFamille = daoFamille.GetRefFamille(article.Famille);
            int RefSousFamille = daoSousFamille.GetRefSousFamille(RefFamille, article.SousFamille);

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
            DAOMarque daoMarque = new DAOMarque();
            DAOFamille daoFamille = new DAOFamille();
            DAOSousFamille daoSousFamille = new DAOSousFamille();

            int RefMarque = daoMarque.GetRefMarque(ArticleUpdated.Marque);
            int RefFamille = daoFamille.GetRefFamille(ArticleUpdated.Famille);
            int RefSousFamille = daoSousFamille.GetRefSousFamille(RefFamille, ArticleUpdated.SousFamille);

            string Cmd = "UPDATE Articles " +
                        "SET Description = '@Description', RefSousFamille = @RefSousFamille, RefMarque = @RefMarque, PrixHT = @PrixHT, Quantite = @Quantite " +
                        "WHERE RefArticle = '@RefArticle';";

            SQLiteCommand Command = new SQLiteCommand(Cmd, Connection);

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

    }
}
