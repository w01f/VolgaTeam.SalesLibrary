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
			$eventModel = CalendarEventRecord::model()->updateEventData($eventId, $eventDataEncoded);

			$emailSettingsEncoded = Yii::app()->request->getPost('emailSettings');
			$emailSettings = \application\models\calendar\models\EmailSettings::createDefault();
			\Utils::loadFromJson($emailSettings, CJSON::encode($emailSettingsEncoded));
			$this->sendEmail('update', $eventModel, $emailSettings);

			echo CJSON::encode(array("success" => true));
		}

		public function actionAddEvent()
		{
			$calendarId = Yii::app()->request->getPost('calendarId');
			$shortcutId = Yii::app()->request->getPost('shortcutId');
			$eventDataEncoded = Yii::app()->request->getPost('eventData');
			$eventModel = CalendarEventRecord::model()->addEventData($calendarId, $shortcutId, $eventDataEncoded);

			$emailSettingsEncoded = Yii::app()->request->getPost('emailSettings');
			$emailSettings = \application\models\calendar\models\EmailSettings::createDefault();
			\Utils::loadFromJson($emailSettings, CJSON::encode($emailSettingsEncoded));
			$this->sendEmail('add', $eventModel, $emailSettings);

			echo CJSON::encode($eventModel);
		}

		public function actionDeleteEvent()
		{
			$eventId = Yii::app()->request->getPost('eventId');
			$eventModel = CalendarEventRecord::model()->getModel($eventId);
			CalendarEventRecord::model()->deleteEvent($eventId);

			$emailSettingsEncoded = Yii::app()->request->getPost('emailSettings');
			$emailSettings = \application\models\calendar\models\EmailSettings::createDefault();
			\Utils::loadFromJson($emailSettings, CJSON::encode($emailSettingsEncoded));
			$this->sendEmail('delete', $eventModel, $emailSettings);

			echo CJSON::encode(array("success" => true));
		}

		public function actionGetEventEditDialog()
		{
			$this->renderPartial('editEvent', array(), false, true);
		}

		/**
		 * @param $emailType string
		 * @param $event \application\models\calendar\models\EventModel
		 * @param $emailSettings \application\models\calendar\models\EmailSettings
		 */
		private function sendEmail($emailType, $event, $emailSettings)
		{
			if (!$emailSettings->enabled)
				return;

			$subject = '';
			switch ($emailType)
			{
				case 'add':
					$subject = 'Meeting Added';
					break;
				case 'update':
					$subject = 'Meeting Changed';
					break;
				case 'delete':
					$subject = 'Meeting Deleted';
					break;
			};

			$eventDateStart = date(\Yii::app()->params['outputDateFormat'], strtotime($event->start));
			$eventTimeStart = date(\Yii::app()->params['outputTimeFormat'], strtotime($event->start));

			$eventDateEnd = date(\Yii::app()->params['outputDateFormat'], strtotime($event->end));
			$eventTimeEnd = date(\Yii::app()->params['outputTimeFormat'], strtotime($event->end));

			if ($eventDateStart == $eventDateEnd)
				$date = $eventDateStart;
			else
				$date = $eventDateStart . ' - ' . $eventDateEnd;

			if ($eventTimeStart == $eventTimeEnd)
				$time = $eventTimeStart;
			else
				$time = $eventTimeStart . ' - ' . $eventTimeEnd;

			$message = Yii::app()->email;
			$message->to = $emailSettings->to;
			$message->subject = $subject;
			$message->from = $emailSettings->from;
			$message->message = sprintf("Day: %s<br>Time: %s<br><br>Details: %s",
				$date,
				$time,
				$event->title);
			$message->send();
		}
	}
