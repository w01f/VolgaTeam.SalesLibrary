using System;
using DevComponents.DotNetBar.Metro;

namespace SalesLibraries.SiteManager.PresentationClasses.LinkConfigProfiles
{
	public partial class FormEditProfile : MetroForm
	{
		public string ProfileName
		{
			get { return textEditProfileName.EditValue as String; }
			set { textEditProfileName.EditValue = value; }
		}

		public FormEditProfile(bool isNewRecord)
		{
			InitializeComponent();
			Text = isNewRecord ? "New Profile" : "Edit Profile";
		}
	}
}
