using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyApplication
{// This class is responsible for user interactions. Displays information to the console.
 // It follows the Single Responsilibility Principle as it only handles display information and user interaction
	public class Display
	{
		CultureInfo polishCulture = new CultureInfo("pl-PL");

		public void DisplayGetAllVehicles(VehicleFleet fleet)
		{
			DisplayVehiclesByType(fleet.GetAllVehicles());
		}

		public void DisplayTotalFleetValue(VehicleFleet fleet)
		{
			Console.WriteLine("7: Total Fleet Value");
			double totalFleetValue = fleet.CalculateTotalFleetValue();
			string formattedCost = totalFleetValue.ToString("C",polishCulture);
			Console.WriteLine($"Total fleet value is {formattedCost}");
		}


		public void DisplayRequireMaintenance(VehicleFleet fleet)
		{
			var vehicles = fleet.GetRequireMaintenanceList();
			DisplayVehiclesByType(vehicles);
	
		}

		

		public void DisplayRentVehicleProcess(VehicleFleet fleet)
		{
			
			Console.WriteLine("9: Rental costs");
			DisplayGetAllVehicles(fleet);

			Console.WriteLine();
			string rentId = InputHelper.ReadString("Enter the ID of the vehicle that you want to learn the rental cost: ");
			var vehicleToShowRent = fleet.GetAllVehicles().FirstOrDefault(v => v.Id == rentId);

			if (vehicleToShowRent == null)
			{
				Console.WriteLine("Vehicle not found.");
				return;
			}
			
			int duration = InputHelper.ReadInt("Enter the duration of the trip in days: ");
			double travelDistance = InputHelper.ReadDouble("Enter the travel distance: ");

			double rentalCost = vehicleToShowRent.RentCostOfVehicle(duration, travelDistance, fleet);
			string formattedRentalCost = rentalCost.ToString("C",polishCulture);
			Console.WriteLine($"The cost of renting this vehicle is: {formattedRentalCost}");
		}

		

		public void DisplayAddVehicleProcess(VehicleFleet fleet)
		{
			try
			{
				int vehicleType = InputHelper.ReadPassengerOrVehicle("What type of vehicle do you want to add? (1 for Passenger, 2 for Cargo)");

				string id;
				while (true)
				{
					id = InputHelper.ReadString("Enter Vehicle ID:");
					var existingVehicle = fleet.GetAllVehicles().FirstOrDefault(v => v.Id == id);

					if (existingVehicle != null)
					{
						Console.WriteLine("A vehicle with this ID already exists. Please enter a different ID.");
						continue;
					}
					break;
				}

				string brand = InputHelper.ReadString("Enter Brand:");
				string model = InputHelper.ReadString("Enter Model:");
				int yearOfManufacture = InputHelper.ReadInt("Enter Year Of Manufacture:");
				string color = InputHelper.ReadString("Enter Color:");
				double price = InputHelper.ReadDouble("Enter Price:");
				string registrationNumber = InputHelper.ReadString("Enter Registration Number:");
				double milage = InputHelper.ReadDouble("Enter Milage:");
				int serviceDetail = InputHelper.ReadInt("Enter Last Service year:");
				ComfortClass comfortClass = InputHelper.ReadComfortClass("Enter Comfort Class ((1-5) 1 for A, 2 for B, 3 for C ...):");

				if (vehicleType == 1)
				{
					double rating = InputHelper.ReadRating("Enter Rating (1-5, decimals allowed):");
					var passengerVehicle = new PassengerVehicle(id, brand, model, yearOfManufacture, color, price, registrationNumber, milage, serviceDetail, comfortClass, rating);
					fleet.AddVehicle(passengerVehicle);
				}
				else if (vehicleType == 2)
				{
					double weight = InputHelper.ReadDouble("Enter Weight:");
					var cargoVehicle = new CargoVehicle(id, brand, model, yearOfManufacture, color, price, registrationNumber, milage, serviceDetail, comfortClass, weight);
					fleet.AddVehicle(cargoVehicle);
				}
				else
				{
					Console.WriteLine("Invalid vehicle type selected.");
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine($"An error occurred while adding a vehicle: {ex.Message}");
			}
			
		}


		
		public void DisplayUpdateVehicleProcess(VehicleFleet fleet)
		{
			try
			{
				Console.WriteLine("3: Update Vehicle");
				DisplayGetAllVehicles(fleet);

				Console.WriteLine();
				int updateVehicleType = InputHelper.ReadPassengerOrVehicle("What type of vehicle do you want to update? (1 for Passenger, 2 for Cargo)");

				string id = "";
				bool found = false;

				while (!found)
				{
					id = InputHelper.ReadString("Enter the ID of the vehicle you want to update:");
					var vehicleToUpdate = fleet.GetAllVehicles().FirstOrDefault(v => v.Id == id);

					if (vehicleToUpdate != null)
					{
						if ((updateVehicleType == 1 && vehicleToUpdate is PassengerVehicle) ||
							(updateVehicleType == 2 && vehicleToUpdate is CargoVehicle))
						{
							found = true;
						}
						else
						{
							Console.WriteLine("Vehicle type does not match the selected ID. Please try again.");
						}
					}
					else
					{
						Console.WriteLine("No vehicle found with the specified ID. Please try again.");
					}
				}

				string newColor = InputHelper.ReadString("Enter new color");
				double newPrice = InputHelper.ReadDouble("Enter new price");
				int newService = InputHelper.ReadInt("Enter new service detail as year");

				IVehicleVisitor updateVisitor;

				if (updateVehicleType == 1) // Passenger vehicle
				{
					double newRating = InputHelper.ReadDouble("Enter new Rating:");
					updateVisitor = new VehicleUpdateVisitor(newColor, newPrice, newService, newRating, null);
				}
				else if (updateVehicleType == 2) // Cargo vehicle
				{
					double newWeight = InputHelper.ReadDouble("Enter new Weight:");
					updateVisitor = new VehicleUpdateVisitor(newColor, newPrice, newService, null, newWeight);
				}
				else
				{
					Console.WriteLine("Invalid vehicle type selected.");
					return; // Exit the method if invalid vehicle type is selected
				}

				fleet.UpdateVehicle(id, updateVisitor);
			}
			catch(Exception ex)
			{
				Console.WriteLine($"An error occurred while updating a vehicle: {ex.Message}");
			}


		}

		public void DisplayGetVehiclesByBrandProcess(VehicleFleet fleet)
		{
			Console.WriteLine("5: Get Vehicle by Brand");
			string brand = InputHelper.ReadString("Enter Brand: ");
			var vehiclesByBrand = fleet.GetVehiclesByBrand(brand);
			DisplayVehiclesByType(vehiclesByBrand);
		}
		public void DisplayRemoveVehicleProcess(VehicleFleet fleet)
		{
			try
			{
				Console.WriteLine("2: Remove Vehicle");
				bool isVehicleRemoved = false;

				while (!isVehicleRemoved)
				{
					string idToRemove = InputHelper.ReadString("Enter Vehicle ID to remove: ");
					isVehicleRemoved = fleet.RemoveVehicleById(idToRemove);

					if (isVehicleRemoved)
					{
						Console.WriteLine($"Vehicle with ID {idToRemove} has been successfully removed.");
					}
					else
					{
						Console.WriteLine("No vehicle found with the specified ID. Please try again.");
					}
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine($"An error occured while removing a vehicle{ex.Message}");
			}
			
		}
		public void DisplayGetVehiclesByBrandAndColorProcess(VehicleFleet fleet)
		{
			Console.WriteLine("6: Get Vehicles by Brand and Color");
			string brand = InputHelper.ReadString("Enter Brand: ");

			string color = InputHelper.ReadString("Enter Color: ");
			var vehiclesByBrandAndColor = fleet.GetVehiclesByBrandAndColor(brand, color);
			DisplayVehiclesByType(vehiclesByBrandAndColor);
		
		}

		public void DisplayVehiclesExceededOperationalTenureProcess(VehicleFleet fleet)
		{
			Console.WriteLine("10: Get list of vehicles that exceeded operational tenure");
			string model = InputHelper.ReadString("Enter model");
			var vehicles = fleet.GetVehiclesExceededOperationalTenure(model);
			DisplayVehiclesByType(vehicles);
		
		}
		private void DisplayVehiclesByType(IEnumerable<Vehicle> vehicles)
		{
			try
			{
				var passengerVehicles = vehicles.OfType<PassengerVehicle>();
				var cargoVehicles = vehicles.OfType<CargoVehicle>();

				Console.WriteLine("Passenger Vehicles:");
				if (!passengerVehicles.Any())
				{
					Console.WriteLine(" - None");
				}
				else
				{
					foreach (var vehicle in passengerVehicles)
					{
						vehicle.DisplayDetails();
					}
				}

				Console.WriteLine("\nCargo Vehicles:");
				if (!cargoVehicles.Any())
				{
					Console.WriteLine(" - None");
				}
				else
				{
					foreach (var vehicle in cargoVehicles)
					{
						vehicle.DisplayDetails();
					}
				}

			}
			catch(Exception ex)
			{
				Console.WriteLine($"An error occurred while trying to display the vehicle types {ex.Message}");
			}
			
		}

		public void ShowMenu()
		{
			Thread.Sleep(1000);
			Console.WriteLine("Choose an operation:");
			Console.WriteLine("1: Add Vehicle");
			Console.WriteLine("2: Remove Vehicle");
			Console.WriteLine("3: Update Vehicle");
			Console.WriteLine("4: Get All Vehicles");
			Console.WriteLine("5: Get Vehicles by Brand");
			Console.WriteLine("6: Get Vehicles by Brand and Color");
			Console.WriteLine("7: Total Fleet Value");
			Console.WriteLine("8: Vehicles Requiring Maintenance");
			Console.WriteLine("9: Rental cost of vehicle");
			Console.WriteLine("10: Get list of vehicles that exceeded operational tenure");
			Console.WriteLine("11: Exit");
			

		}
	}
}
