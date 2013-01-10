<?php
class CalendarController extends IsdController
{
    public function getViewPath()
    {
        return YiiBase::getPathOfAlias($this->pathPrefix . 'calendar');
    }

    public function actionGetCalendarView()
    {
        $this->renderPartial('calendarView', array(), false, true);
    }
}

?>
