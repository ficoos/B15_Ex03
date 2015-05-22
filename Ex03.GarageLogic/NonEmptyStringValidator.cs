using System;

namespace Ex03.GarageLogic
{
	internal class NonEmptyStringValidator : IPropertyValidator
	{
		public string ValidateValue(string i_Value)
		{
			i_Value = i_Value.Trim();
			if (i_Value == string.Empty)
			{
				throw new ArgumentException("String must have value");
			}

			return i_Value;
		}
	}
}