using ceTe.DynamicPDF.Rasterizer;

namespace SalesLibraries.FileManager.Business.Services
{
	static class LicenseHelper
	{
		public static void Register()
		{
			PdfRasterizer.AddLicense("RST20NXDGKFKDLYMw0fsJRu1DsUT9rc8r8C+PEemp2t8HAswYEWFAkaX7h63jGM6Ip9hLi15A4aSVky01IXC9+Tga7jNykC8BW7g");
		}
	}
}
