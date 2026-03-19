using Oracle.ManagedDataAccess.Client;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PieceUsur
{
    public partial class PieceUsur : Form
    {
        private readonly Connect_db db;
        private readonly SMS_Usur sms;
        private readonly LOGS log;
        private readonly PieceUsur_gestion pu;

        private string of;
        private string op;
        private string article;
        private string ligne;
        private string user;
        private string cdc;
        private int qty;
        private string machine;
        private string partrev;

        public PieceUsur(Connect_db dbParam, SMS_Usur smsParam, LOGS logParam, PieceUsur_gestion pieceUsurGestionParam)
        {
            InitializeComponent();

            db = dbParam;
            sms = smsParam;
            log = logParam;
            pu = pieceUsurGestionParam;
        }

        private void PieceUsur_Load(object sender, EventArgs e)
        {
            try
            {
                of = sms.GetOf();
                op = sms.GetOp();
                article = sms.GetArticle();
                ligne = sms.GetLigne();
                user = sms.GetOperateur();
                cdc = sms.GetCdc();
                machine = sms.GetMachine();
                partrev = sms.GetPartrev();
                qty = (sms.GetSnList() != null ? sms.GetSnList().Length : 0);

                Date_textBox.ReadOnly = true;
                OP_textBox.ReadOnly = true;
                CDC_textBox.ReadOnly = true;
                OF_textBox.ReadOnly = true;
                Article_textBox.ReadOnly = true;

                Date_textBox.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                OP_textBox.Text = op;
                CDC_textBox.Text = cdc;
                OF_textBox.Text = of;
                Article_textBox.Text = article;
                User_Text.Text = user;
                Machine_Texbox.Text = machine;

                CreerColonnesPieceGrid();
                AlimenterPieceGrid();
                AjusterHauteurGrid();
            }
            catch (Exception ex)
            {
                log.writeLog("Erreur chargement formulaire PieceUsur : " + ex.Message + " " + ex.StackTrace, "log", 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool isChecked1 = false;

                foreach (DataGridViewRow row in Piece_dataGridView.Rows)
                {
                    bool isChecked = row.Cells["Choix"].Value != null && (bool)row.Cells["Choix"].Value == true;

                    if (isChecked)
                    {
                        isChecked1 = true;

                        string codeMontage = row.Cells["NomPiece"].Value.ToString();
                        int compteur = 0;
                        int.TryParse(row.Cells["Compteur"].Value?.ToString(), out compteur);
                        int nb = pu.VerifierNbOF(codeMontage);

                        if (nb == 0)
                        {
                            pu.InsertOfSelection(codeMontage);
                        }

                        pu.IncrementerCompteurPiece(codeMontage);
                        pu.InsererHistoSelection(codeMontage, compteur.ToString());
                        pu.InsererHistoIncrement(codeMontage, compteur);
                    }
                }

                if (isChecked1)
                {
                    this.Close();
                }
                else
                {
                    DialogResult result = MessageBox.Show(
                        "Vous n'avez coché aucune case. Êtes-vous sûr de vouloir quitter ?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                log.writeLog("Erreur validation : " + ex.Message + " " + ex.StackTrace, "log", 1);
            }
        }

        private void AlimenterPieceGrid()
        {
            OracleDataReader data = null;

            try
            {
                Piece_dataGridView.Rows.Clear();

                data = pu.ChargerPiecesDB();

                int nb = 0;

                if (data != null)
                {
                    while (data.Read())
                    {
                        string codeMontageDb = data["code_montage"] != DBNull.Value ? data["code_montage"].ToString() : "";

                        if (pu.ListeCodeMontageException.Count > 0 && !pu.ListeCodeMontageException.Contains(codeMontageDb))
                        {
                            continue;
                        }

                        nb++;

                        string description = data["description"] != DBNull.Value ? data["description"].ToString() : "";

                        int compteur = 0;
                        int dureeMax = 0;

                        int.TryParse(data["compteur"] != DBNull.Value ? data["compteur"].ToString() : "0", out compteur);
                        int.TryParse(data["dureedev"] != DBNull.Value ? data["dureedev"].ToString() : "0", out dureeMax);

                        int index = Piece_dataGridView.Rows.Add(false, codeMontageDb, description, compteur, dureeMax);

                        if (compteur > dureeMax && dureeMax > 0)
                        {
                            Piece_dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.Red;
                            Piece_dataGridView.Rows[index].DefaultCellStyle.ForeColor = Color.White;
                        }
                    }
                }
                else
                {
                    log.writeLog("ChargerPiecesDB a retourné null.", "log", 1);
                }
            }
            catch (Exception ex)
            {
                log.writeLog("Erreur alimentation Piece_dataGridView : " + ex.Message + " " + ex.StackTrace, "log", 1);
            }
            finally
            {
                if (data != null && !data.IsClosed)
                {
                    data.Close();
                }
            }
        }

        private void AjusterHauteurGrid()
        {
            Piece_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            int hauteur = Piece_dataGridView.ColumnHeadersHeight;

            foreach (DataGridViewRow row in Piece_dataGridView.Rows)
            {
                hauteur += row.Height;
            }

            Piece_dataGridView.Height = hauteur + 2;
            Piece_dataGridView.BackgroundColor = SystemColors.Window;

            Piece_dataGridView.AllowUserToAddRows = false;
            Piece_dataGridView.AllowUserToDeleteRows = false;
            Piece_dataGridView.AllowUserToResizeColumns = false;
            Piece_dataGridView.AllowUserToResizeRows = false;
            Piece_dataGridView.AllowUserToOrderColumns = false;

            Piece_dataGridView.Columns["Choix"].ReadOnly = false;
            Piece_dataGridView.Columns["NomPiece"].ReadOnly = true;
            Piece_dataGridView.Columns["Description"].ReadOnly = true;
            Piece_dataGridView.Columns["Compteur"].ReadOnly = true;
            Piece_dataGridView.Columns["DureeMax"].ReadOnly = true;
        }

        private void CreerColonnesPieceGrid()
        {
            try
            {
                Piece_dataGridView.Columns.Clear();
                Piece_dataGridView.AutoGenerateColumns = false;
                Piece_dataGridView.AllowUserToAddRows = false;
                Piece_dataGridView.RowHeadersVisible = false;

                DataGridViewCheckBoxColumn colCheck = new DataGridViewCheckBoxColumn();
                colCheck.HeaderText = "Choix";
                colCheck.Name = "Choix";
                colCheck.Width = 60;
                Piece_dataGridView.Columns.Add(colCheck);

                DataGridViewTextBoxColumn colNom = new DataGridViewTextBoxColumn();
                colNom.HeaderText = "Nom Pièce";
                colNom.Name = "NomPiece";
                colNom.Width = 150;
                Piece_dataGridView.Columns.Add(colNom);

                DataGridViewTextBoxColumn colDesc = new DataGridViewTextBoxColumn();
                colDesc.HeaderText = "Description";
                colDesc.Name = "Description";
                colDesc.Width = 200;
                Piece_dataGridView.Columns.Add(colDesc);

                DataGridViewTextBoxColumn colCompteur = new DataGridViewTextBoxColumn();
                colCompteur.HeaderText = "Compteur Actuel";
                colCompteur.Name = "Compteur";
                colCompteur.Width = 120;
                Piece_dataGridView.Columns.Add(colCompteur);

                DataGridViewTextBoxColumn colMax = new DataGridViewTextBoxColumn();
                colMax.HeaderText = "Durée Vie Max";
                colMax.Name = "DureeMax";
                colMax.Width = 120;
                Piece_dataGridView.Columns.Add(colMax);
            }
            catch (Exception ex)
            {
                log.writeLog("Erreur création colonnes Piece_dataGridView : " + ex.Message + " " + ex.StackTrace, "log", 1);
            }
        }

    }
}