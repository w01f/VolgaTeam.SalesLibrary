<?php
echo CHtml::openTag('div', array('id' => 'pageHeaderContainer'));
for ($i = 0; $i < 3; $i++)
{
    if (isset($selectedPage->columns) && $selectedPage->enableColumns)
        if (isset($selectedPage->columns[$i]))
        {
            $column = $selectedPage->columns[$i];
            echo CHtml::openTag('div'
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
                $this->widget('application.components.BannerWidget', array('banner' => $column->banner, 'isLinkBanner' => false));
            }
            else
            {
                $widget = $column->getWidget();
                if (isset($widget))
                {
                    echo CHtml::tag('img', array('class' => 'columnWidget', 'src' => 'data:image/png;base64,' . $widget));
                }
                if ($column->showText)
                {
                    echo CHtml::openTag('span', array('class' => 'columnHeader'));
                    echo $column->name;
                    echo CHtml::closeTag('span'); //columnHeader
                }
            }
            echo CHtml::closeTag('div'); //columnHeaderContainer
        }
        else
        {
            echo CHtml::tag('div', array('class' => 'columnHeaderContainer'));
        }
}
echo CHtml::closeTag('div'); //pageHeaderContainer
echo CHtml::openTag('div', array('id' => 'pageContentContainer'));
for ($i = 0; $i < 3; $i++)
{
    $folders = $selectedPage->getFoldersByColumn($i);
    if (isset($folders))
    {
        echo CHtml::openTag('div', array('class' => 'pageColumn', 'id' => 'column' . $i));
        foreach ($folders as $folder)
        {
            echo CHtml::openTag('div'
                , array(
                'class' => 'folderBody'
                , 'style' => 'background-color: #' . $folder->windowBackColor . '; '
                . 'border-color: #' . $folder->borderColor . '; '
            ));
            echo CHtml::openTag('div'
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
                $this->widget('application.components.BannerWidget', array('banner' => $folder->banner, 'isLinkBanner' => false));
            }
            else
            {
                $widget = $folder->getWidget();
                if (isset($widget))
                {
                    echo CHtml::tag('img', array('class' => 'folderWidget', 'src' => 'data:image/png;base64,' . $widget));
                }
                echo CHtml::openTag('span', array('class' => 'folderHeader'));
                echo $folder->name;
                echo CHtml::closeTag('span'); //folderHeader
            }
            echo CHtml::closeTag('div'); //folderHeaderContainer

            if (isset($folder->files))
            {
                echo CHtml::openTag('div'
                    , array(
                    'class' => 'folderLinksScrollArea'));

                echo CHtml::openTag('div', array('class' => 'folderLinksContainer'));
                foreach ($folder->files as $link)
                {
                    $linkContainerClass = (isset($link->originalFormat) && isset($link->availableFormats) ? 'linkContainer clickable' : 'linkContainer');
                    echo CHtml::openTag('div', array('class' => $linkContainerClass));
                    {
                        if (isset($link->banner) && $link->banner->isEnabled)
                        {
                            $this->widget('application.components.BannerWidget', array('banner' => $link->banner, 'isLinkBanner' => true));
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

                            echo CHtml::openTag('div', array('class' => $linkClass
                                , 'style' => 'background-image: ' . (isset($widget) ? "url('data:image/png;base64," . $widget . "'); " : '; ') . $linkFontProperties));
                            {
                                echo $link->name;
                                if (isset($link->note) && $link->note != "")
                                {
                                    echo CHtml::openTag('span', array('class' => 'linkNote'));
                                    echo $link->note;
                                    echo CHtml::closeTag('span'); //linkNote                                    
                                }
                            }
                            echo CHtml::closeTag('div'); //linkText                                                                    
                        }
                        echo CHtml::openTag('div', array(
                            'class' => 'viewDialogContent'
                        ));
                        {
                            $this->widget('application.components.ViewDialogWidget', array('link' => $link));
                        }
                        echo CHtml::closeTag('div'); //viewDialog
                    }
                    echo CHtml::closeTag('div'); //linkContainer
                }
                echo CHtml::closeTag('div'); //folderLinksContainer                      
                echo CHtml::closeTag('div'); //folderLinksScrollArea
            }
            echo CHtml::closeTag('div'); //folderBody            
        }
        echo CHtml::closeTag('div'); //pageColumn
    }
}
echo CHtml::closeTag('div'); //pageContentContainer
?>