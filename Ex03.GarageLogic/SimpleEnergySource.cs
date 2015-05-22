using System;

namespace Ex03.GarageLogic
{
	internal class SimpleEnergySource : IEnergySource
	{
		public float PercentLeft
		{
			get
			{
				return EnergyLeft / r_MaxEnergy;
			}
		}

		public float EnergyLeft { get; private set; }

		private readonly float r_MaxEnergy;

		public float MaxEnergy
		{
			get
			{
				return r_MaxEnergy;
			}
		}

		public void AddEnergy(float i_Amount)
		{
			float targetAmount = EnergyLeft + i_Amount;
			if (targetAmount > r_MaxEnergy)
			{
				throw new ValueOutOfRangeException<float>(i_Amount, 0, MaxEnergy);
			}

			EnergyLeft = targetAmount;
		}

		public SimpleEnergySource(float i_InitialAmount, float i_MaxAmount)
		{
			r_MaxEnergy = i_MaxAmount;
			EnergyLeft = i_InitialAmount;
		}
	}
}