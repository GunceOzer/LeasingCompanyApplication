
using LeasingCompanyApplication;

string jsonFilePath = @"C:\Users\gunce\Desktop\vehicleFleet.json"; // to test it don't forget to change the path

VehicleFleet fleet = new VehicleFleet();

var display = new Display();
try
{
	var vehicles = VehicleLoader.LoadVehiclesFromFile(jsonFilePath);
	foreach (var vehicle in vehicles)
	{
		fleet.AddVehicle(vehicle);
	}
}
catch(Exception ex)
{
	Console.WriteLine($"Error loading vehicles from file: {ex.Message}");
}


bool exit = false;

while (!exit)
{


	//Thread.Sleep(1000);

	Console.WriteLine();
	display.ShowMenu();

	try
	{
		
		int choice = InputHelper.ReadInt("Enter your choice (1-11): ");

		switch (choice)
		{
			case 1:
				display.DisplayAddVehicleProcess(fleet);
				break;
			case 2:
				display.DisplayRemoveVehicleProcess(fleet);
				break;
			case 3:
				display.DisplayUpdateVehicleProcess(fleet);
				break;
			case 4:
				display.DisplayGetAllVehicles(fleet);
				break;
			case 5:
				display.DisplayGetVehiclesByBrandProcess(fleet);
				break;
			case 6:
				display.DisplayGetVehiclesByBrandAndColorProcess(fleet);
				break;
			case 7:
				display.DisplayTotalFleetValue(fleet);
				break;
			case 8:
				display.DisplayRequireMaintenance(fleet);
				break;
			case 9:
				display.DisplayRentVehicleProcess(fleet);
				break;
			case 10:
				display.DisplayVehiclesExceededOperationalTenureProcess(fleet);
				break;
			case 11:
				exit = true;
				break;
			default:
				Console.WriteLine("Invalid choice. Please choose again.");
				break;

		}
	}
	catch (Exception ex)
	{
		Console.WriteLine($"An error occurred: {ex.Message}");
		
	}
}

	Console.WriteLine("Press any key to continue...");
	Console.ReadKey();
	Console.Clear();





