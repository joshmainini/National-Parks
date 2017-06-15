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

		public List<Site> GetAvailableSites(List<int> site_ids, DateTime toDate, DateTime fromDate, int campgroundId)
		{
			List<Site> output = new List<Site>();
            // will return a list of availble reservations based on campgroundId, toDate, and fromDate.
            // We will have a SQL command which will join site with reservations on site_id and check the to and from date and
            //add those items to our list.

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM site WHERE campground_id = @campgroundId", conn);
                    cmd.Parameters.AddWithValue("@campgroundId", campgroundId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Site s = new Site();
                        s.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                        s.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        s.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.Accessible = Convert.ToInt32(reader["accessible"]);
                        s.MaxRvLength = Convert.ToInt32(reader["max_rv_length"]);
                        s.Utilities = Convert.ToInt32(reader["utilities"]);
                        s.TotalCost = Convert.ToDecimal(reader["total_cost"]);

                        output.Add(s);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with the site data, please try again later");
            }
            return sites;
			
		}

        public int SetUpReservation(int site_id, string name, DateTime toDate, DateTime fromDate)
        {
            int reservation_id = 0;

            //using site_id and the name defined by the user, will insert reservation in the database and return back a reservation_id if no records come back and the campground is not closed

            return reservation_id;
        }
	}
}
