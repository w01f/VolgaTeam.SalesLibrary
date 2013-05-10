<?php
	class QpageController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'qpage');
		}

		public function actionShow()
		{
			$accessed = false;
			$pageId = Yii::app()->request->getQuery('id');
			if (isset($pageId))
			{
				$page = QPageStorage::model()->findByPk($pageId);
				if (isset($page) && !$page->isExpired())
				{
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
