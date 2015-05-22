using System.Collections.Generic;

namespace Ex03.GarageLogic
{
	using System;

	public class VehicleInfo
	{
		private readonly string r_Id;

		public string Id
		{
			get
			{
				return r_Id;
			}
		}

		private readonly OwnerInfo r_Owner;

		public OwnerInfo Owner
		{
			get
			{
				return r_Owner;
			}
		}

		public eFixStatus FixStatus { get; set; }

		private readonly WheelInfo[] r_Wheels;

		public IEnumerable<WheelInfo> Wheels
		{
			get
			{
				return r_Wheels;
			}
		}

		private readonly IExtraProperty[] r_ExtraProperties;

		public IEnumerable<IExtraProperty> ExtraProperties
		{
			get
			{
				return r_ExtraProperties;
			}
		}

		private readonly IEnergySource r_EnergySource;

		public IEnergySource EnergySource
		{
			get
			{
				return r_EnergySource;
			}
		}

		internal VehicleInfo(
			string i_Id,
			OwnerInfo i_Owner,
			IEnergySource i_EnergySource,
			WheelInfo[] i_Wheels,
			IExtraProperty[] i_ExtraProperties = null)
		{
			r_Id = i_Id;
			r_Owner = i_Owner;
			r_Wheels = i_Wheels;
			i_ExtraProperties = i_ExtraProperties ?? new IExtraProperty[0];
			r_ExtraProperties = new IExtraProperty[i_ExtraProperties.Length + 1];
			r_ExtraProperties[0] = new SimpleExtraProperty("Manufacturer Name", "Could be any non-empty string", new NonEmptyStringValidator());
			Array.Copy(i_ExtraProperties, 0, r_ExtraProperties, 1, i_ExtraProperties.Length);
			r_EnergySource = i_EnergySource;
			FixStatus = eFixStatus.Fixing;
		}
	}
}