using System;

namespace Ex03.GarageLogic
{
	public class ValueOutOfRangeException<T> : Exception
	{
		private readonly T r_Value;

		public T Value
		{
			get
			{
				return r_Value;
			}
		}

		private readonly T r_Min;

		public T Min
		{
			get
			{
				return r_Min;
			}
		}

		private readonly T r_Max;

		public T Max
		{
			get
			{
				return r_Max;
			}
		}

		public ValueOutOfRangeException(T i_Value, T i_Min, T i_Max) : base("Value out of range")
		{
			r_Value = i_Value;
			r_Min = i_Min;
			r_Max = i_Max;
		}
	}
}