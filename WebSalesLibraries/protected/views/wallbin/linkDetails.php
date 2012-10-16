<tr class ="link-details-container">
    <td class = "link-id-column"></td>
    <td class = "details-button"></td>
    <td class = "library-column"></span></td>
<td colspan="3">
    <table class ="link-details">
        <?php
        if (isset($link))
        {
            $clickClass = ' click-no-mobile';

            if ($link->enableFileCard && isset($link->fileCard))
            {
                echo CHtml::openTag('tr', array('class' => 'file-card'));
                {
                    echo CHtml::openTag('td', array('class' => 'link-id-column'));
                    {
                        echo $link->fileCard->id;
                    }
                    echo CHtml::closeTag('td');

                    echo CHtml::openTag('td', array('class' => 'link-type-column' . $clickClass));
                    {
                        echo CHtml::tag('img', array('src' => 'images/search/search-file-card.png', 'alt' => ''));
                    }
                    echo CHtml::closeTag('td');

                    echo CHtml::openTag('td', array('class' => 'link-name-column' . $clickClass));
                    {
                        echo $link->fileCard->title;
                    }
                    echo CHtml::closeTag('td');

                    echo CHtml::openTag('td', array('class' => 'hidden-content'));
                    {
                        echo CHtml::openTag('div', array(
                            'class' => 'file-card-content'
                        ));
                        {
                            echo $this->renderFile(Yii::getPathOfAlias('application.views.wallbin') . '/fileCard.php', array('link' => $link));
                        }
                        echo CHtml::closeTag('div'); //view-dialog-content
                    }
                    echo CHtml::closeTag('td');
                }
                echo CHtml::closeTag('tr');
            }
            if ($link->enableAttachments && isset($link->attachments))
            {
                foreach ($link->attachments as $attachment)
                {
                    echo CHtml::openTag('tr', array('class' => 'attachment'));
                    {
                        echo CHtml::openTag('td', array('class' => 'link-id-column'));
                        {
                            echo $attachment->id;
                        }
                        echo CHtml::closeTag('td');

                        echo CHtml::openTag('td', array('class' => 'link-type-column' . $clickClass));
                        {
                            switch ($attachment->originalFormat)
                            {
                                case 'ppt':
                                    echo CHtml::tag('img', array('src' => 'images/search/search-powerpoint.png', 'alt' => ''));
                                    break;
                                case 'doc':
                                    echo CHtml::tag('img', array('src' => 'images/search/search-word.png', 'alt' => ''));
                                    break;
                                case 'xls':
                                    echo CHtml::tag('img', array('src' => 'images/search/search-excel.png', 'alt' => ''));
                                    break;
                                case 'pdf':
                                    echo CHtml::tag('img', array('src' => 'images/search/search-pdf.png', 'alt' => ''));
                                    break;
                                case 'video':
                                    echo CHtml::tag('img', array('src' => 'images/search/search-video.png', 'alt' => ''));
                                    break;
                                case 'url':
                                    if (Yii::app()->browser->isMobile())
                                    {
                                        echo CHtml::tag('img', array('src' => 'images/search/search-url-safari.png', 'alt' => ''));
                                    }
                                    else
                                    {
                                        $browser = Yii::app()->browser->getBrowser();
                                        switch ($browser)
                                        {
                                            case 'Internet Explorer':
                                                echo CHtml::tag('img', array('src' => 'images/search/search-url-ie.png', 'alt' => ''));
                                                break;
                                            case 'Chrome':
                                                echo CHtml::tag('img', array('src' => 'images/search/search-url-chrome.png', 'alt' => ''));
                                                break;
                                            case 'Safari':
                                                echo CHtml::tag('img', array('src' => 'images/search/search-url-safari.png', 'alt' => ''));
                                                break;
                                            case 'Firefox':
                                                echo CHtml::tag('img', array('src' => 'images/search/search-url-firefox.png', 'alt' => ''));
                                                break;
                                            case 'Opera':
                                                echo CHtml::tag('img', array('src' => 'images/search/search-url-opera.png', 'alt' => ''));
                                                break;
                                            default:
                                                echo CHtml::tag('img', array('src' => 'images/search/search-url.png', 'alt' => ''));
                                                break;
                                        }
                                    }
                                    break;
                                default:
                                    echo CHtml::tag('img', array('src' => 'images/search/search-undefined-type.png', 'alt' => ''));
                                    break;
                            }
                        }
                        echo CHtml::closeTag('td');

                        echo CHtml::openTag('td', array('class' => 'link-name-column' . $clickClass));
                        {
                            echo $attachment->name;
                        }
                        echo CHtml::closeTag('td');

                        echo CHtml::openTag('td', array('class' => 'hidden-content'));
                        {
                            echo CHtml::openTag('div', array(
                                'class' => 'view-dialog-content'
                            ));
                            {
                                echo $this->renderFile(Yii::getPathOfAlias('application.views.wallbin') . '/viewDialog.php', array('link' => $attachment));
                            }
                            echo CHtml::closeTag('div'); //view-dialog-content
                        }
                        echo CHtml::closeTag('td');
                    }
                    echo CHtml::closeTag('tr');
                }
            }
        }
        ?>
    </table>
</td>
</tr>



