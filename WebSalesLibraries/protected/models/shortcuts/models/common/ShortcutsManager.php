<?

	/**
	 * Class ShortcutsManager
	 */
	class ShortcutsManager
	{
		/** @return datetime */
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
			return \application\models\wallbin\models\web\LibraryManager::getLibrariesRootPath() . DIRECTORY_SEPARATOR . 'Shortcuts';
		}

		/**
		 * @param $isPhone boolean
		 * @return ShortcutGroup[]
		 */
		public static function getAvailableGroups($isPhone)
		{
			$groups = array();
			$selectedSuperGroupTag = self::getSelectedSuperGroup();
			/** @var ShortcutGroupRecord $groupRecords */
			$groupRecords = ShortcutGroupRecord::model()->findAll(array('order' => '`order`'));
			foreach ($groupRecords as $groupRecord)
			{
				$group = new ShortcutGroup($groupRecord, $selectedSuperGroupTag, $isPhone);
				if ($group->isAccessGranted)
					$groups[] = $group;
			}
			return $groups;
		}

		/**
		 * @param string $superGroupTag
		 */
		public static function setSelectedSuperGroup($superGroupTag)
		{
			$superGroupTagName = 'selected-super-group';
			if (Yii::app()->params['menu']['SaveSuperGroup'])
			{
				$cookie = new CHttpCookie($superGroupTagName, $superGroupTag);
				$cookie->expire = time() + (60 * 60 * 24 * 7);
				Yii::app()->request->cookies[$superGroupTagName] = $cookie;
			}
			else
				Yii::app()->session[$superGroupTagName] = $superGroupTag;
		}

		/**
		 * @return string
		 */
		public static function getSelectedSuperGroup()
		{
			$superGroupTagName = 'selected-super-group';
			if (Yii::app()->params['menu']['SaveSuperGroup'])
				return isset(Yii::app()->request->cookies[$superGroupTagName]) ?
					Yii::app()->request->cookies[$superGroupTagName]->value :
					null;
			else
				return isset(Yii::app()->session[$superGroupTagName]) ?
					Yii::app()->session[$superGroupTagName] :
					null;
		}
	}