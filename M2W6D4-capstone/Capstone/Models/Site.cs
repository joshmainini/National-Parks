using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
	public class Site
	{
		private string accessible;
		private string maxOccupancy;
		private string maxRvLength;
		private string utilities;

		public int SiteId { get; set; }
		public int CampgroundId { get; set; }
		public int SiteNumber { get; set; }
		public int MaxOccupancy { get; set; }

		public string Accessible
		{
			get
			{
				return accessible;
			}
			set
			{
                
				if (value == "False")
				{
					accessible = "No";
				}
				else
				{
					accessible = "Yes";
				}
			}
		}
								 
		public string MaxRvLength
		{
			get
			{
				return maxRvLength;
			}
			set
			{
				if (value == "0")
				{
					maxRvLength = "N/A";
				}
                else
                {
                    maxRvLength = value;
                }
				
			}
		}
		public string Utilities
		{
			get
			{
				return utilities;
			}
			set
			{
                
                if (value == "False")
				{
					utilities = "N/A";
				}
				else
				{
					utilities = "Yes";
				}
			}
		}
        public decimal TotalCost { get; set; }
	}
}
