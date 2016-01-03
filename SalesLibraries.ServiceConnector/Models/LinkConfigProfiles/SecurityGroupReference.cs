namespace SalesLibraries.ServiceConnector.LinkConfigProfileService
{
	public partial class SecurityGroupReference
	{
		public bool Selected { get; set; }

		public SecurityGroupReference Clone()
		{
			return new SecurityGroupReference
			{
				id = id,
				name = name,
			};
		}
	}
}
