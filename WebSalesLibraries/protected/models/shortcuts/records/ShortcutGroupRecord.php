<?php

	/**
	 * Class ShortcutGroupRecord
	 * @property string id
	 * @property int order
	 * @property string source_path
	 * @property string date_modify
	 * @property string config
	 */
	class ShortcutGroupRecord extends CActiveRecord
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
			return '{{shortcut_group}}';
		}

		/**
		 * @param $isPhone boolean
		 * @return BaseShortcut[]
		 */
		public function getTopLevelLinks($isPhone)
		{
			$shortcuts = array();
			/** @var  $shortcutRecords ShortcutLinkRecord[] */
			$shortcutRecords = ShortcutLinkRecord::model()->findAll(array('order' => '`order`', 'condition' => 'id_group=:id_group and id_parent is null', 'params' => array(':id_group' => $this->id)));
			foreach ($shortcutRecords as $linkRecord)
			{
				/** @var  $shortcut  BaseShortcut */
				$shortcut = $linkRecord->getModel($isPhone);
				if ($shortcut->isAccessGranted)
					$shortcuts[] = $shortcut;
			}
			return $shortcuts;
		}

		public static function clearData()
		{
			self::model()->deleteAll();
		}

		public static function getLastUpdate()
		{
			return strtotime(Yii::app()->db->createCommand()
				->select('max(date_modify)')
				->from('tbl_shortcut_group')
				->queryScalar());
		}

		/**
	 	* @return string
	 	*/
		public static function getShortcutsRoot()
		{
			return Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Shortcuts';
		}

		/**
		 * @param $isPhone boolean
		 * @return ShortcutGroup[]
		 */
		public static function getAvailableGroups($isPhone)
		{
			$groups = array();
			$groupRecords = self::model()->findAll(array('order' => '`order`'));
			foreach ($groupRecords as $groupRecord)
			{
				$group = new ShortcutGroup($groupRecord, $isPhone);
				if ($group->isAccessGranted)
					$groups[] = $group;
			}
			return $groups;
		}
	}