using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyApplication
{
	// This class manages a collection of Vehicle objects. It has methods like add, remove, update and query vehicles in various ways using LINQ for quering
	public class VehicleFleet
	{
		private List<Vehicle> _vehicles = new List<Vehicle>();
		private string jsonFilePath = @"C:\Users\gunce\Desktop\vehicles.json"; // to test it don't forget to change the path

		public void AddVehicle(Vehicle vehicle)
		{
			if(!_vehicles.Any(v=>v.Id == vehicle.Id))
			{
				_vehicles.Add(vehicle);
				
			}
			else
			{
				Console.WriteLine("Vehicle with the same Id already exists");
			}
			
		}

		public IEnumerable<Vehicle> GetVehiclesByBrand(string brand)
		{
			return _vehicles.Where(v => v.Brand == brand);
		}
		public IEnumerable<Vehicle> GetAllVehicles()
		{
			return _vehicles;
		}

		public IEnumerable<Vehicle> GetVehiclesExceededOperationalTenure(string model)
		{
			return _vehicles.Where(v => v.Model == model && v.ExceedOperationalTenure());
		}

		public double CalculateTotalFleetValue()
		{
			return _vehicles.Sum(v => v.CalculateTotalValue());
		}

		public IEnumerable<Vehicle> GetVehiclesByBrandAndColor(string brand, string color)
		{
			return _vehicles.Where(v => v.Brand == brand && v.Color == color).OrderBy(v=> v.ComfortClass);
		}

		public IEnumerable<Vehicle> GetRequireMaintenanceList()
		{
			return _vehicles.Where(v => v.RequireMaintenance());
		}

		public bool RemoveVehicleById(string id)
		{
			var vehicle = _vehicles.FirstOrDefault(vehicle => vehicle.Id == id);
			if(vehicle != null)
			{
				_vehicles.Remove(vehicle);
				
				return true;
			}
			
			return false;
			
		}

		public void UpdateVehicle(string id , IVehicleVisitor visitor)
		{
			var vehicle = _vehicles.FirstOrDefault(v=> v.Id == id);
			if(vehicle != null)
			{
				vehicle?.Accept(visitor);
				vehicle.DisplayDetails();
				
			}
			
        }

		public double CalculateModelCoefficient(Vehicle vehicle)
		{


			int modelCount = _vehicles.Count(v=> v.Model == vehicle.Model);
			int currentYear = DateTime.Now.Year;
			if(vehicle is PassengerVehicle passengerVehicle)
			{
				double averageRank = _vehicles.OfType<PassengerVehicle>().Average(v => v.Rating);
				// Base coefficient is 1, increase by 0.03 for each same model, decrease by 0.02 for each year of age
				int vehicleAge = currentYear - vehicle.YearOfManufacture.Year;
				return 1 + (modelCount * 0.03) +(averageRank/ 5 * 0.01) - (vehicleAge * 0.02);

			}
			else if (vehicle is CargoVehicle cargoVehicle)
			{
				int vehicleAge = currentYear - vehicle.YearOfManufacture.Year;
				double averageWeight = _vehicles.OfType<CargoVehicle>().Average(v => v.Weight);
				//Base coefficient is 1, increase by 0.04 for each same model, increase by weight factor, decrease by age
				return 1 + (modelCount * 0.04) + (averageWeight / 1000 * 0.01) - (vehicleAge * 0.02);
			}

			return 1;
		}

		

	}
}
