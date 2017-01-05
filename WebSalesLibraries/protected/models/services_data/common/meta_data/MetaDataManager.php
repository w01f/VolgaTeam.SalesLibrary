<?
	namespace application\models\services_data\common\meta_data;

	use application\models\services_data\common\rest\RestResponse;

	/**
	 * Class MetaDataManager
	 */
	class MetaDataManager
	{
		/**
		 * @param $requestData MetaDataGetRequestData
		 * @return RestResponse
		 */
		public static function getData($requestData)
		{
			return RestResponse::success(
				\MetaDataRecord::getData(
					$requestData->dataTag,
					$requestData->propertyName)
			);
		}
	}