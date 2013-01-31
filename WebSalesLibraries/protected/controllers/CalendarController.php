<?php
	class CalendarController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'calendar');
		}

		public function actionGetCalendarView()
		{
			if (file_exists(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..' . DIRECTORY_SEPARATOR . 'schedule.php'))
			{
				$schedule = require(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..' . DIRECTORY_SEPARATOR . 'schedule.php');
				$this->renderPartial('calendarView', array('schedule' => $schedule), false, true);
			}
		}
	}