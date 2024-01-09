using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyApplication
{// A utility class to handle user input, It encapsulates the logic for reading different types of inputs from the console
	public static class InputHelper
	{
		public static string ReadString(string prompt)
		{
			string input;
			do
			{
				Console.WriteLine(prompt);
				input = Console.ReadLine();
			} while (string.IsNullOrWhiteSpace(input));
			return input;
		}
	

		public static int ReadInt(string prompt)
		{
			while (true)
			{
				Console.WriteLine(prompt);
				if (int.TryParse(Console.ReadLine(), out int result))
					return result;
				Console.WriteLine("Invalid input. Please enter a valid integer.");
			}
		}

		public static double ReadDouble(string prompt)
		{
			while (true)
			{
				Console.WriteLine(prompt);
				if (double.TryParse(Console.ReadLine(), out double result))
					return result;
				Console.WriteLine("Invalid input. Please enter a valid number.");
			}
		}

		public static ComfortClass ReadComfortClass(string prompt)
		{
			while (true)
			{
				Console.WriteLine(prompt);
				if (int.TryParse(Console.ReadLine(), out int comfortClassInt) && Enum.IsDefined(typeof(ComfortClass), comfortClassInt))
					return (ComfortClass)comfortClassInt;
				Console.WriteLine("Invalid comfort class selected. Please enter a number between 1 and 5.");
			}
		}

		public static double ReadRating(string prompt)
		{
			while (true)
			{
				Console.WriteLine(prompt);
				if (double.TryParse(Console.ReadLine(), out double rating) && rating >= 1 && rating <= 5)
					return rating;
				Console.WriteLine("Invalid input. Please enter a rating between 1 and 5 (decimals allowed).");
			}
		}

		public static int ReadPassengerOrVehicle(string prompt)
		{
			while (true)
			{
				Console.WriteLine(prompt);
				if (int.TryParse(Console.ReadLine(), out int choose) && (choose == 1 || choose == 2))
					return choose;
				Console.WriteLine("Invalid input.You can choose only 1 or 2");
			}
		}
	}
}
