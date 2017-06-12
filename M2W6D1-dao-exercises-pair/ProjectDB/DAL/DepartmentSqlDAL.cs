using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectDB.DAL
{
    public class DepartmentSqlDAL
    {
		private string connectionString;
		private const string SQL_GetDepartment = "SELECT * FROM department";
		private const string SQL_CreateDepartment = @"INSERT INTO Department VALUES(@name)";
		private const string SQL_UpdateDepartment = @"UPDATE department SET name = @name WHERE department_id = @id";

        //Single Parameter Constructor
        public DepartmentSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Department> GetDepartments()
        {
			List<Department> output = new List<Department>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand(SQL_GetDepartment, conn);
					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						Department d = new Department();
						d.Id = Convert.ToInt32(reader["department_id"]);
						d.Name = Convert.ToString(reader["name"]);

						output.Add(d);
					}
				}
			}
			catch (SqlException e)
			{
				throw;
			}
			return output;
        }

        public bool CreateDepartment(Department newDepartment)
        {
		

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand(SQL_CreateDepartment, conn);
					cmd.Parameters.AddWithValue("@name", newDepartment.Name);
					int rowsAffected = cmd.ExecuteNonQuery();

					return rowsAffected > 0;
				}
			}
			catch (SqlException e)
			{
				throw;
			}
		}

        public bool UpdateDepartment(Department updatedDepartment)
        {
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand(SQL_UpdateDepartment, conn);
					cmd.Parameters.AddWithValue("@name", updatedDepartment.Name);
					cmd.Parameters.AddWithValue("@id", updatedDepartment.Id);	
					
					int rowsAffected = cmd.ExecuteNonQuery();

					return rowsAffected > 0;
				}
			}
			catch (SqlException e)
			{
				throw;
			}
		}

    }
}
