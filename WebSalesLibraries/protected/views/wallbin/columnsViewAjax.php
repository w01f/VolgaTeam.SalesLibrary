<?php
$cacheKey = $selectedPage->parent->name . '\\' . $selectedPage->name . '\\' . Yii::app()->browser->getBrowser();
$cache = Yii::app()->cacheDB->get($cacheKey);
if ($cache === FALSE)
{
    $cache .=CHtml::openTag('div', array('id' => 'pageHeaderContainer'));
    for ($i = 0; $i < 3; $i++)
    {
        if (isset($selectedPage->columns) && $selectedPage->enableColumns)
            if (isset($selectedPage->columns[$i]))
            {
                $column = $selectedPage->columns[$i];
                $cache .=CHtml::openTag('div'
                        , array(
                        'class' => 'columnHeaderContainer'
                        , 'id' => 'columnHeaderContainer' . $i
                        , 'style' => 'font-family: ' . $column->font->name . '; '
                        . 'font-size: ' . $column->font->size . 'pt; '
                        . 'font-weight: ' . ($column->font->isBold ? ' bold' : ' normal') . '; '
                        . 'font-style: ' . ($column->font->isItalic ? ' italic' : ' normal') . '; '
                        . 'text-align: ' . $column->alignment . '; '
                        . 'background-color: #' . $column->backColor . '; '
                        . 'color: #' . $column->foreColor . '; '
                    ));
                if (isset($column->banner) && $column->banner->isEnabled)
                {
                    $this->widget('application.components.BannerWidget', array('banner' => $column->banner, 'isLinkBanner' => false), TRUE);
                }
                else
                {
                    $widget = $column->getWidget();
                    if (isset($widget))
                    {
                        $cache .=CHtml::tag('img', array('class' => 'columnWidget', 'src' => 'data:image/png;base64,' . $widget));
                    }
                    if ($column->showText)
                    {
                        $cache .=CHtml::openTag('span', array('class' => 'columnHeader'));
                        $cache .=$column->name;
                        $cache .=CHtml::closeTag('span'); //columnHeader
                    }
                }
                $cache .=CHtml::closeTag('div'); //columnHeaderContainer
            }
            else
            {
                $cache .=CHtml::tag('div', array('class' => 'columnHeaderContainer'));
            }
    }
    $cache .=CHtml::closeTag('div'); //pageHeaderContainer
    $cache .=CHtml::openTag('div', array('id' => 'pageContentContainer'));
    for ($i = 0; $i < 3; $i++)
    {
        $folders = $selectedPage->getFoldersByColumn($i);
        if (isset($folders))
        {
            $cache .=CHtml::openTag('div', array('class' => 'pageColumn', 'id' => 'column' . $i));
            foreach ($folders as $folder)
            {
                $cache .=CHtml::openTag('div'
                        , array(
                        'class' => 'folderBody'
                        , 'style' => 'background-color: #' . $folder->windowBackColor . '; '
                        . 'border-color: #' . $folder->borderColor . '; '
                    ));
                $cache .=CHtml::openTag('div'
                        , array(
                        'class' => 'folderHeaderContainer'
                        , 'style' => 'font-family: ' . $folder->headerFont->name . '; '
                        . 'font-size: ' . $folder->headerFont->size . 'pt; '
                        . 'font-weight: ' . ($folder->headerFont->isBold ? ' bold' : ' normal') . '; '
                        . 'font-style: ' . ($folder->headerFont->isItalic ? ' italic' : ' normal') . '; '
                        . 'text-align: ' . $folder->headerAlignment . '; '
                        . 'background-color: #' . $folder->headerBackColor . '; '
                        . 'color: #' . $folder->headerForeColor . '; '
                        . 'border-bottom-color: #' . $folder->borderColor . '; '
                    ));
                if (isset($folder->banner) && $folder->banner->isEnabled)
                {
                    $cache .=$this->widget('application.components.BannerWidget', array('banner' => $folder->banner, 'isLinkBanner' => false), true);
                }
                else
                {
                    $widget = $folder->getWidget();
                    if (isset($widget))
                    {
                        $cache .=CHtml::tag('img', array('class' => 'folderWidget', 'src' => 'data:image/png;base64,' . $widget));
                    }
                    $cache .=CHtml::openTag('span', array('class' => 'folderHeader'));
                    $cache .=$folder->name;
                    $cache .=CHtml::closeTag('span'); //folderHeader
                }
                $cache .=CHtml::closeTag('div'); //folderHeaderContainer

                if (isset($folder->files))
                {
                    $cache .=CHtml::openTag('div'
                            , array(
                            'class' => 'folderLinksScrollArea'));

                    $cache .=CHtml::openTag('div', array('class' => 'folderLinksContainer'));
                    foreach ($folder->files as $link)
                    {
                        $linkContainerClass = (isset($link->originalFormat) && isset($link->availableFormats) ? 'linkContainer clickable' : 'linkContainer');
                        $cache .=CHtml::openTag('div', array('class' => $linkContainerClass));
                        {
                            if (isset($link->banner) && $link->banner->isEnabled)
                            {
                                $cache .=$this->widget('application.components.BannerWidget', array('banner' => $link->banner, 'isLinkBanner' => true), true);
                            }
                            else
                            {
                                $widget = $link->getWidget();

                                if ($link->getIsLineBreak())
                                {
                                    $displayWidget = isset($widget) && $widget != '';
                                    $linkClass = 'linkLineBreak' . ($displayWidget ? ' widget' : '');
                                    $linkFontProperties = 'font-family: ' . $link->lineBreakProperties->font->name . '; '
                                        . 'font-size: ' . $link->lineBreakProperties->font->size . 'pt; '
                                        . 'font-weight: ' . ($link->lineBreakProperties->font->isBold ? ' bold' : ' normal') . '; '
                                        . 'font-style: ' . ($link->lineBreakProperties->font->isItalic ? ' italic' : ' normal') . '; '
                                        . 'color: #' . $link->lineBreakProperties->foreColor . '; ';
                                }
                                else
                                {
                                    $displayWidget = $folder->displayLinkWidgets;
                                    $linkClass = 'linkText' . ($displayWidget ? ' widget' : '');
                                    $linkFontProperties = 'font-family: ' . $folder->windowFont->name . '; '
                                        . 'font-size: ' . $folder->windowFont->size . 'pt; '
                                        . 'font-weight: ' . ($link->isBold ? 'bold' : ($folder->windowFont->isBold ? ' bold' : ' normal')) . '; '
                                        . 'font-style: ' . ($folder->windowFont->isItalic ? ' italic' : ' normal') . '; '
                                        . 'color: #' . $folder->windowForeColor . '; ';
                                }

                                $cache .=CHtml::openTag('div', array('class' => $linkClass
                                        , 'style' => 'background-image: ' . (isset($widget) ? "url('data:image/png;base64," . $widget . "'); " : '; ') . $linkFontProperties));
                                {
                                    $cache .=$link->name;
                                    if (isset($link->note) && $link->note != "")
                                    {
                                        $cache .=CHtml::openTag('span', array('class' => 'linkNote'));
                                        $cache .=$link->note;
                                        $cache .=CHtml::closeTag('span'); //linkNote                                    
                                    }
                                }
                                $cache .=CHtml::closeTag('div'); //linkText                                                                    
                            }
                            $cache .=CHtml::openTag('div', array(
                                    'class' => 'viewDialogContent'
                                ));
                            {
                                $cache .=$this->widget('application.components.ViewDialogWidget', array('link' => $link), true);
                            }
                            $cache .=CHtml::closeTag('div'); //viewDialog
                        }
                        $cache .=CHtml::closeTag('div'); //linkContainer
                    }
                    $cache .=CHtml::closeTag('div'); //folderLinksContainer                      
                    $cache .=CHtml::closeTag('div'); //folderLinksScrollArea
                }
                $cache .=CHtml::closeTag('div'); //folderBody            
            }
            $cache .=CHtml::closeTag('div'); //pageColumn
        }
    }
    $cache .=CHtml::closeTag('div'); //pageContentContainer
    Yii::app()->cacheDB->set($cacheKey, $cache, (60 * 60 * 24 * 7));
}
echo $cache;
?>