<?php

	class ShortcutsPageStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{shortcut_page}}';
		}

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

		public function getHomeBar()
		{
			return new HomeBar($this);
		}

		public function getSearchBar()
		{
			return new SearchBar($this->config);
		}

		public function getGrid()
		{
			return new PageGrid($this);
		}

		public static function clearData()
		{
			self::model()->deleteAll();
		}
	}