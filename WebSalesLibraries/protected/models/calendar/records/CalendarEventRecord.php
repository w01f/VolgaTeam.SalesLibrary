<?

	use application\models\calendar\models\EventData;
	use application\models\calendar\models\EventModel;

	/**
	 * Class CalendarEventRecord
	 * @property mixed id
	 * @property mixed id_calendar
	 * @property mixed id_shortcut
	 * @property mixed event_data
	 */
	class CalendarEventRecord extends CActiveRecord
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
			return '{{calendar_event}}';
		}

		/**
		 * @param $idCalendar string
		 * @param $idShortcut string
		 * @return EventModel[]
		 */
		public function getModels($idCalendar, $idShortcut)
		{
			$models = array();
			$records = $this->findAll("id_calendar='" . $idCalendar . "' and id_shortcut='" . $idShortcut . "'");
			foreach ($records as $record)
				$models[] = EventModel::fromRecord($record);
			return $models;
		}

		/**
		 * @param $idEvent string
		 * @return EventModel
		 */
		public function getModel($idEvent)
		{
			if (!empty($idEvent))
			{
				$record = $this->findByPk($idEvent);
				if (isset($record))
					return EventModel::fromRecord($record);
			}
			return null;
		}

		/**
		 * @param $idEvent string
		 * @param $eventModelEncoded array
		 */
		public function updateEventData($idEvent, $eventModelEncoded)
		{
			$record = $this->findByPk($idEvent);
			if (isset($record))
			{
				$eventData = new EventData();
				\Utils::loadFromJson($eventData, CJSON::encode($eventModelEncoded));
				$record->event_data = CJSON::encode($eventData);

				$record->save();
			}
		}

		/**
		 * @param $idCalendar string
		 * @param $idShortcut string
		 * @param $eventDataEncoded array
		 * @return EventModel
		 */
		public function addEventData($idCalendar, $idShortcut, $eventDataEncoded)
		{
			$record = new self();
			$record->id = uniqid();
			$record->id_calendar = $idCalendar;
			$record->id_shortcut = $idShortcut;
			$record->event_data = CJSON::encode($eventDataEncoded);
			$record->save();

			return EventModel::fromRecord($record);
		}

		/**
		 * @param $idEvent string
		 */
		public function deleteEvent($idEvent)
		{
			$this->deleteByPk($idEvent);
		}

		public static function clearData()
		{
			self::model()->deleteAll('id_shortcut not in (select s.id from tbl_shortcut_link s)', array());
		}
	}