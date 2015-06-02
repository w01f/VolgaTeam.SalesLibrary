<?

	/**
	 * Class TabPages
	 */
	class TabPages
	{
		/** @return array */
		public static function getList()
		{
			foreach (Yii::app()->params as $key => $row)
			{
				if (is_array($row))
					if (array_key_exists('position', $row))
						$tabPages[$key] = $row['position'];
			}

			/** @var  $tabShortcuts ShortcutsTabRecord[] */
			$tabShortcuts = ShortcutsTabRecord::model()->findAll(array('order' => '`order`', 'condition' => 'enabled=:enabled', 'params' => array(':enabled' => true)));
			if (isset($tabShortcuts))
				foreach ($tabShortcuts as $tabShortcutsRecord)
					if ($tabShortcutsRecord->isAvailable(Yii::app()->user))
						$tabPages['shortcuts-tab-' . $tabShortcutsRecord->id] = $tabShortcutsRecord->order;

			asort($tabPages);
			return $tabPages;
		}

		/**
		 * @param $tabName string
		 * @return string
		 */
		public static function getTabUrl($tabName)
		{
			if ($tabName == 'home_tab')
				return Yii::app()->createAbsoluteUrl('site/empty');
			else if ($tabName == 'search_full_tab')
				return Yii::app()->createAbsoluteUrl('site/empty');
			else if ($tabName == 'favorites_tab')
				return Yii::app()->createAbsoluteUrl('site/empty');
			else if (strpos($tabName, 'shortcuts-tab-') !== false)
			{
				return Yii::app()->createAbsoluteUrl('shortcuts/getTab', array('tabId' => str_replace('shortcuts-tab-', '', $tabName)));
			}
			return Yii::app()->createAbsoluteUrl('site/empty');
		}
	}