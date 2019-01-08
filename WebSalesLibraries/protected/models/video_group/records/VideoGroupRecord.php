<?

	/**
	 * Class VideoGroupRecord
	 * @property mixed id
	 * @property mixed id_group
	 * @property mixed id_shortcut
	 * @property mixed id_user
	 * @property mixed state
	 */
	class VideoGroupRecord extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		/**
		 * @return string
		 */
		public function tableName()
		{
			return '{{video_group}}';
		}

		/**
		 * @param $idGroup string
		 * @param $idShortcut string
		 * @param $idUser string
		 * @return \application\models\video_group\models\VideoGroupModel
		 */
		public function getModel($idGroup, $idShortcut, $idUser)
		{
			$record = $this->find("id_group='" . $idGroup . "' and id_shortcut='" . $idShortcut . "' and id_user=" . $idUser);
			if (isset($record))
				return \application\models\video_group\models\VideoGroupModel::fromRecord($record);
			return \application\models\video_group\models\VideoGroupModel::createEmpty($idGroup, $idShortcut, $idUser);
		}

		/**
		 * @param $idGroup string
		 * @param $idShortcut string
		 * @param $idUser string
		 * @param $videoItemState \application\models\video_group\models\VideoItemState
		 */
		public function updateVideoItemState($idGroup, $idShortcut, $idUser, $videoItemState)
		{
			$record = $this->find("id_group='" . $idGroup . "' and id_shortcut='" . $idShortcut . "' and id_user=" . $idUser);
			if (!isset($record))
			{
				$record = new self();
				$record->id = uniqid();
				$record->id_group = $idGroup;
				$record->id_shortcut = $idShortcut;
				$record->id_user = $idUser;
			}
			$model = \application\models\video_group\models\VideoGroupModel::fromRecord($record);
			$model->state->itemStates[$videoItemState->index] = $videoItemState;
			$record->state = CJSON::encode($model->state);

			$record->save();
		}

		/**
		 * @param $libraryId
		 */
		public static function clearData()
		{
			self::model()->deleteAll('id_shortcut not in (select s.id from tbl_shortcut_link s)', array());
		}
	}