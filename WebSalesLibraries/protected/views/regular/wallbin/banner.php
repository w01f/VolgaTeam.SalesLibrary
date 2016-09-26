<?
	use application\models\wallbin\models\web\Banner as Banner;

	/**
	 * @var $banner Banner
	 * @var $wrapText bool
	 * @var $tooltip string
	 */
?>
<? if ($banner->showText): ?>
<div class="banner-container mtTool" <? if (isset($tooltip)): ?>mtcontent="<? echo $tooltip; ?>"<? endif;?> style="width: auto">
	<div class="banner-image  <?if($banner->imageVerticalAlignment=='top'):?>banner-image-top<?else:?>banner-image-middle<?endif;?>">
		<img src="data:image/png;base64,<? echo $banner->image;?>">
	</div>
	<div class="banner-text <?if($banner->imageVerticalAlignment=='top'):?>banner-text-top<?else:?>banner-text-middle<?endif;?>">
		<span class="<?echo $wrapText ?
			'banner-text-link-wrap' :
			'banner-text-link-no-wrap';?>"
		      style="font-family: <? echo $banner->font->name; ?>,serif;
				  font-size: <? echo $banner->font->size; ?>pt;
				  font-weight: <? echo $banner->font->isBold ? ' bold' : ' normal'; ?>;
				  font-style: <? echo $banner->font->isItalic ? ' italic' : ' normal'; ?>;
				  text-decoration: <? echo $banner->font->isUnderlined ? ' underline' : ' inherit'; ?>;
				  color: <? echo $banner->foreColor; ?>;">
				<?echo nl2br($banner->text);?>
		</span>
	</div>
</div>
<? else: ?>
<?
	$bannerMarginLeft = '0px';
	$bannerMarginRight = '0px';
	if ($banner->imageAlignment === 'left')
	{
		$bannerMarginLeft = '0px';
		$bannerMarginRight = 'auto';
	}
	else if ($banner->imageAlignment === 'center')
	{
		$bannerMarginLeft = 'auto';
		$bannerMarginRight = 'auto';
	}
	else if ($banner->imageAlignment === 'right')
	{
		$bannerMarginLeft = 'auto';
		$bannerMarginRight = '0px';
	}
	?>
<div class="banner-container mtTool" <? if (isset($tooltip)): ?> mtcontent="<? echo $tooltip; ?>"<? endif;?>  style="text-align: <? echo $banner->imageAlignment; ?>;">
	<img class="banner-image" src="data:image/png;base64,<? echo $banner->image;?>" style="margin-left: <? echo $bannerMarginLeft; ?>; margin-right: <? echo $bannerMarginRight; ?>;">
</div>
<?endif; ?>