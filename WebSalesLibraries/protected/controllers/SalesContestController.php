<?

	/**
	 * Class SalesContestController
	 */
	class SalesContestController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'sales_contest');
		}

		public function actionGetItemList()
		{
			$selectedItemId = Yii::app()->request->getPost('selectedItemId');
			$shortcutId = Yii::app()->request->getPost('shortcutId');
			$listType = Yii::app()->request->getPost('listType');

			/* @var $items \application\models\sales_contest\models\ItemListModel[] */
			$items = SalesContestItemRecord::model()->getListItems($listType, $shortcutId);
			$tableItemsDataset = array();
			foreach ($items as $item)
			{
				/** @var $itemOwner UserRecord */
				$itemOwner = UserRecord::model()->findByPk($item->idOwner);

				$tableItemsDataset[] = array(
					'id' => $item->id,
					'title' => $item->title,
					'itemOwner' => $listType == SalesContestItemRecord::ListTypeOwn ?
						'' :
						$itemOwner->login,
					'advertiser' => $item->advertiser,
					'fileCount' => $item->filesCount > 0 ? $item->filesCount : '',
					'revenue' => !empty($item->revenue) ?
						('$' . (float)$item->revenue) :
						null,
					'dateSubmit' => array(
						'display' => !empty($item->dateSubmit) ?
							date(\Yii::app()->params['outputDateFormat'], strtotime($item->dateSubmit)) :
							null,
						'value' => strtotime($item->dateSubmit)
					),
					'tooltip' => $item->title,
					'isSelected' => $item->id == $selectedItemId,
					'allowEdit' => $listType == SalesContestItemRecord::ListTypeOwn
				);
			}

			echo CJSON::encode(array(
				'info' => SalesContestItemRecord::model()->getItemListInfo($shortcutId),
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
			$shortcutId = Yii::app()->request->getPost('shortcutId');

			$userId = UserIdentity::getCurrentUserId();
			if (!empty($title))
			{
				if (!empty($templateItemId))
					echo SalesContestItemRecord::cloneItem($userId, $title, $templateItemId, $shortcutId);
				else
					echo SalesContestItemRecord::addItem($userId, $title, $shortcutId);
			}
			Yii::app()->end();
		}

		public function actionDeleteItem()
		{
			$selectedItemId = Yii::app()->request->getPost('selectedItemId');
			if (isset($selectedItemId))
				SalesContestItemRecord::deleteItem($selectedItemId);
			Yii::app()->end();
		}

		public function actionSaveItem()
		{
			$selectedItemId = Yii::app()->request->getPost('selectedItemId');
			$shortcutId = Yii::app()->request->getPost('shortcutId');
			$isSubmit = Yii::app()->request->getPost('submit') == "true";
			$title = Yii::app()->request->getPost('title');
			$advertiser = Yii::app()->request->getPost('advertiser');
			$revenue = Yii::app()->request->getPost('revenue');
			$itemContentEncoded = Yii::app()->request->getPost('content');

			/** @var $itemContent \application\models\sales_contest\models\Content */
			if (!empty($itemContentEncoded))
				$itemContent = \application\models\sales_contest\models\Content::fromJson($itemContentEncoded);
			else
				$itemContent = null;

			if (isset($selectedItemId))
			{
				/** @var $itemRecord SalesContestItemRecord */
				$itemRecord = SalesContestItemRecord::model()->findByPk($selectedItemId);
				if (isset($itemRecord))
				{
					$notYetSubmitted = !isset($itemRecord->date_submit);

					$itemRecord->saveItem($title, $advertiser, $revenue, $isSubmit, $itemContent);

					if ($notYetSubmitted &&
						$isSubmit == true &&
						isset($shortcutId))
					{
						/** @var $linkRecord ShortcutLinkRecord */
						$linkRecord = ShortcutLinkRecord::model()->findByPk($shortcutId);
						/**@var $shortcut SalesContestShortcut */
						$shortcut = $linkRecord->getRegularModel(false);
						$submitEmailRecipients = $shortcut->getSubmitEmailRecipients();

						$userId = UserIdentity::getCurrentUserId();
						/** @var $itemOwner UserRecord */
						$itemOwner = UserRecord::model()->findByPk($userId);

						if (count($submitEmailRecipients) > 0 && isset($itemOwner))
						{
							$message = Yii::app()->email;
							$message->to = $submitEmailRecipients;
							$message->subject = sprintf("Win of the week - %s", $title);
							$message->from = Yii::app()->params['email']['sales_contest']['from'];
							if (Yii::app()->params['email']['sales_contest']['copy_enabled'])
							{
								$copyRecipients = array();
								$copyRecipients[] = Yii::app()->params['email']['sales_contest']['copy'];
								if (Yii::app()->params['email']['sales_contest']['copy_user'] && !empty($itemOwner->email))
									$copyRecipients[] = $itemOwner->email;
								$message->cc = $copyRecipients;
							}
							$message->message = sprintf("%s<br><br>%s<br><br>%s",
								sprintf("Submitted by %s %s",
									$itemOwner->login,
									date(\Yii::app()->params['outputDateFormat'], strtotime($itemRecord->date_submit)) . ' at ' . date(\Yii::app()->params['outputTimeFormat'], strtotime($itemRecord->date_submit))),
								sprintf("ID: %s", $title),
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
				$item = SalesContestItemRecord::model()->getEditModel($selectedItemId);
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
			$files = SalesContestFileRecord::model()->getModels($itemId, $fileType);
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
				SalesContestFileRecord::addItem(
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
			$file = SalesContestFileRecord::model()->getModel($fileId);
			$content = SalesContestFileRecord::model()->getFileContent($fileId);
			Yii::app()->getRequest()->sendFile($file->fileName, $content);
			Yii::app()->end();
		}

		public function actionDeleteFile()
		{
			$fileId = Yii::app()->request->getPost('fileId');
			SalesContestFileRecord::deleteFile($fileId);
			Yii::app()->end();
		}
	}
