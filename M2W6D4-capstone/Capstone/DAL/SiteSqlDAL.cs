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

		public List<Site> GetSites(int campGroundId)
		{
            //GetCampGrounds will retrieve all campgrounds within the selected park. 
            List<Site> output = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM site WHERE campground_id = @campgroundId", conn);
                    cmd.Parameters.AddWithValue("@campgroundId", campGroundId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Site s = new Site();
                        s.CampgroundId = campGroundId;
                        s.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        s.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.Accessible = Convert.ToInt32(reader["accessible"]);
                        s.MaxRvLength = Convert.ToInt32(reader["max_rv_length"]);
                        s.Utilities = Convert.ToInt32(reader["utilities"]);

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

