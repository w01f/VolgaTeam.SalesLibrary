<?
	use application\models\services_data\cadmin\models\connection\ConnectionGetRequestData;
	use application\models\services_data\cadmin\models\connection\ConnectionManager;
	use application\models\services_data\cadmin\models\library_data\LibraryGetRequestData;
	use application\models\services_data\cadmin\models\versions_management\ChangesGetRequestData;
	use application\models\services_data\cadmin\models\versions_management\ChangesSetRequestData;
	use application\models\services_data\cadmin\models\versions_management\VersionsManager;
	use application\models\services_data\common\rest\RestResponse;

	/**
	 * Class CloudAdminController
	 */
	class CloudAdminController extends RestController
	{
		/** return boolean */
		protected function getIsPublicController()
		{
			return true;
		}

		public function actionGet()
		{
			$restParams = Yii::app()->request->getRestParams();
			$requestData = CJSON::decode($restParams['dataEncoded'], false);
			$model = $_GET['model'];
			switch ($model)
			{
				case 'connection':
					/** @var $requestData ConnectionGetRequestData */
					$response = ConnectionManager::connect($requestData);
					break;
				case 'librarydata':
					/** @var $requestData LibraryGetRequestData */
					$response = \application\models\services_data\cadmin\models\library_data\LibraryManager::getData($requestData);
					break;
				case 'changes':
					/** @var $requestData ChangesGetRequestData */
					$response = VersionsManager::getChanges($requestData);
					break;
				default:
					$response = RestResponse::error(sprintf("Model '%s' is not recognized", $model));
					break;

			}
			$this->sendResponse(200, CJSON::encode($response));
		}

		public function actionSet()
		{
			$restParams = Yii::app()->request->getRestParams();
			$requestData = CJSON::decode($restParams['dataEncoded'], false);
			$model = $_GET['model'];
			switch ($model)
			{
				case 'changes':
					/** @var $requestData ChangesSetRequestData */
					$response = VersionsManager::applyChanges($requestData);
					break;
				default:
					$response = RestResponse::error(sprintf("Model '%s' is not recognized", $model));
					break;

			}
			$this->sendResponse(200, CJSON::encode($response));
		}
	}