using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.src.DAOs
{
    class DAOMarque : DAO
    {

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
        /// Ajoute une marque à la BDD et renvoie sa Ref
        /// </summary>
        /// <param name="NomMarque"></param>
        /// <returns></returns>
        public int AddMarque(string NomMarque)
        {
            string Cmd = "INSERT INTO Marques(Nom) VALUES('" + NomMarque + "')";
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
    }
}
