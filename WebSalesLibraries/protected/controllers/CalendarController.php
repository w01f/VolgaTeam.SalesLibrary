<?

	/**
	 * Class CalendarController
	 */
	class CalendarController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'calendar');
		}

		public function actionGetEvents()
		{
			$calendarId = Yii::app()->request->getPost('calendarId');
			$shortcutId = Yii::app()->request->getPost('shortcutId');

			$eventModels = CalendarEventRecord::model()->getModels($calendarId, $shortcutId);
			echo CJSON::encode($eventModels);
		}

		public function actionUpdateEvent()
		{
			$eventId = Yii::app()->request->getPost('eventId');
			$eventDataEncoded = Yii::app()->request->getPost('eventData');
			CalendarEventRecord::model()->updateEventData($eventId, $eventDataEncoded);
			echo CJSON::encode(array("success" => true));
		}

		public function actionAddEvent()
		{
			$calendarId = Yii::app()->request->getPost('calendarId');
			$shortcutId = Yii::app()->request->getPost('shortcutId');
			$eventDataEncoded = Yii::app()->request->getPost('eventData');
			$eventModel = CalendarEventRecord::model()->addEventData($calendarId, $shortcutId, $eventDataEncoded);
			echo CJSON::encode($eventModel);
		}

		public function actionDeleteEvent()
		{
			$eventId = Yii::app()->request->getPost('eventId');
			CalendarEventRecord::model()->deleteEvent($eventId);
			echo CJSON::encode(array("success" => true));
		}

		public function actionGetEventEditDialog()
		{
			$this->renderPartial('editEvent', array(), false, true);
		}
	}
