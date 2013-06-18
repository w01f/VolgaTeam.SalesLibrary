<?php
if ($link->isFolder)
{
	$linkContainerClass = 'link-container folder-link';
	$tooltip = 'Folder';
}
else
{
	$linkContainerClass = isset($link->originalFormat) && isset($link->availableFormats) ? 'link-container clickable' : 'link-container';
	$tooltip = $link->tooltip;
}
?>
<div class="<?php echo $linkContainerClass; ?>" id="link<?php echo $link->id; ?>">
	<?php if (!(isset($disableBanner) && $disableBanner) && isset($link->banner) && $link->banner->isEnabled): ?>
		<?php echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/banner.php', array('banner' => $link->banner, 'isLinkBanner' => true, 'tooltip' => (isset($tooltip) ? $tooltip : null)), true); ?>
	<?php else: ?>
		<?php
		$widget = $link->getWidget();
		if ($link->getIsLineBreak())
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
		<div class="<?php echo $linkClass; ?>"
			 style="background-image: <?php echo !(isset($disableWidget) && $disableWidget) && isset($widget) ? "url('data:image/png;base64," . $widget . "')" : ""; ?>; <?php echo $linkFontProperties; ?>">
		<span class="link-text" <?php if (isset($tooltip)): ?>rel="tooltip"
			  title="<? echo $tooltip; ?>"<? endif;?>><?php echo $link->name; ?></span>
			<?php if (isset($link->note) && $link->note != ""): ?>
				<span class="link-note"><?php echo $link->note; ?></span>
			<?php endif; ?>
		</div>
	<?php endif; ?>
	<?php if ($link->isFolder): ?>
		<div class="folder-link-content" id="folder-link-content<?php echo $link->id; ?>"></div>
	<?php endif; ?>
</div>
