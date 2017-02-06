<?
	use application\models\wallbin\models\web\Thumbnail as Thumbnail;

	/**
	 * @var $thumbnail Thumbnail
	 * @var $tooltip string
	 */
?>
<? if ($thumbnail->showText): ?>
    <div class="thumbnail-container" <? if (isset($tooltip)): ?>title="<? echo $tooltip; ?>"<? endif; ?>
         style="width: auto; text-align: <? echo $thumbnail->imageAlignment; ?>;">
        <div style="display: inline-block">
			<? if ($thumbnail->textPosition === 'bottom'): ?>
                <div style="width: 100%;
                        text-align: <? echo $thumbnail->imageAlignment == 'center' ? $thumbnail->textAlignment : $thumbnail->imageAlignment; ?>;">
                    <img class="thumbnail-image" src="data:image/png;base64,<? echo $thumbnail->image; ?>">
                </div>
			<? endif; ?>
            <div style="width: 100%;
                    text-align: <? echo $thumbnail->textAlignment; ?>;
                    padding-left: <? echo $thumbnail->imagePadding; ?>px;
                    padding-right: <? echo $thumbnail->imagePadding; ?>px;
                    padding-top: 8px;
                    padding-bottom: 8px;
                    display: inline-block">
                <span style="font-family: <? echo $thumbnail->font->name; ?>,serif;
                        font-size: <? echo $thumbnail->font->size; ?>pt;
                        font-weight: <? echo $thumbnail->font->isBold ? ' bold' : ' normal'; ?>;
                        font-style: <? echo $thumbnail->font->isItalic ? ' italic' : ' normal'; ?>;
                        text-decoration: <? echo $thumbnail->font->isUnderlined ? ' underline' : ' inherit'; ?>;
                        color: <? echo $thumbnail->foreColor; ?>;">
                        <? echo nl2br($thumbnail->text); ?>
                </span>
            </div>
			<? if ($thumbnail->textPosition === 'top'): ?>
                <div style="width: 100%;
                        text-align: <? echo $thumbnail->imageAlignment == 'center' ? $thumbnail->textAlignment : $thumbnail->imageAlignment; ?>;">
                    <img class="thumbnail-image" src="data:image/png;base64,<? echo $thumbnail->image; ?>">
                </div>
			<? endif; ?>
        </div>
    </div>
<? else: ?>
	<?
	$thumbnailMarginLeft = '0px';
	$thumbnailMarginRight = '0px';
	if ($thumbnail->imageAlignment === 'left')
	{
		$thumbnailMarginLeft = '0px';
		$thumbnailMarginRight = 'auto';
	}
	else if ($thumbnail->imageAlignment === 'center')
	{
		$thumbnailMarginLeft = 'auto';
		$thumbnailMarginRight = 'auto';
	}
	else if ($thumbnail->imageAlignment === 'right')
	{
		$thumbnailMarginLeft = 'auto';
		$thumbnailMarginRight = '0px';
	}
	?>
    <div class="thumbnail-container" <? if (isset($tooltip)): ?> title="<? echo $tooltip; ?>"<? endif; ?>
         style="text-align: <? echo $thumbnail->imageAlignment; ?>;">
        <img class="thumbnail-image" src="data:image/png;base64,<? echo $thumbnail->image; ?>"
             style="margin-left: <? echo $thumbnailMarginLeft; ?>; margin-right: <? echo $thumbnailMarginRight; ?>;">
    </div>
<? endif; ?>