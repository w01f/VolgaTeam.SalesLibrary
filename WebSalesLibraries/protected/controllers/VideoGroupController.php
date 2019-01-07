<?

	/**
	 * Class VideoGroupController
	 */
	class VideoGroupController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'video_group');
		}

		public function actionGetState()
		{
			$groupId = Yii::app()->request->getPost('groupId');
			$shortcutId = Yii::app()->request->getPost('shortcutId');
			$userId = UserIdentity::getCurrentUserId();

			/** @var \application\models\video_group\models\VideoGroupModel $videoGroupModel */
			$videoGroupModel = VideoGroupRecord::model()->getModel($groupId, $shortcutId, $userId);
			echo CJSON::encode($videoGroupModel->state);
		}

		public function actionUpdateItemState()
		{
			$groupId = Yii::app()->request->getPost('groupId');
			$shortcutId = Yii::app()->request->getPost('shortcutId');
			$itemStateEncoded = Yii::app()->request->getPost('itemState');

			if (isset($itemStateEncoded))
			{
				/** @var $itemState \application\models\video_group\models\VideoItemState */
				$itemState = new \application\models\video_group\models\VideoItemState();
				Utils::loadFromJson($itemState, CJSON::encode($itemStateEncoded));
				$userId = UserIdentity::getCurrentUserId();
				/** @var \application\models\video_group\models\VideoGroupModel $videoGroupModel */
				VideoGroupRecord::model()->updateVideoItemState($groupId, $shortcutId, $userId, $itemState);

				echo CJSON::encode(array("success" => true));
			}
		}
	}
