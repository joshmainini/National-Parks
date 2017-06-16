﻿using System;
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
		const string command_Arcadia = "1";
		const string command_Arches = "2";
		const string command_Cuyahoga = "3"; 
		const string command_Quit = "q";
		private string connectionString = "";


		public CapstoneCLI(string connectionString)
		{
			this.connectionString = connectionString;
			
		}
		public void RunCLI()
		{
			ParkSqlDAL park = new ParkSqlDAL(connectionString);
			List<Park> allParks = park.GetAllParks();
			
			ViewParks();

			while (true)
			{
				string command = Console.ReadLine();
				
				switch (command.ToLower())
				{
					case command_Arcadia:
						GetParkInfo(allParks[0]);
						break;
					case command_Arches:
						GetParkInfo(allParks[1]);
						break;
					case command_Cuyahoga:
						GetParkInfo(allParks[2]);
						break;

					case command_Quit:
						return;
					default:
						Console.WriteLine("The command provided was not a valid command, please try again.");
						break;


				}
			}
		}

		public void GetParkInfo(Park park)
		{
			const string command_ViewCampgrounds = "1";
			const string command_SearchReservation = "2";
			const string command_ReturnToPrevious = "3";
			CampGroundSqlDAL campground = new CampGroundSqlDAL(connectionString);
			List<CampGround> allCampgrounds = campground.GetCampGrounds(park.ParkId);

			Console.WriteLine("{0, -10}", park.Name);
			Console.WriteLine("Location: {0, -10}", park.Location);
			Console.WriteLine("Established: " + park.EstablishDate.ToString("MM/dd/yyyy"));
			Console.WriteLine("Area: {0:n0} sq km", park.Area);
			Console.WriteLine("Annual Visitors: {0:n0}", park.Visitors);
			Console.WriteLine();
			Console.WriteLine(park.Description);
			Console.WriteLine();
			Console.WriteLine("Select a Command");
			Console.WriteLine("1) View Campgrounds");
			Console.WriteLine("2) Search for Reservation");
			Console.WriteLine("3) Return to Previous Screen");
			Console.WriteLine();

				Console.WriteLine();
				string command = Console.ReadLine();
				Console.WriteLine();

				switch (command.ToLower())
				{
					case command_ViewCampgrounds:
						ViewCampGrounds(allCampgrounds);
						break;
					case command_SearchReservation:
						;
						break;
					case command_ReturnToPrevious:
						;
						break;
					default:
						Console.WriteLine("The command provided was not a valid command, please try again.");
						break;

			}
		}

		public void ViewCampGrounds(List<CampGround> campgrounds)
		{
			const string command_SearchReservation = "1";
			const string command_ReturnToPrevious = "2";

			Console.WriteLine("{0,-11}{1,-25}{2,-10}{3,-10}{4, -10}", " ", "Name", "Open", "Close", "Daily Fee");
			foreach (CampGround campground in campgrounds)
			{

				Console.WriteLine("#{0,-10}{1,-25}{2,-10}{3,-10}${4,-10:0.00}", campground.CampgroundId, campground.Name, campground.OpenFrom, campground.OpenTo, campground.DailyFee);
				Console.WriteLine();
			}
			Console.WriteLine("Select a Command");
				Console.WriteLine("1) Search for Available Reservations");
				Console.WriteLine("2) Return to Previous Screen");
			
				Console.WriteLine();
				string command = Console.ReadLine();
				Console.WriteLine();

				switch (command.ToLower())
				{
					case command_SearchReservation:
						SearchReservation();
						break;
					case command_ReturnToPrevious:
						;
						break;
					default:
						Console.WriteLine("The command provided was not a valid command, please try again.");
						break;

				}

		}
		public void SearchReservation()
		{
			Console.WriteLine("Which campground (enter 0 to cancel)?__");
			int campgroundId = int.Parse(Console.ReadLine());
			Console.WriteLine("What is the arrival date? __/__/____");
			DateTime fromDate = Convert.ToDateTime(Console.ReadLine());
			Console.WriteLine("What is the departure date? __/__/____");
			string stringDate = Console.ReadLine();
			DateTime toDate = Convert.ToDateTime(stringDate);

			ReservationSqlDAL newRes = new ReservationSqlDAL(connectionString);
			List<Site> sites = newRes.GetAvailableSites(toDate, fromDate, campgroundId);

			foreach (Site site in sites)
			{
				Console.WriteLine("{0}{1}{2}{3}{4}{5}", site.SiteNumber, site.MaxOccupancy, site.Accessible, site.MaxRvLength, site.Utilities, site.TotalCost);
			}

			Console.WriteLine();
			Console.WriteLine("Which site should be reserved (enter 0 to cancel)?__");
			int siteId = int.Parse(Console.ReadLine());
			Console.WriteLine("What name should the reservation be made under? __");
			string name = Console.ReadLine();
			Console.WriteLine();

			int reservationId = newRes.SetUpReservation(siteId, name, toDate, fromDate);

			Console.WriteLine($"The reservation has been made and the confirmation id is {reservationId}");
			Console.WriteLine();



		}

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
