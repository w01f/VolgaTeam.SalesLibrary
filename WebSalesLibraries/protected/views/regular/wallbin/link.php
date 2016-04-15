<?
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
	}
	else
	{
		$isLineBreak = $link->getIsLineBreak();
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
<a class="<? echo $linkContainerClass; ?>" id="link<? echo $link->id; ?>" href="<? echo $link->isDirectUrl ? $link->fileLink : '#'; ?>" target="_blank">
	<? if (!(isset($disableBanner) && $disableBanner) && isset($link->banner) && $link->banner->isEnabled): ?>
		<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/banner.php', array('banner' => $link->banner, 'isLinkBanner' => true, 'tooltip' => (isset($tooltip) ? $tooltip : null)), true); ?>
	<? else: ?>
		<?
		$widget = $link->getWidget();
		if ($isLineBreak)
		{
			$displayWidget = !(isset($disableWidget) && $disableWidget) && isset($widget) && $widget != '';
			$linkClass = 'link-line-break' . ($displayWidget ? ' widget' : '');
			$linkFontProperties = 'font-family: ' . $link->lineBreakProperties->font->name . '; '
				. 'font-size: ' . $link->lineBreakProperties->font->size . 'pt; '
				. 'font-weight: ' . ($link->lineBreakProperties->font->isBold ? ' bold' : ' normal') . '; '
				. 'font-style: ' . ($link->lineBreakProperties->font->isItalic ? ' italic' : ' normal') . '; '
				. 'color: ' . $link->lineBreakProperties->foreColor . '; '
				. 'white-space: nowrap;';
		}
		else
		{
			$displayWidget = isset($link->files) ? $link->parent->displayLinkWidgets : (!(isset($disableWidget) && $disableWidget) && isset($widget) && $widget != '');
			$linkClass = $displayWidget ? ' widget' : '';
			if ($link->extendedProperties->isSpecialFormat && isset($link->extendedProperties->font))
				$font = $link->extendedProperties->font;
			else if (isset($link->parent) && isset($link->parent->windowFont))
				$font = $link->parent->windowFont;
			else
				$font = Font::getDefault();

			if (isset($link->extendedProperties->foreColor))
				$color = $link->extendedProperties->foreColor;
			else if (isset($link->parent) && isset($link->parent->windowForeColor))
				$color = $link->parent->windowForeColor;
			else
				$color = '#000000';

			$linkFontProperties = 'font-family: ' . $font->name . '; '
				. 'font-size: ' . $font->size . 'pt; '
				. 'font-weight: ' . ($link->extendedProperties->isBold ? ' bold' : ($font->isBold ? ' bold' : ' normal')) . '; '
				. 'font-style: ' . ($link->extendedProperties->isItalic ? ' italic' : ($font->isItalic ? ' italic' : ' normal')) . '; '
				. 'text-decoration: ' . ($link->extendedProperties->isUnderline ? ' underline' : ($font->isUnderlined ? ' underline' : ' inherit')) . '; '
				. 'color: ' . $color . '; '
				. 'white-space: nowrap;';
		}
		?>
		<div class="<? echo $linkClass; ?>" draggable="<? echo $draggable?'true':'false'; ?>"
		     style="background-image: <? echo !(isset($disableWidget) && $disableWidget) && isset($widget) ? "url('data:image/png;base64," . $widget . "')" : ""; ?>; <? echo $linkFontProperties; ?>">
			<span class="link-text mtTool" <? if (isset($tooltip)): ?>mtcontent="<? echo $tooltip; ?>"<? endif; ?>><? echo $link->name; ?></span>
			<? if (isset($link->extendedProperties->note) && $link->extendedProperties->note != ""): ?>
				<span class="link-note"><? echo ' - '.$link->extendedProperties->note; ?></span>
			<? endif; ?>
		</div>
	<? endif; ?>
	<? if ($link->isFolder): ?>
		<div class="folder-link-content" id="folder-link-content<? echo $link->id; ?>"></div>
	<? endif; ?>
	<div class="service-data">
		<? echo $link->getLinkData(); ?>
	</div>
</a>
