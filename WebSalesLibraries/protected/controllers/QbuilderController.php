<?php

	/**
	 * Class QBuilderController
	 */
	class QBuilderController extends SoapController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'qbuilder');
		}

		public function actionGetPageList()
		{
			$selectedPageId = Yii::app()->request->getPost('selectedPageId');
			$userId = Yii::app()->user->getId();
			if (isset($userId))
			{
				$pages = QPageRecord::model()->getByOwner($userId);
				$selectedPage = QPageRecord::model()->findByPk($selectedPageId);
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
			$linkRecord = LinkRecord::getLinkById($linkId);
			$logos = QPageRecord::getPageLogoList();
			if (isset($linkRecord))
			{
				$this->renderPartial('addPageLite', array('linkRecord' => $linkRecord, 'logos' => $logos), false, true);
				StatisticActivityRecord::WriteActivity('Email', 'Create Email', array('Name' => $linkRecord->name, 'File' => $linkRecord->file_name, 'Original Format' => $linkRecord->format));
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
					echo QPageRecord::clonePage($userId, $title, $createDate, $clonePageId);
				else
					echo QPageRecord::addPage($userId, $title, $createDate);
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
			if (isset($userId) && isset($createDate) && isset($linkId) && isset($expiresInDays) && isset($restricted))
			{
				$expirationDate = $expiresInDays > 0 ? date(Yii::app()->params['mysqlDateFormat'], strtotime(date("Y-m-d") . ' + ' . $expiresInDays . ' day')) : null;
				echo QPageRecord::addPageLite($userId, $createDate, $subtitle, $logo, $expirationDate, $restricted, $disableBanners, $disableWidgets, $showLinksAsUrl, $recordActivity, $pinCode, $activityEmailCopy, $linkId)->getUrl();

				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
					StatisticActivityRecord::WriteActivity('Email', 'Send Email', array('Name' => $linkRecord->name, 'File' => $linkRecord->file_name, 'Original Format' => $linkRecord->format));
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
			$userId = Yii::app()->user->getId();
			if (isset($userId))
			{
				$pages = QPageRecord::model()->getByOwner($userId);
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

			$recordActivity = Yii::app()->request->getPost('recordActivity');
			$recordActivity = isset($recordActivity) && $recordActivity == "true";

			$pinCode = Yii::app()->request->getPost('pinCode');
			if (isset($pinCode) && $pinCode == '')
				$pinCode = null;

			$activityEmailCopy = Yii::app()->request->getPost('activityEmailCopy');
			if (isset($activityEmailCopy) && $activityEmailCopy == '')
				$activityEmailCopy = null;

			if (isset($selectedPageId))
				QPageRecord::savePage($selectedPageId, $title, $description, $expirationDate, $logo, $header, $footer, $requireLogin, $disableBanners, $disableWidgets, $showLinksAsUrl, $recordActivity, $pinCode, $activityEmailCopy);
		}

		public function actionSetPageOrder()
		{
			$userId = Yii::app()->user->getId();
			$pageId = Yii::app()->request->getPost('pageId');
			$order = intval(Yii::app()->request->getPost('order'));
			if (isset($userId) && isset($pageId) && isset($order))
				QPageRecord::setPageOrder($userId, $pageId, $order);
			Yii::app()->end();
		}

		public function actionGetLinkCart()
		{
			$userId = Yii::app()->user->getId();
			if (isset($userId))
			{
				$links = UserLinkCartRecord::getLinksByUser($userId);
				$this->renderPartial('linkCart', array('links' => $links), false, true);
			}
		}

		public function actionClearLinkCart()
		{
			$userId = Yii::app()->user->getId();
			if (isset($userId))
				UserLinkCartRecord::deleteLinksByUser($userId);
			Yii::app()->end();
		}

		public function actionAddLinksToCart()
		{
			$linkIds = Yii::app()->request->getPost('linkIds');
			$folderId = Yii::app()->request->getPost('folderId');
			$userId = Yii::app()->user->getId();
			if (isset($folderId) && isset($userId))
			{
				$linkIds = FolderRecord::model()->getChildLinkIds($folderId);
				if (isset($linkIds))
					foreach ($linkIds as $linkId)
						UserLinkCartRecord::addLink($userId, $linkId);
			}
			else if (isset($linkIds) && isset($userId))
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
			$userId = Yii::app()->user->getId();
			if (isset($pageId) && isset($userId))
			{
				/** @var $selectedPage QPageRecord */
				$selectedPage = QPageRecord::model()->findByPk($pageId);
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
				$linkRecord = QPageLinkRecord::model()->findByPk($linkInPageId);
				$pageId = $linkRecord->id_page;
				QPageLinkRecord::deleteLink($linkInPageId);
				/** @var $pageRecord QPageRecord */
				$pageRecord = QPageRecord::model()->findByPk($pageId);
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
				$links = $page->getPageLinks();
				if (isset($links))
					$this->renderPartial('pageLinks', array('links' => $links), false, true);
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
		 * @return string[]
		 * @soap
		 */
		public function getPageLogos($sessionKey)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$logos = QPageRecord::getPageLogoList();
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
		 * @param string $sessionKey
		 * @return QPageModel[]
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
		 * @param string $sessionKey
		 * @param string $login
		 * @return QPageModel[]
		 * @soap
		 */
		public function getPagesByUser($sessionKey, $login)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var $userRecord UserRecord */
				$userRecord = UserRecord::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (isset($userRecord))
				{
					$pageRecords = QPageRecord::model()->getByOwner($userRecord->id);
					foreach ($pageRecords as $pageRecord)
					{
						$page = new QPageModel();
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
		 * @param string $sessionKey
		 * @param string $pageId
		 * @return QPageModel
		 * @soap
		 */
		public function getPageContent($sessionKey, $pageId)
		{
			$page = null;
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var $pageRecord QPageRecord */
				$pageRecord = QPageRecord::model()->findByPk($pageId);
				if (isset($pageRecord))
				{
					$page = new QPageModel();
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
								$link = new QPageLinkModel();
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
		 * @param string $sessionKey
		 * @param string $login
		 * @return QPageLinkModel[]
		 * @soap
		 */
		public function getLinkCart($sessionKey, $login)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$userRecord = UserRecord::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (isset($userRecord))
				{
					$linkRecords = UserLinkCartRecord::getLinksByUser($userRecord->id);
					if (isset($linkRecords))
					{
						foreach ($linkRecords as $linkRecord)
						{
							$idArray = explode('---', $linkRecord['id']);
							if (isset($idArray) && count($idArray) == 2)
							{
								$link = new QPageLinkModel();
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
		 * @param string $sessionKey
		 * @param string $linkId
		 * @return bool
		 * @soap
		 */
		public function isLinkAvailable($sessionKey, $linkId)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				return isset($linkRecord);
			}
			return false;
		}

		/**
		 * @param string $sessionKey
		 * @param string $folderId
		 * @return bool
		 * @soap
		 */
		public function isFolderAvailable($sessionKey, $folderId)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				$folderRecord = FolderRecord::model()->findByPk($folderId);
				return isset($folderRecord);
			}
			return false;
		}

		/**
		 * @param string $sessionKey
		 * @param string $login
		 * @param string $linkId
		 * @param string $folderId
		 * @soap
		 */
		public function addLinkToCart($sessionKey, $login, $linkId, $folderId)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var $userRecord UserRecord */
				$userRecord = UserRecord::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (isset($userRecord))
				{
					if (isset($linkId))
						UserLinkCartRecord::addLink($userRecord->id, $linkId);
					else if (isset($folderId))
					{
						$linkIds = FolderRecord::model()->getChildLinkIds($folderId);
						if (isset($linkIds))
							foreach ($linkIds as $linkId)
								UserLinkCartRecord::addLink($userRecord->id, $linkId);
					}
				}
			}
		}

		/**
		 * @param string $sessionKey
		 * @param string $login
		 * @param string $linkId
		 * @param string $subtitle
		 * @param string $createDate
		 * @param int $expiresInDays
		 * @param bool $restricted
		 * @param string $logo
		 * @param bool $disableBanners
		 * @param bool $disableWidgets
		 * @param bool $showLinksAsUrl
		 * @param bool $recordActivity
		 * @param string $pinCode
		 * @param string $activityEmailCopy
		 * @return string
		 * @soap
		 */
		public function emailLink($sessionKey, $login, $linkId, $subtitle, $createDate, $expiresInDays, $restricted, $logo, $disableBanners, $disableWidgets, $showLinksAsUrl, $recordActivity, $pinCode, $activityEmailCopy)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var $userRecord UserRecord */
				$userRecord = UserRecord::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (isset($userRecord))
				{
					$logo = 'data:image/png;base64,' . $logo;
					$userId = $userRecord->id;
					$expirationDate = $expiresInDays > 0 ? date(Yii::app()->params['mysqlDateFormat'], strtotime(date("Y-m-d") . ' + ' . $expiresInDays . ' day')) : null;
					return QPageRecord::addPageLite($userId, $createDate, $subtitle, $logo, $expirationDate, $restricted, $disableBanners, $disableWidgets, $showLinksAsUrl, $recordActivity, $pinCode, $activityEmailCopy, $linkId)->getUrl();
				}
			}
			return null;
		}

		/**
		 * @param string $sessionKey
		 * @param string $linkInCartId
		 * @soap
		 */
		public function deleteLinkFromCart($sessionKey, $linkInCartId)
		{
			if ($this->authenticateBySession($sessionKey))
				UserLinkCartRecord::deleteLink($linkInCartId);
		}

		/**
		 * @param string $sessionKey
		 * @param string $login
		 * @soap
		 */
		public function deleteAllFromCart($sessionKey, $login)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var $userRecord UserRecord */
				$userRecord = UserRecord::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (isset($userRecord))
					UserLinkCartRecord::deleteLinksByUser($userRecord->id);
			}
		}

		/**
		 * @param string $sessionKey
		 * @param string[] $linkInCartIds
		 * @param string $pageId
		 * @param int $order
		 * @soap
		 */
		public function addLinksToPage($sessionKey, $linkInCartIds, $pageId, $order)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var $selectedPage QPageRecord */
				$selectedPage = QPageRecord::model()->findByPk($pageId);
				foreach ($linkInCartIds as $linkInCartId)
					$selectedPage->addLink($linkInCartId, $order);
			}
		}

		/**
		 * @param string $sessionKey
		 * @param string $pageId
		 * @param string $linkInPageId
		 * @param int $firstLinkOrder
		 * @soap
		 */
		public function setPageLinkOrder($sessionKey, $pageId, $linkInPageId, $firstLinkOrder)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var $selectedPage QPageRecord */
				$selectedPage = QPageRecord::model()->findByPk($pageId);
				$selectedPage->setLinkOrder($linkInPageId, $firstLinkOrder);
			}
		}

		/**
		 * @param string $sessionKey
		 * @param string $linkInPageId
		 * @soap
		 */
		public function deleteLinkFromPage($sessionKey, $linkInPageId)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var $linkRecord QPageLinkRecord */
				$linkRecord = QPageLinkRecord::model()->findByPk($linkInPageId);
				$pageId = $linkRecord->id_page;
				QPageLinkRecord::deleteLink($linkInPageId);
				/** @var $pageRecord QPageRecord */
				$pageRecord = QPageRecord::model()->findByPk($pageId);
				$pageRecord->rebuildLinkList(-1);
			}
		}

		/**
		 * @param string $sessionKey
		 * @param string $login
		 * @param string $title
		 * @param string $createDate
		 * @return string
		 * @soap
		 */
		public function addPage($sessionKey, $login, $title, $createDate)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var $userRecord UserRecord */
				$userRecord = UserRecord::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (isset($userRecord))
					return QPageRecord::addPage($userRecord->id, $title, $createDate);
			}
			return null;
		}

		/**
		 * @param string $sessionKey
		 * @param string $pageId
		 * @param string $title
		 * @param string $description
		 * @param string $header
		 * @param string $footer
		 * @param string $expirationDate
		 * @param bool $requireLogin
		 * @param bool $disableBanners
		 * @param bool $disableWidgets
		 * @param bool $showLinksAsUrl
		 * @param bool $recordActivity
		 * @param string $pinCode
		 * @param string $activityEmailCopy
		 * @param string $logo
		 * @soap
		 */
		public function savePageContent($sessionKey, $pageId, $title, $description, $header, $footer, $expirationDate, $requireLogin, $disableBanners, $disableWidgets, $showLinksAsUrl, $recordActivity, $pinCode, $activityEmailCopy, $logo)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				if (isset($expirationDate) && $expirationDate != '')
					$expirationDate = date(Yii::app()->params['mysqlDateFormat'], strtotime($expirationDate));
				$logo = 'data:image/png;base64,' . $logo;
				QPageRecord::savePage($pageId, $title, $description, $expirationDate, $logo, $header, $footer, $requireLogin, $disableBanners, $disableWidgets, $showLinksAsUrl, $recordActivity, $pinCode, $activityEmailCopy);
			}
		}

		/**
		 * @param string $sessionKey
		 * @param string $login
		 * @param string $clonedPageId
		 * @param string $title
		 * @param string $createDate
		 * @return string
		 * @soap
		 */
		public function clonePage($sessionKey, $login, $clonedPageId, $title, $createDate)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var $userRecord UserRecord */
				$userRecord = UserRecord::model()->find('LOWER(login)=?', array(strtolower($login)));
				if (isset($userRecord))
					return QPageRecord::clonePage($userRecord->id, $title, $createDate, $clonedPageId);
			}
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

		/**
		 * @param string $sessionKey
		 * @param string $login
		 * @param string $pageId
		 * @param int $order
		 * @soap
		 */
		public function setPageOrder($sessionKey, $login, $pageId, $order)
		{
			if ($this->authenticateBySession($sessionKey))
			{
				/** @var $userRecord UserRecord */
				$userRecord = UserRecord::model()->find('LOWER(login)=?', array(strtolower($login)));
				$userId = $userRecord->id;
				QPageRecord::setPageOrder($userId, $pageId, $order);
			}
		}
		/////////////////Service Part///////////////////////////
	}
