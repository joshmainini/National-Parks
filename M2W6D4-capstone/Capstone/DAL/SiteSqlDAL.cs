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

		public List<Site> GetCampSites(int campGroundId)
		{
			//This will return all campsites under this campground ID.
			return new List<Site>();
		}
	}
}
