using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent
{
	public class AdditionalDataSource : WallbinCollectionEntity, IDataSource
	{
		#region Persistent Properties
		private string _path;
		[Required]
		public string Path
		{
			get { return _path; }
			set
			{
				if (_path != value)
					MarkAsModified();
				_path = value;
			}
		}

		private int _order;
		[Required]
		public int Order
		{
			get { return _order; }
			set
			{
				if (_order != value)
					MarkAsModified();
				_order = value;
			}
		}
		public virtual Library Library { get; set; }
		#endregion

		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override IChangable Parent
		{
			get { return Library; }
		}

		[NotMapped, JsonIgnore]
		public override int CollectionOrder
		{
			get { return Order; }
			set { Order = value; }
		}

		[NotMapped, JsonIgnore]
		public Guid DataSourceId
		{
			get { return ExtId; }
		}

		[NotMapped, JsonIgnore]
		public string Name
		{
			get
			{
				var name = System.IO.Path.GetFileName(Path);
				return !String.IsNullOrEmpty(name) ? name : System.IO.Path.GetPathRoot(Path);
			}
		}

		[JsonIgnore]
		private string SyncedPath
		{
			get { return System.IO.Path.Combine(Library.Path, Constants.ExtraFoldersRootFolderName, ExtId.ToString()); }
		}
		#endregion

		public override void BeforeSave() { }

		public override void ResetParent()
		{
			Library = null;
		}

		[JsonIgnore]
		private bool _filePathDefined;
		[JsonIgnore]
		private string _filePath;
		public string GetFilePath()
		{
			if (!_filePathDefined)
			{
				_filePath = Directory.Exists(Path) ? Path : SyncedPath;
				_filePathDefined = true;
			}
			return _filePath;
		}
	}
}
