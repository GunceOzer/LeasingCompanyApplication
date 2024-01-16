using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LeasingCompanyApplication
{
	public class PassengerVehicle : Vehicle
	{
		public double Rating { get; set; }
		public PassengerVehicle(string id, string brand, string model, int yearOfManufacture, string color, double price, string registrationNumber, double milage, int serviceDetail, ComfortClass comfortClass, double rating) : base(id, brand, model, yearOfManufacture, color, price, registrationNumber, milage, serviceDetail, comfortClass)
		{
			Rating = rating;
		}

		public override bool ExceedOperationalTenure()
		{
			int currentYear = DateTime.Now.Year;
			return Milage > 100000 || currentYear - ServiceDetail.Year > 5;
		}

		public override bool RequireMaintenance()
		{
			return Milage % 5000 < 1000;
		}

		public override double CalculateTotalValue()
		{
			int age = DateTime.Now.Year - YearOfManufacture.Year;
			double rate = 0.10;
			double calculate = rate * age;
			var currentValue = Price - (Price * calculate);
			return currentValue;
		}
		public override void DisplayDetails()
		{
			CultureInfo polishCulture = new CultureInfo("pl-PL");
			string formattedPrice = Price.ToString("C", polishCulture);
			Console.WriteLine($"Id: {Id},   Brand: {Brand},    Model: {Model},   Year: {YearOfManufacture.Year},   Color: {Color},   Price: {formattedPrice},   Registration Number: {RegistrationNumber},   Milage:{Milage} km,   Last Service:{ServiceDetail.Year},   Comfort Class :{ComfortClass},   Rating:{Rating}");
		}

		public override void Accept(IVehicleVisitor visitor)
		{
			visitor.Visit(this);
		}

		public override double RentCostOfVehicle(int duration, double travelDistance,VehicleFleet fleet)
		{
			double modelCoefficient = fleet.CalculateModelCoefficient(this);
			return duration * travelDistance * modelCoefficient*Rating;

		}
	}
}
