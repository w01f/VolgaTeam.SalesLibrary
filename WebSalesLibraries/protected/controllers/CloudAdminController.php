<?

	/**
	 * Class CloudAdminController
	 */
	class CloudAdminController extends IsdController
	{
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
					$response = \application\models\cadmin\models\library_data\LibraryManager::getData($requestData);
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

		private function sendResponse($status = 200, $body = '', $content_type = 'application/json; charset=UTF-8')
		{
			// set the status
			$status_header = 'HTTP/1.1 ' . $status . ' ' . $this->getStatusCodeMessage($status);
			header($status_header);
			// and the content type
			header('Content-type: ' . $content_type);
			echo $body;
			Yii::app()->end();
		}

		private function getStatusCodeMessage($status)
		{
			// these could be stored in a .ini file and loaded
			// via parse_ini_file()... however, this will suffice
			// for an example
			$codes = Array(
				200 => 'OK',
				400 => 'Bad Request',
				401 => 'Unauthorized',
				402 => 'Payment Required',
				403 => 'Forbidden',
				404 => 'Not Found',
				500 => 'Internal Server Error',
				501 => 'Not Implemented',
			);
			return (isset($codes[$status])) ? $codes[$status] : '';
		}
	}