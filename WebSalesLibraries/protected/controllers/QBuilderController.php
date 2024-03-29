<?
	use application\models\data_query\data_table\DataTableColumnSettings;

	/**
	 * Class QBuilderController
	 */
	class QBuilderController extends SoapController
	{
		/** return array */
		protected function getPublicActionIds()
		{
			return array(
				'quote',
			);
		}

		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'qbuilder');
		}

		public function actionGetPageList()
		{
			$selectedPageId = Yii::app()->request->getPost('selectedPageId');
			$userId = UserIdentity::getCurrentUserId();
			$pages = QPageRecord::model()->getByOwner($userId);
			$selectedPage = QPageRecord::model()->findByPk($selectedPageId);
			$this->renderPartial('pageList', array('pages' => $pages, 'selectedPage' => $selectedPage), false, true);
		}

		public function actionAddPageDialog()
		{
			$isCloning = Yii::app()->request->getPost('isCloning');
			$isCloning = isset($isCloning) && $isCloning == 'true';
			$this->renderPartial('addPage', array('isCloning' => $isCloning), false, true);
		}

		public function actionAddPage()
		{
			$title = Yii::app()->request->getPost('title');
			$createDate = Yii::app()->request->getPost('createDate');
			$templatePageId = Yii::app()->request->getPost('templatePageId');
			$populateFromLinkCart = Yii::app()->request->getPost('populateFromLinkCart');
			$populateFromLinkCart = isset($populateFromLinkCart) && $populateFromLinkCart == 'true';

			$userId = UserIdentity::getCurrentUserId();
			if (isset($title) && $title != '' && isset($createDate))
			{
				if (isset($templatePageId))
					echo QPageRecord::clonePage($userId, $title, $createDate, $templatePageId);
				else
				{
					$linkCartIds = array();
					if ($populateFromLinkCart)
					{
						/** @var UserLinkCartRecord[] $linkRecords */
						$linkRecords = UserLinkCartRecord::model()->findAll('id_user=?', array($userId));
						foreach ($linkRecords as $linkRecord)
							$linkCartIds[] = $linkRecord->id;
					}

					echo QPageRecord::addPage($userId, $title, $createDate, $linkCartIds);
				}
			}
			Yii::app()->end();
		}

		public function actionAddPageLite()
		{
			$subtitle = Yii::app()->request->getPost('subtitle');
			$logo = Yii::app()->request->getPost('logo');
			$linkId = Yii::app()->request->getPost('linkId');
			$createDate = Yii::app()->request->getPost('createDate');

			$expiresInDays = Yii::app()->request->getPost('expiresInDays');
			$expiresInDays = isset($expiresInDays) ? intval($expiresInDays) : null;

			$restricted = Yii::app()->request->getPost('restricted');
			$restricted = isset($restricted) && $restricted == 'true';

			$disableBanners = Yii::app()->request->getPost('disableBanners');
			$disableBanners = isset($disableBanners) && $disableBanners == "true";

			$disableWidgets = Yii::app()->request->getPost('disableWidgets');
			$disableWidgets = isset($disableWidgets) && $disableWidgets == "true";

			$showLinksAsUrl = Yii::app()->request->getPost('showLinksAsUrl');
			$showLinksAsUrl = isset($showLinksAsUrl) && $showLinksAsUrl == "true";

			$recordActivity = Yii::app()->request->getPost('recordActivity');
			$recordActivity = isset($recordActivity) && $recordActivity == "true";

			$pinCode = Yii::app()->request->getPost('pinCode');
			if (isset($pinCode) && $pinCode == '')
				$pinCode = null;

			$activityEmailCopy = Yii::app()->request->getPost('activityEmailCopy');
			if (isset($activityEmailCopy) && $activityEmailCopy == '')
				$activityEmailCopy = null;

			$userId = UserIdentity::getCurrentUserId();
			if (isset($createDate) && isset($linkId) && isset($expiresInDays) && isset($restricted))
			{
				$expirationDate = $expiresInDays > 0 ? date(Yii::app()->params['mysqlDateFormat'], strtotime(date("Y-m-d") . ' + ' . $expiresInDays . ' day')) : null;
				echo QPageRecord::addPageLite($userId, $createDate, $subtitle, $logo, $expirationDate, $restricted, $disableBanners, $disableWidgets, $showLinksAsUrl, $recordActivity, $pinCode, $activityEmailCopy, $linkId)->getUrl();
			}
			Yii::app()->end();
		}

		public function actionDeletePage()
		{
			$selectedPageId = Yii::app()->request->getPost('selectedPageId');
			if (isset($selectedPageId))
				QPageRecord::deletePage($selectedPageId);
			Yii::app()->end();
		}

		public function actionDeletePagesDialog()
		{
			$userId = UserIdentity::getCurrentUserId();
			$pages = QPageRecord::model()->getByOwner($userId);
			$this->renderPartial('deletePages', array('pages' => $pages), false, true);
		}

		public function actionDeletePages()
		{
			$pageIds = Yii::app()->request->getPost('pageIds');
			if (isset($pageIds))
			{
				$pageIds = CJSON::decode($pageIds);
				foreach ($pageIds as $pageId)
					QPageRecord::deletePage($pageId);
			}
			Yii::app()->end();
		}

		public function actionSavePage()
		{
			$selectedPageId = Yii::app()->request->getPost('selectedPageId');
			$title = Yii::app()->request->getPost('title');
			$description = Yii::app()->request->getPost('description');
			$logo = Yii::app()->request->getPost('logo');
			$header = Yii::app()->request->getPost('header');
			$footer = Yii::app()->request->getPost('footer');

			$expirationDate = Yii::app()->request->getPost('expirationDate');
			if (isset($expirationDate) && $expirationDate != '')
				$expirationDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($expirationDate));

			$requireLogin = Yii::app()->request->getPost('requireLogin');
			$requireLogin = isset($requireLogin) && $requireLogin == "true";

			$disableBanners = Yii::app()->request->getPost('disableBanners');
			$disableBanners = isset($disableBanners) && $disableBanners == "true";

			$disableWidgets = Yii::app()->request->getPost('disableWidgets');
			$disableWidgets = isset($disableWidgets) && $disableWidgets == "true";

			$showLinksAsUrl = Yii::app()->request->getPost('showLinksAsUrl');
			$showLinksAsUrl = isset($showLinksAsUrl) && $showLinksAsUrl == "true";

			$autoLaunch = Yii::app()->request->getPost('autoLaunch');
			$autoLaunch = isset($autoLaunch) && $autoLaunch == "true";

			$recordActivity = Yii::app()->request->getPost('recordActivity');
			$recordActivity = isset($recordActivity) && $recordActivity == "true";

			$pinCode = Yii::app()->request->getPost('pinCode');
			if (isset($pinCode) && $pinCode == '')
				$pinCode = null;

			$activityEmailCopy = Yii::app()->request->getPost('activityEmailCopy');
			if (isset($activityEmailCopy) && $activityEmailCopy == '')
				$activityEmailCopy = null;

			if (isset($selectedPageId))
				QPageRecord::savePage($selectedPageId, $title, $description, $expirationDate, $logo, $header, $footer, $requireLogin, $disableBanners, $disableWidgets, $showLinksAsUrl, $autoLaunch, $recordActivity, $pinCode, $activityEmailCopy);
		}

		public function actionSetPageOrder()
		{
			$userId = UserIdentity::getCurrentUserId();
			$pageId = Yii::app()->request->getPost('pageId');
			$order = intval(Yii::app()->request->getPost('order'));
			if (isset($pageId) && isset($order))
				QPageRecord::setPageOrder($userId, $pageId, $order);
			Yii::app()->end();
		}

		public function actionGetLinkCart()
		{
			$userId = UserIdentity::getCurrentUserId();
			$columnSettings = DataTableColumnSettings::createLinkCartColumns();
			$links = UserLinkCartRecord::getLinksByUser($userId, $columnSettings);
			echo CJSON::encode(array(
				'links' => $links,
				'viewOptions' => array(
					'columnSettings' => $columnSettings,
					'showDeleteButton' => true
				)
			));
		}

		public function actionClearLinkCart()
		{
			$userId = UserIdentity::getCurrentUserId();
			if (isset($userId))
				UserLinkCartRecord::deleteLinksByUser($userId);
			Yii::app()->end();
		}

		public function actionPrepareFolderToAddToLinkCart()
		{
			$folderId = Yii::app()->request->getPost('folderId');
			$folderRecord = FolderRecord::model()->findByPk($folderId);
			$this->renderPartial('prepareLinkCart', array('folder' => $folderRecord), false, true);
		}

		public function actionAddLinksToCart()
		{
			$linkIds = Yii::app()->request->getPost('linkIds');
			$userId = UserIdentity::getCurrentUserId();
			if (isset($linkIds))
				foreach ($linkIds as $linkId)
					UserLinkCartRecord::addLink($userId, $linkId);
			Yii::app()->end();
		}

		public function actionAddLinkToPage()
		{
			$pageId = Yii::app()->request->getPost('pageId');
			$linkInCartId = Yii::app()->request->getPost('linkInCartId');
			$order = intval(Yii::app()->request->getPost('order'));
			if (isset($pageId) && isset($linkInCartId))
			{
				/** @var $selectedPage QPageRecord */
				$selectedPage = QPageRecord::model()->findByPk($pageId);
				$selectedPage->addLink($linkInCartId, $order);
			}
			Yii::app()->end();
		}

		public function actionAddAllLinksToPage()
		{
			$pageId = Yii::app()->request->getPost('pageId');
			$userId = UserIdentity::getCurrentUserId();
			if (isset($pageId))
			{
				/** @var $selectedPage QPageRecord */
				$selectedPage = QPageRecord::model()->findByPk($pageId);
				/** @var UserLinkCartRecord[] $linkRecords */
				$linkRecords = UserLinkCartRecord::model()->findAll('id_user=?', array($userId));
				foreach ($linkRecords as $linkRecord)
					$selectedPage->addLink($linkRecord->id, -1);
			}
			Yii::app()->end();
		}

		public function actionDeleteLinkFromCart()
		{
			$linkInCartId = Yii::app()->request->getPost('linkInCartId');
			if (isset($linkInCartId))
				UserLinkCartRecord::deleteLink($linkInCartId);
			Yii::app()->end();
		}

		public function actionDeleteLinkFromPage()
		{
			$linkInPageId = Yii::app()->request->getPost('linkInPageId');
			if (isset($linkInPageId))
			{
				/** @var QPageLinkRecord $linkRecord */
				$linkRecord = QPageLinkRecord::model()->findByPk($linkInPageId);
				$pageId = $linkRecord->id_page;
				QPageLinkRecord::deleteLink($linkInPageId);
				/** @var $pageRecord QPageRecord */
				$pageRecord = QPageRecord::model()->findByPk($pageId);
				$pageRecord->rebuildLinkList(-1);
			}
			Yii::app()->end();
		}

		public function actionDeleteAllLinksFromPage()
		{
			$pageId = Yii::app()->request->getPost('pageId');
			if (isset($pageId))
				QPageLinkRecord::model()->deleteAll('id_page=?', array($pageId));
			Yii::app()->end();
		}

		public function actionSetPageLinkOrder()
		{
			$pageId = Yii::app()->request->getPost('pageId');
			$linkInPageId = Yii::app()->request->getPost('linkInPageId');
			$order = intval(Yii::app()->request->getPost('order'));
			if (isset($pageId) && isset($linkInPageId) && isset($order))
			{
				/** @var $selectedPage QPageRecord */
				$selectedPage = QPageRecord::model()->findByPk($pageId);
				$selectedPage->setLinkOrder($linkInPageId, $order);
			}
			Yii::app()->end();
		}

		public function actionGetPageContent()
		{
			$selectedPageId = Yii::app()->request->getPost('selectedPageId');
			if (isset($selectedPageId))
			{
				$page = QPageRecord::model()->findByPk($selectedPageId);
				$logos = QPageRecord::getPageLogoList();
				$this->renderPartial('pageContent', array('page' => $page, 'logos' => $logos), false, true);
			}
		}

		public function actionGetPageLinks()
		{
			$selectedPageId = Yii::app()->request->getPost('selectedPageId');
			if (isset($selectedPageId))
			{
				/** @var $page QPageRecord */
				$page = QPageRecord::model()->findByPk($selectedPageId);
				$columnSettings = DataTableColumnSettings::createEmpty();
				$links = $page->getPageLinks($columnSettings);
				echo CJSON::encode(array(
					'links' => $links,
					'viewOptions' => array(
						'columnSettings' => $columnSettings,
						'showDeleteButton' => true,
						'reorderSourceField' => 'listOrder'
					)
				));
			}
		}

		/////////////////Service Part///////////////////////////
		/**
		 * @return array
		 */
		public function actions()
		{
			return array(
				'quote' => array(
					'class' => 'CWebServiceAction',
					'classMap' => array(
						'QPageModel' => 'QPageModel',
						'QPageLinkModel' => 'QPageLinkModel',
					),
				),
			);
		}

		/**
		 * @param string $sessionKey
		 * @param string $dateStart
		 * @param string $dateEnd
		 * @param boolean $filterByViewDate
		 * @return QPageModel[]
		 * @soap
		 */
		public function getAllPages($sessionKey, $dateStart, $dateEnd, $filterByViewDate)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$command = Yii::app()->db->createCommand("call sp_get_all_qpages(:start_date,:end_date,:filter_by_view_date)");
				$command->bindValue(":start_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateStart)), PDO::PARAM_STR);
				$command->bindValue(":end_date", date(Yii::app()->params['mysqlDateFormat'], strtotime($dateEnd)), PDO::PARAM_STR);
				$command->bindValue(":filter_by_view_date", $filterByViewDate, PDO::PARAM_BOOL);
				$pageRecords = $command->queryAll();
				foreach ($pageRecords as $pageRecord)
				{
					$page = new QPageModel();
					$page->id = $pageRecord['id'];
					$page->title = $pageRecord['title'];
					$page->isEmail = $pageRecord['is_email'];
					$page->url = Yii::app()->createAbsoluteUrl('qpage/show', array('id' => $pageRecord['id']));
					$page->createDate = $pageRecord['create_date'];
					$page->expirationDate = $pageRecord['expiration_date'];
					$page->pinCode = $pageRecord['pin_code'];
					$page->login = $pageRecord['login'];
					$page->firstName = $pageRecord['first_name'];
					$page->lastName = $pageRecord['last_name'];
					$page->email = $pageRecord['email'];
					$page->isRestricted = $pageRecord['restricted'];
					$page->groups = $pageRecord['groups'];
					$page->totalViews = $pageRecord['total_views'];
					$pages[] = $page;
				}
			}
			if (isset($pages))
				return $pages;
			else
				return null;
		}

		/**
		 * @param string $sessionKey
		 * @param string[] $pageIds
		 * @soap
		 */
		public function deletePages($sessionKey, $pageIds)
		{
			if ($this->authenticateBySession($sessionKey))
				foreach ($pageIds as $pageId)
					QPageRecord::deletePage($pageId);
		}
		/////////////////Service Part///////////////////////////
	}
