<?php

	/**
	 * Class PinCodeForm
	 */
	class PinCodeForm extends CFormModel
	{
		public $pageId;
		public $pinCode;

		/**
		 * @return array
		 */
		public function rules()
		{
			return array(
				array('pinCode', 'validatePinCode'),
			);
		}

		/**
		 * @return array
		 */
		public function attributeLabels()
		{
			return array(
				'pinCode' => 'Pin-code:',
			);
		}

		public function validatePinCode()
		{
			$pageRecord = QPageRecord::model()->findByPk($this->pageId);
			if (!(isset($pageRecord) && ((isset($pageRecord->pin_code) && $pageRecord->pin_code == $this->pinCode) || !isset($pageRecord->pin_code))))
				$this->addError('pinCode', 'Incorrect Pin-code');
		}
	}