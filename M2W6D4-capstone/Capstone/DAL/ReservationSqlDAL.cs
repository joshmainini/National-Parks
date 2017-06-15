using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
	public class ReservationSqlDAL
	{
		private string connectionString;

		public ReservationSqlDAL(string connectionString)
		{
			this.connectionString = connectionString;
		}
		public List<Site> GetAvailableReservations(int campgroundId, DateTime toDate, DateTime fromDate)
		{
			List<Site> sites = new List<Site>();
			// will return a list of availble reservations based on campgroundId, toDate, and fromDate.
			// We will have a SQL command which will join site with reservations on site_id and check the to and from date and
			//add those items to our list.
			return sites;
			
		}
	}
}
