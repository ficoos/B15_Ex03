using System;

namespace Ex03.GarageLogic
{
	public class ValueOutOfRangeException : Exception
	{
		private readonly float r_Value;

		public float Value
		{
			get
			{
				return r_Value;
			}
		}

		private readonly float r_Min;

		public float Min
		{
			get
			{
				return r_Min;
			}
		}

		private readonly float r_Max;

		public float Max
		{
			get
			{
				return r_Max;
			}
		}

		public ValueOutOfRangeException(float i_Value, float i_Min, float i_Max) : base("Value out of range")
		{
			r_Value = i_Value;
			r_Min = i_Min;
			r_Max = i_Max;
		}
	}
}