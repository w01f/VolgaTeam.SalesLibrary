<?php
echo CHtml::openTag('div', array(
    'class' => 'view-dialog-body'
));
{
    if (isset($link->originalFormat) && isset($link->availableFormats))
    {

        echo CHtml::openTag('div', array(
            'class' => 'title'
        ));
        {
            echo CHtml::openTag('div', array(
                'class' => 'link-name'
            ));
            {
                echo $link->name;
            }
            echo CHtml::closeTag('div'); //viewDialogLinkName

            echo CHtml::tag('br');

            if (isset($link->fileName))
            {
                echo CHtml::openTag('div', array(
                    'class' => 'description'
                ));
                {
                    echo $link->fileName;
                }
                echo CHtml::closeTag('div'); //viewDialogDescription

                echo CHtml::tag('br');
            }
        }
        echo CHtml::closeTag('div'); //viewDialogTitle

        echo CHtml::openTag('div', array(
            'class' => 'format-list'
        ));
        {
            echo CHtml::openTag('div', array(
                'class' => 'row'
            ));
            {
                foreach ($link->availableFormats as $format)
                {
                    $imageSource = '';
                    switch ($format)
                    {
                        case 'ppt':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/pptx.png';
                            break;
                        case 'doc':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/docx.png';
                            break;
                        case 'xls':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/xlsx.png';
                            break;
                        case 'png':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/png.png';
                            break;
                        case 'jpeg':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/jpeg.png';
                            break;
                        case 'pdf':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/pdf.png';
                            break;
                        case 'video':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/wmv.png';
                            break;
                        case 'mp4':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/mp4.png';
                            break;
                        case 'ogv':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/ogv.png';
                            break;
                        case 'tab':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/tab.png';
                            break;
                        case 'url':
                            $imageSource = Yii::app()->baseUrl . '/images/fileFormats/url.png';
                            break;
                    }
                    if ($imageSource != '')
                    {
                        echo CHtml::openTag('div', array('class' => 'item'));
                        {
                            echo CHtml::tag('img', array('class' => 'image', 'src' => $imageSource));
                            echo CHtml::openTag('div', array('class' => 'service-data'));
                            {
                                echo CHtml::openTag('div', array('class' => 'file-type'));
                                echo $link->originalFormat;
                                echo CHtml::closeTag('div');
                                echo CHtml::openTag('div', array('class' => 'view-type'));
                                echo $format;
                                echo CHtml::closeTag('div');
                                $viewLinks = $link->getViewSource($format);
                                if (isset($viewLinks))
                                {
                                    echo CHtml::openTag('div', array('class' => 'links'));
                                    echo json_encode($viewLinks);
                                    echo CHtml::closeTag('div');
                                }
                                if ($format == 'png' || $format == 'jpeg')
                                {
                                    $thumbsLinks = $link->getViewSource('thumbs');
                                    if (isset($thumbsLinks))
                                    {
                                        echo CHtml::openTag('div', array('class' => 'thumbs'));
                                        echo json_encode($thumbsLinks);
                                        echo CHtml::closeTag('div');
                                    }
                                }
                            }
                            echo CHtml::closeTag('div'); //service-data
                        }
                        echo CHtml::closeTag('div'); //item
                    }
                }
            }
            echo CHtml::closeTag('div'); //row
        }
        echo CHtml::closeTag('div'); //format-list
    }
    else
    {
        echo CHtml::openTag('div', array(
            'class' => 'viewDialogTitle'
        ));
        {
            echo CHtml::openTag('div', array(
                'class' => 'viewDialogDescription'
            ));
            {
                echo 'This link is not avalable for preview';
            }
            echo CHtml::closeTag('div'); //description

            echo CHtml::tag('br');
        }
        echo CHtml::closeTag('div'); //title
    }
}
echo CHtml::closeTag('div'); //.view-dialog-body
?>
