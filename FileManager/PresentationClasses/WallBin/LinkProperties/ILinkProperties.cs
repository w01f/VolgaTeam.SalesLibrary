using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	public interface ILinkProperties
	{
		void SaveData();

		event EventHandler OnForseClose;
	}
}
