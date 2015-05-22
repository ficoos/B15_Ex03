namespace Ex03.GarageLogic
{
	using System;
	using System.Collections.Generic;

	internal class SimpleMultipleChoiceExtraProperty : IMultipleChoiceExtraProperty, IPropertyValidator
	{
		private readonly SimpleExtraProperty r_SimpleExtraProperty;

		public string Name
		{
			get
			{
				return r_SimpleExtraProperty.Name;
			}
		}

		public string InputHint
		{
			get
			{
				return r_SimpleExtraProperty.InputHint;
			}
		}

		public string Value
		{
			get
			{
				return r_SimpleExtraProperty.Value;
			}

			set
			{
				r_SimpleExtraProperty.Value = value;
			}
		}

		private readonly string[] r_ChoiceOptions;

		public IEnumerable<string> ChoiceOptions
		{
			get
			{
				return this.r_ChoiceOptions;
			}
		}

		public SimpleMultipleChoiceExtraProperty(string i_Name, string[] i_ChoiceOptions)
		{
			this.r_ChoiceOptions = i_ChoiceOptions;
			r_SimpleExtraProperty = new SimpleExtraProperty(i_Name, string.Format("One of {0}", string.Join(", ", i_ChoiceOptions)), this);
		}

		string IPropertyValidator.ValidateValue(string i_Value)
		{
			if (Array.IndexOf(this.r_ChoiceOptions, i_Value) == -1)
			{
				throw new ArgumentException("Value not in choice options");
			}

			return Value;
		}
	}
}