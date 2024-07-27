using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DataTable1.Helper_Class
{
    public class CustomDBConfig
    {

        private readonly string _connectionString;

        public CustomDBConfig()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeDBConnectionString"].ConnectionString;
        }

        public void ExecuteStoredProcedure(string procedureName, List<SqlParameter> parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public SqlDataReader ExecuteStoredProcedureWithReader(string procedureName, List<SqlParameter> parameters)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(procedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }

            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }


        public void ExecuteQuery(string query, List<SqlParameter> parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public SqlDataReader ExecuteQueryWithReader(string query, List<SqlParameter> parameters)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, conn)
            {
                CommandType = CommandType.Text
            };

            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }

            conn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}