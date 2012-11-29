<?php if (isset($searchInfo)): ?>
    <div id="search-grid-info">
        <table>
            <tr>
                <td id ="search-links-info-count">
                    <span><?php echo $searchInfo['count']; ?></span>
                </td>
            </tr>
            <tr>
                <td>
                    <?php if (array_key_exists('condition', $searchInfo)): ?>
                        <span><?php echo $searchInfo['condition']; ?>; </span>
                    <?php endif; ?>            
                    <?php if (array_key_exists('file_types', $searchInfo)): ?>
                        <span><?php echo $searchInfo['file_types']; ?>; </span>
                    <?php endif; ?>                                                            
                    <?php if (array_key_exists('dates', $searchInfo)): ?>
                        <span><?php echo $searchInfo['dates']; ?>; </span>
                    <?php endif; ?>                                    
                </td>
            </tr>
            <?php if (array_key_exists('categories', $searchInfo)): ?>
                <tr>
                    <td>
                        <span><?php echo $searchInfo['categories']; ?></span>
                    </td>
                </tr>
            <?php endif; ?>                                
            <?php if (array_key_exists('libraries', $searchInfo)): ?>
                <tr>
                    <td>
                        <span><?php echo $searchInfo['libraries']; ?></span>
                    </td>
                </tr>
            <?php endif; ?>                
        </table>
    </div>
<?php endif; ?>
<table id ="search-grid-header">
    <tr>
        <td class = "link-id-column"><span>Id</span></td>
        <td class = "details-button"><span></span></td>
        <td class = "library-column"><span><?php echo Yii::app()->params['stations']['column_name']; ?></span></td>
        <td class = "link-type-column"><span>Type</span></td>
        <td class = "link-name-column"><span>Link</span></td>
        <td class = "link-date-column"><span>Date</span></td>
    </tr>
</table>
<div id ="search-grid-body-container">
    <table id ="search-grid-body">
        <?php
        if (isset($links))
        {
            if (Yii::app()->browser->isMobile())
                $clickClass = ' click-mobile';
            else
                $clickClass = ' click-no-mobile';

            $recordNumber = 1;
            foreach ($links as $link)
            {
                $rowClass = (($recordNumber % 2) ? 'odd' : 'even');
                echo CHtml::openTag('tr', array('class' => $rowClass));
                {
                    echo CHtml::openTag('td', array('class' => 'link-id-column'));
                    {
                        echo $link['id'];
                    }
                    echo CHtml::closeTag('td');

                    $detailsButtonClass = 'details-button';
                    if ($link['hasDetails'])
                        $detailsButtonClass .= $clickClass . ' collapsed';
                    echo CHtml::openTag('td', array('class' => $detailsButtonClass));
                    {
                        
                    }
                    echo CHtml::closeTag('td');

                    echo CHtml::openTag('td', array('class' => 'library-column'));
                    {
                        echo $link['library'];
                    }
                    echo CHtml::closeTag('td');

                    echo CHtml::openTag('td', array('class' => 'link-type-column' . $clickClass));
                    {
                        echo CHtml::tag('img', array('src' => $link['file_type'], 'alt' => ''));
                    }
                    echo CHtml::closeTag('td');

                    echo CHtml::openTag('td', array('class' => 'link-name-column' . $clickClass));
                    {
                        echo CHtml::openTag('table', array('class' => 'link-container'));
                        {
                            echo CHtml::openTag('tr');
                            {
                                echo CHtml::openTag('td', array('class' => 'link-name'));
                                {
                                    echo $link['name'];
                                }
                                echo CHtml::closeTag('td');
                            }
                            echo CHtml::closeTag('tr');

                            echo CHtml::openTag('tr');
                            {
                                echo CHtml::openTag('td', array('class' => 'file-name'));
                                {
                                    echo $link['file_name'];
                                }
                                echo CHtml::closeTag('td');
                            }
                            echo CHtml::closeTag('tr');
                        }
                        echo CHtml::closeTag('table');
                    }
                    echo CHtml::closeTag('td');

                    echo CHtml::openTag('td', array('class' => 'link-date-column'));
                    {
                        echo CHtml::openTag('table', array('class' => 'link-container'));
                        {
                            echo CHtml::openTag('tr');
                            {
                                echo CHtml::openTag('td', array('class' => 'link-name'));
                                {
                                    echo date(Yii::app()->params['outputDateFormat'], strtotime($link['date_modify']));
                                }
                                echo CHtml::closeTag('td');
                            }
                            echo CHtml::closeTag('tr');

                            echo CHtml::openTag('tr');
                            {
                                echo CHtml::openTag('td', array('class' => 'file-name'));
                                {
                                    echo date(Yii::app()->params['outputTimeFormat'], strtotime($link['date_modify']));
                                }
                                echo CHtml::closeTag('td');
                            }
                            echo CHtml::closeTag('tr');
                        }
                        echo CHtml::closeTag('table');
                    }
                    echo CHtml::closeTag('td');
                }
                echo CHtml::closeTag('tr');
                $recordNumber++;
            }
        }
        ?>
    </table>
</div>