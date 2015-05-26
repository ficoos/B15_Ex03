namespace Ex03.GarageManagementSystem.ConsoleUI
{
	public struct FrontEndAction
	{
		public delegate void FrontEndActionDelegate();

		private readonly string r_Description;

		public string Description
		{
			get
			{
				return r_Description;
			}
		}

		private readonly FrontEndActionDelegate r_Action;

		public FrontEndActionDelegate Action
		{
			get
			{
				return this.r_Action;
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

		public FrontEndAction(string i_Description, FrontEndActionDelegate i_Action, bool i_RequiresVehiclesInDataStore)
		{
			r_Description = i_Description;
			this.r_Action = i_Action;
			r_RequiresVehiclesInDataStore = i_RequiresVehiclesInDataStore;
		}
	}
}