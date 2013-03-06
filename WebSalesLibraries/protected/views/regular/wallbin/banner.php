<? if ($banner->showText): ?>
<div class="banner-container" <?php if (isset($tooltip)): ?>rel="tooltip" title="<? echo $tooltip; ?>"<? endif;?> style="width: auto">
	<img class="banner-image" src="data:image/png;base64,<? echo $banner->image;?>">
	<span class="<?echo $isLinkBanner ? 'banner-text-link' : 'banner-text';?>"
		  style="font-family: <?php echo $banner->font->name; ?>,serif; font-size: <?php echo $banner->font->size; ?>pt; font-weight: <?php echo $banner->font->isBold ? ' bold' : ' normal'; ?>;font-style: <?php echo $banner->font->isItalic ? ' italic' : ' normal'; ?>;color: <?php echo $banner->foreColor; ?>;">
			<?echo $banner->text;?>
	</span>
</div>
<? else: ?>
<?php
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
<div class="banner-container" <?php if (isset($tooltip)): ?>rel="tooltip" title="<? echo $tooltip; ?>"<? endif;?>>
	<img class="banner-image" src="data:image/png;base64,<? echo $banner->image;?>" style="margin-left: <?php echo $bannerMarginLeft; ?>; margin-right: <?php echo $bannerMarginRight; ?>;">
</div>
<?endif; ?>