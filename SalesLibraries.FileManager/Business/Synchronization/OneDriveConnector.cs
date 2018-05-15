﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.Helpers;
using Newtonsoft.Json.Linq;
using RestSharp;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.FileManager.Controllers;
using File = System.IO.File;

namespace SalesLibraries.FileManager.Business.Synchronization
{
	class OneDriveConnector
	{
		private const string LogFileName = "OneDrive.txt";
		private static int fileChunckSize = 3 * 320 * 1024;

		private readonly StringBuilder _log = new StringBuilder();

		private OneDriveClient _client;
		private string _rootFolderId;
		private readonly Dictionary<string, string> _oneDriveFolders = new Dictionary<String, String>();

		public async Task ProcessLinks(IList<LibraryFileLink> links, CancellationToken cancellationToken)
		{
			InitLog();

			_log.AppendLine("Sync started");

			try
			{
				await Connect();

				await InitRootFolder();

				foreach (var fileLink in links)
				{
					if (fileLink.IsFolder) continue;
					if (cancellationToken.IsCancellationRequested)
					{
						_log.AppendLine("Sync interropted by the user");
						break;
					}
					try
					{
						await UpdateLink(fileLink);
					}
					catch (Exception e)
					{
						_log.AppendLine(e.Message);
					}
				}
			}
			catch (Exception e)
			{
				_log.AppendLine(e.Message);
			}
			finally
			{
				_log.AppendLine("Sync completed");
			}
		}

		public string SaveLog(string path)
		{
			var filePath = Path.Combine(path, LogFileName);
			File.WriteAllText(filePath, _log.ToString());
			return filePath;
		}

		private void InitLog()
		{
			_log.Clear();
		}

		private async Task Connect()
		{
			_client = new OneDriveClient("https://graph.microsoft.com/v1.0", new AuthenticationProvider());
			try
			{
				await _client.Drive.Request().GetAsync();
			}
			catch (Exception e)
			{
				_log.AppendLine("Authentication failed");
				_log.AppendLine(e.Message);
				throw;
			}
		}

		private async Task InitRootFolder()
		{
			try
			{
				var rootItem = await _client
					.Drive
					.Root
					.ItemWithPath(MainController.Instance.Settings.OneDriveSettings.RootPath)
					.Request()
					.GetAsync();
				_rootFolderId = rootItem.Id;
			}
			catch (ServiceException initialException)
			{
				if (initialException.Error?.Code == "itemNotFound")
				{
					var pathParts = MainController.Instance.Settings.OneDriveSettings.RootPath.Split('/');
					var currentParentFolderId = String.Empty;

					foreach (var pathPart in pathParts)
					{
						try
						{
							var partItem = String.IsNullOrEmpty(currentParentFolderId)
								? await _client
									.Drive
									.Root
									.ItemWithPath(pathPart)
									.Request()
									.GetAsync()
								: await _client
									.Drive
									.Items[currentParentFolderId]
									.ItemWithPath(pathPart)
									.Request()
									.GetAsync();
							currentParentFolderId = partItem.Id;
						}
						catch (ServiceException partItemException)
						{
							if (partItemException.Error?.Code == "itemNotFound")
							{
								var folderToCreate = new Item { Folder = new Folder(), Name = pathPart };
								var partItem = String.IsNullOrEmpty(currentParentFolderId)
									? await _client
										.Drive
										.Root
										.Children
										.Request()
										.AddAsync(folderToCreate)
									: await _client
										.Drive
										.Items[currentParentFolderId]
										.Children
										.Request()
										.AddAsync(folderToCreate);
								currentParentFolderId = partItem.Id;
							}
							else
								throw;
						}
					}
					_rootFolderId = currentParentFolderId;
				}
				else
					throw;
			}
		}

		private async Task UpdateLink(LibraryFileLink link)
		{
			var itemPathParts = link.RelativePath.Split('\\').ToList();
			var fileInfo = new FileInfo(link.FullPath);

			if (link.OneDriveSettings.UrlGeneratingDate.HasValue && link.OneDriveSettings.UrlGeneratingDate.Value >= fileInfo.LastWriteTime)
				return;

			Item oneDriveLinkItem;
			if (link.OneDriveSettings.Enable)
			{
				try
				{
					oneDriveLinkItem = await _client
						.Drive
						.Items[link.OneDriveSettings.ItemId]
						.Request()
						.GetAsync();
				}
				catch (ServiceException e)
				{
					if (e.Error?.Code == "itemNotFound")
					{
						link.OneDriveSettings.Reset();
						oneDriveLinkItem = null;
					}
					else
						throw;
				}
			}
			else
			{
				try
				{
					oneDriveLinkItem = await _client
						.Drive
						.Items[_rootFolderId]
						.ItemWithPath(String.Join("/", itemPathParts))
						.Request()
						.GetAsync();

					link.OneDriveSettings.ItemId = oneDriveLinkItem.Id;
				}
				catch (ServiceException e)
				{
					if (e.Error?.Code == "itemNotFound")
						oneDriveLinkItem = null;
					else
						throw;
				}
			}

			if (oneDriveLinkItem == null)
			{
				var linkFolderParts = itemPathParts.Take(itemPathParts.Count - 1).ToList();
				var fileName = itemPathParts.Last();
				var parentFolderId = _rootFolderId;

				if (linkFolderParts.Any())
				{
					var linkFolderKey = String.Join("/", itemPathParts);
					if (_oneDriveFolders.ContainsKey(linkFolderKey))
						parentFolderId = _oneDriveFolders[linkFolderKey];
					else
					{
						var passedParts = new List<string>();
						foreach (var folderPart in linkFolderParts)
						{
							passedParts.Add(folderPart);
							var currentFolderKey = String.Join("/", passedParts);
							if (_oneDriveFolders.ContainsKey(currentFolderKey))
								parentFolderId = _oneDriveFolders[currentFolderKey];
							else
							{
								try
								{
									var folderItem = await _client
										.Drive
										.Items[parentFolderId]
										.ItemWithPath(folderPart)
										.Request()
										.GetAsync();
									parentFolderId = folderItem.Id;
									_oneDriveFolders.Add(currentFolderKey, parentFolderId);
								}
								catch (ServiceException e)
								{
									if (e.Error?.Code == "itemNotFound")
									{
										var folderToCreate = new Item { Folder = new Folder(), Name = folderPart };
										var partItem = await _client
											.Drive
											.Items[parentFolderId]
											.Children
											.Request()
											.AddAsync(folderToCreate);
										parentFolderId = partItem.Id;
										_oneDriveFolders.Add(currentFolderKey, parentFolderId);
									}
									else
										throw;
								}
							}
						}
					}
				}

				using (var contentStream = new FileStream(link.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					if (fileInfo.Length < fileChunckSize)
					{
						oneDriveLinkItem = await _client
							.Drive
							.Items[parentFolderId]
							.ItemWithPath(fileName)
							.Content
							.Request()
							.PutAsync<Item>(contentStream);
					}
					else
					{
						var additionalData = new Dictionary<string, object>
						{
							{"name", fileName},
							{"@microsoft.graph.conflictBehavior", "rename"}
						};

						var restClient = new RestClient("https://graph.microsoft.com");
						var restRequest = new RestRequest(String.Format("/v1.0/me/drive/items/{0}:/{1}:/createUploadSession", parentFolderId, fileName), Method.POST);
						restRequest.AddHeader("Accept", "*/*");
						restRequest.AddHeader("Content-Type", "application/json");
						restRequest.AddHeader("Authorization", String.Format("bearer {0}", ((AuthenticationProvider)_client.AuthenticationProvider).AccessToken));
						restRequest.AddJsonBody(additionalData);
						var restResponse = restClient.Execute(restRequest);
						dynamic restResponseData = JObject.Parse(restResponse.Content);

						if (!String.IsNullOrEmpty(restResponseData?.error_description?.ToString()))
							throw new Exception(String.Format("File uploading failed: {0}", restResponseData?.error_description?.ToString()));

						if (!String.IsNullOrEmpty(restResponseData?.error?.message?.ToString()))
							throw new Exception(String.Format("File uploading failed: {0}", restResponseData?.error?.message?.ToString()));

						var uploadSession = new UploadSession
						{
							AdditionalData = additionalData,
							UploadUrl = restResponseData?.uploadUrl?.ToString(),
							ExpirationDateTime = DateTime.Parse(restResponseData?.expirationDateTime?.ToString()),
							NextExpectedRanges = restResponseData?.nextExpectedRanges?.ToObject<string[]>() ?? new string[] { }
						};

						var provider = new ChunkedUploadProvider(uploadSession, _client, contentStream, fileChunckSize);

						var chunkRequests = provider.GetUploadChunkRequests();
						var readBuffer = new byte[fileChunckSize];
						var trackedExceptions = new List<Exception>();

						foreach (var request in chunkRequests)
						{
							var result = await provider.GetChunkRequestResponseAsync(request, readBuffer, trackedExceptions);
							if (result.UploadSucceeded)
								oneDriveLinkItem = result.ItemResponse;
						}

						if (oneDriveLinkItem == null)
							throw new Exception(String.Format("File uploading failed: {0}", link.FullPath));
					}
				}

				link.OneDriveSettings.ItemId = oneDriveLinkItem.Id;
				_log.AppendLine(String.Format("File uploaded: Path - {0}", link.FullPath));
			}
			else if (!oneDriveLinkItem.FileSystemInfo.LastModifiedDateTime.HasValue || oneDriveLinkItem.FileSystemInfo.LastModifiedDateTime.Value.UtcDateTime < fileInfo.LastWriteTimeUtc)
			{
				if (!String.IsNullOrEmpty(link.OneDriveSettings.UrlId))
				{
					await _client
						.Drive
						.Items[oneDriveLinkItem.Id]
						.Permissions[link.OneDriveSettings.UrlId]
						.Request()
						.DeleteAsync();

					link.OneDriveSettings.UrlId = null;
					link.OneDriveSettings.Url = null;
				}

				using (var contentStream = new FileStream(link.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					if (fileInfo.Length < fileChunckSize)
					{
						oneDriveLinkItem = await _client
							.Drive
							.Items[oneDriveLinkItem.Id]
							.Content
							.Request()
							.PutAsync<Item>(contentStream);
					}
					else
					{
						var additionalData = new Dictionary<string, object>
						{
							{"name", fileInfo.Name},
							{"@microsoft.graph.conflictBehavior", "rename"}
						};

						var restClient = new RestClient("https://graph.microsoft.com");
						var restRequest = new RestRequest(String.Format("/v1.0/me/drive/items/{0}/createUploadSession", oneDriveLinkItem.Id), Method.POST);
						restRequest.AddHeader("Accept", "*/*");
						restRequest.AddHeader("Content-Type", "application/json");
						restRequest.AddHeader("Authorization", String.Format("bearer {0}", ((AuthenticationProvider)_client.AuthenticationProvider).AccessToken));
						restRequest.AddJsonBody(additionalData);
						var restResponse = restClient.Execute(restRequest);
						dynamic restResponseData = JObject.Parse(restResponse.Content);

						if (!String.IsNullOrEmpty(restResponseData?.error_description?.ToString()))
							throw new Exception(String.Format("File uploading failed: {0}", restResponseData?.error_description?.ToString()));

						if (!String.IsNullOrEmpty(restResponseData?.error?.message?.ToString()))
							throw new Exception(String.Format("File uploading failed: {0}", restResponseData?.error?.message?.ToString()));

						var uploadSession = new UploadSession
						{
							AdditionalData = additionalData,
							UploadUrl = restResponseData?.uploadUrl?.ToString(),
							ExpirationDateTime = DateTime.Parse(restResponseData?.expirationDateTime?.ToString()),
							NextExpectedRanges = restResponseData?.nextExpectedRanges?.ToObject<string[]>() ?? new string[] { }
						};

						var provider = new ChunkedUploadProvider(uploadSession, _client, contentStream, fileChunckSize);

						var chunkRequests = provider.GetUploadChunkRequests();
						var readBuffer = new byte[fileChunckSize];
						var trackedExceptions = new List<Exception>();

						foreach (var request in chunkRequests)
						{
							var result = await provider.GetChunkRequestResponseAsync(request, readBuffer, trackedExceptions);
							if (result.UploadSucceeded)
								oneDriveLinkItem = result.ItemResponse;
						}

						if (oneDriveLinkItem == null)
							throw new Exception(String.Format("File uploading failed: {0}", link.FullPath));
					}
				}
				_log.AppendLine(String.Format("File updated: Path - {0}", link.FullPath));
			}

			if (String.IsNullOrEmpty(link.OneDriveSettings.Url))
			{
				var restClient = new RestClient("https://graph.microsoft.com");
				var restRequest = new RestRequest(String.Format("/v1.0/me/drive/items/{0}/createLink", oneDriveLinkItem.Id), Method.POST);
				restRequest.AddHeader("Accept", "*/*");
				restRequest.AddHeader("Content-Type", "application/json");
				restRequest.AddHeader("Authorization", String.Format("bearer {0}", ((AuthenticationProvider)_client.AuthenticationProvider).AccessToken));
				restRequest.AddJsonBody(new
				{
					type = "view",
					scope = "anonymous"
				});
				var restResponse = restClient.Execute(restRequest);
				dynamic restResponseData = JObject.Parse(restResponse.Content);

				if (!String.IsNullOrEmpty(restResponseData?.error_description?.ToString()))
					throw new Exception(String.Format("Shared link assigning failed: {0}", restResponseData?.error_description?.ToString()));

				if (!String.IsNullOrEmpty(restResponseData?.error?.message?.ToString()))
					throw new Exception(String.Format("Shared link assigning failed: {0}", restResponseData?.error?.message?.ToString()));

				link.OneDriveSettings.UrlId = restResponseData?.id;
				link.OneDriveSettings.Url = restResponseData?.link?.webUrl;
				link.OneDriveSettings.UrlGeneratingDate = DateTime.Now;

				_log.AppendLine(String.Format("Shared link assigned: {0}", link.OneDriveSettings.Url));
			}
		}

		internal class AuthenticationProvider : IAuthenticationProvider
		{
			public string AccessToken { get; private set; }

			public Task AuthenticateRequestAsync(HttpRequestMessage request)
			{
				if (String.IsNullOrEmpty(AccessToken))
				{
					var client = new RestClient("https://login.microsoftonline.com");

					var authRequest = new RestRequest("/common/oauth2/v2.0/token", Method.POST);

					authRequest.AddHeader("Accept", "*");
					authRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");

					authRequest.AddParameter("client_id", MainController.Instance.Settings.OneDriveSettings.AppId);
					authRequest.AddParameter("client_secret", MainController.Instance.Settings.OneDriveSettings.AppKey);
					authRequest.AddParameter("grant_type", "refresh_token");
					authRequest.AddParameter("refresh_token", MainController.Instance.Settings.OneDriveSettings.Token);

					var response = client.Execute(authRequest);

					dynamic responseData = JObject.Parse(response.Content);

					if (!String.IsNullOrEmpty(responseData?.error_description?.ToString()))
						throw new Exception(String.Format("Authentication failed: {0}", responseData?.error_description?.ToString()));

					AccessToken = responseData?.access_token;
				}

				request.Headers.Authorization = new AuthenticationHeaderValue("bearer", AccessToken);

				return Task.FromResult(0);
			}
		}
	}
}
