using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using finalApplication.oConnection;

namespace finalApplication.Models
{
    public class Excuse
    {
        public dynamic DefaultView { get; internal set; }

        dbAccess con1;
        OracleCommand cmd;
        OracleConnection aOracleConnection;

        public DataTable QueryReader(string QUERY)
        {
            //Open Connection
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                // Set the command
                cmd = new OracleCommand(QUERY, aOracleConnection);
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                // Bind 
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                CmdTrans.Commit();
                return dt;
            }
            catch (Exception ex)
            {
                CmdTrans.Rollback();
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                Close();
            }
        }
        public DataTable QueryReader(string QUERY, OracleTransaction CmdTrans, OracleConnection aaOracleConnection)
        {
            try
            {
                // Set the command
                cmd = new OracleCommand(QUERY, aaOracleConnection);
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                // Bind 
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        void Open()
        {
            con1 = new dbAccess();
            aOracleConnection = con1.get_con();
        }

        void Close()
        {
            con1.Close(aOracleConnection);
        }
    }
}
