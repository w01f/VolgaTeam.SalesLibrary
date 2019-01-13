<?

	namespace application\models\calendar\models;


	class EventModel
	{
		public $id;
		public $calendarId;
		public $shortcutId;
		public $title;
		public $start;
		public $end;

		/**
		 * @param $record \CalendarEventRecord
		 * @return EventModel
		 */
		public static function fromRecord($record)
		{
			$model = new self();

			$model->id = $record->id;
			$model->calendarId = $record->id_calendar;
			$model->shortcutId = $record->id_shortcut;

			$eventData = new EventData();
			if (!empty($record->event_data))
				\Utils::loadFromJson($eventData, $record->event_data);

			$model->title = $eventData->title;
			$model->start = $eventData->start;
			$model->end = $eventData->end;

			return $model;
		}
	}