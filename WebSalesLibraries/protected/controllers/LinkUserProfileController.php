<?

	/**
	 * Class LinkUserProfileController
	 */
	class LinkUserProfileController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'link_user_profile');
		}

		public function actionGetEditor()
		{
			$userId = UserIdentity::getCurrentUserId();
			$userProfile = UserProfileRecord::getProfile($userId);
			$this->renderPartial('userSettingsEditor', array('userProfile' => $userProfile), false, true);
		}

		public function actionApplyEditorValues()
		{
			$userId = UserIdentity::getCurrentUserId();
			$userProfileValues = Yii::app()->request->getPost('userProfile');
			$userProfile = new UserProfileModel(CJSON::decode($userProfileValues, true));
			UserProfileRecord::saveProfile($userId, $userProfile);
			Yii::app()->end();
		}
	}