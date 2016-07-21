using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Configuration;
using SalesLibraries.ServiceConnector.Models.Rest.Common;
using SalesLibraries.ServiceConnector.Models.Rest.Connection;
using SalesLibraries.ServiceConnector.Models.Rest.VersionsManagement;
using SalesLibraries.ServiceConnector.Models.Rest.Wallbin;
using SalesLibraries.ServiceConnector.Services.Rest;

namespace SalesLibraries.Business.Contexts.Wallbin.Cloud
{
	public class CloudWallbinManager
	{
		private string _localPath;

		public RestServiceConnection ServiceConnection { get; }
		public ConnectionInfo ConnectionInfo { get; private set; }
		public CloudLibraryContext LocalContext { get; private set; }

		public List<ChangeSet> PendingChanges { get; }

		public CloudWallbinManager(RestServiceConnection serviceConnection)
		{
			PendingChanges = new List<ChangeSet>();
			ServiceConnection = serviceConnection;
		}

		public void Init(ConnectionInfo connectionInfo, string localPath)
		{
			ConnectionInfo = connectionInfo;
			_localPath = localPath;
		}

		public void CheckoutData()
		{
			using (var syncContext = new CloudLibraryRemoteContext(ConnectionInfo.LibraryName, _localPath, this))
			{
				RestResponse response;
				try
				{
					response = ServiceConnection.DoRequest(new ChangesGetRequestData
					{
						LibraryId = ConnectionInfo.LibraryId,
						LastUpdate = syncContext.Library.SyncDate ?? DateTime.MinValue
					});
					if (response == null) throw new Exception();
				}
				catch (Exception ex)
				{
					throw new CloudLibraryException("Error loading library updates from server", ex);
				}
				if (response.Result == ResponseResult.Error)
				{
					var error = response.GetData<RestError>();
					throw new CloudLibraryException(error.Message);
				}
				try
				{
					var changeSets = response.GetData<List<ChangeSet>>();
					ApplyChanges(syncContext, changeSets);
				}
				catch (Exception ex)
				{
					throw new CloudLibraryException("Error loading library updates from server", ex);
				}

				CreateLocalContext();
			}
		}

		private void CreateLocalContext()
		{
			var syncContextFilePath = Path.Combine(_localPath, Constants.RemoteStorageFileName);
			var localContextFilePath = Path.Combine(_localPath, Constants.LocalStorageFileName);
			if (!File.Exists(syncContextFilePath))
				throw new CloudLibraryException("Sync context absent");
			try
			{
				File.Copy(syncContextFilePath, localContextFilePath, true);
			}
			catch (Exception ex)
			{
				throw new CloudLibraryException("Error creating local context", ex);
			}
			LocalContext = new CloudLibraryLocalContext(ConnectionInfo.LibraryName, _localPath, this);
			LocalContext.BeforeSave += OnLocalContextBeforeSave;
		}

		public void CheckinData()
		{
			if (!PendingChanges.Any()) return;

			LocalContext.Library.SyncDate = DateTime.Now;
			LocalContext.SaveChanges();

			RestResponse response;
			try
			{
				response = ServiceConnection.DoRequest(new ChangesSetRequestData
				{
					User = ConnectionInfo.User,
					LibraryId = ConnectionInfo.LibraryId,
					PendingChanges = PendingChanges.ToArray()
				});
				if (response == null) throw new Exception();
			}
			catch (Exception ex)
			{
				throw new CloudLibraryException("Error uploading changes on server", ex);
			}
			if (response.Result == ResponseResult.Error)
			{
				var error = response.GetData<RestError>();
				throw new CloudLibraryException(error.Message);
			}
			PendingChanges.Clear();

			var syncContextFilePath = Path.Combine(_localPath, Constants.RemoteStorageFileName);
			var localContextFilePath = Path.Combine(_localPath, Constants.LocalStorageFileName);
			if (!File.Exists(localContextFilePath))
				throw new CloudLibraryException("Local context absent");
			try
			{
				File.Copy(localContextFilePath, syncContextFilePath, true);
			}
			catch (Exception ex)
			{
				throw new CloudLibraryException("Error syncing changes", ex);
			}
		}

		#region Versions management
		private void OnLocalContextBeforeSave(object sender, EventArgs e)
		{
			GetPendingChanges((CloudLibraryContext)sender);
		}

		private void GetPendingChanges(CloudLibraryContext workingContext)
		{
			var chandedEntries = workingContext.ChangeTracker.Entries()
				.Where(entry => entry.State == EntityState.Modified ||
								entry.State == EntityState.Added ||
								entry.State == EntityState.Deleted)
				.ToList();
			foreach (var entry in chandedEntries)
			{
				var wallbinEntity = entry.Entity as WallbinEntity;
				if (wallbinEntity == null)
					throw new CloudLibraryException("Undefined entity type");
				var changeSet = PendingChanges
					.FirstOrDefault(existedChangeSet => existedChangeSet.ChangedObject.Id == wallbinEntity.ExtId);
				if (changeSet == null)
				{
					changeSet = new ChangeSet();
					switch (entry.State)
					{
						case EntityState.Added:
							changeSet.ChangeType = ChangeType.Add;
							break;
						case EntityState.Modified:
							changeSet.ChangeType = ChangeType.Update;
							break;
						case EntityState.Deleted:
							changeSet.ChangeType = ChangeType.Delete;
							break;
						default:
							throw new CloudLibraryException("Undefined entity change state");
					}
					PendingChanges.Add(changeSet);
				}
				else
				{
					if (entry.State == EntityState.Deleted)
					{
						if (changeSet.ChangeType == ChangeType.Add)
							PendingChanges.Remove(changeSet);
						else
							changeSet.ChangeType = ChangeType.Delete;
					}
				}
				changeSet.ChangedObject = wallbinEntity.ExportCloudData();
			}
		}

		private void ApplyChanges(CloudLibraryContext workingContext, IList<ChangeSet> changes)
		{
			foreach (var changeSet in changes.OrderBy(c => c.ChangedObject.ObjectType).ToList())
			{
				switch (changeSet.ChangedObject.ObjectType)
				{
					case ObjectType.Library:
						var sourceLibrary = (ServiceConnector.Models.Rest.Wallbin.Entities.Library)changeSet.ChangedObject;
						workingContext.Library.ImportCloudData(sourceLibrary);
						break;
					case ObjectType.Page:
						var sourceLibraryPage = (ServiceConnector.Models.Rest.Wallbin.Entities.LibraryPage)changeSet.ChangedObject;
						var localLibraryPage = workingContext.Library.Pages.FirstOrDefault(page => page.ExtId == sourceLibraryPage.Id);
						if (localLibraryPage == null)
						{
							localLibraryPage = WallbinEntity.CreateEntity<LibraryPage>();
							localLibraryPage.Library = workingContext.Library;
							workingContext.Library.Pages.Add(localLibraryPage);
						}
						localLibraryPage.ImportCloudData(sourceLibraryPage);
						break;
				}
			}
			workingContext.Library.SyncDate = DateTime.Now;
			workingContext.SaveChanges();
		}
		#endregion
	}
}
