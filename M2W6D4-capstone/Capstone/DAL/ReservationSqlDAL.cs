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

					SqlCommand cmd = new SqlCommand(@"SELECT site.site_id, site.site_number, site.max_occupancy, site.accessible, site.max_rv_length, site.utilities, DATEDIFF(d,reservation.from_date, reservation.to_date) * campground.daily_fee AS total_cost
					FROM site JOIN reservation on reservation.site_id = site.site_id 
					JOIN campground ON campground.campground_id = site.campground_id
					WHERE site.campground_id = @campgroundId 
					AND (MONTH(@fromDate) > campground.open_from_mm AND MONTH(@toDate) < campground.open_to_mm) ", conn);

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
						s.Accessible = Convert.ToInt32(reader["accessible"]);
						s.MaxRvLength = Convert.ToInt32(reader["max_rv_length"]);
						s.Utilities = Convert.ToInt32(reader["utilities"]);
						s.TotalCost = Convert.ToDecimal(reader["total_cost"]);

						output.Add(s);
					}
					SqlCommand cmd1 = new SqlCommand("SELECT * FROM reservation JOIN site ON site.site_id = reservation.site_id WHERE" +
						" site.campground_id = @campgroundId AND @fromDate >= from_date AND @toDate <= to_date", conn);

					cmd1.Parameters.AddWithValue("@campgroundId", campgroundId);
					cmd1.Parameters.AddWithValue("@fromDate", fromDate);
					cmd1.Parameters.AddWithValue("@toDate", toDate);
					List<int> reservedSites = new List<int>();

					while (reader.Read())
					{
						reservedSites.Add(Convert.ToInt32(reader["site_id"]));
					}

					foreach (int siteId in reservedSites)
					{
						for (int i = 0; i < output.Count; i++)
						{
							if (output[i].SiteId == siteId)
							{
								output.Remove(output[i]);
							}
						}
					}

				}

			}
			catch (SqlException ex)
			{
				Console.WriteLine("Something went wrong with the site data, please try again later");
			}
			return output;

		}

		public int SetUpReservation(int site_id, string name, DateTime toDate, DateTime fromDate)
		{
			int id = 0;
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION INSERT INTO reservation(site_id, name, from_date, to_date) VALUES" +
					"(@site_id, @name, @fromDate, @toDate) SELECT * FROM reservation" +
					"WHERE site_id = @site_id COMMIT TRANSACTION;", conn);

					cmd.Parameters.AddWithValue("@name", name);
					cmd.Parameters.AddWithValue("@site_id", name);
					cmd.Parameters.AddWithValue("@toDate", name);
					cmd.Parameters.AddWithValue("@fromDate", name);
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						id = Convert.ToInt32(reader["reservation_id"]);
					}
				}
			}
			catch (SqlException e)
			{
				throw;
			}
			return id;
		}
	}
}

         
