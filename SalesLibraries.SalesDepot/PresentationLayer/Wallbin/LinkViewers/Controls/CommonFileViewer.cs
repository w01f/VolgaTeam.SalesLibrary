﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Business.LinkViewers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Controls
{
	[IntendForClass(typeof(CommonFileLink))]
	[ToolboxItem(false)]
	public partial class CommonFileViewer : UserControl, ILinkViewer
	{
		#region Properties
		public LibraryObjectLink Link { get; private set; }

		public string DisplayName => Link.Name;

		#endregion

		public CommonFileViewer(LibraryObjectLink link)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			Link = link;

			switch (Link.Type)
			{
				case LinkType.PowerPoint:
					laMessage.Text = "Double-Click PowerPoint files to preview";
					break;
				default:
					laMessage.Text = "Double-Click File to preview...";
					break;
			}
		}

		#region IFileViewer Methods
		public void ReleaseResources() { }

		public void Open()
		{
			switch (Link.Type)
			{
				case LinkType.Other:
					LinkManager.OpenCopyOfFile((LibraryFileLink)Link);
					break;
				case LinkType.Folder:
					LinkManager.OpenFolderLink((LibraryFolderLink)Link);
					break;
				case LinkType.Url:
				case LinkType.YouTube:
				case LinkType.Html5:
				case LinkType.QPageLink:
				case LinkType.Vimeo:
					Utils.OpenFile(((HyperLink)Link).Url);
					break;
				case LinkType.Network:
					Utils.OpenFile(Link.FullPath);
					break;
				case LinkType.AppLink:
					Utils.OpenFile(new[] { Link.FullPath, ((AppLink)Link).SecondPath });
					break;
				default:
					LinkManager.OpenLink(Link);
					break;
			}
		}

		public void Save()
		{
			var fileLink = Link as LibraryFileLink;
			if (fileLink == null) return;
			LinkManager.SaveLink("Save copy of the file as", fileLink);
		}

		public void Email()
		{
			var fileLink = Link as LibraryFileLink;
			if (fileLink == null) return;
			LinkManager.EmailLink(fileLink);
		}

		public void Print()
		{
			var fileLink = Link as LibraryFileLink;
			if (fileLink == null) return;
			LinkManager.PrintFile(fileLink);
		}
		#endregion

		private void laMessage_DoubleClick(object sender, EventArgs e)
		{
			LinkManager.OpenLink(Link);
		}
	}
}