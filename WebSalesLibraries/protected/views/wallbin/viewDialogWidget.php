<?php
echo CHtml::openTag('div', array(
    'class' => 'viewDialogBody'
));
{
    if (isset($link->originalFormat) && isset($link->availableFormats))
    {

        echo CHtml::openTag('div', array(
            'class' => 'viewDialogTitle'
        ));
        {
            echo CHtml::openTag('div', array(
                'class' => 'viewDialogLinkName'
            ));
            {
                echo $link->name;
            }
            echo CHtml::closeTag('div'); //viewDialogLinkName

            echo CHtml::tag('br');

            echo CHtml::openTag('div', array(
                'class' => 'viewDialogDescription'
            ));
            {
                echo $link->fileName;
            }
            echo CHtml::closeTag('div'); //viewDialogDescription

            echo CHtml::tag('br');
        }
        echo CHtml::closeTag('div'); //viewDialogTitle

        echo CHtml::openTag('div', array(
            'class' => 'viewDialogFormatList'
        ));
        {
            echo CHtml::openTag('div', array(
                'class' => 'viewDialogFormatListRow'
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
                        echo CHtml::openTag('div', array('class' => 'viewDialogFormatItem'));
                        {
                            echo CHtml::tag('img', array('class' => 'viewDialogFormatImage', 'src' => $imageSource));
                            echo CHtml::openTag('div', array('class' => 'viewDialogFormatServiceData'));
                            {
                                echo CHtml::openTag('div', array('class' => 'viewDialogFormatServiceDataFileType'));
                                echo $link->originalFormat;
                                echo CHtml::closeTag('div');
                                echo CHtml::openTag('div', array('class' => 'viewDialogFormatServiceDataViewType'));
                                echo $format;
                                echo CHtml::closeTag('div');
                                $viewLinks = $link->getViewSource($format);
                                if (isset($viewLinks))
                                {
                                    echo CHtml::openTag('div', array('class' => 'viewDialogFormatServiceDataLinks'));
                                    echo json_encode($viewLinks);
                                    echo CHtml::closeTag('div');
                                }
                                if ($format == 'png' || $format == 'jpeg')
                                {
                                    $thumbsLinks = $link->getViewSource('thumbs');
                                    if (isset($thumbsLinks))
                                    {
                                        echo CHtml::openTag('div', array('class' => 'viewDialogFormatServiceDataThumbs'));
                                        echo json_encode($thumbsLinks);
                                        echo CHtml::closeTag('div');
                                    }
                                }
                            }
                            echo CHtml::closeTag('div'); //viewDialogFormatServiceData
                        }
                        echo CHtml::closeTag('div'); //viewDialogFormatItem
                    }
                }
            }
            echo CHtml::closeTag('div'); //viewDialogFormatListRow
        }
        echo CHtml::closeTag('div'); //viewDialogFormatList
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
            echo CHtml::closeTag('div'); //viewDialogDescription

            echo CHtml::tag('br');
        }
        echo CHtml::closeTag('div'); //viewDialogTitle
    }
}
echo CHtml::closeTag('div'); //Additional Buttons
echo CHtml::openTag('div', array(
    'class' => 'viewDialogBody'
));
echo CHtml::closeTag('div'); //viewDialogBody
?>
