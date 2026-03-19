using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PieceUsur
{
    public class PieceUsur_gestion
    {
        private readonly Connect_db db;
        private readonly SMS_Usur sms;
        private readonly LOGS log;

        public List<string> ListeCodeMontageException = new List<string>();

        private string of;
        private string op;
        private string article;
        private string ligne;
        private string user;
        private string cdc;
        private int qty;
        private string machine;
        private string partrev;

        public PieceUsur_gestion(Connect_db dbParam, SMS_Usur smsParam, LOGS logParam)
        {
            db = dbParam;
            sms = smsParam;
            log = logParam;

            of = sms.GetOf();
            op = sms.GetOp();
            article = sms.GetArticle();
            ligne = sms.GetLigne();
            user = sms.GetOperateur();
            cdc = sms.GetCdc();
            machine = sms.GetMachine();
            partrev = sms.GetPartrev();
            qty = (sms.GetSnList() != null ? sms.GetSnList().Length : 0);
        }

        public OracleDataReader ChargerPiecesDB()
        {
            string query =
                " SELECT code_montage, description, compteur, dureedev FROM ( " +
                "   SELECT 1 AS prio, " +
                "          m.CODEMONTAGE AS code_montage, " +
                "          m.DESCRIPTION AS description, " +
                "          d.COMPTEUR AS compteur, " +
                "          d.DUREEDEVIE AS dureedev " +
                "   FROM TBSCT.EPE_UUM_PIECEUSURE_MONTAGES m " +
                "   LEFT JOIN TBSCT.EPE_UUM_PIECEUSURE_DUREESVIE d " +
                "          ON d.CODEMONTAGE = m.CODEMONTAGE " +
                "   WHERE m.ARTICLE = :p_article " +
                "     AND m.STATE = 'ACTIVE' " +
                "     AND :p_cdc IS NOT NULL " +
                "     AND m.CDC = :p_cdc " +
                "   UNION ALL " +
                "   SELECT 2 AS prio, " +
                "          m.CODEMONTAGE AS code_montage, " +
                "          m.DESCRIPTION AS description, " +
                "          d.COMPTEUR AS compteur, " +
                "          d.DUREEDEVIE AS dureedev " +
                "   FROM TBSCT.EPE_UUM_PIECEUSURE_MONTAGES m " +
                "   LEFT JOIN TBSCT.EPE_UUM_PIECEUSURE_DUREESVIE d " +
                "          ON d.CODEMONTAGE = m.CODEMONTAGE " +
                "   WHERE m.ARTICLE = :p_article " +
                "     AND m.STATE = 'ACTIVE' " +
                "     AND (m.CDC IS NULL OR TRIM(m.CDC) = '') " +
                " ) " +
                " ORDER BY prio, code_montage";

            OracleDataReader data = db.Request(query,
                new OracleParameter("p_article", article),
                new OracleParameter("p_cdc", cdc)
            );

            return data;
        }

        public void IncrementerCompteurPiece(string codeMontage)
        {
            try
            {
                string query =
                    " UPDATE TBSCT.EPE_UUM_PIECEUSURE_DUREESVIE " +
                    " SET COMPTEUR = COMPTEUR + :p_qty " +
                    " WHERE CODEMONTAGE = :p_code ";

                db.ExecuteNonQuery(query,
                    new OracleParameter("p_qty", qty),
                    new OracleParameter("p_code", codeMontage)
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur incrémentation : " + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void InsererHistoSelection(string codeMontage, string compteur)
        {
            try
            {
                string Article_partrev = article + "_" + partrev;

                string query = " INSERT INTO TBSCT.EPE_UUM_PIECEUSURE_HISTO " +
                               " (DATECHGT,UTILISATEUR,ARTICLE,NOOPERATION,MACHINE,CODEMONTAGE,ADMINISTRATEUR,ACTION,LIGNE,NUMOF,COMPTEUR) " +
                               " VALUES " +
                               " (SYSDATE, " +
                               "  '" + user + "', " +
                               "  '" + Article_partrev + "', " +
                               "  '" + op + "', " +
                               "  '" + machine + "', " +
                               "  '" + codeMontage + "', " +
                               "  NULL," +
                               "  'sélection du montage'," +
                               "  '" + ligne + "', " +
                               "  '" + of + "', " +
                               "  '" + compteur + "') ";

                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                log.writeLog("Erreur validation : " + ex.Message + " " + ex.StackTrace, "log", 1);
            }
        }

        public void InsererHistoIncrement(string codeMontage, int compteur)
        {
            try
            {
                int compteur_Inc = compteur + qty;
                string Article_partrev = article + "_" + partrev;

                string query = " INSERT INTO TBSCT.EPE_UUM_PIECEUSURE_HISTO " +
                               " (DATECHGT,UTILISATEUR,ARTICLE,NOOPERATION,MACHINE,CODEMONTAGE,ADMINISTRATEUR,ACTION,LIGNE,NUMOF,COMPTEUR) " +
                               " VALUES " +
                               " (SYSDATE, " +
                               "  '" + user + "', " +
                               "  '" + Article_partrev + "', " +
                               "  '" + op + "', " +
                               "  '" + machine + "', " +
                               "  '" + codeMontage + "', " +
                               "  NULL," +
                               "  'incrémentation du compteur'," +
                               "  '" + ligne + "', " +
                               "  '" + of + "', " +
                               "  '" + compteur_Inc.ToString() + "') ";

                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                log.writeLog("Erreur validation : " + ex.Message + " " + ex.StackTrace, "log", 1);
            }
        }

        public (List<string>, int) CodeMontage_Select()
        {
            List<string> listeCodeMontage = new List<string>();
            int count = 0;
            OracleDataReader data = null;

            try
            {
                string sqlqry =
                    " SELECT DISTINCT m.CODEMONTAGE, " +
                    "        COUNT(DISTINCT m.CODEMONTAGE) OVER() AS NB_TOTAL " +
                    " FROM TBSCT.EPE_UUM_PIECEUSURE_MONTAGES m INNER JOIN " +
                    " TBSCT.EPE_UUM_PIECEUSURE_SELECTIONS s ON m.CODEMONTAGE = s.CODEMONTAGE " +
                    " WHERE TRIM(m.ARTICLE) = TRIM('" + article + "') AND m.STATE = 'ACTIVE' AND s.JOBNUMBER='" + of + "'";

                data = db.Request(sqlqry);

                if (data != null)
                {
                    while (data.Read())
                    {
                        listeCodeMontage.Add(data["CODEMONTAGE"].ToString());
                        count = Convert.ToInt32(data["NB_TOTAL"]);
                    }
                }
            }
            catch (Exception ex)
            {
                log.writeLog("Erreur validation : " + ex.Message + " " + ex.StackTrace, "log", 1);
            }
            finally
            {
                if (data != null && !data.IsClosed)
                {
                    data.Close();
                }
            }

            return (listeCodeMontage, count);
        }

        public int VerifierNbOF_Total()
        {
            int nb2 = 0;
            string Article_partrev = article + "_" + partrev;
            OracleDataReader data = null;

            try
            {
                string query =
                    " SELECT COUNT(*) AS NB " +
                    " FROM TBSCT.EPE_UUM_PIECEUSURE_SELECTIONS " +
                    " WHERE " +
                    "   TRIM(ARTICLE) = TRIM('" + Article_partrev + "') " +
                    "   AND JOBNUMBER = '" + of + "' ";

                data = db.Request(query);

                if (data != null && data.Read())
                {
                    nb2 = Convert.ToInt32(data["NB"]);
                }
            }
            catch (Exception ex)
            {
                log.writeLog("Erreur validation : " + ex.Message + " " + ex.StackTrace, "log", 1);
            }
            finally
            {
                if (data != null && !data.IsClosed)
                {
                    data.Close();
                }
            }

            return nb2;
        }

        public int VerifierNbOF(string codeMontage)
        {
            int nb = 0;
            string Article_partrev = article + "_" + partrev;
            OracleDataReader data = null;

            try
            {
                string query =
                    " SELECT COUNT(*) AS NB " +
                    " FROM TBSCT.EPE_UUM_PIECEUSURE_SELECTIONS " +
                    " WHERE CODEMONTAGE = '" + codeMontage + "' " +
                    "   AND TRIM(ARTICLE) = TRIM('" + Article_partrev + "') " +
                    "   AND JOBNUMBER = '" + of + "' ";

                data = db.Request(query);

                if (data != null && data.Read())
                {
                    nb = Convert.ToInt32(data["NB"]);
                }
            }
            catch (Exception ex)
            {
                log.writeLog("Erreur validation : " + ex.Message + " " + ex.StackTrace, "log", 1);
            }
            finally
            {
                if (data != null && !data.IsClosed)
                {
                    data.Close();
                }
            }

            return nb;
        }

        public void InsertOfSelection(string codeMontage)
        {
            try
            {
                string Article_partrev = article + "_" + partrev;

                string sqlqry =
                    " INSERT INTO TBSCT.EPE_UUM_PIECEUSURE_SELECTIONS " +
                    " (CODEMONTAGE, JOBNUMBER, NOOPERATION, ARTICLE, MACHINE, QTY, DATECREATION, USERCREATION, LIGNE, CDC) " +
                    " VALUES ( " +
                    " '" + codeMontage + "', " +
                    " '" + of + "', " +
                    " '" + op + "', " +
                    " '" + Article_partrev + "', " +
                    " '" + machine + "', " +
                    " '" + qty.ToString() + "', " +
                    " SYSDATE, " +
                    " '" + user + "', " +
                    " '" + ligne + "', " +
                    " '" + cdc + "' ) ";

                db.ExecuteNonQuery(sqlqry);
            }
            catch (Exception ex)
            {
                log.writeLog("Erreur validation : " + ex.Message + " " + ex.StackTrace, "log", 1);
            }
        }

        public string Auto_Selection(string machine)
        {
            string AutoSelection = null;
            OracleDataReader data = null;

            try
            {
                string sqlqry = " SELECT AUTO_SELECTION FROM TBSCT.EPE_UUM_PIECEUSURE_MODES D WHERE MACHINE ='" + machine + "'";

                data = db.Request(sqlqry);

                if (data != null && data.Read())
                {
                    AutoSelection = data["AUTO_SELECTION"].ToString();
                }
            }
            finally
            {
                if (data != null && !data.IsClosed)
                {
                    data.Close();
                }
            }

            return AutoSelection;
        }

        public bool TraiterPieceUsur()
        {
            OracleDataReader data = null;

            try
            {
                data = ChargerPiecesDB();

                if (qty == 0)
                {
                    log.writeLog("Aucun SN reçu.", "log", 0);
                    return false;
                }

                if (data == null)
                {
                    log.writeLog("Aucune donnée trouvée.", "log", 0);
                    return false;
                }

                List<string> listeCodeMontage = new List<string>();
                List<int> listeCompteur = new List<int>();
                List<int> listeDureeMax = new List<int>();

                while (data.Read())
                {
                    string codeMontage = data["code_montage"] != DBNull.Value ? data["code_montage"].ToString() : "";

                    int compteur = 0;
                    int dureeMax = 0;

                    int.TryParse(data["compteur"] != DBNull.Value ? data["compteur"].ToString() : "0", out compteur);
                    int.TryParse(data["dureedev"] != DBNull.Value ? data["dureedev"].ToString() : "0", out dureeMax);

                    listeCodeMontage.Add(codeMontage);
                    listeCompteur.Add(compteur);
                    listeDureeMax.Add(dureeMax);
                }

                if (!data.IsClosed)
                {
                    data.Close();
                }

                int count = listeCodeMontage.Count;
                string AUTO_SELECTION = Auto_Selection(machine);

                if (count == 0)
                {
                    log.writeLog("Aucun code montage trouvé pour cet article.", "log", 0);
                    return false;
                }

                bool ouvrirForm = false;
                bool traitementAutoEffectue = false;

                ListeCodeMontageException.Clear();
                bool ofDejaSelectionne = VerifierNbOF_Total() > 0;

                for (int i = 0; i < listeCodeMontage.Count; i++)
                {
                    string codeMontage = listeCodeMontage[i];
                    int compteur = listeCompteur[i];
                    int dureeMax = listeDureeMax[i];
                    int nb = VerifierNbOF(codeMontage);

                    if (ofDejaSelectionne && nb == 0)
                    {
                        continue;
                    }

                    if ((nb >= 1 && compteur <= dureeMax) || (AUTO_SELECTION == "ACTIVE" && compteur <= dureeMax) || (count == 1 && compteur <= dureeMax))
                    {
                        if (nb == 0)
                        {
                            InsertOfSelection(codeMontage);
                        }

                        IncrementerCompteurPiece(codeMontage);
                        InsererHistoSelection(codeMontage, compteur.ToString());
                        InsererHistoIncrement(codeMontage, compteur);

                        traitementAutoEffectue = true;
                    }
                    else if (compteur > dureeMax || (AUTO_SELECTION == "DESACTIVE" && compteur < dureeMax))
                    {
                        ouvrirForm = true;
                        ListeCodeMontageException.Add(codeMontage);
                    }
                }

                if (ouvrirForm)
                {
                    return true;
                }

                if (traitementAutoEffectue)
                {
                    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                log.writeLog("Erreur validation : " + ex.Message + " " + ex.StackTrace, "log", 1);
                return false;
            }
            finally
            {
                if (data != null && !data.IsClosed)
                {
                    data.Close();
                }
            }
        }
    }
}