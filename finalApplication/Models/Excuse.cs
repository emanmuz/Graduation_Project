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

        public string DELETE_EXCUSE(string LEAVING_ID)
        {
            //Open Connection
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                var cmdText = "DELETE from LEAVING where LEAVING_ID=  " +
                                    LEAVING_ID +

                                "";
                // create command and set properties  
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();

                CmdTrans.Commit();
                return "1";

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

            public string Add(int leaving_id, string emp_name, string emp_id, string admin_id, int status, int tag, DateTime date)
            {
                //Open Connection
                Open();
                OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {

                    var USERS_SEQ = QueryReader("SELECT STD_ID_SEQ.NEXTVAL AS Student_ID FROM Student", CmdTrans, aOracleConnection);
                    // var USERS_ID = USERS_SEQ.Rows[0]["USERS_ID"].ToString();

                    var cmdText = "INSERT INTO EMPLOYEES ( " +
                                        "LEAVING_ID, " +
                                        "EMP_NAME, " +
                                        "EMP_ID," +
                                        "ADMIN_ID," +
                                        "STATUS," +
                                        "TAG," +
                                        "DATE," +

                                    ") VALUES( " +

                                          ":LEAVING_ID, " +
                                        ":EMP_NAME, " +
                                        ":EMP_ID," +
                                        ":ADMIN_ID," +
                                        ":STATUS," +
                                        ":TAG," +
                                        ":DATE," +
                                    ") ";

                    // create command and set properties  
                    OracleCommand cmd = aOracleConnection.CreateCommand();
                    cmd.Transaction = CmdTrans;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = cmdText;

                    cmd.Parameters.Add(":LEAVING_ID", OracleDbType.Int16).Value = leaving_id;
                    cmd.Parameters.Add(":EMP_NAME", OracleDbType.NVarchar2).Value = emp_name;
                    cmd.Parameters.Add(":ADMIN_ID", OracleDbType.NVarchar2).Value = admin_id;
                    cmd.Parameters.Add(":STATUS", OracleDbType.Int16).Value = status;
                    cmd.Parameters.Add(":TAG", OracleDbType.Int16).Value = tag;
                    cmd.Parameters.Add(":date", OracleDbType.NVarchar2).Value = date;



                    cmd.ExecuteNonQuery();


                    CmdTrans.Commit();
                    return emp_name;

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

           

     }
}
    


