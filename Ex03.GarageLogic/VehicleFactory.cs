using System.Collections.Generic;

namespace Ex03.GarageLogic
{
	internal static class VehicleFactory
	{
		public delegate VehicleInfo VehicleCreationDelegate(string i_Id, OwnerInfo i_Owner);

		private static readonly Dictionary<string, VehicleCreationDelegate> sr_AvailableVehicles =
			new Dictionary<string, VehicleCreationDelegate>
				{
					{ "Simple Bike", createSimpleBike },
					{ "Electric Bike", createElectricBike },
					{ "Simple Car", createSimpleCar },
					{ "Electric Car", createElectricCar },
					{ "Truck", createTruck }
				};

		public static IEnumerable<string> AvailableVehicleTypes
		{
			get
			{
				return sr_AvailableVehicles.Keys;
			}
		}

		public static bool IsVehicleTypeSupported(string i_VehicleType)
		{
			return sr_AvailableVehicles.ContainsKey(i_VehicleType);
		}
		
		private static void addCommonBikeProperties(ICollection<IExtraProperty> i_PropertyList)
		{
			i_PropertyList.Add(new SimpleExtraProperty("Engine Volume in cubic centimeters", "Value must be positive", new IntValidator(0)));
			i_PropertyList.Add(new SimpleMultipleChoiceExtraProperty("License type", new[] { "A", "A2", "AB", "B1" }));
		}

		private static void addCommonCarProperties(ICollection<IExtraProperty> i_PropertyList)
		{
			i_PropertyList.Add(new SimpleMultipleChoiceExtraProperty("Color", new[] { "Green", "Black", "White", "Red" }));
			i_PropertyList.Add(new SimpleMultipleChoiceExtraProperty("Number of doors", new[] { "2", "3", "4", "5" }));
		}
		
		private static WheelInfo[] cloneWheels(WheelInfo i_WheelTemplate, int i_Count)
		{
			WheelInfo[] wheels = new WheelInfo[i_Count];

			for (int i = 0; i < i_Count; i++)
			{
				wheels[i] = i_WheelTemplate.DeepClone();
			}

			return wheels;
		}

		private static VehicleInfo createSimpleBike(string i_Id, OwnerInfo i_Owner)
		{
			const float v_MaxAirPressure = 34;
			const int v_NumOfWheels = 2;
			const float v_MaxEnergy = 8;
			List<IExtraProperty> extraProperties = new List<IExtraProperty>();
			addCommonBikeProperties(extraProperties);
			return new VehicleInfo(
				i_Id,
				i_Owner,
				new FuelTank(v_MaxEnergy, v_MaxEnergy, eFuelType.Octan98),
				cloneWheels(new WheelInfo(v_MaxAirPressure), v_NumOfWheels),
				extraProperties.ToArray());
		}

		private static VehicleInfo createSimpleCar(string i_Id, OwnerInfo i_Owner)
		{
			const float v_MaxAirPressure = 31;
			const int v_NumOfWheels = 4;
			const float v_MaxEnergy = 35;
			List<IExtraProperty> extraProperties = new List<IExtraProperty>();
			addCommonCarProperties(extraProperties);
			return new VehicleInfo(
				i_Id,
				i_Owner,
				new FuelTank(v_MaxEnergy, v_MaxEnergy, eFuelType.Octan96),
				cloneWheels(new WheelInfo(v_MaxAirPressure), v_NumOfWheels),
				extraProperties.ToArray());
		}

		private static VehicleInfo createTruck(string i_Id, OwnerInfo i_Owner)
		{
			const float v_MaxAirPressure = 25;
			const int v_NumOfWheels = 16;
			const float v_MaxEnergy = 170;
			List<IExtraProperty> extraProperties = new List<IExtraProperty>();
			addCommonCarProperties(extraProperties);
			extraProperties.Add(new SimpleExtraProperty("Current Weight", "Value must be positive", new FloatValidator(0)));
			extraProperties.Add(new SimpleExtraProperty("Carries dangerous materials", "yes or no", new YesNoValidator()));
			return new VehicleInfo(
				i_Id,
				i_Owner,
				new FuelTank(v_MaxEnergy, v_MaxEnergy, eFuelType.Soler),
				cloneWheels(new WheelInfo(v_MaxAirPressure), v_NumOfWheels),
				extraProperties.ToArray());
		}

		private static VehicleInfo createElectricBike(string i_Id, OwnerInfo i_Owner)
		{
			const float v_MaxAirPressure = 31;
			const int v_NumOfWheels = 2;
			const float v_MaxEnergy = 1.2f;
			List<IExtraProperty> extraProperties = new List<IExtraProperty>();
			addCommonBikeProperties(extraProperties);
			return new VehicleInfo(
				i_Id,
				i_Owner,
				new VehicleBattery(v_MaxEnergy, v_MaxEnergy), 
				cloneWheels(new WheelInfo(v_MaxAirPressure), v_NumOfWheels),
				extraProperties.ToArray());
		}

		private static VehicleInfo createElectricCar(string i_Id, OwnerInfo i_Owner)
		{
			const float v_MaxAirPressure = 31;
			const int v_NumOfWheels = 4;
			const float v_MaxEnergy = 2.2f;
			List<IExtraProperty> extraProperties = new List<IExtraProperty>();
			addCommonCarProperties(extraProperties);
			return new VehicleInfo(
				i_Id,
				i_Owner,
				new VehicleBattery(v_MaxEnergy, v_MaxEnergy),
				cloneWheels(new WheelInfo(v_MaxAirPressure), v_NumOfWheels),
				extraProperties.ToArray());
		}

		public static VehicleInfo CreateVehicle(string i_VehicleType, string i_Id, OwnerInfo i_Owner)
		{
			return sr_AvailableVehicles[i_VehicleType].Invoke(i_Id, i_Owner);
		}
	}
}