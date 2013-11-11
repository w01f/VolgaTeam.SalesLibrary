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