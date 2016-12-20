using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit.LinksGroup
{
	class PdfSettingsEditor:DocumentSettingsEditor
	{
		public PdfSettingsEditor(BarSubItem itemsContainer) : base(itemsContainer){}

		public override void LoadLinks(IEnumerable<BaseLibraryLink> targetLinks)
		{
			var linksList = targetLinks.OfType<PdfLink>().ToList();
			base.LoadLinks(linksList);
		}

		protected override ILinksLoader CreateLoader(IEnumerable<BaseLibraryLink> targetLinks)
		{
			return new DocumentSettingsLoader(this, targetLinks.OfType<PdfLink>());
		}
	}
}
