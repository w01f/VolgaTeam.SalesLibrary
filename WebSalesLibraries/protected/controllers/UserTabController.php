<?

	/**
	 * Class UserTabController
	 */
	class UserTabController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'user_tab');
		}

		public function actionAddLibraryTab()
		{
			if (isset(Yii::app()->user))
			{
				$userId = Yii::app()->user->getId();
				$libraryId = Yii::app()->request->getPost('libraryId');
				UserTabRecord::addLibraryTab($userId, $libraryId);
				echo CJSON::encode(array('result' => true));
			}
			Yii::app()->end();
		}

		public function actionDeleteLibraryTab()
		{
			if (isset(Yii::app()->user))
			{
				$userId = Yii::app()->user->getId();
				$libraryId = Yii::app()->request->getPost('libraryId');
				UserTabRecord::deleteTab($userId, $libraryId);
				echo CJSON::encode(array('result' => true));
			}
			Yii::app()->end();
		}
	}