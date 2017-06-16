using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
	public class CampGroundSqlDAL
	{
		private string connectionString;

		public CampGroundSqlDAL(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public List<CampGround> GetCampGrounds(int parkId)
		{
            //GetCampGrounds will retrieve all campgrounds within the selected park. 
            List<CampGround> output = new List<CampGround>();

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM campground WHERE park_id = @parkId", conn);
                    cmd.Parameters.AddWithValue("@parkId", parkId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        CampGround c = new CampGround();
                        c.ParkId = parkId;
                        c.Name = Convert.ToString(reader["name"]);
                        c.OpenFrom = Convert.ToInt32(reader["open_from_mm"]);
                        c.OpenTo = Convert.ToInt32(reader["open_to_mm"]);
                        c.DailyFee = Convert.ToDecimal(reader["daily_fee"]);
						c.CampgroundId = Convert.ToInt32(reader["campground_id"]);

                        output.Add(c);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with the campground data, please try again later");
            }

            return output; 
		}

	}
}
