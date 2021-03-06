﻿using System;
using DevComponents.DotNetBar.Metro;

namespace SalesLibraries.SiteManager.PresentationClasses.DataQueryCache
{
	public partial class FormEditProfile : MetroForm
	{
		public string ProfileName
		{
			get => textEditProfileName.EditValue as String;
			set => textEditProfileName.EditValue = value;
		}

		public FormEditProfile(bool isNewRecord)
		{
			InitializeComponent();
			Text = isNewRecord ? "New Profile" : "Edit Profile";
		}
	}
}
