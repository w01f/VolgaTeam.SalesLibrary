<? if ($homeBar->configured): ?>
	<?php
	$useText = false;
	$homeBarImageMarginLeft = '0px';
	$homeBarImageMarginRight = '0px';
	if ($homeBar->imageAlignment === 'left')
	{
		$useText = true;
		$homeBarImageMarginLeft = '0px';
		$homeBarImageMarginRight = 'auto';
	}
	else if ($homeBar->imageAlignment === 'center')
	{
		$homeBarImageMarginLeft = 'auto';
		$homeBarImageMarginRight = 'auto';
	}
	else if ($homeBar->imageAlignment === 'right')
	{
		$homeBarImageMarginLeft = 'auto';
		$homeBarImageMarginRight = '0px';
	}
	?>
	<table class="shortcuts-home-bar"
		   style="background-color: <?php echo $homeBar->backColor; ?>;
			   color: <?php echo $homeBar->foreColor; ?>;">
		<tr>
			<td class="logo-container">
				<img src="<? echo $homeBar->imagePath ?>" style="display: block; margin-left: <?php echo $homeBarImageMarginLeft; ?>; margin-right: <?php echo $homeBarImageMarginRight; ?>;">
			</td>
			<? if ($useText): ?>
				<td class="title-container">
					<span class="title"
						  style="padding-right: 10px;
							  font-family: <? echo $homeBar->font->name; ?>,serif;
							  font-size: <?php echo $homeBar->font->size; ?>pt;
							  font-weight: <?php echo $homeBar->font->isBold ? ' bold' : ' normal'; ?>;
							  font-style: <?php echo $homeBar->font->isItalic ? ' italic' : ' normal'; ?>;">
					</span>
				</td>
			<? endif; ?>
		</tr>
	</table>
<?php endif; ?>