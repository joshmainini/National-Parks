using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone
{
	public class CapstoneCLI
	{


		public void ViewParks()
		{
			Console.WriteLine("Select a Park for Further Details");
			Console.WriteLine("1) Acadia");
			Console.WriteLine("2) Arches");
			Console.WriteLine("3) Cuyahoga National Valley Park");
			Console.WriteLine();
			Console.WriteLine("Q) quit");
			Console.WriteLine();

		}
	}
}
