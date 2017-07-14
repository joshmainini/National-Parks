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

		public int SetUpReservation(int siteId, string name, DateTime toDate, DateTime fromDate)
		{
			int id = 0;
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand(@"BEGIN TRANSACTION " +
                     "INSERT INTO reservation(site_id, name, from_date, to_date) " +
                     "VALUES (@siteId, @name, @fromDate, @toDate); " +
                     "SELECT * FROM reservation" +
					 " WHERE site_id = @siteId " +
                     "COMMIT TRANSACTION;", conn);

					cmd.Parameters.AddWithValue("@name", name);
					cmd.Parameters.AddWithValue("@siteId", siteId);
					cmd.Parameters.AddWithValue("@toDate", toDate);
					cmd.Parameters.AddWithValue("@fromDate", fromDate);
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

         
