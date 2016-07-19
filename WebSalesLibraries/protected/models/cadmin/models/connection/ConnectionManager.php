<?

	/**
	 * Class ConnectionManager
	 */
	class ConnectionManager
	{
		const ConnectionRequestTypeConnect = 1;
		const ConnectionRequestTypeDisconnect = 2;

		/**
		 * @param $requestData ConnectionGetRequestData
		 * @return RestResponse
		 */
		public static function connect($requestData)
		{

			switch ($requestData->requestType)
			{
				case self::ConnectionRequestTypeConnect:
					$connectionInfo = ConnectionInfoRecord::addConnection($requestData->userName, $requestData->libraryName);
					if (!isset($connectionInfo))
						return RestResponse::error(sprintf("Couldn't connect to library %s. Check if it is available on server", $requestData->libraryName));
					return RestResponse::success($connectionInfo);
				case self::ConnectionRequestTypeDisconnect:
					ConnectionInfoRecord::deleteConnection($requestData->userName, $requestData->libraryName);
					return RestResponse::success(null);
				default:
					return RestResponse::error(sprintf('Undefined connection request: %', $requestData->requestType));
			}
		}
	}