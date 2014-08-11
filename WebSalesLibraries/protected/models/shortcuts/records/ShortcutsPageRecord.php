<?php

	/**
	 * Class ShortcutsPageRecord
	 * @property string id
	 * @property string id_tab
	 * @property mixed name
	 * @property mixed order
	 * @property mixed image_path
	 * @property mixed source_path
	 * @property mixed enabled
	 * @property mixed config
	 */
	class ShortcutsPageRecord extends CActiveRecord
	{
		/**
		 * @param string $className
		 * @return ShortcutsPageRecord
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
			return '{{shortcut_page}}';
		}

		/**
		 * @param $login string
		 * @return bool
		 */
		public function isEnabled($login)
		{
			$approvedUsers = array();
			$pageConfig = new DOMDocument();
			$pageConfig->loadXML($this->config);
			$xpath = new DomXPath($pageConfig);
			$queryResult = $xpath->query('//Config/ApprovedUsers/User');
			foreach ($queryResult as $userNode)
				$approvedUsers[] = trim($userNode->nodeValue);
			return $this->enabled && (count($approvedUsers) == 0 || in_array($login, $approvedUsers));
		}

		/**
		 * @return HomeBar
		 */
		public function getHomeBar()
		{
			return new HomeBar($this);
		}

		/**
		 * @return SearchBar
		 */
		public function getSearchBar()
		{
			return new SearchBar($this);
		}

		public static function clearData()
		{
			self::model()->deleteAll();
		}
	}