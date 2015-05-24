namespace Ex03.GarageManagementSystem.ConsoleUI
{
	public struct UIFunction
	{
		public delegate void UIFunctionDelegate();

		private readonly string r_Description;

		public string Description
		{
			get
			{
				return r_Description;
			}
		}

		private readonly UIFunctionDelegate r_Function;

		public UIFunctionDelegate Function
		{
			get
			{
				return r_Function;
			}
		}

		private readonly bool r_RequiresVehiclesInDataStore;

		public bool RequiresVehiclesInDataStore
		{
			get
			{
				return r_RequiresVehiclesInDataStore;
			}
		}

		public UIFunction(string i_Description, UIFunctionDelegate i_Function, bool i_RequiresVehiclesInDataStore)
		{
			r_Description = i_Description;
			r_Function = i_Function;
			r_RequiresVehiclesInDataStore = i_RequiresVehiclesInDataStore;
		}
	}
}