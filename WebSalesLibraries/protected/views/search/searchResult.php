<table id ="search-grid-header">
    <tr>
        
        <td class = "library-column"><span>Station</span></td>
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
            $recordNumber = 1;
            foreach ($links as $link)
            {
                $rowClass = (($recordNumber % 2) ? 'odd' : 'even');
                echo CHtml::openTag('tr', array('class' => $rowClass));
                {
                    echo CHtml::openTag('td', array('class' => 'library-column'));
                    {
                        echo $link['library'];
                    }
                    echo CHtml::closeTag('td');

                    echo CHtml::openTag('td', array('class' => 'link-type-column'));
                    {
                        echo CHtml::tag('img', array('src' => $link['file_type'], 'alt' => ''));
                    }
                    echo CHtml::closeTag('td');

                    echo CHtml::openTag('td', array('class' => 'link-name-column'));
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
                        echo '';
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