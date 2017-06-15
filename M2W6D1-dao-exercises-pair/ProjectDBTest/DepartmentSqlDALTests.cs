using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using ProjectDB.Models;
using System.Collections.Generic;
using ProjectDB.DAL;

namespace ProjectDBTest
{
	[TestClass]
	public class DepartmentSqlDALTests
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
		public void GetDepartmentsTest()
		{
			DepartmentSqlDAL departments = new DepartmentSqlDAL(connectionString);

			List<Department> department = departments.GetDepartments();

			Assert.IsNotNull(department);
			Assert.AreEqual(4, department.Count());
		}
		[TestMethod]
		public void CreateDepartmentTest()
		{
			DepartmentSqlDAL departments = new DepartmentSqlDAL(connectionString);

			Department ABC = new Department()
			{
				Name = "ABC"
			};

			bool isTrue = departments.CreateDepartment(ABC);

			Assert.AreEqual(true, isTrue);
		}
		[TestMethod]
		public void UpdateDepartment()
		{
			DepartmentSqlDAL departments = new DepartmentSqlDAL(connectionString);

			Department ABCD = new Department()
			{
				Name = "ABCD",
				Id = 2
			};

			bool isTrue = departments.UpdateDepartment(ABCD);
			List<Department> updatedDepartments = departments.GetDepartments();

			Assert.AreEqual("ABCD", updatedDepartments[0].Name);
			Assert.AreEqual(true, isTrue);

		}
	}
}
