using System.Drawing;

namespace SalesLibraries.Business.Entities.Interfaces
{
	public interface IWidgetSetingsHolder
	{
		bool UseTextColorForWidget { get; set; }
		Color TextColor { get; }
	}
}
