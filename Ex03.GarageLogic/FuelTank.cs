namespace Ex03.GarageLogic
{
	public class FuelTank : IEnergySource
	{
		 private readonly SimpleEnergySource r_InnerImplementation;

		private readonly eFuelType r_FuelType;

		public eFuelType FuelType
		{
			get
			{
				return r_FuelType; 
			}
		}

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
				return LitersLeft;
			}
		}

		float IEnergySource.MaxEnergy
		{
			get
			{
				return TankCapacityInLiters;
			}
		}

		void IEnergySource.AddEnergy(float i_Amount)
		{
			this.AddFuel(i_Amount);
		}

		public float LitersLeft
		{
			get
			{
				return r_InnerImplementation.EnergyLeft;
			}
		}

		public float TankCapacityInLiters
		{
			get
			{
				return r_InnerImplementation.MaxEnergy;
			}
		}

		public void AddFuel(float i_Liters)
		{
			r_InnerImplementation.AddEnergy(i_Liters);
		}

		internal FuelTank(float i_LitersLeft, float i_TankCapacity, eFuelType i_FuelType)
		{
			r_InnerImplementation = new SimpleEnergySource(i_LitersLeft, i_TankCapacity);
			r_FuelType = i_FuelType;
		}
	}
}