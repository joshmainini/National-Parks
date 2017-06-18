using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
	public class SiteSqlDAL
	{
		private string connectionString;

		public SiteSqlDAL(string connectionString)
		{
			this.connectionString = connectionString;
		}

        public List<Site> GetAvailableSites(DateTime toDate, DateTime fromDate, int campgroundId)
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

                    SqlCommand cmd = new SqlCommand(
                        @"SELECT campground.campground_id, site.site_id, site.site_number, site.max_occupancy, site.accessible, site.max_rv_length, site.utilities, DATEDIFF(d,@fromDate, @toDate) * campground.daily_fee AS total_cost
                        FROM site  
                        JOIN campground ON campground.campground_id = site.campground_id
                        WHERE site.campground_id = @campgroundId 
                        AND (MONTH(@fromDate) > campground.open_from_mm 
                        AND MONTH(@toDate) < campground.open_to_mm) 
                        AND site.site_id NOT IN ( 
                        SELECT DISTINCT site.site_id FROM reservation 
                        JOIN site ON site.site_id = reservation.site_id 
                        WHERE site.campground_id = @campgroundId
                        AND (
                        (from_date >= @fromDate AND from_date <= @fromDate)
                        OR (to_date >= @toDate AND to_date <= @toDate)
                        OR (from_date <= @fromDate AND to_date >= @toDate)
                        OR (from_date <= @toDate AND to_date >= @fromDate))
                        )", conn);


                    cmd.Parameters.AddWithValue("@campgroundId", campgroundId);
                    cmd.Parameters.AddWithValue("@fromDate", fromDate);
                    cmd.Parameters.AddWithValue("@toDate", toDate);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site s = new Site();
                        s.CampgroundId = campgroundId;
                        s.SiteId = Convert.ToInt32(reader["site_id"]);
                        s.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        s.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.Accessible = Convert.ToString(reader["accessible"]);
                        s.MaxRvLength = Convert.ToString(reader["max_rv_length"]);
                        s.Utilities = Convert.ToString(reader["utilities"]);
                        s.TotalCost = Convert.ToDecimal(reader["total_cost"]);

                        output.Add(s);
                    }

                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with the site data, please try again later");
            }
            return output;

        }
    }
}

