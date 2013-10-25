<?php
	class ShortcutsLinkStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{shortcut_link}}';
		}

		public function GetModel()
		{
			switch ($this->type)
			{
				case 'mp4':
					return new VideoShortcut($this);
				case 'url':
					return new UrlShortcut($this);
				case 'file':
					return new FileShortcut($this);
				case 'libraryfile':
					return new LibraryLinkShortcut($this);
				case 'window':
					return new WindowShortcut($this);
				case 'page':
					return new PageShortcut($this);
				case 'quicklist':
					return new QuickListShortcut($this);
				case 'search':
					return new SearchShortcut($this);
				case 'none':
					return new EmptyShortcut($this);
			}
			return null;
		}


		public static function clearData()
		{
			self::model()->deleteAll();
		}


	}
