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
				'group' => array(self::BELONGS_TO, 'ShortcutGroupRecord', 'id_group'),
				'parent' => array(self::BELONGS_TO, 'ShortcutLinkRecord', 'id_parent'),
			);
		}

		public function getUniqueId()
		{
			return sprintf('group%s-parent%s-link%s', $this->id_group, (isset($this->id_parent) ? $this->id_parent : ''), $this->id);
		}

		/**
		 * @param $isPhone boolean
		 * @param $parameters array|null
		 * @return BaseShortcut
		 */
		public function getRegularModel($isPhone, $parameters = null)
		{
			$shortcut = null;
			switch ($this->type)
			{
				case 'video':
					$shortcut = new VideoShortcut($this, $isPhone);
					break;
				case 'url':
					$shortcut = new UrlShortcut($this, $isPhone);
					break;
				case 'qpage':
					$shortcut = new QPageShortcut($this, $isPhone);
					break;
				case 'file':
					$shortcut = new FileShortcut($this, $isPhone);
					break;
				case 'download':
					$shortcut = new DownloadShortcut($this, $isPhone);
					break;
				case 'libraryfile':
					$shortcut = new LibraryLinkShortcut($this, $isPhone);
					break;
				case 'window':
					$shortcut = new WindowShortcut($this, $isPhone);
					break;
				case 'quicklist':
					$shortcut = new QuickListShortcut($this, $isPhone);
					break;
				case 'search':
					$shortcut = new SearchLinkShortcut($this, $isPhone);
					break;
				case 'gbookmark':
					$shortcut = new GroupBookmarkShortcut($this, $isPhone);
					break;
				case 'supergroup':
					$shortcut = new SuperGroupShortcut($this, $isPhone);
					break;
				case 'gridbundle':
				case 'carouselbundle':
					if (!$isPhone)
					{
						$savedBundleModeTagName = $this->getUniqueId();
						$parametersHaveBeenSet = isset($parameters);

						if ($parametersHaveBeenSet && array_key_exists('pageViewType', $parameters))
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
								$shortcut = new GridBundleShortcut($this, $isPhone);
								break;
							case 'carouselbundle':
								$defaultCategoryIndex = 1;
								if ($parametersHaveBeenSet && array_key_exists('defaultCategoryIndex', $parameters))
									$defaultCategoryIndex = $parameters['defaultCategoryIndex'];
								$shortcut = new CarouselBundleShortcut($this, $isPhone, $defaultCategoryIndex);
								break;
							default:
								$shortcut = new EmptyShortcut($this, $isPhone);
								break;
						}
					}
					else
						$shortcut = new GridBundleShortcut($this, $isPhone);
					break;
				case 'page':
					$shortcut = new LibraryPageShortcut($this, $isPhone);
					break;
				case 'pagebundle':
					$shortcut = new LibraryPageBundleShortcut($this, $isPhone);
					break;
				case 'library':
					$shortcut = new WallbinShortcut($this, $isPhone);
					break;
				case 'searchapp':
					$shortcut = new SearchAppShortcut($this, $isPhone);
					break;
				case 'qbuilder':
					$shortcut = new QBuilderShortcut($this, $isPhone, $parameters);
					break;
				case 'quizzes':
					$shortcut = new QuizzesShortcut($this, $isPhone);
					break;
				case 'favorites':
					$shortcut = new FavoritesShortcut($this, $isPhone);
					break;
				case 'user_preferences':
					$shortcut = new FavoritesShortcut($this, $isPhone);
					break;
				case 'youtube':
					$shortcut = new YouTubeShortcut($this, $isPhone);
					break;
				case 'vimeo':
					$shortcut = new VimeoShortcut($this, $isPhone);
					break;
				case 'landing':
					$shortcut = new LandingPageShortcut($this, $isPhone);
					break;
				case 'shortcutlink':
					$shortcut = new AliasShortcut($this, $isPhone);
					break;
				case 'left_panel_mobile':
					$shortcut = new NavigationPanelShortcut($this, $isPhone);
					break;
				case 'starssteals':
					$shortcut = new StarStealsShortcut($this, $isPhone);
					break;
				case 'rrq1':
					$shortcut = new SalesRequestsShortcut($this, $isPhone, $parameters);
					break;
				default:
					$shortcut = new EmptyShortcut($this, $isPhone);
					break;
			}

			$shortcut->initRegularModel();

			switch ($this->type)
			{
				case 'page':
					$needToUpdate = false;
					$cookieLibraryName = str_replace(" ", "", str_replace("=", "", str_replace(";", "", (str_replace(",", "", str_replace("'", "", $shortcut->libraryName))))));
					$savedPageViewTypeTag = sprintf('PageViewType-%s', $cookieLibraryName);
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
					if ($needToUpdate)
						$shortcut->updateAction();
					break;
				case 'pagebundle':
					$savedPageViewTypeTag = sprintf('PageViewType-%s', $shortcut->id);
					if (isset($parameters) && array_key_exists('pageViewType', $parameters))
					{
						$shortcut->pageViewType = $parameters['pageViewType'];
						$cookie = new CHttpCookie($savedPageViewTypeTag, $shortcut->pageViewType);
						$cookie->expire = time() + (60 * 60 * 24 * 7);
						Yii::app()->request->cookies[$savedPageViewTypeTag] = $cookie;
					}
					else if (isset(Yii::app()->request->cookies[$savedPageViewTypeTag]))
					{
						$shortcut->pageViewType = Yii::app()->request->cookies[$savedPageViewTypeTag]->value;
					}

					$savedPageSelectorModeTag = sprintf('PageSelectorMode-%s', $shortcut->id);
					if (isset($parameters) && array_key_exists('pageSelectorMode', $parameters))
					{
						$shortcut->pageSelectorMode = $parameters['pageSelectorMode'];
						$cookie = new CHttpCookie($savedPageSelectorModeTag, $shortcut->pageSelectorMode);
						$cookie->expire = time() + (60 * 60 * 24 * 7);
						Yii::app()->request->cookies[$savedPageSelectorModeTag] = $cookie;
					}
					else if (isset(Yii::app()->request->cookies[$savedPageSelectorModeTag]))
					{
						$shortcut->pageSelectorMode = Yii::app()->request->cookies[$savedPageSelectorModeTag]->value;
					}
					break;
				case 'library':
					$cookieLibraryName = str_replace(" ", "", str_replace("=", "", str_replace(";", "", (str_replace(",", "", str_replace("'", "", $shortcut->libraryName))))));
					$savedPageViewTypeTag = sprintf('PageViewType-%s', $cookieLibraryName);
					if (isset($parameters) && array_key_exists('pageViewType', $parameters))
					{
						$shortcut->pageViewType = $parameters['pageViewType'];
						$cookie = new CHttpCookie($savedPageViewTypeTag, $shortcut->pageViewType);
						$cookie->expire = time() + (60 * 60 * 24 * 7);
						Yii::app()->request->cookies[$savedPageViewTypeTag] = $cookie;
					}
					else if (isset(Yii::app()->request->cookies[$savedPageViewTypeTag]))
					{
						$shortcut->pageViewType = Yii::app()->request->cookies[$savedPageViewTypeTag]->value;
					}

					$savedPageSelectorModeTag = sprintf('PageSelectorMode-%s', $cookieLibraryName);
					if (isset($parameters) && array_key_exists('pageSelectorMode', $parameters))
					{
						$shortcut->pageSelectorMode = $parameters['pageSelectorMode'];
						$cookie = new CHttpCookie($savedPageSelectorModeTag, $shortcut->pageSelectorMode);
						$cookie->expire = time() + (60 * 60 * 24 * 7);
						Yii::app()->request->cookies[$savedPageSelectorModeTag] = $cookie;
					}
					else if (isset(Yii::app()->request->cookies[$savedPageSelectorModeTag]))
					{
						$shortcut->pageSelectorMode = Yii::app()->request->cookies[$savedPageSelectorModeTag]->value;
					}
					break;
				case 'shortcutlink':
					/** @var AliasShortcut $shortcut */
					$shortcut = $shortcut->originalShortcut;
					break;
			}
			return $shortcut;
		}

		/**
		 * @return BaseShortcut
		 */
		public function getServiceModel()
		{
			switch ($this->type)
			{
				case 'video':
					$shortcut = new VideoShortcut($this, false);
					break;
				case 'url':
					$shortcut = new UrlShortcut($this, false);
					break;
				case 'qpage':
					$shortcut = new QPageShortcut($this, false);
					break;
				case 'file':
					$shortcut = new FileShortcut($this, false);
					break;
				case 'download':
					$shortcut = new DownloadShortcut($this, false);
					break;
				case 'libraryfile':
					$shortcut = new LibraryLinkShortcut($this, false);
					break;
				case 'window':
					$shortcut = new WindowShortcut($this, false);
					break;
				case 'quicklist':
					$shortcut = new QuickListShortcut($this, false);
					break;
				case 'search':
					$shortcut = new SearchLinkShortcut($this, false);
					break;
				case 'gbookmark':
					$shortcut = new GroupBookmarkShortcut($this, false);
					break;
				case 'supergroup':
					$shortcut = new SuperGroupShortcut($this, false);
					break;
				case 'gridbundle':
					$shortcut = new GridBundleShortcut($this, false);
					break;
				case 'carouselbundle':
					$shortcut = new CarouselBundleShortcut($this, false);
					break;
				case 'page':
					$shortcut = new LibraryPageShortcut($this, false);
					break;
				case 'pagebundle':
					$shortcut = new LibraryPageBundleShortcut($this, false);
					break;
				case 'library':
					$shortcut = new WallbinShortcut($this, false);
					break;
				case 'searchapp':
					$shortcut = new SearchAppShortcut($this, false);
					break;
				case 'qbuilder':
					$shortcut = new QBuilderShortcut($this, false);
					break;
				case 'quizzes':
					$shortcut = new QuizzesShortcut($this, false);
					break;
				case 'favorites':
					$shortcut = new FavoritesShortcut($this, false);
					break;
				case 'user_preferences':
					$shortcut = new FavoritesShortcut($this, false);
					break;
				case 'youtube':
					$shortcut = new YouTubeShortcut($this, false);
					break;
				case 'vimeo':
					$shortcut = new VimeoShortcut($this, false);
					break;
				case 'landing':
					$shortcut = new LandingPageShortcut($this, false);
					break;
				case 'shortcutlink':
					$shortcut = new AliasShortcut($this, false);
					break;
				case 'left_panel_mobile':
					$shortcut = new NavigationPanelShortcut($this, false);
					break;
				case 'starssteals':
					$shortcut = new StarStealsShortcut($this, false);
					break;
				case 'rrq1':
					$shortcut = new SalesRequestsShortcut($this, false);
					break;
				default:
					$shortcut = new EmptyShortcut($this, false);
					break;
			}
			return $shortcut;
		}

		/**
		 * @param $uniqueId string
		 * @param $isPhone boolean
		 * @return BaseShortcut
		 */
		public static function getModelByUniqueId($uniqueId, $isPhone)
		{
			$idArray = explode('-', $uniqueId);
			$groupId = str_replace('group', '', $idArray[0]);
			$parentId = str_replace('parent', '', $idArray[1]);
			$linkId = str_replace('link', '', $idArray[2]);

			/** @var $record ShortcutLinkRecord */
			$record = self::model()
				->find("t.id_group='" . $groupId . "' and t.id='" . $linkId ."' and ". (empty($parentId) ? "t.id_parent is null" : ("t.id_parent = '" . $parentId . "'")));
			if (isset($record))
				return $record->getRegularModel($isPhone);
			return null;
		}

		public static function clearData()
		{
			self::model()->deleteAll();
		}
	}