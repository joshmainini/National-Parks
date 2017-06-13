using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectDB.Models;
using ProjectDB.DAL;
using System.Transactions;
using System.Data.SqlClient;


namespace ProjectDBTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ProjectSqlDALTests

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
        public void GetAllProjectsTest()
        {
            ProjectSqlDAL project = new ProjectSqlDAL(connectionString);
            List<Project> projects = project.GetAllProjects();

            Assert.AreEqual(7, projects.Count);
        }

        [TestMethod]
        public void AssignEmployeeToProjectTest()
        {
            ProjectSqlDAL project = new ProjectSqlDAL(connectionString);
            bool isAssigned = project.AssignEmployeeToProject(1, 2);

            Assert.AreEqual(true, isAssigned);
        }

        [TestMethod]
        public void RemoveEmployeeFromProjectTest()
        {
            ProjectSqlDAL project = new ProjectSqlDAL(connectionString);
            bool isAssigned = project.RemoveEmployeeFromProject(1, 3);

            Assert.AreEqual(true, isAssigned);
        }

        [TestMethod]
        public void CreateProjectTest()
        {
            ProjectSqlDAL project = new ProjectSqlDAL(connectionString);
            Project Baz = new Project
            {
                Name = "Baz",
                StartDate = Convert.ToDateTime("2010-01-01"),
                EndDate = Convert.ToDateTime("2018-01-01")
            };

            bool isAssigned = project.CreateProject(Baz);

            Assert.AreEqual(true, isAssigned);
        }

    }
}
