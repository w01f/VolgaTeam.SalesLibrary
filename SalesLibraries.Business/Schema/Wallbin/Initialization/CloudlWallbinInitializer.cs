using System;
using SalesLibraries.Business.Contexts.Wallbin.Cloud;
using SalesLibraries.ServiceConnector.Models.Rest.Common;
using SalesLibraries.ServiceConnector.Models.Rest.Wallbin;
using SalesLibraries.ServiceConnector.Services.Rest;

namespace SalesLibraries.Business.Schema.Wallbin.Initialization
{
	class CloudlWallbinInitializer<TLibraryContext> : WallbinInitializer<TLibraryContext>
		where TLibraryContext : CloudLibraryContext
	{
		protected override void Seed()
		{
			RestResponse response;
			try
			{
				response = _context.WallbinManager.ServiceConnection.DoRequest(new LibraryGetRequestData
				{
					LibraryId = _context.WallbinManager.ConnectionInfo.LibraryId
				});
				if (response == null) throw new Exception();
			}
			catch (Exception ex)
			{
				throw new RestServiceException("Error loading library data", ex);
			}
			if (response.Result == ResponseResult.Error)
			{
				var error = response.GetData<RestError>();
				throw new RestServiceException(error.Message);
			}
			try
			{
				var libraryData = response.GetData<LibraryDataPackage>();
				_context.ImportCloudData(libraryData);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new RestServiceException("Error parsing library data", ex);
			}
		}
	}
}
