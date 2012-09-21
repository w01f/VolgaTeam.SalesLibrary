<?php
$cache = '';
$cache .=CHtml::openTag('div', array('id' => 'page-header-container'));
for ($i = 0; $i < 3; $i++)
{
    if (isset($libraryPage->columns) && $libraryPage->enableColumns)
        if (isset($libraryPage->columns[$i]))
        {
            $column = $libraryPage->columns[$i];
            $cache .=CHtml::openTag('div'
                    , array(
                    'class' => 'column-header-container'
                    , 'id' => 'column-header-container-' . $i
                    , 'style' => 'font-family: ' . $column->font->name . '; '
                    . 'font-size: ' . $column->font->size . 'pt; '
                    . 'font-weight: ' . ($column->font->isBold ? ' bold' : ' normal') . '; '
                    . 'font-style: ' . ($column->font->isItalic ? ' italic' : ' normal') . '; '
                    . 'text-align: ' . $column->alignment . '; '
                    . 'background-color: ' . $column->backColor . '; '
                    . 'color: ' . $column->foreColor . '; '
                ));
            if (isset($column->banner) && $column->banner->isEnabled)
            {
                $cache .=$this->renderFile(Yii::getPathOfAlias('application.views.wallbin').'/banner.php', array('banner' => $column->banner, 'isLinkBanner' => false), true);
            }
            else
            {
                $widget = $column->getWidget();
                if (isset($widget))
                {
                    $cache .=CHtml::tag('img', array('class' => 'column-widget', 'src' => 'data:image/png;base64,' . $widget));
                }
                if ($column->showText)
                {
                    $cache .=CHtml::openTag('span', array('class' => 'column-header'));
                    $cache .=$column->name;
                    $cache .=CHtml::closeTag('span'); //column-header
                }
            }
            $cache .=CHtml::closeTag('div'); //column-header-container
        }
        else
        {
            $cache .=CHtml::tag('div', array('class' => 'column-header-container'));
        }
}
$cache .=CHtml::closeTag('div'); //page-header-container
$cache .=CHtml::openTag('div', array('id' => 'page-content-container'));
for ($i = 0; $i < 3; $i++)
{
    $folders = $libraryPage->getFoldersByColumn($i);
    if (isset($folders))
    {
        $cache .=CHtml::openTag('div', array('class' => 'page-column', 'id' => 'column' . $i));
        foreach ($folders as $folder)
        {
            $cache .=CHtml::openTag('div'
                    , array(
                    'class' => 'folder-body'
                    , 'style' => 'background-color: ' . $folder->windowBackColor . '; '
                    . 'border-color: ' . $folder->borderColor . '; '
                ));
            $cache .=CHtml::openTag('div'
                    , array(
                    'class' => 'folder-header-container'
                    , 'style' => 'font-family: ' . $folder->headerFont->name . '; '
                    . 'font-size: ' . $folder->headerFont->size . 'pt; '
                    . 'font-weight: ' . ($folder->headerFont->isBold ? ' bold' : ' normal') . '; '
                    . 'font-style: ' . ($folder->headerFont->isItalic ? ' italic' : ' normal') . '; '
                    . 'text-align: ' . $folder->headerAlignment . '; '
                    . 'background-color: ' . $folder->headerBackColor . '; '
                    . 'color: ' . $folder->headerForeColor . '; '
                    . 'border-bottom-color: ' . $folder->borderColor . '; '
                ));
            if (isset($folder->banner) && $folder->banner->isEnabled)
            {
                $cache .=$this->renderFile(Yii::getPathOfAlias('application.views.wallbin').'/banner.php', array('banner' => $folder->banner, 'isLinkBanner' => false), true);
            }
            else
            {
                $widget = $folder->getWidget();
                if (isset($widget))
                {
                    $cache .=CHtml::tag('img', array('class' => 'folder-widget', 'src' => 'data:image/png;base64,' . $widget));
                }
                $cache .=CHtml::openTag('span', array('class' => 'folder-header'));
                $cache .=$folder->name;
                $cache .=CHtml::closeTag('span'); //folder-header
            }
            $cache .=CHtml::closeTag('div'); //folder-header-container

            if (isset($folder->files))
            {
                $cache .=CHtml::openTag('div'
                        , array(
                        'class' => 'folder-links-scroll-area'));

                $cache .=CHtml::openTag('div', array('class' => 'folder-links-container'));
                foreach ($folder->files as $link)
                {
                    $linkContainerClass = (isset($link->originalFormat) && isset($link->availableFormats) ? 'link-container clickable' : 'link-container');
                    $cache .=CHtml::openTag('div', array('class' => $linkContainerClass));
                    {
                        if (isset($link->banner) && $link->banner->isEnabled)
                        {
                            $cache .=$this->renderFile(Yii::getPathOfAlias('application.views.wallbin').'/banner.php', array('banner' => $link->banner, 'isLinkBanner' => true), true);
                        }
                        else
                        {
                            $widget = $link->getWidget();

                            if ($link->getIsLineBreak())
                            {
                                $displayWidget = isset($widget) && $widget != '';
                                $linkClass = 'link-line-break' . ($displayWidget ? ' widget' : '');
                                $linkFontProperties = 'font-family: ' . $link->lineBreakProperties->font->name . '; '
                                    . 'font-size: ' . $link->lineBreakProperties->font->size . 'pt; '
                                    . 'font-weight: ' . ($link->lineBreakProperties->font->isBold ? ' bold' : ' normal') . '; '
                                    . 'font-style: ' . ($link->lineBreakProperties->font->isItalic ? ' italic' : ' normal') . '; '
                                    . 'color: ' . $link->lineBreakProperties->foreColor . '; ';
                            }
                            else
                            {
                                $displayWidget = $folder->displayLinkWidgets;
                                $linkClass = 'link-text' . ($displayWidget ? ' widget' : '');
                                $linkFontProperties = 'font-family: ' . $folder->windowFont->name . '; '
                                    . 'font-size: ' . $folder->windowFont->size . 'pt; '
                                    . 'font-weight: ' . ($link->isBold ? 'bold' : ($folder->windowFont->isBold ? ' bold' : ' normal')) . '; '
                                    . 'font-style: ' . ($folder->windowFont->isItalic ? ' italic' : ' normal') . '; '
                                    . 'color: ' . $folder->windowForeColor . '; ';
                            }

                            $cache .=CHtml::openTag('div', array('class' => $linkClass
                                    , 'style' => 'background-image: ' . (isset($widget) ? "url('data:image/png;base64," . $widget . "'); " : '; ') . $linkFontProperties));
                            {
                                $cache .=$link->name;
                                if (isset($link->note) && $link->note != "")
                                {
                                    $cache .=CHtml::openTag('span', array('class' => 'link-note'));
                                    $cache .=$link->note;
                                    $cache .=CHtml::closeTag('span'); //link-note                                    
                                }
                            }
                            $cache .=CHtml::closeTag('div'); //link-text                                                                    
                        }
                        $cache .=CHtml::openTag('div', array(
                                'class' => 'view-dialog-content'
                            ));
                        {
                            $cache .=$this->renderFile(Yii::getPathOfAlias('application.views.wallbin').'/viewDialog.php', array('link' => $link), true);
                        }
                        $cache .=CHtml::closeTag('div'); //view-dialog-content
                    }
                    $cache .=CHtml::closeTag('div'); //link-container
                }
                $cache .=CHtml::closeTag('div'); //folder-links-container                      
                $cache .=CHtml::closeTag('div'); //folder-links-scroll-area
            }
            $cache .=CHtml::closeTag('div'); //folder-body            
        }
        $cache .=CHtml::closeTag('div'); //page-column
    }
}
$cache .=CHtml::closeTag('div'); //page-content-container
echo $cache;
?>