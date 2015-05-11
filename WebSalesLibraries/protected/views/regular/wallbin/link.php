<?
	/** @var $link LibraryLink */
	if ($link->isFolder)
	{
		$isLineBreak = false;
		$linkContainerClass = 'link-container folder-link';
		$tooltip = 'Folder';
	}
	else
	{
		$isLineBreak = $link->getIsLineBreak();
		if ($isLineBreak)
			$linkContainerClass = 'link-container line-break';
		else
			$linkContainerClass = 'link-container clickable';
		$tooltip = $link->tooltip;
	}

	if (!$isLineBreak && isset($link->extendedProperties->hoverNote) && $link->extendedProperties->hoverNote != '')
		$tooltip = $link->extendedProperties->hoverNote . ' (' . $tooltip . ')';
	else if ($isLineBreak && isset($link->lineBreakProperties->note) && $link->lineBreakProperties->note != '')
		$tooltip = $link->lineBreakProperties->note;

	if ($link->extendedProperties->isRestricted)
		$linkContainerClass .= ' restricted';
?>
<div class="<? echo $linkContainerClass; ?>" id="link<? echo $link->id; ?>">
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

			if ($link->extendedProperties->isSpecialFormat && isset($link->extendedProperties->foreColor))
				$color = $link->extendedProperties->foreColor;
			else if (isset($link->parent) && isset($link->parent->windowForeColor))
				$color = $link->parent->windowForeColor;
			else
				$color = '#000000';

			$linkFontProperties = 'font-family: ' . $font->name . '; '
				. 'font-size: ' . $font->size . 'pt; '
				. 'font-weight: ' . ($link->extendedProperties->isBold ? 'bold' : ($font->isBold ? ' bold' : ' normal')) . '; '
				. 'font-style: ' . ($font->isItalic ? ' italic' : ' normal') . '; '
				. 'color: ' . $color . '; '
				. 'white-space: nowrap;';
		}
		?>
		<div class="<? echo $linkClass; ?>"
			 style="background-image: <? echo !(isset($disableWidget) && $disableWidget) && isset($widget) ? "url('data:image/png;base64," . $widget . "')" : ""; ?>; <? echo $linkFontProperties; ?>">
		<span class="link-text" <? if (isset($tooltip)): ?>rel="tooltip"
			  title="<? echo $tooltip; ?>"<? endif; ?>><? echo $link->name; ?></span>
			<? if (isset($link->extendedProperties->note) && $link->extendedProperties->note != ""): ?>
				<span class="link-note"><? echo $link->extendedProperties->note; ?></span>
			<? endif; ?>
		</div>
	<? endif; ?>
	<? if ($link->isFolder): ?>
		<div class="folder-link-content" id="folder-link-content<? echo $link->id; ?>"></div>
	<? endif; ?>
</div>
