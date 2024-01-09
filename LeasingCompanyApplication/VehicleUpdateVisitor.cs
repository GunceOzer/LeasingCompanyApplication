using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyApplication
{
	public class VehicleUpdateVisitor : IVehicleVisitor
	{
		private string _color;
		private double _price;
		private DateTime _service;
		private double? _rating;
		private double? _weight;

		public VehicleUpdateVisitor(string color, double price, int service, double? rating, double? weight)
		{
			_color = color;
			_price = price;
			_service = new DateTime(service,1,1);
			_rating = rating;
			_weight = weight;
		}

		public void Visit(PassengerVehicle passengerVehicle)
		{
			passengerVehicle.Color = _color;
			passengerVehicle.Price = _price;
			passengerVehicle.ServiceDetail = _service;
			if (_rating.HasValue)
			{
				passengerVehicle.Rating = _rating.Value;
			}

		}
		public void Visit(CargoVehicle cargoVehicle)
		{
			cargoVehicle.Color = _color;
			cargoVehicle.Price = _price;
			cargoVehicle.ServiceDetail= _service;
			if (_weight.HasValue)
			{
				cargoVehicle.Weight = _weight.Value;
			}
		}
	}
}
