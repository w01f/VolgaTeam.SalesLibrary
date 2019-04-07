<?

	/**
	 * Class BillboardRequestsController
	 */
	class BillboardRequestsController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'billboard_requests');
		}

		public function actionGetItemList()
		{
			$selectedItemId = Yii::app()->request->getPost('selectedItemId');
			$shortcutId = Yii::app()->request->getPost('shortcutId');
			$listType = Yii::app()->request->getPost('listType');
			$statusFilter = Yii::app()->request->getPost('statusFilter');

			/* @var $items \application\models\billboard_requests\models\ItemListModel[] */
			$items = BillboardRequestItemRecord::model()->getListItems($listType, $shortcutId, $statusFilter);
			$tableItemsDataset = array();
			foreach ($items as $item)
			{
				/** @var $itemOwner UserRecord */
				$itemOwner = UserRecord::model()->findByPk($item->idOwner);

				$tableItemsDataset[] = array(
					'id' => $item->id,
					'title' => $item->title,
					'itemOwner' => $listType == BillboardRequestItemRecord::ListTypeOwn ?
						'' :
						$itemOwner->login,
					'dateSubmit' => array(
						'display' => !empty($item->dateSubmit) ?
							date(\Yii::app()->params['outputDateFormat'], strtotime($item->dateSubmit)) :
							null,
						'value' => strtotime($item->dateSubmit)
					),
					'dateNeeded' => array(
						'display' => date(\Yii::app()->params['outputDateFormat'], strtotime($item->dateNeeded)),
						'value' => strtotime($item->dateNeeded)
					),
					'assignedTo' => $item->assignedTo,
					'status' => $item->status,
					'tooltip' => $item->title,
					'isSelected' => $item->id == $selectedItemId,
					'allowEdit' => $listType == BillboardRequestItemRecord::ListTypeOwn
				);
			}

			$statusList = \application\models\billboard_requests\models\Dictionaries::getStatusList();
			$statusFilterMarkup = '<option value="' . \application\models\billboard_requests\models\Dictionaries::StatusItemAll . '" ' . ($statusFilter == \application\models\billboard_requests\models\Dictionaries::StatusItemAll ? ' selected' : '') . '>' . \application\models\billboard_requests\models\Dictionaries::StatusItemAll . '</option>';
			foreach ($statusList as $statusListItem)
				$statusFilterMarkup .= ('<option value="' . $statusListItem . '" ' . ($statusFilter == $statusListItem ? ' selected' : '') . '>' . $statusListItem . '</option>');


			echo CJSON::encode(array(
				'settings' => array(
					'statusFilterMarkup' => $statusFilterMarkup
				),
				'info' => BillboardRequestItemRecord::model()->getItemListInfo($shortcutId, $statusFilter),
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
					echo BillboardRequestItemRecord::cloneItem($userId, $title, $templateItemId);
				else
					echo BillboardRequestItemRecord::addItem($userId, $title);
			}
			Yii::app()->end();
		}

		public function actionDeleteItem()
		{
			$selectedItemId = Yii::app()->request->getPost('selectedItemId');
			if (isset($selectedItemId))
				BillboardRequestItemRecord::deleteItem($selectedItemId);
			Yii::app()->end();
		}

		public function actionSaveItem()
		{
			$selectedItemId = Yii::app()->request->getPost('selectedItemId');
			$shortcutId = Yii::app()->request->getPost('shortcutId');
			$title = Yii::app()->request->getPost('title');
			$status = Yii::app()->request->getPost('status');
			$assignedTo = Yii::app()->request->getPost('assignedTo');
			$dateNeeded = Yii::app()->request->getPost('dateNeeded');
			$dateCompleted = Yii::app()->request->getPost('dateCompleted');
			$itemContentEncoded = Yii::app()->request->getPost('content');

			/** @var $itemContent \application\models\billboard_requests\models\Content */
			if (!empty($itemContentEncoded))
				$itemContent = \application\models\billboard_requests\models\Content::fromJson($itemContentEncoded);
			else
				$itemContent = null;

			if (isset($selectedItemId))
			{
				/** @var $itemRecord BillboardRequestItemRecord */
				$itemRecord = BillboardRequestItemRecord::model()->findByPk($selectedItemId);
				if (isset($itemRecord))
				{
					$notYetSubmitted = !isset($itemRecord->date_submit);

					$itemRecord->saveItem($title, $status, $assignedTo, $dateNeeded, $dateCompleted, $itemContent);

					if ($notYetSubmitted &&
						$status === \application\models\billboard_requests\models\Dictionaries::StatusItemSubmitted &&
						isset($shortcutId))
					{
						/** @var $linkRecord ShortcutLinkRecord */
						$linkRecord = ShortcutLinkRecord::model()->findByPk($shortcutId);
						/**@var $shortcut BillboardRequestsShortcut */
						$shortcut = $linkRecord->getRegularModel(false);
						$submitEmailRecipients = $shortcut->getSubmitEmailRecipients();

						$userId = UserIdentity::getCurrentUserId();
						/** @var $itemOwner UserRecord */
						$itemOwner = UserRecord::model()->findByPk($userId);

						if (count($submitEmailRecipients) > 0 && isset($itemOwner))
						{
							$message = Yii::app()->email;
							$message->to = $submitEmailRecipients;
							$message->subject = sprintf("Research Request - %s", $title);
							$message->from = Yii::app()->params['email']['billboard_requests']['from'];
							if (Yii::app()->params['email']['billboard_requests']['copy_enabled'])
							{
								$copyRecipients = array();
								$copyRecipients[] = Yii::app()->params['email']['billboard_requests']['copy'];
								if (Yii::app()->params['email']['billboard_requests']['copy_user'] && !empty($itemOwner->email))
									$copyRecipients[] = $itemOwner->email;
								$message->cc = $copyRecipients;
							}
							$message->message = sprintf("%s<br><br>%s<br><br>%s",
								sprintf("Submitted by %s %s",
									$itemOwner->login,
									date(\Yii::app()->params['outputDateFormat'], strtotime($itemRecord->date_submit)) . ' at ' . date(\Yii::app()->params['outputTimeFormat'], strtotime($itemRecord->date_submit))),
								sprintf("Request ID: %s", $title),
								\Yii::app()->createAbsoluteUrl('shortcuts/getSinglePage', array(
									'linkId' => $shortcutId,
									'itemId' => $selectedItemId
								)));
							$message->send();
						}
					}
				}
			}
		}

		public function actionGetItemContent()
		{
			$shortcutId = Yii::app()->request->getPost('shortcutId');
			$selectedItemId = Yii::app()->request->getPost('selectedItemId');
			$isAdminRole = Yii::app()->request->getPost('isAdminRole') == "true";
			if (isset($selectedItemId))
			{
				$item = BillboardRequestItemRecord::model()->getEditModel($selectedItemId);
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
			$files = BillboardRequestFileRecord::model()->getModels($itemId, $fileType);
			$this->renderPartial('itemFiles', array(
				'files' => $files
			), false, true);
		}

		public function actionUploadFile()
		{
			$itemId = Yii::app()->request->getQuery('itemId');
			$fileType = Yii::app()->request->getQuery('fileType');

			$filesForUploading = array();
			if (is_array($_FILES['file']['name']))
				for ($i = 0; $i < count($_FILES['file']['name']); $i++)
					$filesForUploading[] = array(
						'name' => $_FILES['file']['name'][$i],
						'tmp_name' => $_FILES['file']['tmp_name'][$i]
					);
			else
				$filesForUploading[] = array(
					'name' => $_FILES['file']['name'],
					'tmp_name' => $_FILES['file']['tmp_name']
				);

			foreach ($filesForUploading as $fileForUploading)
				BillboardRequestFileRecord::addItem(
					$itemId,
					$fileForUploading['name'],
					$fileType,
					$fileForUploading['tmp_name']);

			Yii::app()->end();
		}

		public function actionDownloadFile()
		{
			$fileId = Yii::app()->request->getQuery('fileId');
			if (empty($fileId))
				$fileId = Yii::app()->request->getPost('fileId');
			$file = BillboardRequestFileRecord::model()->getModel($fileId);
			$content = BillboardRequestFileRecord::model()->getFileContent($fileId);
			Yii::app()->getRequest()->sendFile($file->fileName, $content);
			Yii::app()->end();
		}

		public function actionDeleteFile()
		{
			$fileId = Yii::app()->request->getPost('fileId');
			BillboardRequestFileRecord::deleteFile($fileId);
			Yii::app()->end();
		}
	}
