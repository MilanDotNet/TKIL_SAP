using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Odbc;
using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace PatronashAPI
{
    public class CommonDB
    {
        private readonly IWebHostEnvironment _env;
        string strConnString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        IWebHostEnvironment env;
        public CommonDB()
        {
            GC.Collect();

            conn = new SqlConnection();
            conn.ConnectionString = strConnString;
            cmd = new SqlCommand();
            cmd.Connection = conn;
            _env = env;
        }
        public DataTable GetDataTable(string SpName, SqlParameter[] ParaColl)
        {
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd.CommandText = SpName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(ParaColl);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                return dt;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataSet GetDataSet(string SpName, SqlParameter[] ParaColl)
        {
            DataSet ds = new DataSet();
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd.CommandText = SpName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(ParaColl);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                return ds;
            }
            finally
            {
                conn.Close();
            }
        }

        public string ExecuteQuery(string Query)
        {

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 60;
                cmd.CommandText = Query;
                cmd.ExecuteNonQuery();

                return "Sucess";
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
            finally
            {
                conn.Close();
            }
        }

        public string CRUDOperation(string SpName, SqlParameter[] ParaColl)
        {

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd.CommandText = SpName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(ParaColl);
                cmd.ExecuteNonQuery();

                return "Sucess";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                conn.Close();
            }
        }

        public int CRUDOperationInt(string SpName, SqlParameter[] ParaColl)
        {
            int flg = 0;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd.CommandText = SpName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(ParaColl);
                cmd.ExecuteNonQuery();
                flg = 1;
                return flg;
            }
            catch (Exception)
            {
                return flg;
            }
            finally
            {
                conn.Close();
            }
        }

        public void LogMessage(string Message)
        {

            string fileName = Path.Combine(_env.ContentRootPath, "Logs.txt");
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " : " + Message);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}