using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using finalApplication.oConnection;

namespace finalApplication.Models
{ 
    public class Employees
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

        public string Add(int emp_id, string emp_name, string jop_titel, int start_hour, int total_hours, int leaving_hour, int jop_type, int days, string img_path,
            DateTime emp_dob, string user_name, string phone, int age, string city, string edu_degree)
        {
            //Open Connection
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {

                var USERS_SEQ = QueryReader("SELECT STD_ID_SEQ.NEXTVAL AS Student_ID FROM Student", CmdTrans, aOracleConnection);
                // var USERS_ID = USERS_SEQ.Rows[0]["USERS_ID"].ToString();

                var cmdText = "INSERT INTO EMPLOYEES ( " +
                                    "EMP_ID, " +
                                    "EMP_NAME, " +
                                    "JOP_TITEL," +
                                    "START_HOUR," +
                                    "TOTAL_HOURS," +
                                    "LEAVING_HOUR," +
                                    "JOP_TYPE," +
                                    "DAYS," +
                                    "IMG_PATH" +
                                    "EMP_DOB" +
                                    "USER_NAME" +
                                    "PHONE" +
                                    "AGE" +
                                    "CITY" +
                                    "EDU_DEGREE" +
                                ") VALUES( " +

                                    ":EMP_ID, " +
                                    ":EMP_NAME, " +
                                    ":JOP_TITEL," +
                                    ":START_HOUR," +
                                    ":TOTAL_HOURS," +
                                    ":LEAVING_HOUR," +
                                    ":JOP_TYPE," +
                                    ":DAYS," +
                                    ":IMG_PATH" +
                                    ":EMP_DOB" +
                                    ":USER_NAME" +
                                    ":PHONE" +
                                    ":AGE" +
                                    ":CITY" +
                                    ":EDU_DEGREE" +
                                ") ";

                // create command and set properties  
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = cmdText;

                cmd.Parameters.Add(":EMP_ID", OracleDbType.Int16).Value = emp_id;
                cmd.Parameters.Add(":EMP_NAME", OracleDbType.NVarchar2).Value = emp_name;
                cmd.Parameters.Add(":JOP_TITEL", OracleDbType.NVarchar2).Value = jop_titel;
                cmd.Parameters.Add(":START_HOUR", OracleDbType.Int16).Value = start_hour;
                cmd.Parameters.Add(":TOTAL_HOURS", OracleDbType.Int16).Value = total_hours;
                cmd.Parameters.Add(":LEAVING_HOUR", OracleDbType.Int16).Value = leaving_hour;
                cmd.Parameters.Add(":JOP_TYPE", OracleDbType.Int16).Value = jop_type;
                cmd.Parameters.Add(":DAYS", OracleDbType.Int16).Value = days;
                cmd.Parameters.Add(":IMG_PATH", OracleDbType.NVarchar2).Value = img_path;
                cmd.Parameters.Add(":EMP_DOB", OracleDbType.Date).Value = emp_dob;
                cmd.Parameters.Add(":USER_NAME", OracleDbType.NVarchar2).Value = user_name;
                cmd.Parameters.Add(":PHONE", OracleDbType.NVarchar2).Value = phone;
                cmd.Parameters.Add(":AGE", OracleDbType.Int16).Value = age;
                cmd.Parameters.Add(":CITY", OracleDbType.NVarchar2).Value = city;
                cmd.Parameters.Add(":EDU_DEGREE", OracleDbType.NVarchar2).Value = edu_degree;


                cmd.ExecuteNonQuery();


                CmdTrans.Commit();
                return user_name;

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

            public static string CALCULATE_TIME(String a, string b)
            {
                int inhour = Int32.Parse(a);
                int outhour = Int32.Parse(b);
                int ins = inhour * 60;
                int outs = outhour * 60;
                int total = (ins - outs);
                if (total < 0)
                {
                    total = total * -1;

                }
                total = total / 60;

                String s;
                s = total.ToString();
                return s;


            }
        

        public string DELETE_EMPLOYEE(string EMP_ID)
        {
            //Open Connection
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                var cmdText = "DELETE from EMPLOYEES where EMP_ID=  " +
                                    EMP_ID +

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


        public string Update(int emp_id, string emp_name, string jop_titel, int start_hour, int total_hours, int leaving_hour, int jop_type, int days, string img_path,
            DateTime emp_dob, string user_name, string phone, int age, string city, string edu_degree)
        {
            //Open Connection
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            Console.WriteLine(emp_name, phone);
            try
            {


                var cmdText = "UPDATE EMPLOYEES SET EMP_NAME= :EMP_NAME, JOP_TITEL= :JOP_TITEL, START_HOUR= :START_HOUR," +
                    " TOTAL_HOURS= :TOTAL_HOURS, LEAVING_HOUR= :LEAVING_HOUR, JOP_TYPE= :JOP_TYPE, DAYS= :DAYS , IMG_PATH= :IMG_PATH , EMP_DOB= :EMP_DOB" +
                    " USER_NAME= :USER_NAME, PHONE= :PHONE, AGE= :AGE, CITY= :CITY, EDU_DEGREE= :EDU_DEGREE where EMP_ID= :EMP_ID";



                // create command and set properties  
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = cmdText;



                cmd.Parameters.Add(":EMP_ID", OracleDbType.Int16).Value = emp_id;
                cmd.Parameters.Add(":EMP_NAME", OracleDbType.NVarchar2).Value = emp_name;
                cmd.Parameters.Add(":JOP_TITEL", OracleDbType.NVarchar2).Value = jop_titel;
                cmd.Parameters.Add(":START_HOUR", OracleDbType.Int16).Value = start_hour;
                cmd.Parameters.Add(":TOTAL_HOURS", OracleDbType.Int16).Value = total_hours;
                cmd.Parameters.Add(":LEAVING_HOUR", OracleDbType.Int16).Value = leaving_hour;
                cmd.Parameters.Add(":JOP_TYPE", OracleDbType.Int16).Value = jop_type;
                cmd.Parameters.Add(":DAYS", OracleDbType.Int16).Value = days;
                cmd.Parameters.Add(":IMG_PATH", OracleDbType.NVarchar2).Value = img_path;
                cmd.Parameters.Add(":EMP_DOB", OracleDbType.Date).Value = emp_dob;
                cmd.Parameters.Add(":USER_NAME", OracleDbType.NVarchar2).Value = user_name;
                cmd.Parameters.Add(":PHONE", OracleDbType.NVarchar2).Value = phone;
                cmd.Parameters.Add(":AGE", OracleDbType.Int16).Value = age;
                cmd.Parameters.Add(":CITY", OracleDbType.NVarchar2).Value = city;
                cmd.Parameters.Add(":EDU_DEGREE", OracleDbType.NVarchar2).Value = edu_degree;

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
