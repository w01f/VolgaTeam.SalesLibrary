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
			$userId = UserIdentity::getCurrentUserId();
			$libraryId = Yii::app()->request->getPost('libraryId');
			UserTabRecord::addLibraryTab($userId, $libraryId);
			echo CJSON::encode(array('result' => true));
			Yii::app()->end();
		}

		public function actionDeleteLibraryTab()
		{
			$userId = UserIdentity::getCurrentUserId();
			$libraryId = Yii::app()->request->getPost('libraryId');
			UserTabRecord::deleteTab($userId, $libraryId);
			echo CJSON::encode(array('result' => true));
			Yii::app()->end();
		}
	}