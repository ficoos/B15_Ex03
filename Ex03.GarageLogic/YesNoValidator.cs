using System;

namespace Ex03.GarageLogic
{
	internal class YesNoValidator : IPropertyValidator
	{
		public string ValidateValue(string i_Value)
		{
			string value;
			if (string.Equals(i_Value, "yes", StringComparison.CurrentCultureIgnoreCase))
			{
				value = "Yes";
			}
			else if (string.Equals(i_Value, "no", StringComparison.CurrentCultureIgnoreCase))
			{
				value = "No";
			}
			else
			{
				throw new ArgumentException("Value is invalid");
			}

			return value;
		}
	}
}