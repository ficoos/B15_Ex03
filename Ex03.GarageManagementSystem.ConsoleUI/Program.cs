using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageManagementSystem.ConsoleUI
{
	using Ex03.GarageLogic;

	public class Program
	{
		private static void assert(bool i_Condition)
		{
			if (i_Condition)
			{
				Console.WriteLine(":)");
			}
			else
			{
				Console.WriteLine(":(");
			}
		}

		public static void Main()
		{
			VehicleDataStore dataStore = new VehicleDataStore();
			OwnerInfo owner = new OwnerInfo("God", "666");

			foreach (string vehicleType in dataStore.AvailableVehicleTypes)
			{
				VehicleInfo vehicle = dataStore.CreateVehicle(vehicleType, vehicleType, owner);
				assert(vehicle == dataStore.FindVehicle(vehicle.Id));
				Console.WriteLine(vehicle.Id);
				foreach (IExtraProperty property in vehicle.ExtraProperties)
				{
					Console.WriteLine("{0} ({1}): ", property.Name, property.InputHint);
				}
				assert(dataStore.FindVehicle("sad") == null);
			}

			dataStore.FindVehicle("Simple Bike").FixStatus = eFixStatus.Fixed;
			foreach (VehicleInfo vehicle in dataStore.GetVehiclesFilteredByFixStatus(eFixStatus.Fixed))
			{
				assert(vehicle == dataStore.FindVehicle("Simple Bike"));
			}

			Console.ReadLine();
		}
	}
}
