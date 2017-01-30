﻿using System;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Folders.Controls
{
	public partial class ClassicFolderBox
	{
		private void toolStripMenuItemSelectAll_Click(object sender, EventArgs e)
		{
			SelectAll();
		}

		private void toolStripMenuItemResetAll_Click(object sender, EventArgs e)
		{
			ResetSecurity();
		}
	}
}
