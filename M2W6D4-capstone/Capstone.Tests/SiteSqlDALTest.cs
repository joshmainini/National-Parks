using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.Configuration;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone.Tests
{
    [TestClass]
    public class SiteSqlDALTest
    {
        private TransactionScope tran;
		private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
		//private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;

		[TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
        }

        /*
        * CLEANUP
        * Rollback the Transaction and get rid of the new records added for the test.        
        */
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }
    }
}
