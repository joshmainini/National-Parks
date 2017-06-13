using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectDB.Models;
using ProjectDB.DAL;
using System.Transactions;


namespace ProjectDBTest
{
	[TestClass]
	public class EmployeeSqlDALTest
	{
		private TransactionScope tran;
		private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=Projects;Integrated Security=True";


		[TestInitialize]
		public void Initialize()
		{   
			tran = new TransactionScope();

		}
		[TestCleanup]
		public void Cleanup()
		{
			tran.Dispose();
		}

		[TestMethod]
		public void GetAllEmployeesTest()
		{
			EmployeeSqlDAL employee = new EmployeeSqlDAL(connectionString);
			List<Employee> employees = employee.GetAllEmployees();

			Assert.AreEqual(12, employees.Count);

			
		}
		[TestMethod]
		public void GetEmployeesWithoutProjectsTest()
		{
			EmployeeSqlDAL employee = new EmployeeSqlDAL(connectionString);
			List<Employee> employees = employee.GetEmployeesWithoutProjects();

			Assert.AreEqual(1, employees.Count);
		}
		[TestMethod]
		public void SearchTest()
		{
			EmployeeSqlDAL employee = new EmployeeSqlDAL(connectionString);
			List<Employee> employees = employee.Search("Flo","Henderson");

			Assert.AreEqual(1, employees.Count);
		}
	}
}
