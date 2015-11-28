using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.Business.Schema.Wallbin
{
	class WallbinInitializer : IDatabaseInitializer<LibraryContext>
	{
		private const int CurrentRevision = 1;
		private LibraryContext _context;

		public void InitializeDatabase(LibraryContext context)
		{
			_context = context;
			UpdateSchema();
		}

		private void UpdateSchema()
		{
			var schemaFolderPath = Path.Combine(GlobalSettings.ApplicationRootPath, "Schema", "Wallbin");
			if (!File.Exists(Path.Combine(_context.DataSourcePath, Constants.StorageFileName)))
			{
				_context.Database.CreateIfNotExists();
				ApplySchemaVersion(Path.Combine(schemaFolderPath, "Base"));
				Seed();
			}
			if (!_context.Versions.Any() || _context.Versions.Max(v => v.Revision) < CurrentRevision)
			{
				const string schemaVersionPrefix = "Version";
				foreach (var versionPath in Directory.GetDirectories(schemaFolderPath, schemaVersionPrefix + "*").OrderBy(path => Convert.ToInt32(Path.GetFileName(path).Replace(schemaVersionPrefix, String.Empty))))
				{
					var releaseNumber = Convert.ToInt32(Path.GetFileName(versionPath).Replace(schemaVersionPrefix, String.Empty));
					if (_context.Versions.Any(r => r.Revision == releaseNumber)) continue;
					ApplySchemaVersion(versionPath);
					_context.Versions.Add(new DBVersion { Revision = releaseNumber });
					_context.SaveChanges();
				}
			}
		}

		private void ApplySchemaVersion(string storagePath)
		{
			const string sequenceFileName = "sequense.txt";
			var sequenceFilePath = Path.Combine(storagePath, sequenceFileName);
			if (!File.Exists(sequenceFilePath)) return;
			foreach (var scriptFile in File.ReadAllLines(sequenceFilePath))
			{
				if (String.IsNullOrEmpty(scriptFile)) continue;
				var scriptPath = Path.Combine(storagePath, scriptFile);
				if (!File.Exists(scriptPath)) continue;
				foreach (var scriptPart in File.ReadAllText(scriptPath).Split(';'))
				{
					if (String.IsNullOrEmpty(scriptPart)) continue;
					_context.Database.ExecuteSqlCommand(scriptPart);
				}
			}
		}

		private void Seed()
		{
			var library = new Library
			{
				Context = _context,
				Name = _context.LibraryName,
				Path = _context.DataSourcePath
			};
			library.ImportLegacyData(_context.DataSourcePath);
			library.BeforeSave();
			if (!library.Pages.Any())
				library.AddPage();
			_context.Libraries.Add(library);
			_context.SaveChanges();
		}
	}
}
