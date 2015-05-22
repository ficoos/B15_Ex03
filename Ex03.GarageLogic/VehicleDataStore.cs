using System.Collections.Generic;

namespace Ex03.GarageLogic
{
	using System;
	using System.ComponentModel;

	public class VehicleDataStore
	{
		private readonly Dictionary<string, VehicleInfo> r_Vehicles;

		public IEnumerable<VehicleInfo> Vehicles
		{
			get
			{
				return r_Vehicles.Values;
			}
		}

		public IEnumerable<string> AvailableVehicleTypes
		{
			get
			{
				return VehicleFactory.AvailableVehicleTypes;
			}
		}

		public VehicleDataStore()
		{
			r_Vehicles = new Dictionary<string, VehicleInfo>();
		}

		public VehicleInfo FindVehicle(string i_Id)
		{
			VehicleInfo result;
			if (!r_Vehicles.TryGetValue(i_Id, out result))
			{
				// Make sure result doesn't contain garbage value
				result = null;
			}

			return result;
		}

		public VehicleInfo CreateVehicle(string i_VehicleType, string i_Id, OwnerInfo i_Owner)
		{
			if (r_Vehicles.ContainsKey(i_Id))
			{
				throw new ArgumentException("ID already in use");
			}

			VehicleInfo result = VehicleFactory.CreateVehicle(i_VehicleType, i_Id, i_Owner);
			r_Vehicles[result.Id] = result;

			return result;
		}

		public IEnumerable<VehicleInfo> GetVehiclesFilteredByFixStatus(eFixStatus i_FixStatus)
		{
			foreach (VehicleInfo vehicle in r_Vehicles.Values)
			{
				if (vehicle.FixStatus == i_FixStatus)
				{
					yield return vehicle;
				}
			}
		}
	}
}