namespace Ex03.GarageLogic
{
	public interface IEnergySource
	{
		float PercentLeft { get; }
		
		float EnergyLeft { get; }
		
		float MaxEnergy { get; }

		void AddEnergy(float i_Amount);
	}
}
