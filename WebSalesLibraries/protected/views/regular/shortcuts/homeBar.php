<?
	/** @var $homeBar HomeBar */
	/** @var $enableSearchBar boolean */
	if ($homeBar->configured): ?>
		<?
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
			   style="background-color: <? echo $homeBar->backColor; ?>;
				   color: <? echo $homeBar->foreColor; ?>;">
			<tr>
				<td class="sub-search-bar"></td>
				<td class="logo-container">
					<img src="<? echo $homeBar->imagePath ?>" style="display: block; margin-left: <? echo $homeBarImageMarginLeft; ?>; margin-right: <? echo $homeBarImageMarginRight; ?>;">
				</td>
				<? if ($useText): ?>
					<td class="title-container">
					<span class="title"
						  style="padding-right: 10px;
							  font-family: <? echo $homeBar->font->name; ?>,serif;
							  font-size: <? echo $homeBar->font->size; ?>pt;
							  font-weight: <? echo $homeBar->font->isBold ? ' bold' : ' normal'; ?>;
							  font-style: <? echo $homeBar->font->isItalic ? ' italic' : ' normal'; ?>;">
					</span>
					</td>
				<? endif; ?>
				<? if ($enableSearchBar): ?>
					<td class="buttons-container">
						<img class="expanded" src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/search-bar/collapse.png' ?>" alt=""/>
					</td>
				<? endif; ?>
			</tr>
		</table>
	<? endif; ?>