using System.Drawing;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;

namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface IBannerSettingsHolder
	{
		BannerSettings Banner { get; }
		Color BannerBackColor { get; }
	}
}
