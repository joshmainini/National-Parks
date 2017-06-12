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
        private const string SQL_GetEmployees = "SELECT * FROM employee";
        private const string SQL_SearchEmployee = "SELECT * FROM employee WHERE first_name = @firstName AND last_name = @lastName";
        private const string SQL_EmployeesWithoutProject = "SELECT * FROM employee e LEFT JOIN project_employee pe ON pe.employee_id = e.employee_id WHERE project_id IS NULL";

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

                    SqlCommand cmd = new SqlCommand(SQL_GetEmployees, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee e = new Employee();
                        e.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        e.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        e.FirstName = Convert.ToString(reader["first_name"]);
                        e.LastName = Convert.ToString(reader["last_name"]);
                        e.JobTitle = Convert.ToString(reader["job_title"]);
                        e.Gender = Convert.ToString(reader["gender"]);
                        e.HireDate = Convert.ToDateTime(reader["hire_date"]);
                        e.BirthDate = Convert.ToDateTime(reader["birth_date"]);

                        output.Add(e);
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
            List<Employee> output = new List<Employee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_SearchEmployee, conn);
                    cmd.Parameters.AddWithValue("@firstName", firstname);
                    cmd.Parameters.AddWithValue("@lastName", lastname);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee e = new Employee();
                        e.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        e.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        e.FirstName = Convert.ToString(reader["first_name"]);
                        e.LastName = Convert.ToString(reader["last_name"]);
                        e.JobTitle = Convert.ToString(reader["job_title"]);
                        e.Gender = Convert.ToString(reader["gender"]);
                        e.HireDate = Convert.ToDateTime(reader["hire_date"]);
                        e.BirthDate = Convert.ToDateTime(reader["birth_date"]);

                        output.Add(e);
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }
            return output;
        }

        public List<Employee> GetEmployeesWithoutProjects()
        {
            List<Employee> output = new List<Employee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_EmployeesWithoutProject, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee e = new Employee();
                        e.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        e.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        e.FirstName = Convert.ToString(reader["first_name"]);
                        e.LastName = Convert.ToString(reader["last_name"]);
                        e.JobTitle = Convert.ToString(reader["job_title"]);
                        e.Gender = Convert.ToString(reader["gender"]);
                        e.HireDate = Convert.ToDateTime(reader["hire_date"]);
                        e.BirthDate = Convert.ToDateTime(reader["birth_date"]);

                        output.Add(e);
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }
            return output;
        }
    }
}
