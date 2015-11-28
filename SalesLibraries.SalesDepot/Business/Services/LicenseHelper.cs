using EO.WebBrowser;
using Vintasoft.Imaging;

namespace SalesLibraries.SalesDepot.Business.Services
{
	static class LicenseHelper
	{
		public static void Register()
		{
			ImagingGlobalSettings.RegisterImaging("William Byrd",
				"billy@newlocaldirect.com",
				"IhdrSwyenp4CiNJnDzdnhXPg2TUsJa0Rykz4Gpmc11FMGFz5vEt0bFKIrMR6VziCH7YfvX8Ofnznh9z7WuhhZCFCp4mtvRBq1OJhX2M/1PS2GifwTp2aQlioVFHW7VhIgKdFerrBs6YDG6M15DKyiWqFJo4Ks6wyqEH5ANvmj5EI");
			Runtime.AddLicense("MEaBpLHLn3Xj7fQQ7azc6c/nrqXg5/YZ8p7cwp61n1mXpM0M66Xm+8+4iVmX" +
				"pLHLn1mXwPIP41nr/QEQvFu807/u56vm8fbNn6/c9gQU7qe0psLgrWmZpMDp" +
				"jEOXpLHLu2jY8P0a9neEjrHLn1mz8wMP5KvA8vcan53Y+PbooW+mtsPasWmo" +
				"ubPL9Z7p9/oa7XaZtcbNn2i1kZvLn1mXwAQU5qfY+AYd5Hfg8/LW5azp/sEi" +
				"6HrL6OXs7YKt8+nsvHazswQU5qfY+AYd5HeEjs3a66La6f8e5HeEjnXj7fQQ" +
				"7azcwp61n1mXpM0X6Jzc8gQQyJ21u8PjtmuouMTfsHWm8PoO5Kfq6doPvQ==");
		}
	}
}
