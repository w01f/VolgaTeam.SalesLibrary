<?php

	/**
	 * Class ShortcutLinkRecord
	 * @property string id
	 * @property string id_group
	 * @property null|string id_parent
	 * @property string type
	 * @property int order
	 * @property string source_path
	 * @property string config
	 * @property ShortcutLinkRecord[] subLinks
	 */
	class ShortcutLinkRecord extends CActiveRecord
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
				'subLinks' => array(self::HAS_MANY, 'ShortcutLinkRecord', 'id_parent', 'order' => 'subLinks.`order`',),
			);
		}

		/**
		 * @param $isPhone boolean
		 * @param $parameters array|null
		 * @return BaseShortcut
		 */
		public function getModel($isPhone, $parameters = null)
		{
			switch ($this->type)
			{
				case 'video':
					return new VideoShortcut($this, $isPhone);
				case 'url':
					return new UrlShortcut($this, $isPhone);
				case 'qpage':
					return new QPageShortcut($this, $isPhone);
				case 'file':
					return new FileShortcut($this, $isPhone);
				case 'download':
					return new DownloadShortcut($this, $isPhone);
				case 'libraryfile':
					return new LibraryLinkShortcut($this, $isPhone);
				case 'window':
					return new WindowShortcut($this, $isPhone);
				case 'page':
					return new LibraryPageShortcut($this, $isPhone);
				case 'quicklist':
					return new QuickListShortcut($this, $isPhone);
				case 'search':
					return new SearchLinkShortcut($this, $isPhone);
				case 'gbookmark':
					return new GroupBookmarkShortcut($this, $isPhone);
				case 'gridbundle':
				case 'carouselbundle':
					if (!$isPhone)
					{
						$savedBundleModeTagName = $this->getUniqueId();
						if (isset($parameters) && array_key_exists('pageViewType', $parameters))
						{
							$bundleType = $parameters['pageViewType'];
							$cookie = new CHttpCookie($savedBundleModeTagName, $bundleType);
							$cookie->expire = time() + (60 * 60 * 24 * 7);
							Yii::app()->request->cookies[$savedBundleModeTagName] = $cookie;
						}
						else
						{
							if (isset(Yii::app()->request->cookies[$savedBundleModeTagName]))
								$bundleType = Yii::app()->request->cookies[$savedBundleModeTagName]->value;
							else
								$bundleType = $this->type;
						}
						switch ($bundleType)
						{
							case 'gridbundle':
								return new GridBundleShortcut($this, $isPhone);
							case 'carouselbundle':
								return new CarouselBundleShortcut($this, $isPhone);
							default:
								return new EmptyShortcut($this, $isPhone);
						}
					}
					else
						return new GridBundleShortcut($this, $isPhone);
				case 'library':
					$shortcut = new WallbinShortcut($this, $isPhone);
					$needToUpdate = false;
					$savedPageViewTypeTag = sprintf('PageViewType-%s', $shortcut->library->id);
					if (isset($parameters) && array_key_exists('pageViewType', $parameters))
					{
						$shortcut->pageViewType = $parameters['pageViewType'];
						$cookie = new CHttpCookie($savedPageViewTypeTag, $shortcut->pageViewType);
						$cookie->expire = time() + (60 * 60 * 24 * 7);
						Yii::app()->request->cookies[$savedPageViewTypeTag] = $cookie;
						$needToUpdate = true;
					}
					else if (isset(Yii::app()->request->cookies[$savedPageViewTypeTag]))
					{
						$shortcut->pageViewType = Yii::app()->request->cookies[$savedPageViewTypeTag]->value;
						$needToUpdate = true;
					}

					$savedPageSelectorModeTag = sprintf('PageSelectorMode-%s', $shortcut->library->id);
					if (isset($parameters) && array_key_exists('pageSelectorMode', $parameters))
					{
						$shortcut->pageSelectorMode = $parameters['pageSelectorMode'];
						$cookie = new CHttpCookie($savedPageSelectorModeTag, $shortcut->pageSelectorMode);
						$cookie->expire = time() + (60 * 60 * 24 * 7);
						Yii::app()->request->cookies[$savedPageSelectorModeTag] = $cookie;
						$needToUpdate = true;
					}
					else if (isset(Yii::app()->request->cookies[$savedPageSelectorModeTag]))
					{
						$shortcut->pageSelectorMode = Yii::app()->request->cookies[$savedPageSelectorModeTag]->value;
						$needToUpdate = true;
					}

					if ($needToUpdate)
						$shortcut->updateAction();

					return $shortcut;
					break;
				case 'searchapp':
					return new SearchAppShortcut($this, $isPhone);
				case 'qbuilder':
					return new QBuilderShortcut($this, $isPhone);
				case 'quizzes':
					return new QuizzesShortcut($this, $isPhone);
				case 'favorites':
					return new FavoritesShortcut($this, $isPhone);
				default:
					return new EmptyShortcut($this, $isPhone);
			}
		}

		public function getUniqueId()
		{
			$groupRecord = ShortcutGroupRecord::model()->findByPk($this->id_group);
			return sprintf('group%s-link%s', $groupRecord->order, $this->order);
		}

		public static function clearData()
		{
			self::model()->deleteAll();
		}
	}