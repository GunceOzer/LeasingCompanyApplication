using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyApplication
{
	//Responsible for loading vehicles from JSON file.
	public class VehicleLoader
	{
	
		public static List<Vehicle> LoadVehiclesFromFile(string jsonFilePath)
		{
			try
			{
				using StreamReader streamReader = new StreamReader(jsonFilePath);
				var json = streamReader.ReadToEnd();
				var settings = new JsonSerializerSettings
				{
					Converters = new List<JsonConverter> { new VehicleJsonConverter() },
					Formatting = Formatting.Indented
				};
				return JsonConvert.DeserializeObject<List<Vehicle>>(json, settings);
			}
			catch (FileNotFoundException ex)
			{
				Console.WriteLine($"File not found: {ex.Message}");
			}
			catch (JsonException ex)
			{
				Console.WriteLine($"JSON parsing error: {ex.Message}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
			return new List<Vehicle>(); // Returns an empty list in case of error

		}

	}
}
