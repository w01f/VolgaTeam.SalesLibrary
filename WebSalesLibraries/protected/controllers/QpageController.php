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
					$this->redirect($page->getUrlInternal());
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
				if (isset($pageId))
				{
					/** @var $page QPageRecord */
					$page = QPageRecord::model()->findByPk($pageId);
					if (isset($page) && !$page->isExpired())
					{
						if (isset($page->pin_code))
						{
							$pinCodedModel->pageId = $pageId;
							$this->render('login', array('formData' => $pinCodedModel, 'page' => $page));
						}
						else
							$this->redirect($page->getUrlInternal());
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
			if (isset($pageId))
				$this->renderPage($pageId, true);
		}

		public function actionGetPublic()
		{
			$pageId = Yii::app()->request->getQuery('id');
			if (isset($pageId))
				$this->renderPage($pageId, false);
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
		 * @param $pageId
		 * @param $protected
		 */
		private function renderPage($pageId, $protected)
		{
			/** @var $page QPageRecord */
			$page = QPageRecord::model()->findByPk($pageId);
			if (isset($page))
				StatisticActivityRecord::writeQPageActivity($page->id, 'QSite', sprintf('Open %s', $protected ? 'secure' : 'public'), array(
					'id' => $page->id,
					'qsite title' => $page->title,
					'url' => $page->getUrl(),
				));
			if (isset($page) && (($page->restricted && $protected) || !$page->restricted))
			{
				$this->pageTitle = $page->title;
				if ($protected)
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
