using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace ProjectDB.DAL
{
    public class EmployeeSqlDAL
    {
        private string connectionString;
		private const string SQL_GetAllEmployees = "SELECT * FROM employee";

        // Single Parameter Constructor
        public EmployeeSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Employee> GetAllEmployees()
        {
			List<Employee> output = new List<Employee>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand(SQL_GetAllEmployees, conn);
					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						Employee e = new Employee();
						e.DepartmentId = Convert.ToInt32(reader["department_id"]);
						e.EmployeeId = Convert.ToInt32(reader["employee_id"]);
						e.BirthDate = Convert.ToDateTime(reader["birth_date"]);

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

        public List<Employee> Search(string firstname, string lastname)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetEmployeesWithoutProjects()
        {
            throw new NotImplementedException();
        }
    }
}
