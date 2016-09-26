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
			$linkContainerClass .= ' url-external';
		else
			$linkContainerClass .= ' url-internal';
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
   href="<? echo $link->isDirectUrl ? $link->fileLink : '#'; ?>" target="_blank">
	<? if (!(isset($disableBanner) && $disableBanner) && isset($link->banner) && $link->banner->isEnabled): ?>
		<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/banner.php',
			array(
				'banner' => $link->banner,
				'wrapText' => $link->extendedProperties->isTextWordWrap,
				'tooltip' => (isset($tooltip) ? $tooltip : null)), true);
		?>
	<? else: ?>
		<?
		$widget = $link->getWidget();
		$isLinkStaticFontSize = false;
		if ($isLineBreak)
		{
			if (!empty($link->lineBreakProperties->foreColor))
				$color = $link->lineBreakProperties->foreColor;
			else if (isset($link->parent) && !empty($link->parent->windowForeColor))
				$color = $link->parent->windowForeColor;
			else
				$color = '#000000';
			$displayWidget = !(isset($disableWidget) && $disableWidget) && isset($widget) && $widget != '';
			$linkClass = 'link-line-break' . (!$link->extendedProperties->isTextWordWrap ? ' link-line-break-no-wrap' : ' link-line-break-wrap') . ($displayWidget ? ' widget' : '');
			$linkFontProperties = 'font-family: ' . $link->lineBreakProperties->font->name . '; '
				. 'font-size: ' . $link->lineBreakProperties->font->size . 'pt; '
				. 'font-weight: ' . ($link->lineBreakProperties->font->isBold ? ' bold' : ' normal') . '; '
				. 'font-style: ' . ($link->lineBreakProperties->font->isItalic ? ' italic' : ' normal') . '; '
				. 'text-decoration: ' . ($link->lineBreakProperties->font->isUnderlined ? ' underline' : ' none') . '; '
				. 'color: ' . $color . '; '
				. (!$link->extendedProperties->isTextWordWrap ? 'white-space: nowrap; ' : '')
				. (!(isset($disableWidget) && $disableWidget) && isset($widget) ? "background-image: url('data:image/png;base64," . $widget . "');" : '');
			$isLinkStaticFontSize = true;
		}
		else
		{
			$displayWidget = isset($link->files) ? $link->parent->displayLinkWidgets : (!(isset($disableWidget) && $disableWidget) && isset($widget) && $widget != '');
			$linkClass = $displayWidget ? ' widget' : '';

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
				$font = Font::getDefault();

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
			. (!$link->extendedProperties->isTextWordWrap ? 'white-space: nowrap; ' : '')
				. (!(isset($disableWidget) && $disableWidget) && isset($widget) ? "background-image: url('data:image/png;base64," . $widget . "');" : '');
		}
		?>
		<div class="<? echo $linkClass; ?>" draggable="<? echo $draggable ? 'true' : 'false'; ?>"
		     style="<? echo $linkFontProperties; ?>">
			<span class="link-text<? echo !$link->extendedProperties->isTextWordWrap ? ' link-text-no-wrap' : ' link-text-wrap'; ?><? echo $isLinkStaticFontSize != true ? ' link-text-sized' : ''; ?> mtTool"
				<? if (isset($tooltip)): ?>mtcontent="<? echo $tooltip; ?>"<? endif; ?>><? echo nl2br($link->name); ?></span>
			<? if (isset($link->extendedProperties->note) && $link->extendedProperties->note != ""): ?>
				<span
					class="link-note<? echo $isLinkStaticFontSize != true ? ' link-note-sized' : ''; ?>"><? echo ' - ' . $link->extendedProperties->note; ?></span>
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
