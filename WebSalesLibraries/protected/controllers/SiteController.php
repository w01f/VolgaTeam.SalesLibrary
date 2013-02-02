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
			$libraryManager = new LibraryManager();
			if (count($libraryManager->getLibraries()) > 0)
				$this->render('index');
			else
				$this->render('unauthorized');
		}

		public function actionError()
		{
			if ($error = Yii::app()->errorHandler->error)
			{
				if (Yii::app()->request->isAjaxRequest)
					echo $error['message'];
				else
					$this->render('errorMessage', $error);
			}
		}

		public function actionLogin()
		{
			$loginModel = new LoginForm();

			$attributes = Yii::app()->request->getPost('LoginForm');
			if (isset($attributes))
			{
				$loginModel->attributes = $attributes;
				if ($loginModel->validate() && $loginModel->login())
				{
					if ($loginModel->needToResetPassword)
					{
						$this->redirect($this->createUrl('site/changePassword', array(
							'login' => $loginModel->login,
							'password' => $loginModel->password,
							'rememberMe' => $loginModel->rememberMe
						)));
					}
					else
						$this->redirect(Yii::app()->user->returnUrl);
				}
			}
			$this->render('login', array('formData' => $loginModel));
		}

		public function actionLogout()
		{
			Yii::app()->user->logout();
			Yii::app()->end();
		}

		public function actionChangePassword()
		{
			$changePasswordModel = new ChangePasswordForm();
			$attributes = Yii::app()->request->getPost('ChangePasswordForm');
			if (isset($attributes))
			{
				$changePasswordModel->attributes = $attributes;
				$changePasswordModel->login = $attributes['login'];
				$changePasswordModel->oldPassword = $attributes['oldPassword'];
				$changePasswordModel->rememberMe = $attributes['rememberMe'];
				if ($changePasswordModel->validate() && $changePasswordModel->changePassword())
					$this->redirect(Yii::app()->user->returnUrl);
				else
					$this->render('changePassword', array('formData' => $changePasswordModel));
			}
			else
			{
				$login = Yii::app()->request->getQuery('login');
				$oldPassword = Yii::app()->request->getQuery('password');
				$rememberMe = Yii::app()->request->getQuery('rememberMe', false);
				if (isset($login) && isset($oldPassword))
				{
					$changePasswordModel->login = $login;
					$changePasswordModel->oldPassword = $oldPassword;
					$changePasswordModel->rememberMe = $rememberMe;
					$this->render('changePassword', array('formData' => $changePasswordModel));
				}
			}
		}

		public function actionRecoverPasswordDialog()
		{
			$this->renderPartial('recoverPassword', array(), false, true);
		}

		public function actionRecoverPasswordDialogSuccess()
		{
			$this->renderPartial('successDialog', array('header' => 'Password Recovery', 'content' => 'A temporary password has been sent<br>Check your inbox of junk mail filter'), false, true);
		}

		public function actionValidateUserByEmail()
		{
			$login = Yii::app()->request->getPost('login');
			$email = Yii::app()->request->getPost('email');
			$result = 'Error while validating user. Try again or contact to technical support';
			if (isset($login) && isset($email))
				$result = UserStorage::validateUserByEmail($login, $email);
			echo $result;
		}

		public function actionRecoverPassword()
		{
			$login = Yii::app()->request->getPost('login');
			if (isset($login))
			{
				$password = UserStorage::generatePassword();
				UserStorage::changePassword($login, $password);
				ResetPasswordStorage::resetPasswordForUser($login, $password, false);
			}
		}

		public function actionDownloadDialog()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			if (isset($linkId))
			{
				$linkRecord = LinkStorage::getLinkById($linkId);
				if (isset($linkRecord))
				{
					if ($linkRecord->format == 'video' || $linkRecord->format == 'mp4')
						$this->renderPartial('downloadVideoDialog', array('format' => $linkRecord->format), false, true);
					else
						$this->renderPartial('downloadDialog', array(), false, true);
				}
				else
				{
					$attachmentRecord = AttachmentStorage::getAttachmentById($linkId);
					if (isset($attachmentRecord))
					{
						if ($attachmentRecord->format == 'video' || $attachmentRecord->format == 'mp4')
							$this->renderPartial('downloadVideoDialog', array('format' => $attachmentRecord->format), false, true);
						else
							$this->renderPartial('downloadDialog', array(), false, true);
					}
				}
			}
		}

		public function actionDownloadFile()
		{
			$linkId = Yii::app()->request->getQuery('linkId');
			$format = Yii::app()->request->getQuery('format');
			if (isset($linkId))
			{
				$linkRecord = LinkStorage::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$libraryManager->getLibraries();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					if (isset($library))
					{
						$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
						$link->load($linkRecord);
						if ($linkRecord->format == 'video' || $linkRecord->format == 'mp4')
						{
							if ($format == 'wmv')
								$path = $link->filePath;
							else if ($format == 'mp4')
							{
								$previewRecord = PreviewStorage::model()->find('id_container =? and type=?', array($linkRecord->id_preview, 'mp4'));
								if (isset($previewRecord))
									$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecord->relative_path);
							}
						}
					}
				}
				else
				{
					$attachmentRecord = AttachmentStorage::getAttachmentById($linkId);
					if (isset($attachmentRecord))
					{
						$libraryManager = new LibraryManager();
						$libraryManager->getLibraries();
						$library = $libraryManager->getLibraryById($attachmentRecord->id_library);
						if (isset($library))
						{
							if ($attachmentRecord->format == 'video' || $attachmentRecord->format == 'mp4')
							{
								if ($format == 'wmv')
									$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $attachmentRecord->path);
								else if ($format == 'mp4')
								{
									$previewRecord = PreviewStorage::model()->find('id_container =? and type=?', array($attachmentRecord->id_preview, 'mp4'));
									if (isset($previewRecord))
										$path = $library->storagePath . DIRECTORY_SEPARATOR . str_replace('\\', '/', $previewRecord->relative_path);
								}
							}
						}
					}
				}
			}
			if (isset($path))
				return Yii::app()->getRequest()->sendFile(basename($path), @file_get_contents($path));
			return null;
		}

		public function actionEmailLinkDialog()
		{
			$userId = Yii::app()->user->getId();
			if (isset($userId))
				$availableEmails = UserRecipientStorage::getRecipientsByUser($userId);
			if (!isset($availableEmails))
				$availableEmails = null;
			$this->renderPartial('emailDialog', array('availableEmails' => $availableEmails), false, true);
		}

		public function actionEmailLinkSend()
		{
			$linkId = Yii::app()->request->getPost('linkId');
			$emailTo = Yii::app()->request->getPost('emailTo');
			$emailCopyTo = Yii::app()->request->getPost('emailCopyTo');
			$emailFrom = Yii::app()->request->getPost('emailFrom');
			$emailToMe = Yii::app()->request->getPost('emailToMe');
			$emailSubject = Yii::app()->request->getPost('emailSubject');
			$emailBody = Yii::app()->request->getPost('emailBody');
			$expiresIn = Yii::app()->request->getPost('expiresIn');
			if (isset($expiresIn) && $expiresIn == '')
				unset($expiresIn);

			if (isset($emailTo) && isset($emailFrom) && isset($emailSubject) && isset($emailBody) && $emailTo != '' && $emailFrom != '')
			{
				$linkRecord = LinkStorage::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryId = $linkRecord->id_library;
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($libraryId);
					if (isset($library))
					{
						$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
						$link->load($linkRecord);
						EmailedLinkStorage::sendLink($link, $emailTo, $emailCopyTo, $emailFrom, $emailSubject, $emailBody, $expiresIn, $emailToMe);
					}
					else
					{
						echo 'Library was not found';
						echo $linkRecord->id_library;
					}
				}
				else
				{
					$attachmentRecord = AttachmentStorage::getAttachmentById($linkId);
					if (isset($attachmentRecord))
					{
						$linkRecord = LinkStorage::getLinkById($attachmentRecord->id_link);
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
								EmailedLinkStorage::sendAttachment($attachment, $emailTo, $emailCopyTo, $emailFrom, $emailSubject, $emailBody, $expiresIn, $emailToMe);
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
				$emailedLinkRecord = EmailedLinkStorage::getEmailedLink($id);
				if (isset($emailedLinkRecord))
				{
					$userRecord = UserStorage::model()->find('LOWER(login)=?', array(strtolower($emailedLinkRecord->sender_login)));
					if (isset($userRecord))
						$senderName = $userRecord->first_name . ' ' . $userRecord->last_name;
					else
						$senderName = 'adSalesApp User';
					$linkRecord = LinkStorage::getLinkById($emailedLinkRecord->id_link);
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
						$attachmentRecord = AttachmentStorage::getAttachmentById($emailedLinkRecord->id_link);
						if (isset($attachmentRecord))
						{
							$linkRecord = LinkStorage::getLinkById($attachmentRecord->id_link);
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

		public function actionDisclaimerWarning()
		{
			$this->renderPartial('disclaimerWarning', array('content' => Yii::app()->params['login']['disclaimerWarningText']), false, true);
		}
	}
