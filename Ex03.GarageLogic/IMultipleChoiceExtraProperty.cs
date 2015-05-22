using System.Collections.Generic;

namespace Ex03.GarageLogic
{
	internal interface IMultipleChoiceExtraProperty : IExtraProperty
	{
		IEnumerable<string> ChoiceOptions { get; }
	}
}