﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Bacchus.src.DAOs
{
    class DAOFamille : DAO
    {

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
    }
}
