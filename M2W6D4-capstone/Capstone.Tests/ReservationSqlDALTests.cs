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
            List<int> siteIds = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<Site> result = new List<Site>();
            ReservationSqlDAL reservation = new ReservationSqlDAL(connectionString);


            result = reservation.GetAvailableSites(siteIds, Convert.ToDateTime("2017-06-24"), Convert.ToDateTime("2017-06-25"));

            Assert.AreEqual(9, result.Count);

            result = reservation.GetAvailableSites(siteIds, Convert.ToDateTime("2017-06-13"), Convert.ToDateTime("2017-06-17"));

            Assert.AreEqual(5, result.Count);
        }

        [TestMethod]
        public void SetUpReservationTest()
        {
            ReservationSqlDAL slot = new ReservationSqlDAL(connectionString);

            Assert.AreEqual(45, slot.SetUpReservation(1, "Smith family", Convert.ToDateTime("2017-06-01"), Convert.ToDateTime("2017-06-07")));
        }
    }
}
