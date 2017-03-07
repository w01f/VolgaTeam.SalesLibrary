<?

	/**
	 * Class CarouselBundleShortcut
	 */
	class CarouselBundleShortcut extends BundleShortcut
	{
		public $config;
		public $defaultCategoryIndex;

		/**
		 * @param ShortcutLinkRecord $linkRecord
		 * @param $isPhone boolean
		 * @param int $defaultCategoryIndex
		 */
		public function __construct($linkRecord, $isPhone, $defaultCategoryIndex)
		{
			parent::__construct($linkRecord, $isPhone);

			$this->defaultCategoryIndex = $defaultCategoryIndex;

			$this->type = 'carouselbundle';
			$this->viewName = $this->isPhone ? 'bundle' : 'carouselBundle';
		}

		public function loadPageConfig()
		{
			parent::loadPageConfig();

			$this->config = $this->linkRecord->config;
			$config = new DOMDocument();
			$config->loadXML($this->config);
			$xpath = new DomXPath($config);

			$allowSwitchViewTags = $xpath->query('//Config/TileToggle');
			$this->allowSwitchView = $allowSwitchViewTags->length > 0 ? filter_var(trim($allowSwitchViewTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Carouselbundle';
		}

		/**
		 * @return array
		 */
		public function getDisplayParameters()
		{
			$config = new DOMDocument();
			$config->loadXML($this->config);
			$xpath = new DomXPath($config);

			$configPrefix = Yii::app()->browser->isMobile() ? 'Tablet' : 'Desktop';

			$this->getLinks();

			return array(
				'showPageModeToggle' => $this->allowSwitchView,

				'predefinedDataList' => $this->getGroupedLinkData(),

				'carouselWidth' => intval(trim($xpath->query('//Carousel/' . $configPrefix . '/carouselWidth')->item(0)->nodeValue)),
				'carouselHeight' => intval(trim($xpath->query('//Carousel/' . $configPrefix . '/carouselHeight')->item(0)->nodeValue)),
				'carouselStartPosition' => trim($xpath->query('//Carousel/' . $configPrefix . '/carouselStartPosition')->item(0)->nodeValue),
				'carouselTopology' => trim($xpath->query('//Carousel/' . $configPrefix . '/carouselTopology')->item(0)->nodeValue),
				'carouselXRadius' => intval(trim($xpath->query('//Carousel/' . $configPrefix . '/carouselXRadius')->item(0)->nodeValue)),
				'carouselYRadius' => intval(trim($xpath->query('//Carousel/' . $configPrefix . '/carouselYRadius')->item(0)->nodeValue)),
				'carouselXRotation' => intval(trim($xpath->query('//Carousel/' . $configPrefix . '/carouselXRotation')->item(0)->nodeValue)),
				'carouselYOffset' => intval(trim($xpath->query('//Carousel/' . $configPrefix . '/carouselYOffset')->item(0)->nodeValue)),
				'showPrevButton' => trim($xpath->query('//Carousel/' . $configPrefix . '/showPrevButton')->item(0)->nodeValue),
				'showNextButton' => trim($xpath->query('//Carousel/' . $configPrefix . '/showNextButton')->item(0)->nodeValue),
				'thumbnailWidth' => intval(trim($xpath->query('//Carousel/' . $configPrefix . '/thumbnailWidth')->item(0)->nodeValue)),
				'thumbnailHeight' => intval(trim($xpath->query('//Carousel/' . $configPrefix . '/thumbnailHeight')->item(0)->nodeValue)),
				'thumbnailBorderSize' => intval(trim($xpath->query('//Carousel/' . $configPrefix . '/thumbnailBorderSize')->item(0)->nodeValue)),
				'showScrollbar' => trim($xpath->query('//Carousel/' . $configPrefix . '/showScrollbar')->item(0)->nodeValue),
				'showComboBox' => trim($xpath->query('//Carousel/' . $configPrefix . '/showComboBox')->item(0)->nodeValue),
				'comboBoxPosition' => trim($xpath->query('//Carousel/' . $configPrefix . '/comboBoxPosition')->item(0)->nodeValue),

				'carouselHolderDivId' => "shortcuts-links-carousel-view",
				'displayType' => "responsive",
				'autoScale' => "yes",
				'skinPath' => "vendor/carousel/load/skin_modern_silver",

				//main settings
				'backgroundColor' => "#FFFFFF",
				'scrollbarBackgroundImagePath' => "vendor/carousel/load/skin_modern_silver/scrollbarBackground.jpg",
				'showCenterImage' => "no",
				'centerImagePath' => "vendor/carousel/load/logo.png",
				'centerImageYOffset' => 0,
				'showDisplay2DAlways' => "no",
				'slideshowDelay' => 5000,
				'autoplay' => "no",
				'showSlideshowButton' => "no",
				'disableNextAndPrevButtonsOnMobile' => "no",
				'controlsMaxWidth' => 1200,
				'controlsHeight' => 50,
				'controlsPosition' => "bottom",
				'slideshowTimerColor' => "#777777",
				'rightClickContextMenu' => "developer",
				'addKeyboardSupport' => "yes",
				'fluidWidthZIndex' => 1000,

				//thumbnail settings
				'thumbnailMinimumAlpha' => .3,
				'thumbnailBackgroundColor' => "#666666",
				'thumbnailBorderColor1' => "#4b4b4b",
				'thumbnailBorderColor2' => "#e4e4e4",
				'transparentImages' => "no",
				'maxNumberOfThumbnailsOnMobile' => 13,
				'showThumbnailsGradient' => "yes",
				'showThumbnailsHtmlContent' => "no",
				'textBackgroundColor' => "#111111",
				'textBackgroundOpacity' => .1,
				'showText' => "yes",
				'showTextBackgroundImage' => "yes",
				'showFullTextWithoutHover' => "no",
				'showThumbnailBoxShadow' => "yes",
				'thumbnailBoxShadowCss' => "0px 2px 2px #555555",
				'showReflection' => "yes",
				'reflectionHeight' => 50,
				'reflectionDistance' => 0,
				'reflectionOpacity' => .2,

				//scrollbar settings
				'disableScrollbarOnMobile' => "yes",
				'enableMouseWheelScroll' => "yes",
				'scrollbarHandlerWidth' => 300,
				'scrollbarTextColorNormal' => "#777777",
				'scrollbarTextColorSelected' => "#000000",

				//combobox settings
				'startAtCategory' => $this->defaultCategoryIndex,
				'selectLabel' => "default",
				'allCategoriesLabel' => "All Categories",
				'showAllCategories' => "no",
				'selectorBackgroundNormalColor1' => "#FFFFFF",
				'selectorBackgroundNormalColor2' => "#FFFFFF",
				'selectorBackgroundSelectedColor1' => "#FFFFFF",
				'selectorBackgroundSelectedColor2' => "#FFFFFF",
				'selectorTextNormalColor' => "#000000",
				'selectorTextSelectedColor' => "#000000",
				'buttonBackgroundNormalColor1' => "#FFFFFF",
				'buttonBackgroundNormalColor2' => "#FFFFFF",
				'buttonBackgroundSelectedColor1' => "#FFFFFF",
				'buttonBackgroundSelectedColor2' => "#FFFFFF",
				'buttonTextNormalColor' => "#000000",
				'buttonTextSelectedColor' => "#000000",
				'comboBoxShadowColor' => "#222222",
				'comboBoxHorizontalMargins' => 12,
				'comboBoxVerticalMargins' => 20,
				'comboBoxCornerRadius' => 0,
			);
		}

		/**
		 * @return array
		 */
		public function getGroupedLinkData()
		{
			$groupedLinks = array();
			foreach ($this->links as $link)
			{
				if ($link->carouselGroup != '')
					$groupedLinks[$link->carouselGroup][] = $link;
				else
					$groupedLinks['No Group'][] = $link;
			}

			$dataList = array();

			foreach ($groupedLinks as $groupName => $links)
			{
				$dataItems = array();
				$mediaItems = array();

				foreach ($links as $link)
				{
					/** @var $link BaseShortcut */
					switch ($link->type)
					{
						case 'none':
							$mediaType = 'none';
							break;
						default:
							$mediaType = 'func';
							break;
					}
					switch ($link->type)
					{
						case 'video':
							$handlerType = 'video';
							break;
						case 'page':
							$handlerType = 'library-page';
							break;
						case 'libraryfile':
							$handlerType = 'library-file';
							break;
						case 'download':
							$handlerType = 'download';
							break;
						case 'url':
						case 'qpage':
						case 'file':
						case 'window':
						case 'search':
						case 'quicklist':
							$handlerType = 'direct';
							break;
						default:
							$handlerType = 'none';
							break;
					}
					$mediaItem = array(
						'dataType' => $mediaType,
						'url' => $link->getSourceLink(),
						'target' => '_blank',
					);

					$dataItems[] = array(
						'thumbPath' => $link->imageContent,
						'thumbText' => '<p class="largeLabel">' . $link->title . '</p><p class="smallLabel">' . $link->description . '</p>',
						'textTitleOffset' => 35,
						'textDescriptionOffsetTop' => 10,
						'textDescriptionOffsetBottom' => 7,
						'mediaType' => $mediaType,
						'url' => $link->getSourceLink(),
						'target' => '_blank',
						'secondObj' => $mediaItem,

						'handlerType' => $handlerType,
						'type' => $link->type,
						'samePage' => isset($link->samePage) ? $link->samePage : false,
						'dataContent' => $link->getMenuItemData(),
					);
					$mediaItems[] = $mediaItem;
				}

				$dataList[] = array(
					'name' => $groupName,
					'dataItems' => $dataItems,
					'mediaItems' => $mediaItems
				);
			}
			return $dataList;
		}
	}