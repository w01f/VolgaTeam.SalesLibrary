<?php
class EmailedLinkStorage extends CActiveRecord
{
    public static function model($className = __CLASS__)
    {
        return parent::model($className);
    }

    public function tableName()
    {
        return '{{emailed_link}}';
    }

    public static function saveEmailedLink($id, $linkId, $libraryId, $filePath, $fileLink, $expiresIn, $senderLogin, $senderEmail, $recipients)
    {
        $emailedLink = new EmailedLinkStorage();
        $emailedLink->id = $id;
        $emailedLink->id_link = $linkId;
        $emailedLink->id_library = $libraryId;
        $emailedLink->path = $filePath;
        $emailedLink->link = $fileLink;
        $emailedLink->initial_date = date(Yii::app()->params['mysqlDateFormat'], strtotime(date('y:m:d')));
        $emailedLink->expires_in = $expiresIn;
        $emailedLink->sender_login = $senderLogin;
        $emailedLink->sender_email = $senderEmail;
        $emailedLink->recipients = $recipients;
        $emailedLink->save();
    }

	public static function sendLink($link,$emailTo,$emailCopyTo,$emailFrom,$emailSubject,$emailBody,$expiresIn,$emailToMe)
	{
		$emailFolder = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . Yii::app()->params['emailTemp'];
		$emailFolderLink = Yii::app()->getBaseUrl(true) . '/' . Yii::app()->params['librariesRoot'] . '/' . Yii::app()->params['emailTemp'];
		if (!file_exists($emailFolder))
			mkdir($emailFolder, 0, true);

		$id = uniqid();
		$destinationPath = $emailFolder . DIRECTORY_SEPARATOR . $id . $link->fileName;
		$destinationLink = str_replace(' ', '%20', htmlspecialchars($emailFolderLink . '/' . $id . $link->fileName));

		if (!file_exists($destinationPath))
			copy($link->filePath, $destinationPath);

		$recipients = explode(";", $emailTo);
		$recipientsCopy = isset($emailCopyTo) && $emailCopyTo != '' ? explode(";", $emailCopyTo) : null;
		if (isset($recipientsCopy))
			$recipientsWhole = array_merge($recipients, $recipientsCopy);
		else
			$recipientsWhole = $recipients;

		$userId = Yii::app()->user->getId();
		if (isset($userId))
			UserRecipientStorage::setRecipientsForUser($userId, $recipientsWhole);

		EmailedLinkStorage::saveEmailedLink($id, $link->id, $link->libraryId, $destinationPath, $destinationLink, $expiresIn, Yii::app()->user->login, $emailFrom, implode('; ', $recipientsWhole));

		if ($link->originalFormat == 'mp4' || $link->originalFormat == 'video')
			$destinationLink = Yii::app()->getBaseUrl(true) . Yii::app()->createUrl('site/emailLinkGet', array('emailId' => $id));

		if ($emailToMe == 'true')
			$recipientsCopy[] = $emailFrom;

		$message = Yii::app()->email;
		$message->to = $recipients;
		$message->cc = $recipientsCopy;
		$message->subject = $emailSubject;
		$message->from = $emailFrom;
		$message->view = 'sendLink';
		$message->viewVars = array('body' => $emailBody, 'link' => $destinationLink, 'expiresIn' => $expiresIn);
		$message->send();
	}

	public static function sendAttachment($attachment,$emailTo,$emailCopyTo,$emailFrom,$emailSubject,$emailBody,$expiresIn,$emailToMe)
	{
		$emailFolder = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . Yii::app()->params['emailTemp'];
		$emailFolderLink = Yii::app()->getBaseUrl(true) . '/' . Yii::app()->params['librariesRoot'] . '/' . Yii::app()->params['emailTemp'];
		if (!file_exists($emailFolder))
			mkdir($emailFolder, 0, true);

		$id = uniqid();
		$destinationPath = $emailFolder . DIRECTORY_SEPARATOR . $id . $attachment->name;
		$destinationLink = str_replace(' ', '%20', htmlspecialchars($emailFolderLink . '/' . $id . $attachment->name));

		if (!file_exists($destinationPath))
			copy($attachment->fullPath, $destinationPath);

		$recipients = explode(";", $emailTo);
		$recipientsCopy = isset($emailCopyTo) && $emailCopyTo != '' ? explode(";", $emailCopyTo) : null;
		if (isset($recipientsCopy))
			$recipientsWhole = array_merge($recipients, $recipientsCopy);
		else
			$recipientsWhole = $recipients;

		$userId = Yii::app()->user->getId();
		if (isset($userId))
			UserRecipientStorage::setRecipientsForUser($userId, $recipientsWhole);

		EmailedLinkStorage::saveEmailedLink($id, $attachment->id, $attachment->libraryId, $destinationPath, $destinationLink, $expiresIn, Yii::app()->user->login, $emailFrom, implode('; ', $recipientsWhole));

		if ($attachment->originalFormat == 'mp4' || $attachment->originalFormat == 'video')
			$destinationLink = Yii::app()->getBaseUrl(true) . Yii::app()->createUrl('site/emailLinkGet', array('emailId' => $id));

		if ($emailToMe == 'true')
			$recipientsCopy[] = $emailFrom;

		$message = Yii::app()->email;
		$message->to = $recipients;
		$message->cc = $recipientsCopy;
		$message->subject = $emailSubject;
		$message->from = $emailFrom;
		$message->view = 'sendLink';
		$message->viewVars = array('body' => $emailBody, 'link' => $destinationLink, 'expiresIn' => $expiresIn);
		$message->send();
	}
    
    public static function getEmailedLink($id)
    {
        return self::model()->findByPk($id);
    }
}
