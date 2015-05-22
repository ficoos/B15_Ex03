namespace Ex03.GarageLogic
{
	public interface IExtraProperty
	{
		string Name { get; }

		string InputHint { get; }

		string Value { get; set; }
	}
}
