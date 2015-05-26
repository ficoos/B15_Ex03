namespace Ex03.GarageLogic
{
	public class FuelTank : IEnergySource
	{
		 private readonly LimitedContainer r_InnerImplementation;

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
				return r_InnerImplementation.AmountLeft;
			}
		}

		public float TankCapacityInLiters
		{
			get
			{
				return r_InnerImplementation.MaxAmount;
			}
		}

		public void AddFuel(float i_Liters)
		{
			r_InnerImplementation.AddAmount(i_Liters);
		}

		internal FuelTank(float i_LitersLeft, float i_TankCapacity, eFuelType i_FuelType)
		{
			r_InnerImplementation = new LimitedContainer(i_LitersLeft, i_TankCapacity);
			r_FuelType = i_FuelType;
		}
	}
}