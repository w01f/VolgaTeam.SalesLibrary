<?

	/**
	 * Class SalesRequestsController
	 */
	class SalesRequestsController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'sales_requests');
		}

		public function actionGetItemList()
		{
			$selectedItemId = Yii::app()->request->getPost('selectedItemId');
			$shortcutId = Yii::app()->request->getPost('shortcutId');
			$listType = Yii::app()->request->getPost('listType');
			$statusFilter = Yii::app()->request->getPost('statusFilter');

			/* @var $items \application\models\sales_requests\models\ItemListModel[] */
			$items = SalesRequestItemRecord::model()->getListItems($listType, $shortcutId, $statusFilter);
			$tableItemsDataset = array();
			foreach ($items as $item)
			{
				/** @var $itemOwner UserRecord */
				$itemOwner = UserRecord::model()->findByPk($item->idOwner);

				$tableItemsDataset[] = array(
					'id' => $item->id,
					'title' => $item->title,
					'itemOwner' => $listType == SalesRequestItemRecord::ListTypeOwn ?
						'' :
						$itemOwner->login,
					'dateNeeded' => array(
						'display' => date(\Yii::app()->params['outputDateFormat'], strtotime($item->dateNeeded)),
						'value' => strtotime($item->dateNeeded)
					),
					'assignedTo' => $item->assignedTo,
					'status' => $item->status,
					'tooltip' => $item->title,
					'isSelected' => $item->id == $selectedItemId,
					'allowEdit' => $listType == SalesRequestItemRecord::ListTypeOwn
				);
			}

			$statusList = \application\models\sales_requests\models\Dictionaries::getStatusList();
			$statusFilterMarkup = '<option value="' . \application\models\sales_requests\models\Dictionaries::StatusItemAll . '"' . ($statusFilter == \application\models\sales_requests\models\Dictionaries::StatusItemAll ? ' selected' : '') . '>' . \application\models\sales_requests\models\Dictionaries::StatusItemAll . '</option>';
			foreach ($statusList as $statusListItem)
				$statusFilterMarkup .= ('<option value="' . $statusListItem . '"' . ($statusFilter == $statusListItem ? ' selected' : '') . '>' . $statusListItem . '</option>');


			echo CJSON::encode(array(
				'settings' => array(
					'statusFilterMarkup' => $statusFilterMarkup
				),
				'dataset' => $tableItemsDataset
			));
			Yii::app()->end();
		}

		public function actionAddItemDialog()
		{
			$isCloning = Yii::app()->request->getPost('isCloning');
			$isCloning = isset($isCloning) && $isCloning == 'true';
			$this->renderPartial('addItem', array('isCloning' => $isCloning), false, true);
		}

		public function actionAddItem()
		{
			$title = Yii::app()->request->getPost('title');
			$templateItemId = Yii::app()->request->getPost('templateItemId');

			$userId = UserIdentity::getCurrentUserId();
			if (!empty($title))
			{
				if (!empty($templateItemId))
					echo SalesRequestItemRecord::cloneItem($userId, $title, $templateItemId);
				else
					echo SalesRequestItemRecord::addItem($userId, $title);
			}
			Yii::app()->end();
		}

		public function actionDeleteItem()
		{
			$selectedItemId = Yii::app()->request->getPost('selectedItemId');
			if (isset($selectedItemId))
				SalesRequestItemRecord::deleteItem($selectedItemId);
			Yii::app()->end();
		}

		public function actionSaveItem()
		{
			$selectedItemId = Yii::app()->request->getPost('selectedItemId');
			$title = Yii::app()->request->getPost('title');
			$status = Yii::app()->request->getPost('status');
			$assignedTo = Yii::app()->request->getPost('assignedTo');
			$dateNeeded = Yii::app()->request->getPost('dateNeeded');
			$dateCompleted = Yii::app()->request->getPost('dateCompleted');
			$itemContentEncoded = Yii::app()->request->getPost('content');

			/** @var $itemContent \application\models\sales_requests\models\Content */
			if (!empty($itemContentEncoded))
				$itemContent = \application\models\sales_requests\models\Content::fromJson($itemContentEncoded);
			else
				$itemContent = null;

			if (isset($selectedItemId))
				SalesRequestItemRecord::saveItem($selectedItemId, $title, $status, $assignedTo, $dateNeeded, $dateCompleted, $itemContent);
		}

		public function actionGetItemContent()
		{
			$shortcutId = Yii::app()->request->getPost('shortcutId');
			$selectedItemId = Yii::app()->request->getPost('selectedItemId');
			$isAdminRole = Yii::app()->request->getPost('isAdminRole') == "true";
			if (isset($selectedItemId))
			{
				$item = SalesRequestItemRecord::model()->getEditModel($selectedItemId);
				$itemUrl = \Yii::app()->createAbsoluteUrl('shortcuts/getSinglePage', array(
					'linkId' => $shortcutId,
					'itemId' => $selectedItemId
				));
				$this->renderPartial('itemContent', array(
					'item' => $item,
					'itemUrl' => $itemUrl,
					'isAdminRole' => $isAdminRole
				), false, true);
			}
		}

		public function actionGetItemFiles()
		{
			$itemId = Yii::app()->request->getPost('itemId');
			$fileType = Yii::app()->request->getPost('fileType');
			$files = SalesRequestFileRecord::model()->getModels($itemId, $fileType);
			$this->renderPartial('itemFiles', array(
				'files' => $files
			), false, true);
		}

		public function actionUploadFile()
		{
			$itemId = Yii::app()->request->getQuery('itemId');
			$fileType = Yii::app()->request->getQuery('fileType');
			SalesRequestFileRecord::addItem(
				$itemId,
				$_FILES['file']['name'],
				$fileType,
				$_FILES['file']['tmp_name']);
			Yii::app()->end();
		}

		public function actionDownloadFile()
		{
			$fileId = Yii::app()->request->getQuery('fileId');
			if (empty($fileId))
				$fileId = Yii::app()->request->getPost('fileId');
			$file = SalesRequestFileRecord::model()->getModel($fileId);
			$content = SalesRequestFileRecord::model()->getFileContent($fileId);
			Yii::app()->getRequest()->sendFile($file->fileName, $content);
			Yii::app()->end();
		}

		public function actionDeleteFile()
		{
			$fileId = Yii::app()->request->getPost('fileId');
			SalesRequestFileRecord::deleteFile($fileId);
			Yii::app()->end();
		}
	}
