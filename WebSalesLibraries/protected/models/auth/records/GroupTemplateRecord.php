<?php

	/**
	 * Class GroupTemplateRecord
	 * @property string id
	 * @property mixed name
	 */
	class GroupTemplateRecord extends CActiveRecord
	{
		/**
		 * @param string $className
		 * @return CActiveRecord
		 */
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		/**
		 * @return string
		 */
		public function tableName()
		{
			return '{{group_template}}';
		}

		/**
		 * @param $groupTemplateContent
		 */
		public static function updateData($groupTemplateContent)
		{
			if ($groupTemplateContent !== false)
			{
				foreach ($groupTemplateContent as $line)
					$groupTemplates[] = trim($line);
			}
			if (isset($groupTemplates))
				foreach ($groupTemplates as $groupTemplate)
				{
					$groupTemplateId = uniqid();
					$groupTemplateRecord = new GroupTemplateRecord();
					$groupTemplateRecord->id = $groupTemplateId;
					$groupTemplateRecord->name = $groupTemplate;
					$groupTemplateRecord->save();
				}
		}

		public static function clearData()
		{
			self::model()->deleteAll();
		}
	}
