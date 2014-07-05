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
			$linkContainerClass = isset($link->originalFormat) && isset($link->availableFormats) ? 'link-container clickable' : 'link-container';
		$tooltip = $link->tooltip;
	}
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
			$font = isset($link->parent) && isset($link->parent->windowFont) ? $link->parent->windowFont : Font::getDefault();
			$linkFontProperties = 'font-family: ' . $font->name . '; '
				. 'font-size: ' . $font->size . 'pt; '
				. 'font-weight: ' . ($link->isBold ? 'bold' : ($font->isBold ? ' bold' : ' normal')) . '; '
				. 'font-style: ' . ($font->isItalic ? ' italic' : ' normal') . '; '
				. 'color: ' . (isset($link->parent) && isset($link->parent->windowForeColor) ? $link->parent->windowForeColor : '#000000') . '; '
				. 'white-space: nowrap;';
		}
		?>
		<div class="<? echo $linkClass; ?>"
			 style="background-image: <? echo !(isset($disableWidget) && $disableWidget) && isset($widget) ? "url('data:image/png;base64," . $widget . "')" : ""; ?>; <? echo $linkFontProperties; ?>">
		<span class="link-text" <? if (isset($tooltip)): ?>rel="tooltip"
			  title="<? echo $tooltip; ?>"<? endif; ?>><? echo $link->name; ?></span>
			<? if (isset($link->note) && $link->note != ""): ?>
				<span class="link-note"><? echo $link->note; ?></span>
			<? endif; ?>
		</div>
	<? endif; ?>
	<? if ($link->isFolder): ?>
		<div class="folder-link-content" id="folder-link-content<? echo $link->id; ?>"></div>
	<? endif; ?>
</div>
