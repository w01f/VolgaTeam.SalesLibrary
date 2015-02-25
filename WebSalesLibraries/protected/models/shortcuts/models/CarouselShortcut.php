<?php

	/**
	 * Class CarouselShortcut
	 */
	class CarouselShortcut extends BaseShortcut
	{
		/** @var BaseShortcut[] */
		public $subLinks;

		/** @param $linkRecord ShortcutsLinkRecord */
		public function __construct($linkRecord)
		{
			parent::__construct($linkRecord);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$this->subLinks = array();
			$subLinkRecords = $linkRecord->getSubLinks();
			foreach ($subLinkRecords as $subLinkRecord)
				$this->subLinks[] = $subLinkRecord->getModel();
		}

		/** @return array */
		public function getCategoryContent()
		{
			$dataItems = array();
			$mediaItems = array();
			foreach ($this->subLinks as $link)
			{
				switch ($link->type)
				{
					case 'url':
					case 'file':
						$mediaType = 'link';
						break;
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