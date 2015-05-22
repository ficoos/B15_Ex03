namespace Ex03.GarageLogic
{
	public class VehicleBattery : IEnergySource
	{
		private readonly SimpleEnergySource r_InnerImplementation;

		public float PercentLeft
		{
			get
			{
				return r_InnerImplementation.PercentLeft;
			}
		}

		float IEnergySource.EnergyLeft
		{
			get
			{
				return ChargeLeftInHours;
			}
		}

		float IEnergySource.MaxEnergy
		{
			get
			{
				return MaxChargeDurationInHours;
			}
		}

		void IEnergySource.AddEnergy(float i_Amount)
		{
			Charge(i_Amount);
		}

		public float ChargeLeftInHours
		{
			get
			{
				return r_InnerImplementation.EnergyLeft;
			}
		}

		public float MaxChargeDurationInHours
		{
			get
			{
				return r_InnerImplementation.MaxEnergy;
			}
		}

		public void Charge(float i_Hours)
		{
			r_InnerImplementation.AddEnergy(i_Hours);
		}

		internal VehicleBattery(float i_EnergyLeft, float i_MaxEnergy)
		{
			r_InnerImplementation = new SimpleEnergySource(i_EnergyLeft, i_MaxEnergy);
		}
	}
}