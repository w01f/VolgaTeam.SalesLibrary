 <?php

	/**
	 * Class ShortcutsLinkRecord
	 * @property null|string id
	 * @property string id_page
	 * @property mixed order
	 * @property mixed type
	 * @property mixed source_path
	 * @property mixed image_path
	 * @property string config
	 */
	class ShortcutsLinkRecord extends CActiveRecord
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
			return '{{shortcut_link}}';
		}

		/**
		 * @return EmptyShortcut|
		 * FileShortcut|
		 * LibraryLinkShortcut|
		 * PageShortcut|
		 * QuickListShortcut|
		 * SearchShortcut|
		 * UrlShortcut|
		 * VideoShortcut|
		 * WindowShortcut|
		 * null
		 */
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