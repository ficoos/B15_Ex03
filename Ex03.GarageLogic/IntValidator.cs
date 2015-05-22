namespace Ex03.GarageLogic
{
	internal class IntValidator : IPropertyValidator
	{
		private readonly int r_Min;

		private readonly int r_Max;

		public IntValidator(int i_Min = int.MinValue, int i_Max = int.MaxValue)
		{
			r_Min = i_Min;
			r_Max = i_Max;
		}

		public string ValidateValue(string i_Value)
		{
			int value = int.Parse(i_Value);
			if (value < r_Min || value > r_Max)
			{
				throw new ValueOutOfRangeException<int>(value, r_Min, r_Max);
			}

			return value.ToString();
		}
	}
}