using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Schema.Wallbin.Initialization.Patches;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.Business.Schema.Wallbin.Initialization
{
	public abstract class WallbinInitializer<TLibraryContext> : IDatabaseInitializer<TLibraryContext>
		where TLibraryContext : LibraryContext
	{
		//TODO: Remove in next version OneDriveLinkSettings class and OneDriveEncoded field inside LibraryLink table
		private const int CurrentRevision = 11;

		protected TLibraryContext _context;
		protected PatchController _patchController = new PatchController();

		public void InitializeDatabase(TLibraryContext context)
		{
			_context = context;
			UpdateSchema();
		}

		private void UpdateSchema()
		{
			var schemaFolderPath = Path.Combine(GlobalSettings.ApplicationRootPath, "Schema", "Wallbin");

			var firstInitialization = false;
			if (!File.Exists(_context.DataSourceFilePath))
			{
				_context.Database.CreateIfNotExists();
				ApplySchemaVersion(Path.Combine(schemaFolderPath, "Base"));
				firstInitialization = true;
			}
			var newVersions = new List<int>();
			if (firstInitialization || !_context.Versions.Any() || _context.Versions.Max(v => v.Revision) < CurrentRevision)
			{
				const string schemaVersionPrefix = "Version";
				foreach (var versionPath in Directory.GetDirectories(schemaFolderPath, schemaVersionPrefix + "*").OrderBy(path => Convert.ToInt32(Path.GetFileName(path).Replace(schemaVersionPrefix, String.Empty))))
				{
					var releaseNumber = Convert.ToInt32(Path.GetFileName(versionPath).Replace(schemaVersionPrefix, String.Empty));
					if (!firstInitialization && _context.Versions.Any(r => r.Revision == releaseNumber)) continue;
					if (releaseNumber > CurrentRevision) continue;
					ApplySchemaVersion(versionPath, releaseNumber);
					newVersions.Add(releaseNumber);
				}
			}
			if (firstInitialization)
				Seed();
			if (newVersions.Any())
			{
				foreach (var version in newVersions)
					_context.Versions.Add(new DBVersion { Revision = version });
				_context.SaveChanges();
			}
		}

		private void ApplySchemaVersion(string storagePath, int? releaseNumber = null)
		{
			if (File.Exists(Path.Combine(storagePath, "sequense.txt")))
			{
				foreach (var scriptFileName in File.ReadAllLines(Path.Combine(storagePath, "sequense.txt")))
				{
					if (String.IsNullOrEmpty(scriptFileName)) continue;
					var scriptPath = Path.Combine(storagePath, scriptFileName);
					if (!File.Exists(scriptPath)) continue;
					foreach (var scriptPart in File.ReadAllText(scriptPath).Split(';'))
					{
						if (String.IsNullOrEmpty(scriptPart)) continue;
						_context.Database.ExecuteSqlCommand(scriptPart);
					}
				}
			}
			else
			{
				var beforePatchScriptFilePath = Path.Combine(storagePath, "BeforePatch", "sequense.txt");
				if (File.Exists(beforePatchScriptFilePath))
				{
					foreach (var scriptFile in File.ReadAllLines(beforePatchScriptFilePath))
					{
						if (String.IsNullOrEmpty(scriptFile)) continue;
						var scriptPath = Path.Combine(storagePath, "BeforePatch", scriptFile);
						if (!File.Exists(scriptPath)) continue;
						foreach (var scriptPart in File.ReadAllText(scriptPath).Split(';'))
						{
							if (String.IsNullOrEmpty(scriptPart)) continue;
							_context.Database.ExecuteSqlCommand(scriptPart);
						}
					}
				}

				if (releaseNumber.HasValue)
				{
					var actualPatches = _patchController.Patches.Where(p => p.Version == releaseNumber.Value).ToList();
					actualPatches.ForEach(p => p.Apply(_context));
					_context.SaveChanges();
				}

				var afterPatchScriptFilePath = Path.Combine(storagePath, "AfterPatch", "sequense.txt");
				if (File.Exists(afterPatchScriptFilePath))
				{
					foreach (var scriptFile in File.ReadAllLines(afterPatchScriptFilePath))
					{
						if (String.IsNullOrEmpty(scriptFile)) continue;
						var scriptPath = Path.Combine(storagePath, "AfterPatch", scriptFile);
						if (!File.Exists(scriptPath)) continue;
						foreach (var scriptPart in File.ReadAllText(scriptPath).Split(';'))
						{
							if (String.IsNullOrEmpty(scriptPart)) continue;
							_context.Database.ExecuteSqlCommand(scriptPart);
						}
					}
				}
			}
		}

		protected abstract void Seed();
	}
}
