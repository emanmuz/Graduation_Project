using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace finalApplication.oConnection
{
    public class dbAccess
    {
        OracleConnection aOracleConnection;

        public dbAccess()
        {
            aOracleConnection = GetConnection();
        }

        public OracleConnection get_con()
        {
            return aOracleConnection;
        }


        public OracleConnection GetConnection()
        {

            if (aOracleConnection != null)
            {
                if (aOracleConnection.State != ConnectionState.Open)
                    aOracleConnection.Open();
            }
            else
            {

                string connString = connectionString();

                if (aOracleConnection == null)
                    aOracleConnection = new OracleConnection(connString);
                aOracleConnection.Open();
            }


            return aOracleConnection;


        }

        public void Close(OracleConnection aOracleConnection)
        {
            if (aOracleConnection != null && aOracleConnection.State == ConnectionState.Open)
            {
                aOracleConnection.Close();
            }
        }

        public string connectionString()
        {

            string json = "";
            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                json = r.ReadToEnd();
            }
            
            dynamic array = JsonConvert.DeserializeObject(json);
            return array["OracleConnectionString"]["ConnectionString"];
        }
    }
}
