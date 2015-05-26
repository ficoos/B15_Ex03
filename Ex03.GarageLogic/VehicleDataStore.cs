using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
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

		public bool IsEmpty
		{
			get
			{
				return r_Vehicles.Count == 0;
			}
		}

		public VehicleDataStore()
		{
			r_Vehicles = new Dictionary<string, VehicleInfo>();
		}

		public VehicleInfo FindVehicle(string i_Id)
		{
			VehicleInfo result;
			r_Vehicles.TryGetValue(i_Id, out result);
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