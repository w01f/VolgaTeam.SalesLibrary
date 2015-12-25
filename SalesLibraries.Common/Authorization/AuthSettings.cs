using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.Common.Authorization
{
	[Serializable]
	public class AuthSettings
	{
		private const string EncryptionKey = "MAKV2SPBNI99212";
		[NonSerialized]
		private readonly byte[] _salt = { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
		private StorageFile _settingsFile;

		public string Password { get; set; }
		public string Login { get; set; }

		public bool HasCredentials
		{
			get { return !String.IsNullOrEmpty(Login) && !String.IsNullOrEmpty(Password); }
		}

		public void Init(StorageFile settingsFile)
		{
			_settingsFile = settingsFile;
		}

		public static AuthSettings Load(StorageFile settingsFile)
		{
			var settings = SettingsSerializeHelper.Load<AuthSettings>(settingsFile);
			settings.Init(settingsFile);
			return settings;
		}

		public void Save()
		{
			this.Save(_settingsFile);
		}

		public string GetPassword()
		{
			var clearText = String.Empty;
			var cipherBytes = Convert.FromBase64String(Password);
			using (var encryptor = Aes.Create())
			{
				var pdb = new Rfc2898DeriveBytes(EncryptionKey, _salt);
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);
				using (var ms = new MemoryStream())
				{
					using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cs.Write(cipherBytes, 0, cipherBytes.Length);
						cs.Close();
					}
					clearText = Encoding.Unicode.GetString(ms.ToArray());
				}
			}
			return clearText;
		}

		public void SetPassword(string password)
		{
			var clearBytes = Encoding.Unicode.GetBytes(password);
			using (var encryptor = Aes.Create())
			{
				var pdb = new Rfc2898DeriveBytes(EncryptionKey, _salt);
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);
				using (var ms = new MemoryStream())
				{
					using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(clearBytes, 0, clearBytes.Length);
						cs.Close();
					}
					Password = Convert.ToBase64String(ms.ToArray());
				}
			}
		}
	}
}
