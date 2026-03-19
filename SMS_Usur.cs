using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceUsur
{
    public class SMS_Usur
    {
        private static string OP;
        private static string OF;
        private static string CDC;
        private static string OPERATEUR;
        private static string ARTICLE;
        private static string LIGNE;
        private static string Partrev;
        private static string Machine;


        private static string[] SN_LIST = new string[] { };
       

        public void SetOp(string op)
        {
            OP = op;
        }
        public string GetOp()
        {
            return OP;
        }
        public void SetOf(string of)
        {
            OF = of;
        }
        public string GetOf()
        {
            return OF;
        }
        public void SetCdc(string cdc)
        {
            CDC = cdc;
        }

        public string GetCdc()
        {
            return CDC;
        }
        public void SetOperateur(string opt)
        {
            OPERATEUR = opt;
        }

        public string GetOperateur()
        {
            return OPERATEUR;
        }
        public void SetArticle(string art)
        {
            ARTICLE = art;
        }

        public string GetArticle()
        {
            return ARTICLE;
        }
        public void SetLigne(string ligne)
        {
             LIGNE = ligne;
        }

        public string GetLigne() 
        {
            return LIGNE;
        }
        public void SetPartrev(string partrev)
        {
            Partrev = partrev;
        }
        public string GetPartrev()
        {
            return Partrev;
        }
        public void SetSnList(string snRaw)
        {
            if (string.IsNullOrWhiteSpace(snRaw))
            {
                SN_LIST = new string[] { };
                return;
            }

            SN_LIST = snRaw
                .Split(new[] { '$' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .Distinct()
                .ToArray();
        }

        public string[] GetSnList()
        {
            return SN_LIST;
        }

        public void SetMachine(string machine)
        {
            Machine = machine;
        }
        public string GetMachine()
        {
            return Machine;
        }
    }
}
