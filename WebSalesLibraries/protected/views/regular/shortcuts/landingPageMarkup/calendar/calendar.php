<?

	use application\models\shortcuts\models\landing_page\regular_markup\calendar\CalendarBlock;

	/** @var $contentBlock CalendarBlock */

	$blockId = sprintf('calendar-%s', $contentBlock->id);
	$headerTextAppearance = $contentBlock->settings->headerStyle;
?>
<style>
    <? echo '#'.$blockId ?> .fc-toolbar .btn.active
    {
        -webkit-box-shadow: none;
        box-shadow: none;
    }

    <? echo '#'.$blockId ?> .fc-event.ui-state-disabled
    {
        opacity: 1;
    }

    <? echo '#'.$blockId ?> .fc-event
    {
        z-index: 999;
    }

    <? echo '#'.$blockId ?> .fc-toolbar .fc-left .btn
    {
        border-color: <? echo Utils::formatColor($contentBlock->settings->navigationButtonStyleLeft->borderColor);?>;
        color: <? echo Utils::formatColor($contentBlock->settings->navigationButtonStyleLeft->foreColorRegular);?>;
        background-color: <? echo Utils::formatColor($contentBlock->settings->navigationButtonStyleLeft->backColorRegular);?>;
    }

    <? echo '#'.$blockId ?> .fc-toolbar .fc-left .btn.active
    {
        color: <? echo Utils::formatColor($contentBlock->settings->navigationButtonStyleLeft->foreColorSelected);?>;
        background-color: <? echo Utils::formatColor($contentBlock->settings->navigationButtonStyleLeft->backColorSelected);?>;
    }

    <? echo '#'.$blockId ?> .fc-toolbar .fc-right .btn
    {
        border-color: <? echo Utils::formatColor($contentBlock->settings->navigationButtonStyleRight->borderColor);?>;
        color: <? echo Utils::formatColor($contentBlock->settings->navigationButtonStyleRight->foreColorRegular);?>;
        background-color: <? echo Utils::formatColor($contentBlock->settings->navigationButtonStyleRight->backColorRegular);?>;
    }

    <? echo '#'.$blockId ?> .fc-toolbar .fc-right .btn.active
    {
        color: <? echo Utils::formatColor($contentBlock->settings->navigationButtonStyleRight->foreColorSelected);?>;
        background-color: <? echo Utils::formatColor($contentBlock->settings->navigationButtonStyleRight->backColorSelected);?>;
    }

    <?if($contentBlock->settings->disableWeekend):?>
        <? echo '#'.$blockId ?> .fc-sat,
        <? echo '#'.$blockId ?> .fc-sun
        {
            background-color: #ececec;
        }
    <?endif;?>

    <? echo '#'.$blockId; ?> .fc-center h2
    {
        <? if (isset($headerTextAppearance->color)): ?>
            color: <? echo Utils::formatColor($headerTextAppearance->color); ?> !important;
        <? endif; ?>
        <? if ($headerTextAppearance->lineHeight > 0): ?>
            line-height: <? echo $headerTextAppearance->lineHeight; ?>px !important;
        <? endif; ?>
        <? if (isset($headerTextAppearance->alignment)): ?>
            text-align: <? echo $headerTextAppearance->alignment; ?> !important;
        <? endif; ?>
        <? if (isset($headerTextAppearance->font)): ?>
            font-family: <? echo FontReplacementHelper::replaceFont($headerTextAppearance->font->name); ?> !important;
            font-size: <? echo $headerTextAppearance->font->size->extraSmall; ?>pt !important;
            font-weight: <? echo $headerTextAppearance->font->isBold ? 'bold' : 'normal'; ?> !important;
            font-style: <? echo $headerTextAppearance->font->isItalic ? 'italic' : 'normal'; ?> !important;
            text-decoration: <? echo $headerTextAppearance->font->isUnderlined ? 'underline' : 'inherit'; ?> !important;
        <? endif; ?>
    }

    @media (min-width: 768px)
    {
        <? echo '#'.$blockId; ?> .fc-center h2
        {
            <? if (isset($headerTextAppearance->font)): ?>
                font-size: <? echo $headerTextAppearance->font->size->small; ?>pt !important;
            <? endif; ?>
        }
    }

    @media (min-width: 992px)
    {
        <? echo '#'.$blockId; ?> .fc-center h2
        {
            <? if (isset($headerTextAppearance->font)): ?>
                font-size: <? echo $headerTextAppearance->font->size->medium; ?>pt !important;
            <? endif; ?>
        }
    }

    @media (min-width: 1200px)
    {
        <? echo '#'.$blockId; ?> .fc-center h2
        {
            <? if (isset($headerTextAppearance->font)): ?>
                font-size: <? echo $headerTextAppearance->font->size->large; ?>pt !important;
            <? endif; ?>
        }
    }
</style>
<div id="<? echo $blockId; ?>" class="landing-page-calendar" oncontextmenu="return false;">
    <div class="service-data calendar-settings">
		<? echo CJSON::encode(array(
			'defaultView' => $contentBlock->settings->defaultView,
			'defaultDate' => $contentBlock->settings->defaultDate,
			'allowEdit' => $contentBlock->settings->allowEdit,
			'disableWeekend' => $contentBlock->settings->disableWeekend,
			'minTime' => $contentBlock->settings->minTime,
			'maxTime' => $contentBlock->settings->maxTime,
			'viewToggles' => count($contentBlock->settings->viewToggles) > 1 ?
				implode(',', $contentBlock->settings->viewToggles) :
				'',
			'hideLeftNavigationButtonsForViews' => $contentBlock->settings->hideLeftNavigationButtonsForViews,
			'emailSettings' => $contentBlock->settings->emailSettings,
		)); ?>
    </div>
</div>