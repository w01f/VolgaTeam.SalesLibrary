<?
	/**
	 * @var $banner Banner
	 * @var $isLinkBanner bool
	 * @var $tooltip string
	 */
?>
<? if ($banner->showText): ?>
<div class="banner-container mtTool" <? if (isset($tooltip)): ?>mtcontent="<? echo $tooltip; ?>"<? endif;?> style="width: auto">
	<img class="banner-image" src="data:image/png;base64,<? echo $banner->image;?>">
	<span class="<?echo $isLinkBanner ? 'banner-text-link' : 'banner-text';?>"
		  style="font-family: <? echo $banner->font->name; ?>,serif;
			font-size: <? echo $banner->font->size; ?>pt;
			font-weight: <? echo $banner->font->isBold ? ' bold' : ' normal'; ?>;
			font-style: <? echo $banner->font->isItalic ? ' italic' : ' normal'; ?>;
			text-decoration: <? echo $banner->font->isUnderlined ? ' underline' : ' inherit'; ?>;
			color: <? echo $banner->foreColor; ?>;">
			<?echo $banner->text;?>
	</span>
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
<div class="banner-container mtTool" <? if (isset($tooltip)): ?> mtcontent="<? echo $tooltip; ?>"<? endif;?>>
	<img class="banner-image" src="data:image/png;base64,<? echo $banner->image;?>" style="margin-left: <? echo $bannerMarginLeft; ?>; margin-right: <? echo $bannerMarginRight; ?>;">
</div>
<?endif; ?>