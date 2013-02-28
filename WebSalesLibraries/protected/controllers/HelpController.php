<?php
	class HelpController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'help');
		}

		public function actionGetPage()
		{
			$pageId = Yii::app()->request->getPost('pageId');
			if (isset($pageId))
			{
				$linkRecords = HelpLinkStorage::model()->findAll(array('order' => '`order`', 'condition' => 'id_page=:id_page', 'params' => array(':id_page' => $pageId)));
				if (isset($linkRecords))
				{
					$pageRecord = HelpPageStorage::model()->findByPk($pageId);
					if (isset($pageRecord))
					{
						$tabRecord = HelpTabStorage::model()->findByPk($pageRecord->id_tab);
						StatisticActivityStorage::WriteActivity('Help', 'Page Changed', array('Tab' => $tabRecord->name, 'Button' => $pageRecord->name));
					}
					$this->render('page', array('linkRecords' => $linkRecords));
				}
			}
		}

	}
