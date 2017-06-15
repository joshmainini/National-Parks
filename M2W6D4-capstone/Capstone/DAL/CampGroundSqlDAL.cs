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
			return new List<CampGround>(); 
		}

	}
}
