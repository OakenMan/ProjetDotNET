﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.src.DAOs
{
    class DAOSousFamille : DAO
    {

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
                else
                {
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
