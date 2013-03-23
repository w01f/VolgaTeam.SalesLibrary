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

		public static function sendLink($link, $partId, $partFormat, $emailTo, $emailCopyTo, $emailFrom, $emailSubject, $emailBody, $expiresIn, $emailToMe)
		{
			$emailFolder = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . Yii::app()->params['emailTemp'];
			$emailFolderLink = Yii::app()->getBaseUrl(true) . '/' . Yii::app()->params['librariesRoot'] . '/' . Yii::app()->params['emailTemp'];
			if (!file_exists($emailFolder))
				mkdir($emailFolder, 0, true);

			$id = uniqid();

			$extension = '';
			if (isset($partFormat) && $partFormat != "null")
			{
				if ($partFormat == 'new office')
					$extension = $link->originalFormat == 'ppt' ? '.pptx' : '.docx';
				else if ($partFormat == 'old office')
					$extension = $link->originalFormat == 'ppt' ? '.ppt' : '.doc';
				else
					$extension = '.' . $partFormat;
			}
			$destinationFileName = $id . (isset($partId) && $partId != "null" ? ('_part' . ($partId + 1)) : '') . '_' . $link->fileName . $extension;
			$destinationPath = $emailFolder . DIRECTORY_SEPARATOR . $destinationFileName;
			if ($link->originalFormat == 'mp4' || $link->originalFormat == 'wmv' || $link->originalFormat == 'video')
				$destinationLink = Yii::app()->getBaseUrl(true) . Yii::app()->createUrl('site/emailLinkGet', array('emailId' => $id));
			else
				$destinationLink = str_replace(' ', '%20', htmlspecialchars($emailFolderLink . '/' . $destinationFileName));

			if (!file_exists($destinationPath))
			{
				if (isset($partId) && $partId != "null" && isset($partFormat) && $partFormat != "null")
				{
					$index = intval($partId);
					$previewRecords = PreviewStorage::model()->findAll('id_container =? and type=? order by length(relative_path), relative_path', array($link->previewId, $partFormat));
					if (count($previewRecords) > $index)
						$sourcePath = $link->parent->parent->parent->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecords[$index]->relative_path);
				}
				else if (isset($partFormat) && $partFormat != "null")
				{
					$previewRecord = PreviewStorage::model()->find('id_container =? and type=?', array($link->previewId, 'pdf'));
					if (isset($previewRecord))
						$sourcePath = $link->parent->parent->parent->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecord->relative_path);
				}
				else
					$sourcePath = $link->filePath;
				copy($sourcePath, $destinationPath);
			}

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

		public static function sendAttachment($attachment, $partId, $partFormat, $emailTo, $emailCopyTo, $emailFrom, $emailSubject, $emailBody, $expiresIn, $emailToMe)
		{
			$emailFolder = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . Yii::app()->params['emailTemp'];
			$emailFolderLink = Yii::app()->getBaseUrl(true) . '/' . Yii::app()->params['librariesRoot'] . '/' . Yii::app()->params['emailTemp'];
			if (!file_exists($emailFolder))
				mkdir($emailFolder, 0, true);

			$id = uniqid();
			$extension = '';
			if (isset($partFormat) && $partFormat != "null")
			{
				if ($partFormat == 'new office')
					$extension = $attachment->originalFormat == 'ppt' ? '.pptx' : '.docx';
				else if ($partFormat == 'old office')
					$extension = $attachment->originalFormat == 'ppt' ? '.ppt' : '.doc';
				else
					$extension = '.' . $partFormat;
			}
			$destinationFileName = $id . (isset($partId) && $partId != "null" ? ('_part' . ($partId + 1)) : '') . '_' . $attachment->name . $extension;
			$destinationPath = $emailFolder . DIRECTORY_SEPARATOR . $destinationFileName;
			if ($attachment->originalFormat == 'mp4' || $attachment->originalFormat == 'wmv' || $attachment->originalFormat == 'video')
				$destinationLink = Yii::app()->getBaseUrl(true) . Yii::app()->createUrl('site/emailLinkGet', array('emailId' => $id));
			else
				$destinationLink = str_replace(' ', '%20', htmlspecialchars($emailFolderLink . '/' . $destinationFileName));

			if (!file_exists($destinationPath))
			{
				if (isset($partId) && $partId != "null" && isset($partFormat) && $partFormat != "null")
				{
					$index = intval($partId);
					$previewRecords = PreviewStorage::model()->findAll('id_container =? and type=? order by length(relative_path), relative_path', array($attachment->previewId, $partFormat));
					if (count($previewRecords) > $index)
						$sourcePath = $attachment->parent->parent->parent->parent->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecords[$index]->relative_path);
				}
				else if (isset($partFormat) && $partFormat != "null")
				{
					$previewRecord = PreviewStorage::model()->find('id_container =? and type=?', array($attachment->previewId, 'pdf'));
					if (isset($previewRecord))
						$sourcePath = $attachment->parent->parent->parent->parent->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecord->relative_path);
				}
				else
					$sourcePath = $attachment->fullPath;
				copy($sourcePath, $destinationPath);
			}

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
