<?php

	/**
	 * Class PasswordForm
	 */
	class PublicPageContentShortcutPasswordForm extends CFormModel
	{
		public $shortcutId;
		public $password;
		public $isPhone;
		public $showError;

		/**
		 * @return array
		 */
		public function rules()
		{
			return array(
				array('password', 'validatePassword'),
			);
		}

		/**
		 * @return array
		 */
		public function attributeLabels()
		{
			return array(
				'password' => 'Password:',
			);
		}

		public function validatePassword()
		{
			/** @var  $shortcutRecord ShortcutLinkRecord */
			$shortcutRecord = ShortcutLinkRecord::model()->findByPk($this->shortcutId);
			/** @var  $shortcut PageContentShortcut */
			$shortcut = $shortcutRecord->getModel($this->isPhone);
			$shortcut->loadPageConfig();
			if (!isset($this->password) || !(isset($this->password) && $this->password == $shortcut->publicPassword))
				$this->addError('password', 'Incorrect Password');
		}
	}