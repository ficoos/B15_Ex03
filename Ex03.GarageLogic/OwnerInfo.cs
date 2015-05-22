namespace Ex03.GarageLogic
{
	public class OwnerInfo
	{
		public string Name { get; set; }

		public string PhoneNumber { get; set; }

		public OwnerInfo(string i_Name, string i_PhoneNumber)
		{
			Name = i_Name;
			PhoneNumber = i_PhoneNumber;
		}
	}
}