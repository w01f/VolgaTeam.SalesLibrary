using ceTe.DynamicPDF.Rasterizer;
using Vintasoft.Imaging;

namespace SalesLibraries.CloudAdmin.Business.Services
{
	static class LicenseHelper
	{
		public static void Register()
		{
			ImagingGlobalSettings.RegisterImaging("William Byrd",
				"billy@newlocaldirect.com",
				"IhdrSwyenp4CiNJnDzdnhXPg2TUsJa0Rykz4Gpmc11FMGFz5vEt0bFKIrMR6VziCH7YfvX8Ofnznh9z7WuhhZCFCp4mtvRBq1OJhX2M/1PS2GifwTp2aQlioVFHW7VhIgKdFerrBs6YDG6M15DKyiWqFJo4Ks6wyqEH5ANvmj5EI");
			PdfRasterizer.AddLicense("RST20NXDGKFKDLYMw0fsJRu1DsUT9rc8r8C+PEemp2t8HAswYEWFAkaX7h63jGM6Ip9hLi15A4aSVky01IXC9+Tga7jNykC8BW7g");
		}
	}
}
