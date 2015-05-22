using System;

namespace Ex03.GarageLogic
{
	public class WheelInfo
	{
		public string ManufacturerName { get; set; }

		private float m_AirPressure;

		public float AirPressure
		{
			get
			{
				return this.m_AirPressure;
			}

			set
			{
				if (value < 0 || value > r_MaxAirPressure)
				{
					throw new ValueOutOfRangeException<float>(value, 0, MaxAirPressure);
				}

				this.m_AirPressure = value;
			}
		}

		private readonly float r_MaxAirPressure;

		public float MaxAirPressure
		{
			get
			{
				return r_MaxAirPressure;
			}
		}

		public WheelInfo(float i_MaxAirPressure, float i_AirPressure = 0, string i_ManufacturerName = "Unknown")
		{
			ManufacturerName = i_ManufacturerName;
			AirPressure = i_AirPressure;
			r_MaxAirPressure = i_MaxAirPressure;
		}

		internal WheelInfo DeepClone()
		{
			return this.MemberwiseClone() as WheelInfo;
		}
	}
}
