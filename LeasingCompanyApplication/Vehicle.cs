using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyApplication
{

		// This is an abstract class based for Cargo Vehicle and PassengerVehicle .
		// It contains the common properties and abstract methods that must be implemented by derived class
		// This is an example of Template Method pattern , whre an abstract class defines the structure of how its methods work,
		//and the concrete classes implement these details
		
		public abstract class Vehicle
		{
			public string Id { get; set; }
			public string Brand { get; set; }
			public string Model { get; set; }
			public DateTime YearOfManufacture { get; set; }
			public string Color { get; set; }
			public double Price { get; set; }
			public string RegistrationNumber { get; set; }

			// I defined the Milage and Service properties in Vehicle class because both Passenger and Cargo classes has the same property. Also it means I used DRY(don't repeat yourself) principle
			public double Milage { get; set; }
			public DateTime ServiceDetail { get; set; }

			public ComfortClass ComfortClass { get; set; }


			public Vehicle(string id, string brand, string model, int yearOfManufacture, string color, double price, string registrationNumber, double milage,  int serviceDetail,ComfortClass comfortClass)
			{
				Id = id;
				Brand = brand;
				Model = model;
				YearOfManufacture = new DateTime(yearOfManufacture,1,1);
				Color = color;
				Price = price;
				RegistrationNumber = registrationNumber;
				Milage = milage;
				ServiceDetail = new DateTime(serviceDetail,1,1);
				ComfortClass = comfortClass;
				
			}

			public abstract bool RequireMaintenance();
			public abstract bool ExceedOperationalTenure();
			public abstract double CalculateTotalValue();
			public abstract void DisplayDetails();
			public abstract void Accept(IVehicleVisitor visitor);
			public abstract double RentCostOfVehicle(int duration, double travelDistance,VehicleFleet fleet);
			
		}
	

}
