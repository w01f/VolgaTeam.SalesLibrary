<?php
	class QbuilderController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'qbuilder');
		}

		public function actionIndex()
		{
			$this->pageTitle = "quickSITES APP";
			$this->render('index');
		}

		public function actionGetMainPage()
		{
			$userId = Yii::app()->user->getId();
			if (isset($userId))
			{
				$links = UserLinkCartStorage::getLinksByUser($userId);
				$pages = QPageStorage::model()->getByOwner($userId);
				$selectedPage = count($pages) > 0 ? $pages[0] : null;
				$logos = QPageStorage::getPageLogoList();
				$this->renderPartial('mainPage', array('pages' => $pages, 'selectedPage' => $selectedPage, 'links' => $links, 'logos' => $logos), false, true);
			}
		}

		public function actionGetPageList()
		{
			$selectedPageId = Yii::app()->request->getPost('selectedPageId');
			$userId = Yii::app()->user->getId();
			if (isset($userId))
			{
				$pages = QPageStorage::model()->getByOwner($userId);
				$selectedPage = QPageStorage::model()->findByPk($selectedPageId);
				$this->renderPartial('pageList', array('pages' => $pages, 'selectedPage' => $selectedPage), false, true);
			}
		}

		public function actionAddPageDialog()
		{
			$clone = Yii::app()->request->getPost('clone');
			$clone = isset($clone) && $clone == 'true';
			$this->renderPartial('addPage', array('clone' => $clone), false, true);
		}

		public function actionAddPageLiteDialog()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$linkRecord = LinkStorage::getLinkById($linkId);
			$logos = QPageStorage::getPageLogoList();
			if (isset($linkRecord))
			{
				$this->renderPartial('addPageLite', array('linkRecord' => $linkRecord, 'logos' => $logos), false, true);
				StatisticActivityStorage::WriteActivity('Email', 'Create Email', array('Name' => $linkRecord->name, 'File' => $linkRecord->file_name, 'Original Format' => $linkRecord->format));
			}
		}

		public function actionAddPage()
		{
			$title = Yii::app()->request->getPost('title');
			$createDate = Yii::app()->request->getPost('createDate');
			$clonePageId = Yii::app()->request->getPost('clonePageId');
			$userId = Yii::app()->user->getId();
			if (isset($title) && $title != '' && isset($userId) && isset($createDate))
			{
				if (isset($clonePageId))
					echo QPageStorage::clonePage($userId, $title, $createDate, $clonePageId);
				else
					echo QPageStorage::addPage($userId, $title, $createDate);
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

			$userId = Yii::app()->user->getId();
			if (isset($logo) && isset($userId) && isset($createDate) && isset($linkId) && isset($expiresInDays) && isset($restricted))
			{
				$expirationDate = $expiresInDays > 0 ? date(Yii::app()->params['mysqlDateFormat'], strtotime(date("Y-m-d") . ' + ' . $expiresInDays . ' day')) : null;
				echo QPageStorage::addPageLite($userId, $createDate, $subtitle, $logo, $expirationDate, $restricted, $disableBanners, $disableWidgets, $showLinksAsUrl, $recordActivity, $pinCode, $activityEmailCopy, $linkId)->getUrl();

				$linkRecord = LinkStorage::getLinkById($linkId);
				if (isset($linkRecord))
					StatisticActivityStorage::WriteActivity('Email', 'Send Email', array('Name' => $linkRecord->name, 'File' => $linkRecord->file_name, 'Original Format' => $linkRecord->format));
			}
			Yii::app()->end();
		}

		public function actionDeletePage()
		{
			$selectedPageId = Yii::app()->request->getPost('selectedPageId');
			if (isset($selectedPageId))
				QPageStorage::deletePage($selectedPageId);
			Yii::app()->end();
		}

		public function actionDeletePagesDialog()
		{
			$userId = Yii::app()->user->getId();
			if (isset($userId))
			{
				$pages = QPageStorage::model()->getByOwner($userId);
				$this->renderPartial('deletePages', array('pages' => $pages), false, true);
			}
		}

		public function actionDeletePages()
		{
			$pageIds = Yii::app()->request->getPost('pageIds');
			if (isset($pageIds))
			{
				$pageIds = CJSON::decode($pageIds);
				foreach ($pageIds as $pageId)
					QPageStorage::deletePage($pageId);
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

			$recordActivity = Yii::app()->request->getPost('recordActivity');
			$recordActivity = isset($recordActivity) && $recordActivity == "true";

			$pinCode = Yii::app()->request->getPost('pinCode');
			if (isset($pinCode) && $pinCode == '')
				$pinCode = null;

			$activityEmailCopy = Yii::app()->request->getPost('activityEmailCopy');
			if (isset($activityEmailCopy) && $activityEmailCopy == '')
				$activityEmailCopy = null;

			if (isset($selectedPageId))
				QPageStorage::savePage($selectedPageId, $title, $description, $expirationDate, $logo, $header, $footer, $requireLogin, $disableBanners, $disableWidgets, $showLinksAsUrl, $recordActivity, $pinCode, $activityEmailCopy);
		}

		public function actionSetPageOrder()
		{
			$userId = Yii::app()->user->getId();
			$pageId = Yii::app()->request->getPost('pageId');
			$order = intval(Yii::app()->request->getPost('order'));
			if (isset($userId) && isset($pageId) && isset($order))
				QPageStorage::setPageOrder($userId, $pageId, $order);
			Yii::app()->end();
		}

		public function actionGetLinkCart()
		{
			$userId = Yii::app()->user->getId();
			if (isset($userId))
			{
				$links = UserLinkCartStorage::getLinksByUser($userId);
				$this->renderPartial('linkCart', array('links' => $links), false, true);
			}
		}

		public function actionClearLinkCart()
		{
			$userId = Yii::app()->user->getId();
			if (isset($userId))
				UserLinkCartStorage::deleteLinksByUser($userId);
			Yii::app()->end();
		}

		public function actionAddLinkToCart()
		{
			$singleLinkId = Yii::app()->request->getPost('linkId');
			$folderId = Yii::app()->request->getPost('folderId');
			$userId = Yii::app()->user->getId();
			if (isset($folderId) && isset($userId))
			{
				$linkIds = FolderStorage::model()->getChildLinkIds($folderId);
				if (isset($linkIds))
					foreach ($linkIds as $linkId)
						UserLinkCartStorage::addLink($userId, $linkId);
			}
			else if (isset($singleLinkId) && isset($singleLinkId))
				UserLinkCartStorage::addLink($userId, $singleLinkId);
			Yii::app()->end();
		}

		public function actionAddLinkToPage()
		{
			$pageId = Yii::app()->request->getPost('pageId');
			$linkInCartId = Yii::app()->request->getPost('linkInCartId');
			$order = intval(Yii::app()->request->getPost('order'));
			if (isset($pageId) && isset($linkInCartId))
			{
				$selectedPage = QPageStorage::model()->findByPk($pageId);
				$selectedPage->addLink($linkInCartId, $order);
			}
			Yii::app()->end();
		}

		public function actionAddAllLinksToPage()
		{
			$pageId = Yii::app()->request->getPost('pageId');
			$userId = Yii::app()->user->getId();
			if (isset($pageId) && isset($userId))
			{
				$selectedPage = QPageStorage::model()->findByPk($pageId);
				$linkRecords = UserLinkCartStorage::model()->findAll('id_user=?', array($userId));
				foreach ($linkRecords as $linkRecord)
					$selectedPage->addLink($linkRecord->id, -1);
			}
			Yii::app()->end();
		}

		public function actionDeleteLinkFromCart()
		{
			$linkInCartId = Yii::app()->request->getPost('linkInCartId');
			if (isset($linkInCartId))
				UserLinkCartStorage::deleteLink($linkInCartId);
			Yii::app()->end();
		}

		public function actionDeleteLinkFromPage()
		{
			$linkInPageId = Yii::app()->request->getPost('linkInPageId');
			if (isset($linkInPageId))
			{
				$linkRecord = QPageLinkStorage::model()->findByPk($linkInPageId);
				$pageId = $linkRecord->id_page;
				QPageLinkStorage::deleteLink($linkInPageId);
				$pageRecord = QPageStorage::model()->findByPk($pageId);
				$pageRecord->rebuildLinkList(-1);
			}
			Yii::app()->end();
		}

		public function actionSetPageLinkOrder()
		{
			$pageId = Yii::app()->request->getPost('pageId');
			$linkInPageId = Yii::app()->request->getPost('linkInPageId');
			$order = intval(Yii::app()->request->getPost('order'));
			if (isset($pageId) && isset($linkInPageId) && isset($order))
			{
				$selectedPage = QPageStorage::model()->findByPk($pageId);
				$selectedPage->setLinkOrder($linkInPageId, $order);
			}
			Yii::app()->end();
		}

		public function actionGetPageContent()
		{
			$selectedPageId = Yii::app()->request->getPost('selectedPageId');
			if (isset($selectedPageId))
			{
				$page = QPageStorage::model()->findByPk($selectedPageId);
				$logos = QPageStorage::getPageLogoList();
				$this->renderPartial('pageContent', array('page' => $page, 'logos' => $logos), false, true);
			}
		}

		public function actionGetPageLinks()
		{
			$selectedPageId = Yii::app()->request->getPost('selectedPageId');
			if (isset($selectedPageId))
			{
				$page = QPageStorage::model()->findByPk($selectedPageId);
				$links = $page->getPageLinks();
				if (isset($links))
					$this->renderPartial('pageLinks', array('links' => $links), false, true);
			}
		}

		public function actionEmailPageDialog()
		{
			$userId = Yii::app()->user->getId();
			$selectedPageId = Yii::app()->request->getPost('selectedPageId');
			if (isset($userId))
				$availableEmails = UserRecipientStorage::getRecipientsByUser($userId);
			if (!isset($availableEmails))
				$availableEmails = null;
			if (isset($selectedPageId))
			{
				$page = QPageStorage::model()->findByPk($selectedPageId);
				if (isset($page))
					$this->renderPartial('emailDialog', array('page' => $page, 'availableEmails' => $availableEmails), false, true);
			}
		}

		/////////////////Service Part///////////////////////////
		public function actions()
		{
			return array(
				'quote' => array(
					'class' => 'CWebServiceAction',
					'classMap' => array(
						'QPageRecord' => 'QPageRecord',
						'QPageLinkRecord' => 'QPageLinkRecord',
					),
				),
			);
		}

		protected function authenticateBySession($sessionKey)
		{
			$data = Yii::app()->cacheDB->get($sessionKey);
			if ($data !== FALSE)
				return TRUE;
			else
				return FALSE;
		}

		/**
		 * @param string $login
		 * @param string $password
		 * @return string session key
		 * @soap
		 */
		public function getSessionKey($login, $password)
		{
			$identity = new UserIdentity($login, $password);
			$identity->authenticate();
			if ($identity->errorCode === UserIdentity::ERROR_NONE)
			{
				$sessionKey = strval(md5(mt_rand()));
				Yii::app()->cacheDB->set($sessionKey, $login, (60 * 60 * 24 * 7));
				return $sessionKey;
			}
			else
				return '';
		}

		/**
		 * @param string Session Key
		 * @return string[]
		 * @soap
		 */
		public function getPageLogos($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$logos = QPageStorage::getPageLogoList();
				if (isset($logos))
					foreach ($logos as $logo)
						$pageLogos[] = str_replace('data:image/png;base64,', '', $logo);
			}
			if (isset($pageLogos))
				return $pageLogos;
			else
				return null;
		}

		/**
		 * @param string Session Key
		 * @return QPageRecord[]
		 * @soap
		 */
		public function getAllPages($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$command = Yii::app()->db->createCommand("call sp_get_all_qpages()");
				$pageRecords = $command->queryAll();
				foreach ($pageRecords as $pageRecord)
				{
					$page = new QPageRecord();
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
					$page->groups = $pageRecord['groups'];
					$pages[] = $page;
				}
			}
			if (isset($pages))
				return $pages;
			else
				return null;
		}

		/**
		 * @param string Session Key
		 * @param string Login
		 * @return QPageRecord[]
		 * @soap
		 */
		public function getPagesByUser($sessionKey, $login)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$userRecord = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (isset($userRecord))
				{
					$pageRecords = QPageStorage::model()->getByOwner($userRecord->id);
					foreach ($pageRecords as $pageRecord)
					{
						$page = new QPageRecord();
						$page->id = $pageRecord['id'];
						$page->title = $pageRecord['title'];
						$page->isEmail = $pageRecord['is_email'];
						$page->createDate = $pageRecord['create_date'];
						$pages[] = $page;
					}
				}
			}
			if (isset($pages))
				return $pages;
			else
				return null;
		}

		/**
		 * @param string Session Key
		 * @param string Page Id
		 * @return QPageRecord
		 * @soap
		 */
		public function getPageContent($sessionKey, $pageId)
		{
			$page = null;
			if ($this->authenticateBySession($sessionKey))
			{
				$pageRecord = QPageStorage::model()->findByPk($pageId);
				if (isset($pageRecord))
				{
					$page = new QPageRecord();
					$page->id = $pageRecord->id;
					$page->title = $pageRecord->title;
					$page->isEmail = $pageRecord->is_email;
					$page->url = Yii::app()->createAbsoluteUrl('qpage/show', array('id' => $pageRecord->id));
					$page->createDate = $pageRecord->create_date;
					$page->expirationDate = $pageRecord->expiration_date;
					$page->subtitle = $pageRecord->subtitle;
					$page->header = $pageRecord->header;
					$page->footer = $pageRecord->footer;
					$page->isRestricted = $pageRecord->restricted;
					$page->disableBanners = $pageRecord->disable_banners;
					$page->disableWidgets = $pageRecord->disable_widgets;
					$page->showLinksAsUrl = $pageRecord->show_links_as_url;
					$page->recordActivity = $pageRecord->record_activity;
					$page->pinCode = $pageRecord->pin_code;
					$page->activityEmailCopy = $pageRecord->activity_email_copy;
					$page->logo = isset($pageRecord->logo) ? str_replace('data:image/png;base64,', '', $pageRecord->logo) : null;

					$linkRecords = $pageRecord->getPageLinks();
					if (isset($linkRecords))
					{
						foreach ($linkRecords as $linkRecord)
						{
							$idArray = explode('---', $linkRecord['id']);
							if (isset($idArray) && count($idArray) == 2)
							{
								$link = new QPageLinkRecord();
								$link->id = str_replace('id', '', $idArray[0]);
								$link->parentId = $pageRecord['id'];
								$link->linkId = str_replace('link', '', $idArray[1]);
								$link->libraryId = $linkRecord['id_library'];
								$link->name = $linkRecord['name'];
								$link->fileName = $linkRecord['file_name'];
								$link->libraryName = $linkRecord['library'];
								$link->logo = $linkRecord['file_type'];
								$page->links[] = $link;
							}
						}
					}
				}
			}
			return $page;
		}

		/**
		 * @param string Session Key
		 * @param string Login
		 * @return QPageLinkRecord[]
		 * @soap
		 */
		public function getLinkCart($sessionKey, $login)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$userRecord = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (isset($userRecord))
				{
					$linkRecords = UserLinkCartStorage::getLinksByUser($userRecord->id);
					if (isset($linkRecords))
					{
						foreach ($linkRecords as $linkRecord)
						{
							$idArray = explode('---', $linkRecord['id']);
							if (isset($idArray) && count($idArray) == 2)
							{
								$link = new QPageLinkRecord();
								$link->id = str_replace('cart', '', $idArray[0]);
								$link->parentId = null;
								$link->linkId = str_replace('link', '', $idArray[1]);
								$link->libraryId = $linkRecord['id_library'];
								$link->name = isset($linkRecord['name']) && $linkRecord['name'] != '' ? $linkRecord['name'] : (isset($linkRecord['file_name']) ? $linkRecord['file_name'] : '');
								$link->fileName = $linkRecord['file_name'];
								$link->libraryName = $linkRecord['library'];
								$link->logo = $linkRecord['file_type'];
								$links[] = $link;
							}
						}
					}
				}
			}
			if (isset($links))
				return $links;
			else
				return null;
		}

		/**
		 * @param string Session Key
		 * @param string LinkId
		 * @return bool
		 * @soap
		 */
		public function isLinkAvailable($sessionKey, $linkId)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$linkRecord = LinkStorage::getLinkById($linkId);
				return isset($linkRecord);
			}
			return false;
		}

		/**
		 * @param string Session Key
		 * @param string FolderId
		 * @return bool
		 * @soap
		 */
		public function isFolderAvailable($sessionKey, $folderId)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$folderRecord = FolderStorage::model()->findByPk($folderId);
				return isset($folderRecord);
			}
			return false;
		}

		/**
		 * @param string Session Key
		 * @param string login
		 * @param string linkId
		 * @param string folderId
		 * @soap
		 */
		public function addLinkToCart($sessionKey, $login, $linkId, $folderId)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$userRecord = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (isset($userRecord))
				{
					if (isset($linkId))
						UserLinkCartStorage::addLink($userRecord->id, $linkId);
					else if (isset($folderId))
					{
						$linkIds = FolderStorage::model()->getChildLinkIds($folderId);
						if (isset($linkIds))
							foreach ($linkIds as $linkId)
								UserLinkCartStorage::addLink($userRecord->id, $linkId);
					}
				}
			}
		}

		/**
		 * @param string Session Key
		 * @param string login
		 * @param string linkId
		 * @param string subtitle
		 * @param string createData
		 * @param int expiresInDays
		 * @param bool restricted
		 * @param string logo
		 * @param bool disableBanners
		 * @param bool disableWidgets
		 * @param bool showLinksAsUrl
		 * @param bool recordActivity
		 * @param string pinCode
		 * @param string activityEmailCopy
		 * @return string
		 * @soap
		 */
		public function emailLink($sessionKey, $login, $linkId, $subtitle, $createDate, $expiresInDays, $restricted, $logo, $disableBanners, $disableWidgets, $showLinksAsUrl, $recordActivity, $pinCode, $activityEmailCopy)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$userRecord = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (isset($userRecord))
				{
					$logo = 'data:image/png;base64,' . $logo;
					$userId = $userRecord->id;
					$expirationDate = $expiresInDays > 0 ? date(Yii::app()->params['mysqlDateFormat'], strtotime(date("Y-m-d") . ' + ' . $expiresInDays . ' day')) : null;
					return QPageStorage::addPageLite($userId, $createDate, $subtitle, $logo, $expirationDate, $restricted, $disableBanners, $disableWidgets, $showLinksAsUrl, $recordActivity, $pinCode, $activityEmailCopy, $linkId)->getUrl();
				}
			}
			return null;
		}

		/**
		 * @param string Session Key
		 * @param string linkInCartId
		 * @soap
		 */
		public function deleteLinkFromCart($sessionKey, $linkInCartId)
		{
			if ($this->authenticateBySession($sessionKey))
				UserLinkCartStorage::deleteLink($linkInCartId);
		}

		/**
		 * @param string Session Key
		 * @param string login
		 * @soap
		 */
		public function deleteAllFromCart($sessionKey, $login)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$userRecord = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (isset($userRecord))
					UserLinkCartStorage::deleteLinksByUser($userRecord->id);
			}
		}

		/**
		 * @param string Session Key
		 * @param string[] linkInCartIds
		 * @param string pageId
		 * @param int order
		 * @soap
		 */
		public function addLinksToPage($sessionKey, $linkInCartIds, $pageId, $order)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$selectedPage = QPageStorage::model()->findByPk($pageId);
				foreach ($linkInCartIds as $linkInCartId)
					$selectedPage->addLink($linkInCartId, $order);
			}
		}

		/**
		 * @param string Session Key
		 * @param string pageId
		 * @param string linkInPageId
		 * @param int firstLinkOrder
		 * @soap
		 */
		public function setPageLinkOrder($sessionKey, $pageId, $linkInPageId, $firstLinkOrder)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$selectedPage = QPageStorage::model()->findByPk($pageId);
				$selectedPage->setLinkOrder($linkInPageId, $firstLinkOrder);
			}
		}

		/**
		 * @param string Session Key
		 * @param string linkInPageId
		 * @soap
		 */
		public function deleteLinkFromPage($sessionKey, $linkInPageId)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$linkRecord = QPageLinkStorage::model()->findByPk($linkInPageId);
				$pageId = $linkRecord->id_page;
				QPageLinkStorage::deleteLink($linkInPageId);
				$pageRecord = QPageStorage::model()->findByPk($pageId);
				$pageRecord->rebuildLinkList(-1);
			}
		}

		/**
		 * @param string Session Key
		 * @param string login
		 * @param string title
		 * @param string createDate
		 * @return string
		 * @soap
		 */
		public function addPage($sessionKey, $login, $title, $createDate)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$userRecord = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (isset($userRecord))
					return QPageStorage::addPage($userRecord->id, $title, $createDate);
			}
			return null;
		}

		/**
		 * @param string Session Key
		 * @param string pageId
		 * @param string title
		 * @param string description
		 * @param string header
		 * @param string footer
		 * @param string expirationDate
		 * @param bool requireLogin
		 * @param bool disableBanners
		 * @param bool disableWidgets
		 * @param bool showLinksAsUrl
		 * @param bool recordActivity
		 * @param string pinCode
		 * @param string activityEmailCopy
		 * @param string logo
		 * @soap
		 */
		public function savePageContent($sessionKey, $pageId, $title, $description, $header, $footer, $expirationDate, $requireLogin, $disableBanners, $disableWidgets, $showLinksAsUrl, $recordActivity, $pinCode, $activityEmailCopy, $logo)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				if (isset($expirationDate) && $expirationDate != '')
					$expirationDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($expirationDate));
				$logo = 'data:image/png;base64,' . $logo;
				QPageStorage::savePage($pageId, $title, $description, $expirationDate, $logo, $header, $footer, $requireLogin, $disableBanners, $disableWidgets, $showLinksAsUrl, $recordActivity, $pinCode, $activityEmailCopy);
			}
		}

		/**
		 * @param string Session Key
		 * @param string login
		 * @param string Cloned Page Id
		 * @param string title
		 * @param string createDate
		 * @return string
		 * @soap
		 */
		public function clonePage($sessionKey, $login, $clonedPageId, $title, $createDate)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$userRecord = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (isset($userRecord))
					return QPageStorage::clonePage($userRecord->id, $title, $createDate, $clonedPageId);
			}
			return null;
		}

		/**
		 * @param string Session Key
		 * @param string[] Page Ids
		 * @soap
		 */
		public function deletePages($sessionKey, $pageIds)
		{
			if ($this->authenticateBySession($sessionKey))
				foreach ($pageIds as $pageId)
					QPageStorage::deletePage($pageId);
		}

		/**
		 * @param string Session Key
		 * @param string login
		 * @param string pageId
		 * @param int order
		 * @soap
		 */
		public function setPageOrder($sessionKey, $login, $pageId, $order)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$userRecord = UserStorage::model()->find('LOWER(login)=?', array(strtolower($login)));
				$userId = $userRecord->id;
				QPageStorage::setPageOrder($userId, $pageId, $order);
			}
		}
		/////////////////Service Part///////////////////////////
	}
