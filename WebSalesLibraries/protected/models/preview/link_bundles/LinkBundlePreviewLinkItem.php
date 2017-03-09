<?

	/**
	 * Class LinkBundlePreviewLinkItem
	 */
	class LinkBundlePreviewLinkItem extends LinkBundlePreviewBaseItem
	{
		public $libraryLinkId;
		public $libraryLinkFormat;

		/**
		 * @param $bundleItem LibraryLinkBundleItem
		 */
		public function __construct($bundleItem)
		{
			parent::__construct($bundleItem);
			$this->itemType = 'link';
			$this->hasContent = true;
			$this->contentView = 'linkBundleLinkContent';
			$this->libraryLinkId = $bundleItem->libraryLinkId;
			if (isset($this->libraryLinkId))
			{
				$linkRecord = LinkRecord::getLinkById($this->libraryLinkId);
				if (isset($linkRecord))
					$this->libraryLinkFormat = $linkRecord->original_format;
			}
		}

		/**
		 * @return string
		 */
		public function getItemData()
		{
			$result = parent::getItemData();
			$result .= '<div class="library-link-id">' . $this->libraryLinkId . '</div>';
			return $result;
		}
	}