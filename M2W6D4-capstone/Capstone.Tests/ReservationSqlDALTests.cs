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
    public class ReservationSqlDALTests
    {
        private TransactionScope tran;
        private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
		   /*ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;*/

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

        [TestMethod]
        public void GetAvailableSitesTest()
        {
			int campgroundId = 1;
            List<Site> result = new List<Site>();
            ReservationSqlDAL reservation = new ReservationSqlDAL(connectionString);


            result = reservation.GetAvailableSites(Convert.ToDateTime("2017-06-24"), Convert.ToDateTime("2017-06-25"), campgroundId);

            Assert.AreEqual(11, result.Count);

			result = reservation.GetAvailableSites(Convert.ToDateTime("2017-06-13"), Convert.ToDateTime("2017-06-17"), campgroundId);

            Assert.AreEqual(7, result.Count);

            result = reservation.GetAvailableSites(Convert.ToDateTime("2017-06-01"), Convert.ToDateTime("2017-07-04"), campgroundId);

            Assert.AreEqual(5, result.Count);

        }

        [TestMethod]
        public void SetUpReservationTest()
        {
            ReservationSqlDAL slot = new ReservationSqlDAL(connectionString);

            Assert.IsNotNull(slot.SetUpReservation(1, "Smith family", Convert.ToDateTime("2017-06-01"), Convert.ToDateTime("2017-06-07")));
        }
    }
}
