using System.Drawing;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;

namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface IBannerSettingsHolder: IChangable
	{
		string Name { get; }
		string ObjectDisplayName { get; }
		BannerSettings Banner { get; set; }
		Color BannerBackColor { get; }
	}
}
