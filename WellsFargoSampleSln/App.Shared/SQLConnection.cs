using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace App.Shared
{
    public class SQLConnection 
    {
        private string _connectionString;

        public SQLConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataSet ExecuteQuery(string query)
        {
            SqlConnection connection = null;
            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = new DataSet();

            try
            {
               
                    connection = new SqlConnection(_connectionString);
                    sqlDataAdapter = new SqlDataAdapter(query, connection);
                    connection.Open();

                    sqlDataAdapter.Fill(ds);

                    return ds;
              
            }
            finally
            {
                if (sqlDataAdapter != null)
                {
                    sqlDataAdapter.Dispose();
                }

                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public DataSet ExecuteStoredProc(string spName, IEnumerable<SqlParameter> paramList)
        {
            SqlConnection connection = null;
            DataSet ds = new DataSet();

            try
            {
                    connection = new SqlConnection(_connectionString);

                    var command = new SqlCommand(spName, connection);
                    command.CommandTimeout = 300; //300 seconds
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (SqlParameter param in paramList)
                    {
                        command.Parameters.Add(param);
                    }

                    connection.Open();

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = command;
                    sqlDataAdapter.Fill(ds);

                    return ds;
              
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public DataSet ExecuteStoredProc(string spName)
        {
            SqlConnection connection = null;
            DataSet ds = new DataSet();

            try
            {
               
                    connection = new SqlConnection(_connectionString);

                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandTimeout = 300; //300 seconds
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = command;
                    sqlDataAdapter.Fill(ds);

                    return ds;
               
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }


        public bool TestConnection()
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand("SELECT 1", connection);

                connection.Open();

                var result = (int)command.ExecuteScalar();

                return result == 1;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection = null;
                }

                command = null;
            }
        }

    }
}
