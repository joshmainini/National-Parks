using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
	public class ParkSqlDAL
	{
		private string connectionString;

		public ParkSqlDAL(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public List<Park> GetAllParks()
		{
			List<Park> parks = new List<Park>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand("SELECT * FROM park", conn);

					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						Park p = new Park();
						p.ParkId = Convert.ToInt32(reader["park_id"]);
						p.Name = Convert.ToString(reader["name"]);
						p.Location = Convert.ToString(reader["location"]);
						p.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
						p.Area = Convert.ToInt32(reader["area"]);
						p.Visitors = Convert.ToInt32(reader["visitors"]);
						p.Description = Convert.ToString(reader["description"]);

						parks.Add(p);
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine("Unable to retrieve park data, please try again at a later time.");
			}
				return parks;

		}

	}
}
