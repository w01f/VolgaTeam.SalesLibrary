<?php
class CleanExpiredEmailsCommand extends CConsoleCommand
{
    public function run($args)
    {
        echo "Job started...\n";

        $emailRecords = EmailedLinkStorage::model()->findAll('expires_in is not null');
        if (isset($emailRecords))
        {
            foreach ($emailRecords as $emailRecord)
            {
                $today = strtotime('today');
                $expiredDate = strtotime($emailRecord->initial_date . ' + ' . $emailRecord->expires_in . ' day');
                if ($expiredDate < $today)
                {
                    $link = LinkStorage::getLinkById($emailRecord->id_link);
                    if(isset($link))
                        $linkName = $link->name;
                    else
                        $linkName = $emailRecord->link;
                    $outputMessage = "Link expired: ".$linkName." for ".$emailRecord->recipients."\n";
                    
                    if(is_readable($emailRecord->path))
                        unlink($emailRecord->path);
                    
                    $emailRecord->delete();
                    
                    echo $outputMessage;
                }
            }
        }
        echo "Job completed.\n";
    }
}

?>
