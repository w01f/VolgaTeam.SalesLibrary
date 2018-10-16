<?

	/**
	 * Class StarStealsController
	 */
	class StarStealsController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'star_steals');
		}

		public function actionGetItemList()
		{
			$selectedItemId = Yii::app()->request->getPost('selectedItemId');
			$userId = UserIdentity::getCurrentUserId();
			$pages = StarStealsItemRecord::model()->getListItems($userId);
			$this->renderPartial('itemList', array('items' => $pages, 'selectedItemId' => $selectedItemId), false, true);
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
			$shortcutId = Yii::app()->request->getPost('shortcutId');
			$templateItemId = Yii::app()->request->getPost('templateItemId');

			$userId = UserIdentity::getCurrentUserId();
			if (!empty($title))
			{
				if (isset($templatePageId))
					echo StarStealsItemRecord::cloneItem($userId, $title, $templateItemId);
				else
					echo StarStealsItemRecord::addItem($userId, $title);
			}
			Yii::app()->end();
		}

		public function actionDeleteItem()
		{
			$selectedItemId = Yii::app()->request->getPost('selectedItemId');
			if (isset($selectedItemId))
				StarStealsItemRecord::deleteItem($selectedItemId);
			Yii::app()->end();
		}

		public function actionDeleteItemsDialog()
		{
			$userId = UserIdentity::getCurrentUserId();
			$listItems = StarStealsItemRecord::model()->getListItems($userId);
			$this->renderPartial('deleteItems', array('items' => $listItems), false, true);
		}

		public function actionDeleteItems()
		{
			$itemIds = Yii::app()->request->getPost('itemIds');
			if (isset($itemIds))
			{
				$itemIds = CJSON::decode($itemIds);
				foreach ($itemIds as $itemId)
					StarStealsItemRecord::deleteItem($itemId);
			}
			Yii::app()->end();
		}

		public function actionSaveItem()
		{
			$selectedItemId = Yii::app()->request->getPost('selectedItemId');
			$title = Yii::app()->request->getPost('title');
			$itemContentEncoded = Yii::app()->request->getPost('content');

			if (!empty($itemContentEncoded))
				$itemContent = \application\models\star_steals\models\Content::fromJson($itemContentEncoded);

			if (isset($selectedItemId))
				StarStealsItemRecord::saveItem($selectedItemId, $title, $itemContent);
		}

		public function actionSetItemOrder()
		{
			$userId = UserIdentity::getCurrentUserId();
			$itemId = Yii::app()->request->getPost('itemId');
			$order = intval(Yii::app()->request->getPost('order'));
			if (isset($itemId) && isset($order))
				StarStealsItemRecord::setItemOrder($userId, $itemId, $order);
			Yii::app()->end();
		}

		public function actionGetItemContent()
		{
			$selectedItemId = Yii::app()->request->getPost('selectedItemId');
			if (isset($selectedItemId))
			{
				$item = StarStealsItemRecord::model()->getEditModel($selectedItemId);
				$this->renderPartial('itemContent', array('item' => $item), false, true);
			}
		}
	}
