using System.Globalization;

namespace Ex03.GarageLogic
{
	internal class FloatValidator : IPropertyValidator
	{
		private readonly float r_Min;

		private readonly float r_Max;

		public FloatValidator(float i_Min = float.MinValue, float i_Max = float.MaxValue)
		{
			this.r_Min = i_Min;
			this.r_Max = i_Max;
		}

		public string ValidateValue(string i_Value)
		{
			float value = float.Parse(i_Value);
			if (value < this.r_Min || value > this.r_Max)
			{
				throw new ValueOutOfRangeException<float>(value, this.r_Min, this.r_Max);
			}

			return value.ToString(CultureInfo.CurrentCulture);
		}
	}
}