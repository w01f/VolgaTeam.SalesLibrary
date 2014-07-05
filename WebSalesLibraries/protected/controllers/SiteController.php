<?php

	class SiteController extends IsdController
	{
		public $defaultAction = 'index';

		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'site');
		}

		public function actionIndex()
		{
			$this->pageTitle = Yii::app()->name;
			$tickerRecords = TickerLinkRecord::getLinks();
			$this->render('index', array('tickerRecords' => $tickerRecords));
		}

		public function actionError()
		{
			if ($error = Yii::app()->errorHandler->error)
			{
				$this->pageTitle = Yii::app()->name . ' - Error';
				if (Yii::app()->request->isAjaxRequest)
					echo $error['message'];
				else
					$this->render('errorMessage', $error);
			}
		}

		public function actionDownloadDialog()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			if (isset($linkId))
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
				{
					if ($linkRecord->format == 'video' || $linkRecord->format == 'wmv' || $linkRecord->format == 'mp4')
						$this->renderPartial('downloadVideoDialog', array('format' => $linkRecord->format), false, true);
				}
				else
				{
					$attachmentRecord = AttachmentRecord::getAttachmentById($linkId);
					if (isset($attachmentRecord))
					{
						if ($attachmentRecord->format == 'video' || $attachmentRecord->format == 'wmv' || $attachmentRecord->format == 'mp4')
							$this->renderPartial('downloadVideoDialog', array('format' => $attachmentRecord->format), false, true);
					}
				}
			}
		}

		public function actionDownloadFile()
		{
			$linkId = Yii::app()->request->getQuery('linkId');
			$partId = Yii::app()->request->getQuery('partId');
			$partFormat = Yii::app()->request->getQuery('partFormat');
			$format = Yii::app()->request->getQuery('format');
			if (isset($linkId))
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$libraryManager->getLibraries();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					if (isset($library))
					{
						$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
						$link->load($linkRecord);
						$name = $link->name;
						$fileName = $link->fileName;
						$originalFormat = $linkRecord->format;
						if ($linkRecord->format == 'video' || $linkRecord->format == 'mp4' || $linkRecord->format == 'wmv')
						{
							if ($format == 'wmv')
							{
								if ($linkRecord->format == 'wmv')
									$path = $link->filePath;
								else
								{
									/** @var $previewRecord PreviewRecord */
									$previewRecord = PreviewRecord::model()->find('id_container =? and type=?', array($linkRecord->id_preview, 'wmv'));
									if (isset($previewRecord))
										$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecord->relative_path);
									else
										$path = $link->filePath;
								}
							}
							else if ($format == 'mp4')
							{
								if ($linkRecord->format == 'mp4')
									$path = $link->filePath;
								else
								{
									/** @var $previewRecord PreviewRecord */
									$previewRecord = PreviewRecord::model()->find('id_container =? and type=?', array($linkRecord->id_preview, 'mp4'));
									if (isset($previewRecord))
										$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecord->relative_path);
								}
							}
							if (isset($path))
								$fileName = basename($path);
						}
						else if (isset($partId) && $partId != "null" && isset($partFormat) && $partFormat != "null")
						{
							$index = intval($partId);
							$previewRecords = PreviewRecord::model()->findAll('id_container =? and type=? order by length(relative_path), relative_path', array($linkRecord->id_preview, $partFormat));
							if (count($previewRecords) > $index)
							{
								$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecords[$index]->relative_path);
								$fileName = str_replace('.' . $link->fileExtension, '', $link->fileName) . "-" . basename($path);
							}
						}
						else if ($format == 'pdf')
						{
							/** @var $previewRecord PreviewRecord */
							$previewRecord = PreviewRecord::model()->find('id_container =? and type=?', array($linkRecord->id_preview, 'pdf'));
							if (isset($previewRecord))
							{
								$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecord->relative_path);
								$fileName = basename($path);
							}
						}
						else
							$path = $link->filePath;
					}
				}
				else
				{
					$attachmentRecord = AttachmentRecord::getAttachmentById($linkId);
					if (isset($attachmentRecord))
					{
						$name = $attachmentRecord->name;
						$fileName = $attachmentRecord->name;
						$originalFormat = $attachmentRecord->format;
						$libraryManager = new LibraryManager();
						$libraryManager->getLibraries();
						$library = $libraryManager->getLibraryById($attachmentRecord->id_library);
						if (isset($library))
						{
							if ($attachmentRecord->format == 'video' || $attachmentRecord->format == 'wmv' || $attachmentRecord->format == 'mp4')
							{
								if ($format == 'wmv')
								{
									if ($attachmentRecord->format == 'wmv')
										$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $attachmentRecord->path);
									else
									{
										/** @var $previewRecord PreviewRecord */
										$previewRecord = PreviewRecord::model()->find('id_container =? and type=?', array($attachmentRecord->id_preview, 'wmv'));
										if (isset($previewRecord))
											$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecord->relative_path);
										else
											$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $attachmentRecord->path);
									}
								}
								else if ($format == 'mp4')
								{
									if ($format == 'mp4')
									{
										if ($attachmentRecord->format == 'mp4')
											$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $attachmentRecord->path);
										else
										{
											/** @var $previewRecord PreviewRecord */
											$previewRecord = PreviewRecord::model()->find('id_container =? and type=?', array($attachmentRecord->id_preview, 'mp4'));
											if (isset($previewRecord))
												$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecord->relative_path);
										}
									}
								}
								if (isset($path))
									$fileName = basename($path);
							}
							else if (isset($partId) && $partId != "null" && isset($partFormat) && $partFormat != "null")
							{
								$index = intval($partId);
								$previewRecords = PreviewRecord::model()->findAll('id_container =? and type=? order by length(relative_path), relative_path', array($attachmentRecord->id_preview, $partFormat));
								if (count($previewRecords) > $index)
								{
									$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecords[$index]->relative_path);
									$fileName = $attachmentRecord->name . "-" . basename($path);
								}
							}
							else if ($format == 'pdf')
							{
								/** @var $previewRecord PreviewRecord */
								$previewRecord = PreviewRecord::model()->find('id_container =? and type=?', array($attachmentRecord->id_preview, $format));
								if (isset($previewRecord))
								{
									$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecord->relative_path);
									$fileName = basename($path);
								}
							}
							else
								$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $attachmentRecord->path);
						}
					}
				}
			}
			if (isset($path))
			{
				/** @var $name string */
				/** @var $fileName string */
				/** @var $originalFormat string */
				StatisticActivityRecord::WriteActivity('Link', 'Download', array('Name' => $name, 'File' => basename($path), 'Original Format' => $originalFormat, 'Format' => $format));
				return Yii::app()->getRequest()->sendFile($fileName, @file_get_contents($path));
			}
			Yii::app()->end();
			return null;
		}

		public function actionEmailLinkDialog()
		{
			$userId = Yii::app()->user->getId();
			if (isset($userId))
				$availableEmails = UserRecipientRecord::getRecipientsByUser($userId);
			if (!isset($availableEmails))
				$availableEmails = null;
			$this->renderPartial('emailDialog', array('availableEmails' => $availableEmails), false, true);
		}

		public function actionEmailLinkSend()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$partId = Yii::app()->request->getPost('partId');
			$partFormat = Yii::app()->request->getPost('partFormat');
			$emailTo = Yii::app()->request->getPost('emailTo');
			$emailCopyTo = Yii::app()->request->getPost('emailCopyTo');
			$emailFrom = Yii::app()->request->getPost('emailFrom');
			$emailToMe = Yii::app()->request->getPost('emailToMe');
			$emailSubject = Yii::app()->request->getPost('emailSubject');
			$emailBody = Yii::app()->request->getPost('emailBody');
			$expiresIn = Yii::app()->request->getPost('expiresIn');
			if (isset($expiresIn) && $expiresIn == '')
				$expiresIn = null;

			if (isset($emailTo) && isset($emailFrom) && isset($emailSubject) && isset($emailBody) && $emailTo != '' && $emailFrom != '')
			{
				$linkRecord = LinkRecord::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryId = $linkRecord->id_library;
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($libraryId);
					if (isset($library))
					{
						$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
						$link->load($linkRecord);
						StatisticActivityRecord::WriteActivity('Link', 'Email', array('Name' => $link->name, 'File' => $link->fileName, 'Format' => $link->originalFormat, 'To' => $emailTo, 'Copy' => $emailCopyTo, 'Subject' => $emailSubject));
						EmailedLinkRecord::sendLink($link, $partId, $partFormat, $emailTo, $emailCopyTo, $emailFrom, $emailSubject, $emailBody, $expiresIn, $emailToMe);
					}
					else
					{
						echo 'Library was not found';
						echo $linkRecord->id_library;
					}
				}
				else
				{
					$attachmentRecord = AttachmentRecord::getAttachmentById($linkId);
					if (isset($attachmentRecord))
					{
						$linkRecord = LinkRecord::getLinkById($attachmentRecord->id_link);
						if (isset($linkRecord))
						{
							$libraryId = $linkRecord->id_library;
							$libraryManager = new LibraryManager();
							$library = $libraryManager->getLibraryById($libraryId);
							if (isset($library))
							{
								$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
								$link->load($linkRecord);
								$attachment = new Attachment($link);
								$attachment->load($attachmentRecord);
								StatisticActivityRecord::WriteActivity('Link', 'Email', array('Name' => $attachment->name, 'File' => $attachment->name, 'Format' => $attachment->originalFormat, 'To' => $emailTo, 'Copy' => $emailCopyTo, 'Subject' => $emailSubject));
								EmailedLinkRecord::sendAttachment($attachment, $partId, $partFormat, $emailTo, $emailCopyTo, $emailFrom, $emailSubject, $emailBody, $expiresIn, $emailToMe);
							}
							else
							{
								echo 'Library was not found';
								echo $linkRecord->id_library;
							}
						}
					}
				}
			}
			Yii::app()->end();
		}

		public function actionEmailLinkSuccess()
		{
			$this->renderPartial('successDialog', array('header' => 'Email sent', 'content' => 'The email has been sent by the adSALESapps server.<br>Tell your Recipient they MAY want to check their Spam or Junk mail if they do not receive the link.'), false, true);
		}

		public function actionEmailLinkGet()
		{
			$id = Yii::app()->request->getQuery('emailId');
			if (isset($id))
			{
				$emailedLinkRecord = EmailedLinkRecord::getEmailedLink($id);
				if (isset($emailedLinkRecord))
				{
					/** @var $userRecord UserRecord */
					$userRecord = UserRecord::model()->find('LOWER(login)=?', array(strtolower($emailedLinkRecord->sender_login)));
					if (isset($userRecord))
						$senderName = $userRecord->first_name . ' ' . $userRecord->last_name;
					else
						$senderName = 'adSalesApp User';
					$linkRecord = LinkRecord::getLinkById($emailedLinkRecord->id_link);
					if (isset($linkRecord))
					{
						$libraryManager = new LibraryManager();
						$libraryManager->getLibraries();
						$library = $libraryManager->getLibraryById($emailedLinkRecord->id_library);
						if (isset($library))
						{
							$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
							$link->load($linkRecord);
							$this->render('emailVideo', array('link' => $link, 'expiresIn' => $emailedLinkRecord->expires_in, 'senderName' => $senderName));
						}
						else
						{
							echo 'Library was not found';
							echo $emailedLinkRecord->id_library;
						}
					}
					else
					{
						$attachmentRecord = AttachmentRecord::getAttachmentById($emailedLinkRecord->id_link);
						if (isset($attachmentRecord))
						{
							$linkRecord = LinkRecord::getLinkById($attachmentRecord->id_link);
							if (isset($linkRecord))
							{
								$libraryManager = new LibraryManager();
								$libraryManager->getLibraries();
								$library = $libraryManager->getLibraryById($emailedLinkRecord->id_library);
								if (isset($library))
								{
									$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
									$link->load($linkRecord);
									$attachment = new Attachment($link);
									$attachment->load($attachmentRecord);
									$this->render('emailVideo', array('link' => $attachment, 'expiresIn' => $emailedLinkRecord->expires_in, 'senderName' => $senderName));
								}
								else
								{
									echo 'Library was not found';
									echo $emailedLinkRecord->id_library;
								}
							}
						}
					}
				}
			}
			Yii::app()->end();
		}

		public function actionSwitchVersion()
		{
			$version = Yii::app()->request->getPost('siteVersion');
			if (isset($version))
			{
				Yii::app()->cacheDB->set('siteVersion', $version, (60 * 60 * 24 * 7));
				Yii::app()->end();
			}
		}
	}
