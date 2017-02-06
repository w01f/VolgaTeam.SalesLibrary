<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * @var $link LibraryLink
	 * @var $disableBanner boolean
	 * @var $disableWidget boolean
	 * @var $authorized boolean
	 */

	$tooltip = $link->tooltip;

	$draggable = false;
	if ($link->isFolder)
	{
		$isLineBreak = false;
		$linkContainerClass = 'link-container folder-link';
	}
	else if ($link->isDirectUrl)
	{
		$isLineBreak = false;
		$linkContainerClass = 'link-container url';
		if ($link->isExternalUrl)
		{
			$linkContainerClass .= ' url-external';
		}
		else
		{
			$linkContainerClass .= ' url-internal';
			$draggable = true;
		}
	}
	else
	{
		$isLineBreak = $link->isLineBreak;
		if ($isLineBreak)
			$linkContainerClass = 'link-container line-break';
		else
		{
			$linkContainerClass = 'link-container clickable';
			$draggable = true;
		}
	}

	if ($link->isAppLink)
		$linkContainerClass .= ' hidden-app-link';
	if ($link->extendedProperties->isRestricted)
		$linkContainerClass .= ' restricted';
	if ($authorized)
		$linkContainerClass .= ' log-activity';
?>
<a class="<? echo $linkContainerClass; ?>" id="link<? echo $link->id; ?>"
   href="<? echo $link->isDirectUrl ? $link->fileLink : '#'; ?>" target="_blank"
   draggable="<? echo $draggable ? 'true' : 'false'; ?>">
	<? if (!(isset($disableBanner) && $disableBanner) && isset($link->banner) && $link->banner->isEnabled): ?>
		<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/banner.php',
			array(
				'banner' => $link->banner,
				'wrapText' => $link->extendedProperties->isTextWordWrap,
				'tooltip' => (isset($tooltip) ? $tooltip : null)), true);
		?>
	<? elseif (!(isset($disableBanner) && $disableBanner) && isset($link->thumbnail) && $link->thumbnail->isEnabled): ?>
		<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/thumbnail.php',
			array(
				'thumbnail' => $link->thumbnail,
				'tooltip' => (isset($tooltip) ? $tooltip : null)), true);
		?>
	<? else: ?>
		<?
		$widgetData = $link->getWidgetData();
		$isLinkStaticFontSize = false;
		if ($isLineBreak)
		{
			if (!empty($link->lineBreakProperties->foreColor))
				$color = $link->lineBreakProperties->foreColor;
			else if (isset($link->parent) && !empty($link->parent->windowForeColor))
				$color = $link->parent->windowForeColor;
			else
				$color = '#000000';
			$displayWidget = !(isset($disableWidget) && $disableWidget) && isset($widgetData['base']) && $widgetData['base'] != '';
			$linkFontProperties = 'font-family: ' . $link->lineBreakProperties->font->name . '; '
				. 'font-size: ' . $link->lineBreakProperties->font->size . 'pt; '
				. 'font-weight: ' . ($link->lineBreakProperties->font->isBold ? ' bold' : ' normal') . '; '
				. 'font-style: ' . ($link->lineBreakProperties->font->isItalic ? ' italic' : ' normal') . '; '
				. 'text-decoration: ' . ($link->lineBreakProperties->font->isUnderlined ? ' underline' : ' none') . '; '
				. 'color: ' . $color . '; '
				. (!$link->extendedProperties->isTextWordWrap ? 'white-space: nowrap; ' : '');
			$isLinkStaticFontSize = true;
		}
		else
		{
			$displayWidget = isset($link->files) ? $link->parent->displayLinkWidgets : (!(isset($disableWidget) && $disableWidget) && isset($widgetData['base']) && $widgetData['base'] != '');

			$isDefaultFont = true;
			if ($link->extendedProperties->isSpecialFormat && isset($link->extendedProperties->font))
			{
				$isLinkStaticFontSize = true;
				$font = $link->extendedProperties->font;
				$isDefaultFont = false;
			}
			else if (isset($link->parent) && isset($link->parent->windowFont))
				$font = $link->parent->windowFont;
			else
				$font = Font::createDefault();

			if (!empty($link->extendedProperties->foreColor))
				$color = $link->extendedProperties->foreColor;
			else if (isset($link->parent) && !empty($link->parent->windowForeColor))
				$color = $link->parent->windowForeColor;
			else
				$color = '#000000';

			$linkFontProperties = 'font-family: ' . $font->name . '; '
				. (!$isDefaultFont ? 'font-size: ' . $font->size . 'pt; ' : ' ')
				. 'font-weight: ' . ($link->extendedProperties->isBold ? ' bold' : ($font->isBold ? ' bold' : ' normal')) . '; '
				. 'font-style: ' . ($link->extendedProperties->isItalic ? ' italic' : ($font->isItalic ? ' italic' : ' normal')) . '; '
				. 'text-decoration: ' . ($link->extendedProperties->isUnderline ? ' underline' : ($font->isUnderlined ? ' underline' : ' inherit')) . '; '
				. 'color: ' . $color . '; '
				. (!$link->extendedProperties->isTextWordWrap ? 'white-space: nowrap; ' : '');
		}
		?>
        <div class="link-text-container<? echo $isLinkStaticFontSize != true ? ' link-text-container-sized' : ''; ?>"
		     <? if (isset($tooltip)): ?>title="<? echo $tooltip; ?>"<? endif; ?>
             style="<? echo $linkFontProperties; ?>">
			<? if ($displayWidget): ?>
                <div class="link-widget<? echo !$link->extendedProperties->isTextWordWrap ? ' link-widget-no-wrap' : ' link-widget-wrap'; ?>">
                    <img class="base-image" src="data:image/png;base64,<? echo $widgetData['base']; ?>">
					<? if ($link->isFolder && isset($widgetData['alternative'])): ?>
                        <img class="alternative-image"
                             src="data:image/png;base64,<? echo $widgetData['alternative']; ?>" style="display: none;">
					<? endif; ?>
                </div>
			<? endif; ?>
            <div class="link-text<? echo !$link->extendedProperties->isTextWordWrap ? ' link-text-no-wrap' : ' link-text-wrap'; ?>">
				<? echo nl2br($link->name); ?>
				<? if (isset($link->extendedProperties->note) && $link->extendedProperties->note != ""): ?>
                    <span class="link-note">
						<? echo ' - ' . $link->extendedProperties->note; ?>
					</span>
				<? endif; ?>
            </div>
        </div>
	<? endif; ?>
	<? if ($link->isFolder): ?>
        <div class="folder-link-content" id="folder-link-content<? echo $link->id; ?>"></div>
	<? endif; ?>
    <div class="service-data">
		<? echo $link->getLinkData(); ?>
    </div>
</a>
