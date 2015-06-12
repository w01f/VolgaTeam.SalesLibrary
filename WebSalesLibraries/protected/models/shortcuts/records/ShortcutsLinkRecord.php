<?php

	/**
	 * Class ShortcutsLinkRecord
	 * @property null|string id
	 * @property string id_tab
	 * @property null|string id_page
	 * @property null|string id_group
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
		 * @return array
		 */
		public function relations()
		{
			return array(
				'subLinks' => array(self::HAS_MANY, 'ShortcutsLinkRecord', 'id_group', 'order' => 'subLinks.`order`',),
			);
		}

		/**
		 * @return ShortcutsLinkRecord[]
		 */
		public function getSubLinks()
		{
			return $this->subLinks;
		}

		/**
		 * @return ShortcutsPageRecord
		 */
		public function getParentPage()
		{
			if (isset($this->id_group))
			{
				/** @var $parentLinkRecord ShortcutsLinkRecord */
				$parentLinkRecord = self::model()->findByPk($this->id_group);
				$idPage = $parentLinkRecord->id_page;
			}
			else
				$idPage = $this->id_page;
			return ShortcutsPageRecord::model()->findByPk($idPage);
		}

		/**
		 * @return BaseShortcut
		 */
		public function getModel()
		{
			switch ($this->type)
			{
				case 'mp4':
					return new VideoShortcut($this);
				case 'url':
					return new UrlShortcut($this);
				case 'file':
					return new FileShortcut($this);
				case 'download':
					return new DownloadShortcut($this);
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
				case 'carousel':
					return new CarouselShortcut($this);
			}
			return null;
		}

		public static function clearData()
		{
			self::model()->deleteAll();
		}
	}