<ul data-role="listview" data-theme="c" data-divider-theme="c">
    <li data-role="list-divider" >
        <h4>                        
            <table class ="link-container">
                <tr>
                    <td>                        
                        <span class ="name">
                            <?php
                            if (isset($link->name) && $link->name != '')
                                echo $link->name;
                            else if (isset($link->fileName) && $link->fileName != '')
                                echo $link->fileName;
                            ?>
                        </span>
                    </td>
                </tr>
                <?php if (isset($link->name) && $link->name != ''): ?>                            
                    <tr>
                        <td>                       
                            <span class ="file"><?php echo $link->fileName; ?></span>
                        </td>
                    </tr>
                <?php endif; ?>
            </table>              
        </h4>
    </li>    
</ul>
<?php
if (isset($link->fileCard)):
    $fileCard = $link->fileCard;
    ?>
    <div class ="file-card-body">
        <table class ="details">
            <?php if (isset($fileCard->advertiser)): ?>
                <tr>
                    <td class ="title">Advertiser:</td>
                    <td><?php echo $fileCard->advertiser ?><br></td>
                </tr>
            <?php endif; ?>

            <?php if (isset($fileCard->dateSold)): ?>
                <tr>
                    <td class ="title">Date Sold:</td>
                    <td><?php echo date(Yii::app()->params['outputDateFormat'], strtotime($fileCard->dateSold)) ?><br></td>
                </tr>
            <?php endif; ?>

            <?php if (isset($fileCard->broadcastClosed)): ?>
                <tr>
                    <td class ="title">Broadcast $ Closed:</td>
                    <td><?php echo '$' . number_format($fileCard->broadcastClosed, 2, '.', ',') ?><br></td>
                </tr>
            <?php endif; ?>            

            <?php if (isset($fileCard->digitalClosed)): ?>
                <tr>
                    <td class ="title">Digital $ Closed:</td>
                    <td><?php echo '$' . number_format($fileCard->digitalClosed, 2, '.', ',') ?><br></td>
                </tr>
            <?php endif; ?>                        

            <?php if (isset($fileCard->publishingClosed)): ?>
                <tr>
                    <td class ="title">Publishing $ Closed:</td>
                    <td><?php echo '$' . number_format($fileCard->publishingClosed, 2, '.', ',') ?><br></td>
                </tr>
            <?php endif; ?>                        

            <?php if (isset($fileCard->notes)): ?>
                <tr>
                    <td class ="title" colspan="2">Important Info:</td>
                </tr>
                <?php
                foreach ($fileCard->notes as $note)
                {
                    echo CHtml::openTag('tr', array());
                    {
                        echo CHtml::openTag('td', array('colspan' => '2'));
                        {
                            echo $note;
                        }
                        echo CHtml::closeTag('td');
                    }
                    echo CHtml::closeTag('tr');
                }
                ?>                                                                                
            <?php endif; ?>                                                                                
            <tr>
                <td class ="separator" colspan="2"></td>
            </tr>                
            <?php if (isset($fileCard->salesStation)): ?>
                <tr>
                    <td class ="title">Station or Newspaper:</td>
                    <td><?php echo $fileCard->salesStation ?><br></td>
                </tr>
            <?php endif; ?>                            

            <?php if (isset($fileCard->salesName)): ?>
                <tr>
                    <td class ="title">Sales Contact:</td>
                    <td><?php echo $fileCard->salesName ?><br></td>
                </tr>
            <?php endif; ?>                

            <?php if (isset($fileCard->salesEmail)): ?>
                <tr>
                    <td class ="title">Email:</td>
                    <td><?php echo $fileCard->salesEmail ?><br></td>
                </tr>
            <?php endif; ?>                                

            <?php if (isset($fileCard->salesPhone)): ?>
                <tr>
                    <td class ="title">Phone #:</td>
                    <td><?php echo $fileCard->salesPhone ?><br></td>
                </tr>
            <?php endif; ?>                                                
        </table>
    </div>
<?php endif; ?>
