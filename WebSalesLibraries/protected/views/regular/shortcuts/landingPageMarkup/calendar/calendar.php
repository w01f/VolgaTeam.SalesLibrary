<?

	use application\models\shortcuts\models\landing_page\regular_markup\calendar\CalendarBlock;

	/** @var $contentBlock CalendarBlock */

	$blockId = sprintf('calendar-%s', $contentBlock->id);
?>
<div id="<? echo $blockId; ?>" class="landing-page-calendar">
    <div class="service-data calendar-settings">
		<? echo CJSON::encode(array(
			'defaultView' => $contentBlock->settings->defaultView,
			'defaultDate' => $contentBlock->settings->defaultDate,
			'allowEdit' => $contentBlock->settings->allowEdit
		)); ?>
    </div>
</div>