<?php
	if (isset($link->folderContent))
	{
		$linkContainerClass = 'link-container folder-link';
		if ($link->browser != 'mobile')
			$tooltip = 'Folder';
	}
	else
	{
		$linkContainerClass = isset($link->originalFormat) && isset($link->availableFormats) ? 'link-container clickable' : 'link-container';
		if ($link->browser != 'mobile')
			$tooltip = $link->tooltip;
	}
?>
<div class="<?php echo $linkContainerClass; ?>">
	<?php if (isset($link->banner) && $link->banner->isEnabled): ?>
	<?php echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/banner.php', array('banner' => $link->banner, 'isLinkBanner' => true, 'tooltip' => (isset($tooltip) ? $tooltip : null)), true); ?>
	<?php else: ?>
	<?php
	$widget = $link->getWidget();
	if ($link->getIsLineBreak())
	{
		$displayWidget = isset($widget) && $widget != '';
		$linkClass = 'link-line-break' . ($displayWidget ? ' widget' : '');
		$linkFontProperties = 'font-family: ' . $link->lineBreakProperties->font->name . '; '
			. 'font-size: ' . $link->lineBreakProperties->font->size . 'pt; '
			. 'font-weight: ' . ($link->lineBreakProperties->font->isBold ? ' bold' : ' normal') . '; '
			. 'font-style: ' . ($link->lineBreakProperties->font->isItalic ? ' italic' : ' normal') . '; '
			. 'color: ' . $link->lineBreakProperties->foreColor . '; ';
	}
	else
	{
		$displayWidget = $link->parent->displayLinkWidgets;
		$linkClass = $displayWidget ? ' widget' : '';
		$linkFontProperties = 'font-family: ' . $link->parent->windowFont->name . '; '
			. 'font-size: ' . $link->parent->windowFont->size . 'pt; '
			. 'font-weight: ' . ($link->isBold ? 'bold' : ($link->parent->windowFont->isBold ? ' bold' : ' normal')) . '; '
			. 'font-style: ' . ($link->parent->windowFont->isItalic ? ' italic' : ' normal') . '; '
			. 'color: ' . $link->parent->windowForeColor . '; ';
	}
	?>
	<div class="<?php echo $linkClass; ?>"
		 style="background-image: <?php echo isset($widget) ? "url('data:image/png;base64," . $widget . "')" : ""; ?>;">
		<span class="link-text" <?php echo $linkFontProperties; ?>" <?php if (isset($tooltip)): ?>rel="tooltip"
		title="<? echo $tooltip; ?>"<? endif;?>><?php echo $link->name; ?></span>
		<?php if (isset($link->note) && $link->note != ""): ?>
		<span class="link-note"><?php echo $link->note; ?></span>
		<?php endif; ?>
	</div>
	<?php endif; ?>
	<?php if (isset($link->folderContent)): ?>
	<div class="folder-link-content">
		<?php foreach ($link->folderContent as $contentLink): ?>
		<?php echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/link.php', array('link' => $contentLink), true); ?>
		<?php endforeach; ?>
	</div>
	<?php endif; ?>
	<div class="view-dialog-content">
		<?php echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/viewDialog.php', array('link' => $link), true); ?>
	</div>
</div>
