<?php
	class QpageController extends IsdController
	{
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
					$page = QPageStorage::model()->findByPk($pinCodedModel->pageId);
					$this->redirect($page->getUrlInternal());
				}
				else
					$this->render('login', array('formData' => $pinCodedModel));
			}
			else
			{
				$accessed = false;
				$pageId = Yii::app()->request->getQuery('id');
				if (isset($pageId))
				{
					$page = QPageStorage::model()->findByPk($pageId);
					if (isset($page) && !$page->isExpired())
					{
						if (isset($page->pin_code))
						{
							$pinCodedModel->pageId = $pageId;
							$this->render('login', array('formData' => $pinCodedModel));
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
				$pageRecord = QPageStorage::model()->findByPk($pageId);
				if (isset($pageRecord))
					$pageOwner = UserStorage::model()->findByPk($pageRecord->id_owner);
				$linkRecord = LinkStorage::getLinkById($linkId);
				if (isset($linkRecord) && isset($pageRecord) && isset($pageOwner) && $pageRecord->record_activity)
				{
					$ip = Yii::app()->request->getUserHostAddress();
					$message = Yii::app()->email;
					$message->to = $pageOwner->email;
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

		private function renderPage($pageId, $protected)
		{
			$page = QPageStorage::model()->findByPk($pageId);
			if (isset($page) && (($page->restricted && $protected) || !$page->restricted))
			{
				$this->pageTitle = $page->title;
				$tickerRecords = null;
				if ($page->show_ticker)
					$tickerRecords = TickerLinkStorage::getLinks();
				$this->render('index', array('page' => $page, 'tickerRecords' => $tickerRecords));
			}
			else
			{
				$this->pageTitle = 'quickSITE';
				$this->render('unauthorized');
			}
		}
	}
