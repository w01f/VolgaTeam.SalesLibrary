<?

	/**
	 * Class CarouselPage
	 */
	class CarouselPage extends PageModel
	{
		public $config;

		/**
		 * @param $pageRecord ShortcutsPageRecord
		 */
		public function __construct($pageRecord)
		{
			parent::__construct($pageRecord);
			$this->config = $pageRecord->config;
			$this->type = 'carousel';
			$this->viewPath = 'carousel';
		}

		/**
		 * @return array
		 */
		public function getDisplayParameters()
		{
			$config = new DOMDocument();
			$config->loadXML($this->config);
			$xpath = new DomXPath($config);

			$dataList = array();
			foreach ($this->links as $link)
			{
				/** @var $link CarouselShortcut */
				$dataList[] = $link->getCategoryContent($this);
			}

			return array(
				'predefinedDataList' => $dataList,
				'carouselWidth' => intval(trim($xpath->query('//Carousel/carouselWidth')->item(0)->nodeValue)),
				'carouselHeight' => intval(trim($xpath->query('//Carousel/carouselHeight')->item(0)->nodeValue)),
				'carouselStartPosition' => trim($xpath->query('//Carousel/carouselStartPosition')->item(0)->nodeValue),
				'carouselTopology' => trim($xpath->query('//Carousel/carouselTopology')->item(0)->nodeValue),
				'carouselXRadius' => intval(trim($xpath->query('//Carousel/carouselXRadius')->item(0)->nodeValue)),
				'carouselYRadius' => intval(trim($xpath->query('//Carousel/carouselYRadius')->item(0)->nodeValue)),
				'carouselXRotation' => intval(trim($xpath->query('//Carousel/carouselXRotation')->item(0)->nodeValue)),
				'carouselYOffset' => intval(trim($xpath->query('//Carousel/carouselYOffset')->item(0)->nodeValue)),
				'showPrevButton' => trim($xpath->query('//Carousel/showPrevButton')->item(0)->nodeValue),
				'showNextButton' => trim($xpath->query('//Carousel/showNextButton')->item(0)->nodeValue),
				'thumbnailWidth' => intval(trim($xpath->query('//Carousel/thumbnailWidth')->item(0)->nodeValue)),
				'thumbnailHeight' => intval(trim($xpath->query('//Carousel/thumbnailHeight')->item(0)->nodeValue)),
				'thumbnailBorderSize' => intval(trim($xpath->query('//Carousel/thumbnailBorderSize')->item(0)->nodeValue)),
				'showScrollbar' => trim($xpath->query('//Carousel/showScrollbar')->item(0)->nodeValue),
				'showComboBox' => trim($xpath->query('//Carousel/showComboBox')->item(0)->nodeValue),
				'comboBoxPosition' => trim($xpath->query('//Carousel/comboBoxPosition')->item(0)->nodeValue),

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
				'startAtCategory' => 1,
				'selectLabel' => "default",
				'allCategoriesLabel' => "All Categories",
				'showAllCategories' => "no",
				'selectorBackgroundNormalColor1' => "#fcfdfd",
				'selectorBackgroundNormalColor2' => "#e4e4e4",
				'selectorBackgroundSelectedColor1' => "#a7a7a7",
				'selectorBackgroundSelectedColor2' => "#8e8e8e",
				'selectorTextNormalColor' => "#8b8b8b",
				'selectorTextSelectedColor' => "#FFFFFF",
				'buttonBackgroundNormalColor1' => "#e7e7e7",
				'buttonBackgroundNormalColor2' => "#e7e7e7",
				'buttonBackgroundSelectedColor1' => "#a7a7a7",
				'buttonBackgroundSelectedColor2' => "#8e8e8e",
				'buttonTextNormalColor' => "#000000",
				'buttonTextSelectedColor' => "#FFFFFF",
				'comboBoxShadowColor' => "#000000",
				'comboBoxHorizontalMargins' => 12,
				'comboBoxVerticalMargins' => 20,
				'comboBoxCornerRadius' => 0,

				//lightbox settings
				'addLightBoxKeyboardSupport' => "yes",
				'showLightBoxNextAndPrevButtons' => "yes",
				'showLightBoxZoomButton' => "yes",
				'showLightBoxInfoButton' => "yes",
				'showLighBoxSlideShowButton' => "yes",
				'showLightBoxInfoWindowByDefault' => "no",
				'slideShowAutoPlay' => "no",
				'lightBoxVideoAutoPlay' => "no",
				'lightBoxVideoWidth' => 640,
				'lightBoxVideoHeight' => 480,
				'lightBoxIframeWidth' => 800,
				'lightBoxIframeHeight' => 600,
				'lightBoxBackgroundColor' => "#000000",
				'lightBoxInfoWindowBackgroundColor' => "#FFFFFF",
				'lightBoxItemBorderColor1' => "#fcfdfd",
				'lightBoxItemBorderColor2' => "#e4FFe4",
				'lightBoxItemBackgroundColor' => "#333333",
				'lightBoxMainBackgroundOpacity' => .8,
				'lightBoxInfoWindowBackgroundOpacity' => .9,
				'lightBoxBorderSize' => 5,
				'lightBoxBorderRadius' => 0,
				'lightBoxSlideShowDelay' => 4000
			);
		}
	}