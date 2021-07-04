using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Data.SqlClient;

namespace Medico
{
    class DB
    {
        public static string connectionStr = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=hoData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //connection
        SqlConnection conn = new SqlConnection(connectionStr);
        //queries
        public SqlCommand cmd = new SqlCommand();

        public void SetCommand(string SQLStatement)
        {

            // cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = SQLStatement;
        }

        public  bool RunNonQuery(string SQLStatement, string Message = "")
        {
            bool test = false;
            try
            {
                SetCommand(SQLStatement);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Open();
                }
                else if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //conn.Open();
                //bnfz el queries
                 cmd.ExecuteNonQuery();
                if (Message != "")
                {
                    MessageBox.Show(Message);
                    test = true;
                }
                return test;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
                return test;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable RunReader(string Selectstatement)
        {
            // return data in tables
            DataTable tbl = new DataTable();
            SetCommand(Selectstatement);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
            else if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                //read from database
                tbl.Load( cmd.ExecuteReader());
                conn.Close();
                return tbl;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
                return tbl;
            }

        }
        public DataSet RunReaderds(string Selectstatement)
        {
            DataSet dts = new DataSet();
            //SetCommand(Selectstatement);
       
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Open();
                }
                else if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            try
            {
                SqlDataAdapter a = new SqlDataAdapter(Selectstatement, conn);
                // tbl.Load(cmd.ExecuteReader());

                a.Fill(dts);
            }
            catch
            { }
            finally
            {
                conn.Close();
            }
            return dts;
        }


        //        void add()
        //        {
        //            SqlConnection conn = new SqlConnection(connectionStr);
        //            conn.Open();
        //            SqlCommand cmd1 = new SqlCommand(@"INSERT INTO IMS_COMPLAINTS (COMPLAINT_ID , COM_SER , BRANCH_CODE ,PROVIDER_CODE,SUBJECT_CODE,Esclated_to,PROBLEM ,COM_DATE,COM_REPLAY,SOLVED_BY,CREATED_BY,UPDATED_BY,COM_CHECKED,PROVIDER_NAME,BRANCH_NAME)  Values
        //                                                                                (:ID,:SER,:PRAN,:SUBJECT,:PROV,:SUB,:ESCL,:PROB,:DATE,:REPLAY,:SOLVED,:CREATED,:UPDATED,:SHECKED,:PRNAME,:BRANAME)", conn);
        //            cmd1.Parameters.Add(":ID", SqlDbType.Varchar2).Value = Convert.ToInt32(textBox1.Text.ToString());
        //            cmd1.Parameters.Add(":name", SqlDbType.Varchar2).Value = textBox2.Text.ToString();
        //            cmd1.ExecuteNonQuery();
        //            conn.Close();

        //        }
    }
}
