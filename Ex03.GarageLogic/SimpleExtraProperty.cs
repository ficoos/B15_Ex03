namespace Ex03.GarageLogic
{
	internal class SimpleExtraProperty : IExtraProperty
	{
		private readonly IPropertyValidator r_Validator;

		private readonly string r_Name;

		public string Name
		{
			get
			{
				return r_Name;
			}
		}

		private readonly string r_InputHint;

		public string InputHint
		{
			get
			{
				return r_InputHint;
			}
		}

		private string m_Value;

		public string Value
		{
			get
			{
				return m_Value;
			}

			set
			{
				m_Value = r_Validator.ValidateValue(value);
			}
		}

		public SimpleExtraProperty(string i_Name, string i_InputHint, IPropertyValidator i_Validator)
		{
			r_Name = i_Name;
			r_InputHint = i_InputHint;
			r_Validator = i_Validator;
		}
	}
}