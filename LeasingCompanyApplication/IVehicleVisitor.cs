using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyApplication
{
	// I used Visitor design parttern to update the vehicle properties. The VehicleUpdateVisitor provides a way to update vehicle 
	public interface IVehicleVisitor
	{
		void Visit(PassengerVehicle passengerVehicle);
		void Visit(CargoVehicle cargoVehicle);
		
	}
}
