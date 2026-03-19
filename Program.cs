using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;







namespace PieceUsur
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            LOGS log = new LOGS();
            Connect_db db = new Connect_db();

            log.writeLog("Lancement application piece usur ", "historique", 0);
            SMS_Usur sms = new SMS_Usur();
            // Check whether the environment variable exists.
            string value = Environment.GetEnvironmentVariable("DB_ROOT");
            if (value == null)
            {
                log.writeLog("Problème récupération environement d'exécution de l'application", "log", 1);
            }
            else 
            {
                // set the value of db root
                db.setDB_root(value);
                db.readDBfile();
                db.Connect(); //connection to database
                // Invoke this sample with an arbitrary set of command line arguments.
                //get arguments values and set values 
                //app won't be launch if can't get all arguments parameters
                try
                {

                    sms.SetOf(args[0]);         // OF
                    sms.SetOp(args[1]);         // OP
                    sms.SetCdc(args[2]);        // CDC
                    sms.SetArticle(args[3]);    // ARTICLE
                    sms.SetLigne(args[4]);     // Ligne
                    sms.SetOperateur(args[5]);  // USER/OPERATEUR
                    sms.SetPartrev(args[6]);     
                    sms.SetSnList(args[7]);     // SN "123$102$..."
                    sms.SetMachine(args[8]);     

                    var snList = sms.GetSnList();                

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    ApplicationConfiguration.Initialize();


                    PieceUsur_gestion pu = new PieceUsur_gestion(db, sms, log);
                    bool ouvrirForm = pu.TraiterPieceUsur();
                    if (ouvrirForm)
                    {
                        PieceUsur frm = new PieceUsur(db, sms, log, pu);
                        Application.Run(frm);
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    //message show when parameters mising
                    MessageBox.Show("Problème de recupération des arguments lors de l'exécusion de l'application");
                    log.writeLog("Problème de recupération des arguments lors de l'exécusion de l'application" + e.StackTrace, "log", 0);


                }
            }
        }
    }
}