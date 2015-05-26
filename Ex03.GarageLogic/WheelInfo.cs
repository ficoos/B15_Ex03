namespace Ex03.GarageLogic
{
	public class WheelInfo
	{
		public string ManufacturerName { get; set; }

		private readonly LimitedContainer r_LimitedContainer;

		public float AirPressure
		{
			get
			{
				return r_LimitedContainer.AmountLeft;
			}
		}

		public void Inflate(float i_Amount)
		{
			r_LimitedContainer.AddAmount(i_Amount);
		}

		public float MaxAirPressure
		{
			get
			{
				return r_LimitedContainer.MaxAmount;
			}
		}

		public WheelInfo(float i_MaxAirPressure, float i_AirPressure = 0, string i_ManufacturerName = "Unknown")
		{
			ManufacturerName = i_ManufacturerName;
			r_LimitedContainer = new LimitedContainer(i_MaxAirPressure, i_AirPressure);
		}

		internal WheelInfo DeepClone()
		{
			return new WheelInfo(MaxAirPressure, AirPressure, ManufacturerName);
		}
	}
}
