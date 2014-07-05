<? if (isset($link->fileCard)):
    $fileCard = $link->fileCard;
    ?>
    <div class ="file-card-body">
        <div class ="title"><? echo $link->name ?><br></div>
        <br>
        <table class ="details">
            <? if (isset($fileCard->advertiser)): ?>
                <tr>
                    <td class ="title">Advertiser:</td>
                    <td><? echo $fileCard->advertiser ?><br></td>
                </tr>
            <? endif; ?>

            <? if (isset($fileCard->dateSold)): ?>
                <tr>
                    <td class ="title">Date Sold:</td>
                    <td><? echo date(Yii::app()->params['outputDateFormat'], strtotime($fileCard->dateSold)) ?><br></td>
                </tr>
            <? endif; ?>

            <? if (isset($fileCard->broadcastClosed)): ?>
                <tr>
                    <td class ="title">Broadcast $ Closed:</td>
                    <td><? echo '$' . number_format($fileCard->broadcastClosed, 2, '.', ',') ?><br></td>
                </tr>
            <? endif; ?>            

            <? if (isset($fileCard->digitalClosed)): ?>
                <tr>
                    <td class ="title">Digital $ Closed:</td>
                    <td><? echo '$' . number_format($fileCard->digitalClosed, 2, '.', ',') ?><br></td>
                </tr>
            <? endif; ?>                        

            <? if (isset($fileCard->publishingClosed)): ?>
                <tr>
                    <td class ="title">Publishing $ Closed:</td>
                    <td><? echo '$' . number_format($fileCard->publishingClosed, 2, '.', ',') ?><br></td>
                </tr>
            <? endif; ?>                        

            <? if (isset($fileCard->notes)): ?>
                <tr>
                    <td class ="title">Important Info:</td>
                </tr>
                <?
                foreach ($fileCard->notes as $note)
                {
                    echo CHtml::openTag('tr', array());
                    {
                        echo CHtml::openTag('td', array());
                        {
                            echo $note;
                        }
                        echo CHtml::closeTag('td');
                    }
                    echo CHtml::closeTag('tr');
                }
                ?>                                                                                
            <? endif; ?>                                                                                
        </table>
        <br>
        <table class ="sales-info">
            <? if (isset($fileCard->salesStation)): ?>
                <tr>
                    <td class ="title">Station or Newspaper:</td>
                    <td><? echo $fileCard->salesStation ?><br></td>
                </tr>
            <? endif; ?>                            
            
            <? if (isset($fileCard->salesName)): ?>
                <tr>
                    <td class ="title">Sales Contact:</td>
                    <td><? echo $fileCard->salesName ?><br></td>
                </tr>
            <? endif; ?>                

            <? if (isset($fileCard->salesEmail)): ?>
                <tr>
                    <td class ="title">Email:</td>
                    <td><? echo $fileCard->salesEmail ?><br></td>
                </tr>
            <? endif; ?>                                

            <? if (isset($fileCard->salesPhone)): ?>
                <tr>
                    <td class ="title">Phone #:</td>
                    <td><? echo $fileCard->salesPhone ?><br></td>
                </tr>
            <? endif; ?>                                                
        </table>
    </div>
<? endif; ?>
