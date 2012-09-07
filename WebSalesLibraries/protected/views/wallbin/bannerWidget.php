<?php
if ($banner->showText)
{
    echo CHtml::openTag('div'
        , array(
        'class' => 'banner-container'
    ));

    echo CHtml::tag('img', array('class' => 'banner-image'
        , 'src' => 'data:image/png;base64,' . $banner->image
    ));
    echo CHtml::openTag('span', array('class' => ($isLinkBanner ? 'banner-text-link' : 'banner-text')
        , 'style' => 'font-family: ' . $banner->font->name . '; '
        . 'font-size: ' . $banner->font->size . 'pt; '
        . 'font-weight: ' . ($banner->font->isBold ? ' bold' : ' normal') . '; '
        . 'font-style: ' . ($banner->font->isItalic ? ' italic' : ' normal') . '; '
        . 'color: ' . $banner->foreColor . '; '
    ));
    echo $banner->text;
    echo CHtml::closeTag('span'); //banner-text                                
    echo CHtml::closeTag('div'); //banner-container
}
else
{
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
    echo CHtml::tag('img', array('class' => 'banner-image'
        , 'src' => 'data:image/png;base64,' . $banner->image
        , 'style' => 'margin-left: ' . $bannerMarginLeft . '; '
        . 'margin-right: ' . $bannerMarginRight . '; '
    ));
}
?>
