﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using OutlookSalesDepotAddIn.Controls.Wallbin;
using OutlookSalesDepotAddIn.Forms;
using SalesDepot.CoreObjects.BusinessClasses;

namespace OutlookSalesDepotAddIn.BusinessClasses
{
	public class PageDecorator
	{
		private readonly List<FolderBoxControl> _boxes = new List<FolderBoxControl>();
		private Panel _headerPanel;
		private Image _logo;
		private Panel _parentPanel;

		public PageDecorator(LibraryDecorator parent, LibraryPage page)
		{
			Page = page;
			Parent = parent;
			Container = new Panel();
			Container.Resize += WallBin_Resize;
			ScrollBox = new XtraScrollableControl();
			Container.Controls.Add(ScrollBox);
			TabPage = new XtraTabPage();
			TabPage.Tag = this;
			TabPage.Text = page.Name.Replace("&", "&&");
		}

		public LibraryPage Page { get; private set; }
		public Panel Container { get; private set; }
		public XtraScrollableControl ScrollBox { get; private set; }
		public XtraTabPage TabPage { get; private set; }
		public LibraryDecorator Parent { get; private set; }
		public FolderBoxControl ActiveBox { get; set; }

		public bool ReadyToShow { get; private set; }

		private void GetPageLogo()
		{
			var dir = new DirectoryInfo(Path.Combine(SettingsManager.Instance.LibraryLogoFolder, Parent.Parent.Package.Folder.Name));
			if (dir.Exists)
			{
				FileInfo[] files = dir.GetFiles(string.Format(SettingsManager.PageLogoFileTemplate, (Page.Order + 1).ToString()));
				if (files.Length > 0)
					_logo = new Bitmap(files[0].FullName);
			}
			if (_logo == null)
				_logo = Parent.Parent.Logo;
		}

		private void BuildDisplayBoxes()
		{
			foreach (var box in _boxes)
				box.Dispose();
			_boxes.Clear();

			Page.Folders.Sort((x, y) => x.ColumnOrder.CompareTo(y.ColumnOrder) == 0 ? x.RowOrder.CompareTo(y.RowOrder) : x.ColumnOrder.CompareTo(y.ColumnOrder));
			foreach (var box in Page.Folders.Select(folder => new FolderBoxControl { Folder = folder, Decorator = this }))
				_boxes.Add(box);
		}

		private void BuildPage()
		{
			Container.Dock = DockStyle.Fill;
			Container.BorderStyle = BorderStyle.None;
			ScrollBox.Dock = DockStyle.Fill;
			ScrollBox.AlwaysScrollActiveControlIntoView = false;
			BuildColumnTitles();
			BuildColumns();
		}

		private void BuildColumnTitles()
		{
			if (!Page.EnableColumnTitles) return;
			_headerPanel = new Panel();
			_headerPanel.BorderStyle = BorderStyle.None;
			_headerPanel.Height = 0;
			_headerPanel.Dock = DockStyle.Top;
			Container.Controls.Add(_headerPanel);
			_headerPanel.SendToBack();

			foreach (var columnTitle in Page.ColumnTitles)
			{
				var columnTitleControl = new ColumnTitleControl(columnTitle);
				columnTitleControl.Dock = columnTitle.ColumnOrder == 2 ? DockStyle.Fill : DockStyle.Left;
				_headerPanel.Controls.Add(columnTitleControl);
				columnTitleControl.BringToFront();
			}
		}

		private void BuildColumns()
		{
			_parentPanel = new Panel();
			_parentPanel.Height = ScrollBox.Height;
			_parentPanel.Dock = DockStyle.Top;
			ScrollBox.Controls.Add(_parentPanel);
			_parentPanel.BringToFront();

			var panel = new ColumnPanel();
			panel.AllowDrop = true;
			panel.Dock = DockStyle.Left;
			panel.Order = 0;
			_parentPanel.Controls.Add(panel);
			panel.BringToFront();

			panel = new ColumnPanel();
			panel.AllowDrop = true;
			panel.Dock = DockStyle.Left;
			panel.Order = 1;
			_parentPanel.Controls.Add(panel);
			panel.BringToFront();

			panel = new ColumnPanel();
			panel.AllowDrop = true;
			panel.Dock = DockStyle.Fill;
			panel.Order = 2;
			_parentPanel.Controls.Add(panel);
			panel.BringToFront();
		}

		private void LinkBoxesToColumns()
		{
			_boxes.Sort((x, y) => x.Column.CompareTo(y.Column) == 0 ? x.RowOrder.CompareTo(y.RowOrder) : x.Column.CompareTo(y.Column));
			foreach (Control control in _parentPanel.Controls)
				control.Controls.Clear();
			foreach (var box in _boxes)
				_parentPanel.Controls[2 - box.Column].Controls.Add(box);
		}

		private void UpdateHeaderSize()
		{
			if (_headerPanel == null) return;
			_headerPanel.Visible = true;
			var panelWidth = _headerPanel.Width / 3;
			var panelHeight = 0;
			foreach (Control panel in _headerPanel.Controls)
			{
				panel.Width = panelWidth;
				var controlHeight = (panel as ColumnTitleControl).GetHeight();
				if (panelHeight < controlHeight)
					panelHeight = controlHeight;
			}
			_headerPanel.Height = panelHeight;
		}

		private void UpdateContentSize()
		{
			foreach (Control control in _parentPanel.Controls)
			{
				var panel = control as ColumnPanel;
				panel.Visible = true;
			}

			foreach (Control panel in _parentPanel.Controls)
			{
				((ColumnPanel)panel).ResizePanel();
				if (((ColumnPanel)panel).Order < 2)
				{
					panel.Dock = DockStyle.Left;
				}
				else
				{
					panel.Dock = DockStyle.Fill;
					panel.BringToFront();
				}
			}

			var realHeight = 0;
			for (var i = 0; i < 3; i++)
			{
				var columnHeight = _boxes.Where(box => box.Column == i).Sum(box => box.Height);
				if (realHeight < columnHeight)
					realHeight = columnHeight;
			}
			_parentPanel.Height = realHeight;
		}

		private void ApplyPageLogo()
		{
			FormMain.Instance.labelItemHomePackageLogo.Image = _logo;
			FormMain.Instance.ribbonBarHomeStations.RecalcLayout();
			FormMain.Instance.ribbonPanelHome.PerformLayout();
		}

		private void WallBin_Resize(object sender, EventArgs e)
		{
			Parent.EmptyPanel.BringToFront();
			UpdatePage();
			Parent.EmptyPanel.SendToBack();
		}

		public void UpdatePage(bool forse = false)
		{
			if (!ReadyToShow) return;
			if (forse)
			{
				LinkBoxesToColumns();

				foreach (var box in _boxes)
					box.UpdateView();
			}
			UpdateHeaderSize();

			UpdateContentSize();
		}

		public void Apply(bool silent = false)
		{
			if (!ReadyToShow)
			{
				GetPageLogo();
				BuildPage();
				BuildDisplayBoxes();
				ReadyToShow = true;
			}
			UpdatePage(true);
			if (silent) return;
			ApplyPageLogo();
		}

		public IEnumerable<LibraryLink> GetSelectedLinks()
		{
			return _boxes.SelectMany(b => b.GetSelectedLinks());
		}

		public void ClearSelectedLinks()
		{
			_boxes.ForEach(b => b.ClearSelectedLinks());
		}
	}
}