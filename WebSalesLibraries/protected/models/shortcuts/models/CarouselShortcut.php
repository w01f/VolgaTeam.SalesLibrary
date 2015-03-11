<?php

	/**
	 * Class CarouselShortcut
	 */
	class CarouselShortcut extends BaseShortcut
	{
		public $ribbonLogoPath;
		/** @var BaseShortcut[] */
		public $subLinks;

		/** @param $linkRecord ShortcutsLinkRecord */
		public function __construct($linkRecord)
		{
			parent::__construct($linkRecord);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->ribbonLogoPath = $baseUrl . $linkRecord->source_path . '/rbnlogo.png' . '?' . $linkRecord->id_page . $linkRecord->id;

			$this->subLinks = array();
			/* @var $subLinkRecords ShortcutsLinkRecord[] */
			$subLinkRecords = $linkRecord->getSubLinks();
			foreach ($subLinkRecords as $subLinkRecord)
				$this->subLinks[] = $subLinkRecord->getModel();
		}

		/**
		 * @param $parentPage CarouselPage
		 * @return array
		 */
		public function getCategoryContent($parentPage)
		{
			$dataItems = array();
			$mediaItems = array();
			foreach ($this->subLinks as $link)
			{
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
					case 'mp4':
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
					'url' => $link->sourceLink,
					'target' => '_blank',
				);
				$dataItems[] = array(
					'thumbPath' => $link->imagePath,
					'thumbText' => '<p class="largeLabel">' . $link->name . '</p><p class="smallLabel">' . $link->tooltip . '</p>',
					'textTitleOffset' => 35,
					'textDescriptionOffsetTop' => 10,
					'textDescriptionOffsetBottom' => 7,
					'mediaType' => $mediaType,
					'url' => $link->sourceLink,
					'target' => '_blank',
					'secondObj' => $mediaItem,

					'handlerType' => $handlerType,
					'type' => $link->type,
					'samePage' => $link->samePage,
					'dataContent' => $link->getServiceData(),
				);
				$mediaItems[] = $mediaItem;
			}
			return array(
				'name' => $this->title,
				'logo' => @getimagesize($this->ribbonLogoPath) ? $this->ribbonLogoPath : $parentPage->ribbonLogoPath,
				'dataItems' => $dataItems,
				'mediaItems' => $mediaItems
			);
		}

		/**
		 * @return string
		 */
		public function getServiceData()
		{
			$result = '';
			return $result;
		}
	}