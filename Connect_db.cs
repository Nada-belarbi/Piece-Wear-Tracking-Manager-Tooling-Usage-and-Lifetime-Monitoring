using Oracle.ManagedDataAccess.Client;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace PieceUsur
{
    public class Connect_db : IDisposable
    {
        readonly LOGS log = new LOGS();
        static OracleConnection con;
        OracleCommand cmd;
        OracleDataReader reader;

        private static string DB_ROOT;
        private static string chaineConnexion;
        private static string environnement;

        readonly Password pass = new Password();
        string password;

        public void setDB_root(string db_root)
        {
            DB_ROOT = db_root;
        }

        public string getDB_root()
        {
            return DB_ROOT;
        }

        public string getEnv()
        {
            return environnement;
        }

        public void readDBfile()
        {
            string uid;
            string host_name;
            string port;
            string service_name;

            string db_path = getDB_root();
            db_path += "/connect_securedb.db";

            try
            {
                if (File.Exists(db_path))
                {
                    foreach (string line in File.ReadLines(db_path))
                    {
                        if (line.StartsWith("pwd"))
                        {
                            password = line.Trim();
                            password = line.Remove(0, 4);
                            password = pass.Decrypt(password);
                        }
                    }

                    var data = File
                        .ReadAllLines(db_path)
                        .Select(x => x.Split('='))
                        .Where(x => x.Length > 1)
                        .ToDictionary(x => x[0].Trim(), x => x[1]);

                    uid = data["uid"];
                    host_name = data["host_name"];
                    port = data["port"];
                    service_name = data["service_name"];
                    environnement = data["environnement"];

                    chaineConnexion = $"Data Source={host_name}:{port}/{service_name}; User Id={uid}; password={password}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Connect()
        {
            try
            {
                if (con == null)
                {
                    con = new OracleConnection();
                }

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.ConnectionString = chaineConnexion;
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                log.writeLog(ex.ToString(), "log", 1);
                MessageBox.Show(ex.Message);
            }
        }

        public void Close()
        {
            try
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }

                if (con != null)
                {
                    if (con.State != System.Data.ConnectionState.Closed)
                    {
                        con.Close();
                    }

                    con.Dispose();
                    con = null;
                }
            }
            catch (Exception ex)
            {
                log.writeLog("Erreur fermeture connexion : " + ex.Message, "log", 1);
            }
        }

        public OracleDataReader SelectData(string query)
        {
            try
            {
                Connect();

                cmd = con.CreateCommand();
                cmd.CommandText = query;

                reader = cmd.ExecuteReader();
                log.writeLog(query, "trace", 0);
            }
            catch (Exception ex)
            {
                log.writeLog(query, "trace", 0);
                log.writeLog(ex.ToString(), "log", 1);
                MessageBox.Show(ex.Message, "Error");
            }

            return reader;
        }

        public OracleDataReader Request(string query, params OracleParameter[] parameters)
        {
            try
            {
                Connect();

                cmd = con.CreateCommand();
                cmd.CommandText = query;
                cmd.BindByName = true;
                cmd.Parameters.Clear();

                if (parameters != null)
                {
                    foreach (OracleParameter p in parameters)
                    {
                        if (p.Value == null)
                            p.Value = DBNull.Value;

                        cmd.Parameters.Add(p);
                    }
                }

                reader = cmd.ExecuteReader();
                log.writeLog(query, "trace", 0);
            }
            catch (Exception ex)
            {
                log.writeLog($"Erreur exécution d'une requete {ex.Message} {ex.Source}", "log", 1);
            }

            return reader;
        }

        public int ExecuteNonQuery(string query, params OracleParameter[] parameters)
        {
            int nb = 0;

            try
            {
                Connect();

                cmd = con.CreateCommand();
                cmd.CommandText = query;
                cmd.BindByName = true;
                cmd.Parameters.Clear();

                if (parameters != null)
                {
                    foreach (OracleParameter p in parameters)
                    {
                        if (p.Value == null)
                            p.Value = DBNull.Value;

                        cmd.Parameters.Add(p);
                    }
                }

                nb = cmd.ExecuteNonQuery();
                log.writeLog(query, "trace", 0);
            }
            catch (Exception ex)
            {
                log.writeLog($"Erreur ExecuteNonQuery : {ex.Message} {ex.Source}", "log", 1);
            }

            return nb;
        }

        public void Dispose()
        {
            Close();

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }

            if (reader != null)
            {
                reader.Dispose();
                reader = null;
            }
        }
    }
}