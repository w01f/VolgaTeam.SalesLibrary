<?
	use application\models\services_data\batch_tagger\BatchTaggerSetRequestData;
	use application\models\services_data\common\dictionaries\DictionariesManager;
	use application\models\services_data\common\dictionaries\LibraryLinksGetRequestData;
	use application\models\services_data\common\dictionaries\SearchCategoriesGetRequestData;
	use application\models\services_data\common\dictionaries\SecurityGetRequestData;
	use application\models\services_data\common\dictionaries\ShortcutLinksGetRequestData;
	use application\models\services_data\common\dictionaries\SuperFiltersGetRequestData;
	use application\models\services_data\common\meta_data\MetaDataGetRequestData;
	use application\models\services_data\common\meta_data\MetaDataManager;
	use application\models\services_data\common\rest\RestResponse;
	use application\models\wallbin\models\web\LibraryManager;

	/**
	 * Class FileManagerDataController
	 */
	class FileManagerDataController extends RestController
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
				case 'metadata':
					/** @var $requestData MetaDataGetRequestData */
					$response = MetaDataManager::getData($requestData);
					break;
				case 'security':
					/** @var $requestData SecurityGetRequestData */
					$response = DictionariesManager::getSecurity();
					break;
				case 'searchcategories':
					/** @var $requestData SearchCategoriesGetRequestData */
					$response = DictionariesManager::getCategories();
					break;
				case 'superfilters':
					/** @var $requestData SuperFiltersGetRequestData */
					$response = DictionariesManager::getSuperFilters();
					break;
				case 'librarylinks':
					/** @var $requestData LibraryLinksGetRequestData */
					$response = DictionariesManager::getLibraryLinks();
					break;
				case 'shortcutlinks':
					/** @var $requestData ShortcutLinksGetRequestData */
					$response = DictionariesManager::getShortcutLinks();
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
				case 'batchtagger':
					/** @var $requestData BatchTaggerSetRequestData */
					foreach ($requestData->linkInfo as $linkInfo)
					{
						$linkRecord = LinkRecord::getLinkById($linkInfo->linkId);
						$linkRecord->tags = $linkInfo->keywords;
						$linkRecord->save();

						LinkCategoryRecord::model()->deleteAll('id_link=?', array($linkRecord->id));
						foreach ($linkInfo->categories as $category)
							LinkCategoryRecord::updateData($category);
					}
					$response = RestResponse::success(null);
					break;
				default:
					$response = RestResponse::error(sprintf("Model '%s' is not recognized", $model));
					break;
			}
			$this->sendResponse(200, CJSON::encode($response));
		}
	}