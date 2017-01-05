<?
	namespace application\models\services_data\cadmin\models\library_data;
	use application\models\services_data\common\rest\RestResponse;

	/**
	 * Class LibraryManager
	 */
	class LibraryManager
	{
		/**
		 * @param $requestData LibraryGetRequestData
		 * @return RestResponse
		 */
		public static function getData($requestData)
		{
			$libraryData = LibraryDataPackage::get($requestData->libraryId);
			if (isset($libraryData))
				return RestResponse::success($libraryData);
			return RestResponse::error('Library not found');
		}
	}