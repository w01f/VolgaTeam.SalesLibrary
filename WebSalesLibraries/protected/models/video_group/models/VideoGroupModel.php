<?

	namespace application\models\video_group\models;


	class VideoGroupModel
	{
		public $id;
		public $idUser;
		public $idShortcut;

		/** @var VideoGroupState */
		public $state;

		public function __construct()
		{
			$this->state = new VideoGroupState();
		}

		/**
		 * @param $idGroup string
		 * @param $idShortcut string
		 * @param $idUser string
		 * @return VideoGroupModel
		 */
		public static function createEmpty($idGroup, $idShortcut, $idUser)
		{
			$instance = new self();
			$instance->id = $idGroup;
			$instance->idShortcut = $idShortcut;
			$instance->idUser = $idUser;
			return $instance;
		}

		/**
		 * @param $record \VideoGroupRecord
		 * @return VideoGroupModel
		 */
		public static function fromRecord($record)
		{
			$model = new self();

			$model->id = $record->id;
			$model->idShortcut = $record->id_shortcut;
			$model->idUser = $record->id_user;
			$model->state = new VideoGroupState();

			if (!empty($record->state))
				\Utils::loadFromJson($model->state, $record->state);

			return $model;
		}
	}