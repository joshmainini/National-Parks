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

		    ViewParks();
            
		}

		public void GetParkInfo(Park park)
		{
            const string command_ViewCampgrounds = "1";
			const string command_SearchReservation = "2";
            const string command_ReturnToPrevious = "3";
			
            CampGroundSqlDAL campground = new CampGroundSqlDAL(connectionString);
            List<CampGround> allCampgrounds = campground.GetCampGrounds(park.ParkId);

                Console.Clear();
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
			string command = Console.ReadLine();

			switch (command.ToLower())
			{
				case command_ViewCampgrounds:
					ViewCampGrounds(park.ParkId);
					break;
				case command_SearchReservation:
					SearchReservation(park.ParkId, command);
					break;
				case command_ReturnToPrevious:
					ViewParks();
					break;
				default:
					Console.WriteLine("The command provided was not a valid command, please try again.");
					break;

		}
		}

		public void ViewCampGrounds(int parkId)
		{
            const string command_SearchReservation = "1";
            const string command_ReturnToPrevious = "2";

            CampGroundSqlDAL campground = new CampGroundSqlDAL(connectionString);
            List<CampGround> allCampgrounds = campground.GetCampGrounds(parkId);

            Console.Clear();
			Console.WriteLine("{0,-11}{1,-25}{2,-10}{3,-10}{4, -10}", " ", "Name", "Open", "Close", "Daily Fee");
			foreach (CampGround item in allCampgrounds)
			{

				Console.WriteLine("#{0,-10}{1,-25}{2,-10}{3,-10}${4,-10:0.00}", item.CampgroundId, item.Name, item.OpenFrom, item.OpenTo, item.DailyFee);
				Console.WriteLine();
			}
			    Console.WriteLine("Select a Command");
				Console.WriteLine("1) Search for Available Reservations");
				Console.WriteLine("2) Return to Parks");
                Console.WriteLine();
				string command = Console.ReadLine();
				Console.WriteLine();

				switch (command.ToLower())
				{
					case command_SearchReservation:
						SearchReservation(parkId, command);
                        
						break;
					case command_ReturnToPrevious:
					    ViewParks();
						break;
					default:
						Console.WriteLine("The command provided was not a valid command, please try again.");
                        
						break;

				}

		}
        public void SearchReservation(int parkId, string commandOption)
        {
            if (commandOption == "1")
            {
                Console.WriteLine("Which campground (enter 0 to cancel)?__");
                Console.WriteLine();
            }
            else if(commandOption == "2")
            {
                Console.WriteLine("Would you like to make a reservation in this park? (any number for yes and [0] for no)?__");
                Console.WriteLine();
            }
            int inputId = int.Parse(Console.ReadLine());
            if (inputId == 0)
            {
                Console.Clear();
                ViewParks();
                Console.WriteLine();
            }
            else if( inputId != 0)
            {

                Console.WriteLine("What is the arrival date? __/__/____");
                Console.WriteLine();
                DateTime fromDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("What is the departure date? __/__/____");
                Console.WriteLine();
                string stringDate = Console.ReadLine();
                DateTime toDate = Convert.ToDateTime(stringDate);

                SiteSqlDAL newAvailSites = new SiteSqlDAL(connectionString);
                List<Site> sites = new List<Site>();

                if (commandOption == "2")
                {
                    sites = newAvailSites.GetAvailableSitesFromPark(toDate, fromDate, parkId);

                }
                else if (commandOption == "1")
                {
                    sites = newAvailSites.GetAvailableSites(toDate, fromDate, inputId);
                }

                if (sites.Count == 0)
                {
                    Console.Clear();
                    Console.WriteLine("there are no sites available. please enter an alternative date range");
                    SearchReservation(parkId, commandOption);
                }
                else
                {

                    Console.WriteLine("{0, -20}{1, -20}{2, -20}{3, -20}{4, -20}{5, -20}", "Site Id", "Max Occup.", "Accessible", "Max RV Length", "Utility", "Cost");
                    foreach (Site site in sites)
                    {
                        Console.WriteLine("{0, -20}{1,-20}{2,-20}{3,-20}{4,-20}${5,-20:0.00}", site.SiteId, site.MaxOccupancy, site.Accessible, site.MaxRvLength, site.Utilities, site.TotalCost);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Which site should be reserved (enter 0 to cancel)?__");
                    Console.WriteLine();
                    int siteId = int.Parse(Console.ReadLine());
                    if (siteId == 0)
                    {
                        Console.Clear();
                        ViewParks();
                    }
                    Console.WriteLine();
                    Console.WriteLine("What name should the reservation be made under? __");
                    string name = Console.ReadLine();
                    Console.WriteLine();

                    ReservationSqlDAL newRes = new ReservationSqlDAL(connectionString);
                    int reservationId = newRes.SetUpReservation(siteId, name, toDate, fromDate);

                    Console.WriteLine($"The reservation has been made and the confirmation id is {reservationId}");
                    Console.WriteLine();

                }
            }
            else
            {
                ViewCampGrounds(parkId);
            }
        }
		public bool ViewParks()
		{
            ParkSqlDAL park = new ParkSqlDAL(connectionString);
            List<Park> allParks = park.GetAllParks();

            Console.Clear();
            Console.WriteLine("Select a Park for Further Details");
			Console.WriteLine("1) Acadia");
			Console.WriteLine("2) Arches");
			Console.WriteLine("3) Cuyahoga National Valley Park");
			Console.WriteLine();
			Console.WriteLine("Q) quit");
			Console.WriteLine();

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
                    return false;
                default:
                    Console.WriteLine("The command provided was not a valid command, please try again.");
                    break;

                
            }
            return true;

        }
	}
}
