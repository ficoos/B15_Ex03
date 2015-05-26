namespace Ex03.GarageLogic
{
	internal class LimitedContainer
	{
		public float PercentLeft
		{
			get
			{
				return this.AmountLeft / this.r_MaxAmount;
			}
		}

		public float AmountLeft { get; private set; }

		private readonly float r_MaxAmount;

		public float MaxAmount
		{
			get
			{
				return this.r_MaxAmount;
			}
		}

		public void AddAmount(float i_Amount)
		{
			float targetAmount = this.AmountLeft + i_Amount;
			if (targetAmount > this.r_MaxAmount)
			{
				throw new ValueOutOfRangeException(i_Amount, 0, this.MaxAmount);
			}

			this.AmountLeft = targetAmount;
		}

		public LimitedContainer(float i_MaxAmount, float i_InitialAmount = 0)
		{
			r_MaxAmount = i_MaxAmount;
			AmountLeft = 0;
			AddAmount(i_InitialAmount);
		}
	}
}