<?php

	/**
	 * Class QPageController
	 */
	class QPageController extends IsdController
	{
		/** return array */
		protected function getPublicActionIds()
		{
			return array(
				'show',
				'getPublic',
			);
		}

		protected function beforeAction($action)
		{
			/** @var CHttpRequest $request */
			$request = Yii::app()->request;

			if (UserIdentity::isUserAuthorized())
				return true;

			if (!(in_array($action->id, $this->getPublicActionIds())))
			{
				$useForThumbnail = $request->getQuery('useForThumbnail') !== null;
				if (!$useForThumbnail)
				{
					Yii::app()->user->loginRequired();
					return false;
				}
			}

			return true;
		}

		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'qpage');
		}

		public function actionShow()
		{
			$pinCodedModel = new PinCodeForm();
			$attributes = Yii::app()->request->getPost('PinCodeForm');
			if (isset($attributes))
			{
				$pinCodedModel->attributes = $attributes;
				$pinCodedModel->pageId = $attributes['pageId'];
				$pinCodedModel->pinCode = $attributes['pinCode'];
				if ($pinCodedModel->validate())
				{
					/** @var $page QPageRecord */
					$page = QPageRecord::model()->findByPk($pinCodedModel->pageId);
					$this->redirect($page->getUrlInternal(false));
				}
				else
				{
					/** @var $page QPageRecord */
					$page = QPageRecord::model()->findByPk($attributes['pageId']);
					$this->render('login', array('formData' => $pinCodedModel, 'page' => $page));
				}
			}
			else
			{
				$accessed = false;
				$pageId = Yii::app()->request->getQuery('id');
				$useForThumbnail = Yii::app()->request->getQuery('useForThumbnail') !== null;
				if (isset($pageId))
				{
					/** @var $page QPageRecord */
					$page = QPageRecord::model()->findByPk($pageId);
					if (isset($page) && !$page->isExpired())
					{
						if (!$useForThumbnail && isset($page->pin_code))
						{
							$pinCodedModel->pageId = $pageId;
							$this->render('login', array('formData' => $pinCodedModel, 'page' => $page));
						}
						else
							$this->redirect($page->getUrlInternal($useForThumbnail));
						$accessed = true;
					}
				}
				if (!$accessed)
				{
					$this->pageTitle = 'quickSITE';
					$this->render('unauthorized');
				}
			}
		}

		public function actionGetProtected()
		{
			$pageId = Yii::app()->request->getQuery('id');
			$useForThumbnail = Yii::app()->request->getQuery('useForThumbnail') !== null;
			if (isset($pageId))
				$this->renderPage($pageId, true, $useForThumbnail);
		}

		public function actionGetPublic()
		{
			$pageId = Yii::app()->request->getQuery('id');
			if (isset($pageId))
				$this->renderPage($pageId, false, true);
		}

		public function actionRecordActivity()
		{
			$pageId = Yii::app()->request->getPost('pageId');
			$userEmail = Yii::app()->request->getPost('userEmail');
			$linkId = Yii::app()->request->getPost('linkId');
			if (isset($linkId) && isset($pageId))
			{
				/** @var $pageRecord QPageRecord */
				$pageRecord = QPageRecord::model()->findByPk($pageId);
				/** @var $pageOwner UserRecord */
				if (isset($pageRecord))
					$pageOwner = UserRecord::model()->findByPk($pageRecord->id_owner);
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord) && isset($pageRecord) && isset($pageOwner) && $pageRecord->record_activity)
				{
					$ip = Yii::app()->request->getUserHostAddress();
					$message = Yii::app()->email;
					$message->to = $pageOwner->email;
					if (isset($pageRecord->activity_email_copy) && $pageRecord->activity_email_copy != '')
						$message->cc = $pageRecord->activity_email_copy;
					$message->subject = 'quickSITE Notification';
					$message->from = Yii::app()->params['email']['from'];
					$message->message = 'Someone just viewed a file on this quickSITE: ' . $pageRecord->getUrl() .
						'<br>Link Name clicked: ' . $linkRecord->name .
						(isset($userEmail) && $userEmail != '' ? ('<br>' . $userEmail) : '') .
						'<br>IP Address: ' . $ip;
					$message->send();
				}
			}
		}

		/**
		 * @param $pageId string
		 * @param $protected boolean
		 * @param $useForThumbnail boolean
		 */
		private function renderPage($pageId, $protected, $useForThumbnail)
		{
			/** @var $page QPageRecord */
			$page = QPageRecord::model()->findByPk($pageId);
			if (isset($page))
				StatisticActivityRecord::writeQPageActivity($page->id, 'QSite', sprintf('Open %s', $protected ? 'secure' : 'public'), array(
					'id' => $page->id,
					'qsite title' => $page->title,
					'url' => $page->getUrl(),
				));
			if (isset($page) && (($page->restricted && $protected) || !$page->restricted || $useForThumbnail))
			{
				$this->pageTitle = $page->title;
				if ($protected && !$useForThumbnail)
					$menuGroups = ShortcutsManager::getAvailableGroups($this->isPhone);
				else
					$menuGroups = array();
				$this->render('index', array('menuGroups' => $menuGroups, 'page' => $page));
			}
			else
			{
				$this->pageTitle = 'quickSITE';
				$this->render('unauthorized');
			}
		}
	}
