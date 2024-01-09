using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System;

namespace LeasingCompanyApplication
{
	// It is a custom JSON converter to handle deserialization of Vehicle objects. 
	public class VehicleJsonConverter : Newtonsoft.Json.JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(Vehicle));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
		{
			JObject item = JObject.Load(reader);

			if (item["Rating"] != null) // if the JObject contains a property named Rating , it represents PassengerVehicle 
			{
				var passengerVehicle = item.ToObject<PassengerVehicle>(serializer);
				return passengerVehicle;
			}
			else if (item["Weight"] != null) // if the JObject contains a property named Weigth , it represents CargoVehicle
			{
				var cargoVehicle = item.ToObject<CargoVehicle>(serializer);
				return cargoVehicle;
			}
			else
			{
				throw new JsonSerializationException("Unknown vehicle type");
			}
		}

		public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
