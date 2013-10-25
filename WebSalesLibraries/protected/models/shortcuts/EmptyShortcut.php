<?php
	class EmptyShortcut
	{
		public $type;
		public $imagePath;

		public function __construct($linkRecord)
		{
			$this->type = 'none';
			$this->imagePath = Yii::app()->getBaseUrl(true) . $linkRecord->image_path . '?' . $linkRecord->id;
		}
	}