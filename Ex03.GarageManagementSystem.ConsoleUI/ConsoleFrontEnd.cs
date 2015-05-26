using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.GarageManagementSystem.ConsoleUI
{
	public class ConsoleFrontEnd
    {
        private readonly VehicleDataStore r_DataStore;

	    private readonly FrontEndAction[] r_AvailableActions;

		private bool m_IsRunning;

		private static eFuelType askForFuelType()
		{
			string fuelTypeString = UserInputHelper.SelectFromList(
				"Please select a fuel type",
				Enum.GetNames(typeof(eFuelType)),
				StringComparer.CurrentCultureIgnoreCase);
			return (eFuelType)Enum.Parse(typeof(eFuelType), fuelTypeString);
		}

		private static void printVehicleInformation(VehicleInfo i_Vehicle)
		{
			printBasicVehicleInformation(i_Vehicle);
			printExtraProperties(i_Vehicle);
			printEnergySourceInformation(i_Vehicle.EnergySource);
			printWheelInformation(i_Vehicle.Wheels);
		}

		private static void printWheelInformation(IEnumerable<WheelInfo> i_Wheels)
		{
			Console.WriteLine("Wheel Information:");
			int wheelCounter = 1;
			foreach (WheelInfo wheelInfo in i_Wheels)
			{
				Console.WriteLine(
@"* Wheel {0}
	Manufacturer: {1}
	Current Air Pressure: {2}",
					wheelCounter,
					wheelInfo.ManufacturerName,
					wheelInfo.AirPressure);
				wheelCounter++;
			}
		}

		private static void printEnergySourceInformation(IEnergySource i_EnergySource)
		{
			string units = "hours";
			string energyDescriptor = "Charge";
			if (i_EnergySource is FuelTank)
			{
				Console.WriteLine("Fuel Type: {0}", ((FuelTank)i_EnergySource).FuelType);
				units = "liters";
				energyDescriptor = "Fuel";
			}

			Console.WriteLine(
				"{4}: {1} {0} left out of {2} {0} ({3:N0}%)",
				units,
				i_EnergySource.EnergyLeft,
				i_EnergySource.MaxEnergy,
				i_EnergySource.PercentLeft * 100,
				energyDescriptor);
		}

		private static void printExtraProperties(VehicleInfo i_Vehicle)
		{
			foreach (IExtraProperty property in i_Vehicle.ExtraProperties)
			{
				Console.WriteLine("{0}: {1}", property.Name, property.Value);
			}
		}

		private static void printBasicVehicleInformation(VehicleInfo i_Vehicle)
		{
			Console.WriteLine(
@"--- Vehicle Information ---
License Number: {0}
Fix Status: {1}
Owner Name: {2}
Owner Phone Number: {3}",
				i_Vehicle.Id,
				i_Vehicle.FixStatus,
				i_Vehicle.Owner.Name,
				i_Vehicle.Owner.PhoneNumber);
		}

		private static OwnerInfo askForOwnerInformation()
		{
			string ownerName = UserInputHelper.AskForNonEmptyString("Owner's Name");
			string ownerPhoneNumber = UserInputHelper.AskForNonEmptyString("Owner's Phone Number");
			return new OwnerInfo(ownerName, ownerPhoneNumber);
		}

		private static void askForExtraProperties(VehicleInfo i_Vehicle)
		{
			foreach (IExtraProperty property in i_Vehicle.ExtraProperties)
			{
				bool valueOk = false;
				while (!valueOk)
				{
					string value = UserInputHelper.AskForNonEmptyString(string.Format("{0} ({1})", property.Name, property.InputHint));
					try
					{
						property.Value = value;
						valueOk = true;
					}
					catch (FormatException)
					{
						Console.WriteLine("Could not format property, please try again");
					}
					catch (ArgumentException)
					{
						Console.WriteLine("Could not format property, please try again");
					}
					catch (ValueOutOfRangeException)
					{
						Console.WriteLine("Value is out of range, please try again");
					}
				}
			}
		}

		private static eFixStatus askForVehicleStatus()
		{
			eFixStatus result = eFixStatus.Fixed;
			string selection = UserInputHelper.SelectFromList(
				"Please select a vehicle status",
				new[] { "fixing", "fixed", "payed" },
				StringComparer.CurrentCultureIgnoreCase);
			switch (selection)
			{
				case "fixed":
					result = eFixStatus.Fixed;
					break;
				case "fixing":
					result = eFixStatus.Fixing;
					break;
				case "payed":
					result = eFixStatus.Payed;
					break;
			}

			return result;
		}

        public ConsoleFrontEnd()
        {
            r_DataStore = new VehicleDataStore();
	        const bool v_RequiresVehiclesInDataStore = true;
			this.r_AvailableActions = new[]
			{
				new FrontEndAction("Insert a new vehicle to the garage", insertVehicleToGarage, !v_RequiresVehiclesInDataStore), 
				new FrontEndAction("Show all License numbers of vehicles in the garage", showVehiclesInDataStore, v_RequiresVehiclesInDataStore),
				new FrontEndAction("Change vehicle's status", changeVehicleStatus, v_RequiresVehiclesInDataStore),
				new FrontEndAction("Inflate wheels", inflateWheels, v_RequiresVehiclesInDataStore),
				new FrontEndAction("Fuel a vehicle", fuelVehicle, v_RequiresVehiclesInDataStore),
				new FrontEndAction("Charge an electric vehicle", chargeElectricVehicle, v_RequiresVehiclesInDataStore),
				new FrontEndAction("Show vehicle's information", showVehicleInformation, v_RequiresVehiclesInDataStore),
				new FrontEndAction("exit", exit, !v_RequiresVehiclesInDataStore),
			};

	        m_IsRunning = false;
        }

		private void showVehicleInformation()
		{
			printVehicleInformation(askForExistingVehicle());
		}

		public void ShowMainMenu()
	    {
		    m_IsRunning = true;
			while (m_IsRunning)
            {
                Console.Clear();
	            Console.WriteLine("Welcome to Saggi, Maor and son's Garage!!");
	            printFunctionList();
	            int userChoice = UserInputHelper.GetValueFromUser<int>(
			            "Please enter the number of the action you want to perform",
			            int.TryParse,
			            "Please enter a number");
	            try
	            {
		            FrontEndAction action = this.r_AvailableActions[userChoice - 1];
		            if (action.RequiresVehiclesInDataStore && r_DataStore.IsEmpty)
		            {
			            Console.WriteLine("Garage is empty");
		            }
		            else
		            {
			            action.Action();
		            }
	            }
	            catch (IndexOutOfRangeException)
	            {
					Console.WriteLine("Invalid request number, please try again");
	            }
				
                if (m_IsRunning)
                {
                    Console.WriteLine("Press any key to go back to main menu.");
                    Console.ReadKey();
                }
            }
        }

		private void exit()
		{
			m_IsRunning = false;
			Console.WriteLine("Have a nice day!");
			Console.ReadKey();
		}

		private void printFunctionList()
		{
			int optionCounter = 1;
			foreach (FrontEndAction uiFunction in this.r_AvailableActions)
			{
				Console.WriteLine("{0}) {1}.", optionCounter, uiFunction.Description);
				optionCounter++;
			}
		}

		private string askForVehicleType()
		{
			return UserInputHelper.SelectFromList(
				"Vehicle Type",
				r_DataStore.AvailableVehicleTypes,
				StringComparer.CurrentCultureIgnoreCase);
		}

		private void insertVehicleToGarage()
        {
			string licenseNumber = UserInputHelper.AskForNonEmptyString("License Number");
			VehicleInfo vehicle = r_DataStore.FindVehicle(licenseNumber);
			if (vehicle != null)
			{
				Console.WriteLine("Vehicle already in garage, changing it's status to 'Fixing'");
				vehicle.FixStatus = eFixStatus.Fixing;
			}
			else
			{
				Console.WriteLine("Please enter the following information:");
				string vehicleType = askForVehicleType();
				OwnerInfo owner = askForOwnerInformation();
				vehicle = r_DataStore.CreateVehicle(vehicleType, licenseNumber, owner);
				askForExtraProperties(vehicle);
				askForWheelInformation(vehicle);
			}
        }

		private void askForWheelInformation(VehicleInfo i_Vehicle)
		{
			int wheelCounter = 1;
			foreach (WheelInfo wheel in i_Vehicle.Wheels)
			{
				Console.WriteLine("Please enter information for wheel no {0}", wheelCounter);
				wheel.ManufacturerName = UserInputHelper.AskForNonEmptyString("Manufacturer Name");
				bool airPressureSet = false;
				while (!airPressureSet)
				{
					try
					{
						float amountToAdd = UserInputHelper.GetValueFromUser<float>(
							"Current Air Pressure",
							float.TryParse,
							"Please enter a valid air pressure");
						wheel.Inflate(amountToAdd);
						airPressureSet = true;
					}
					catch (ValueOutOfRangeException)
					{
						Console.WriteLine("Value out of range, please try again (maximum: {0})", wheel.MaxAirPressure);
					}
				}

				wheelCounter++;
			}
		}

		private void inflateWheels()
		{
			VehicleInfo vehicle = askForExistingVehicle();
			foreach (WheelInfo wheel in vehicle.Wheels)
			{
				wheel.Inflate(wheel.MaxAirPressure - wheel.AirPressure);
			}
		}

		private void fuelVehicle()
        {
            VehicleInfo vehicle = askForExistingVehicle();

			FuelTank fuelTank = vehicle.EnergySource as FuelTank;
			if (fuelTank == null)
			{
				Console.WriteLine("This vehicle is not powered by fuel");
			}
			else
			{
                eFuelType correctFuelType = fuelTank.FuelType;
                bool gotRightFuelType = askForFuelType() == correctFuelType;
                while (!gotRightFuelType)
                {
					Console.WriteLine("Wrong fuel type, please try again");
	                gotRightFuelType = askForFuelType() == correctFuelType;
                }

				addToEnergySource(fuelTank, "liters", 1f);
            }
        }

		private void addToEnergySource(IEnergySource i_EnergySource, string i_EnergyUnit, float i_EnergyUnitConversionRatio)
		{
			float maxEnergyToAllow = (i_EnergySource.MaxEnergy - i_EnergySource.EnergyLeft) / i_EnergyUnitConversionRatio;
			string question = string.Format("How much {0} do you want to add (maximum {1} {0})?", i_EnergyUnit, maxEnergyToAllow);
			string errorString = string.Format("Please enter a valid number of {0}", i_EnergyUnit);
			float amuontToAdd = UserInputHelper.GetValueFromUser<float>(question, float.TryParse, errorString) * i_EnergyUnitConversionRatio;
			try
			{
				i_EnergySource.AddEnergy(amuontToAdd);
			}
			catch (ValueOutOfRangeException)
			{
				Console.WriteLine("This is too much, please enter a valid amount");
			}
		}

		private void chargeElectricVehicle()
        {
			VehicleInfo vehicle = askForExistingVehicle();

			VehicleBattery bettery = vehicle.EnergySource as VehicleBattery;
			if (bettery == null)
			{
				Console.WriteLine("This vehicle is not powered by a battery");
			}
			else
			{
				addToEnergySource(bettery, "minutes", 1f / 60f);
			}
        }

        private void showVehiclesInDataStore()
        {
	        IEnumerable<VehicleInfo> vehicles = r_DataStore.Vehicles;
	        bool shouldFilter = UserInputHelper.AskYesNoQuestion("Would you like to filter the list");
	        if (shouldFilter)
	        {
		        vehicles = r_DataStore.GetVehiclesFilteredByFixStatus(askForVehicleStatus());
	        }

	        bool anyFound = false;
	        foreach (VehicleInfo vehicle in vehicles)
	        {
				Console.WriteLine(vehicle.Id);
		        anyFound = true;
	        }

            if (!anyFound)
            {
                Console.WriteLine("No matching vehicles found");
            }
        }

		private VehicleInfo askForExistingVehicle()
		{
			VehicleInfo vehicle = null;
			while (vehicle == null)
			{
				string liscenceNumber = UserInputHelper.AskForNonEmptyString("Please enter the license number of the vehicle");
				vehicle = r_DataStore.FindVehicle(liscenceNumber);
				if (vehicle == null)
				{
					Console.WriteLine("Vehicle not found, please try again");
				}
			}

			return vehicle;
		}

		private void changeVehicleStatus()
		{
			VehicleInfo vehicle = askForExistingVehicle();
			eFixStatus fixStatus = askForVehicleStatus();
			vehicle.FixStatus = fixStatus;
			Console.WriteLine("Vehicle status changes successfully");
		}
    }
}
