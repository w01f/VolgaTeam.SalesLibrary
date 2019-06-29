<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\LibraryPage as LibraryPage;
	use application\models\wallbin\models\web\LibraryFolder as LibraryFolder;
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class LinkBundlePreviewBaseItem
	 */
	abstract class LinkBundlePreviewBaseItem
	{
		public $id;
		public $itemType;
		public $isDefault;
		public $visible;

		public $image;
		public $title;
		public $hoverTip;

		public $hasContent;
		public $contentView;

		/**
		 * @param $bundleItem BaseLinkBundleItem
		 */
		public function __construct($bundleItem)
		{
			$this->id = uniqid();
			$this->visible = !property_exists($bundleItem, 'visible') || $bundleItem->visible;
			$this->image = $bundleItem->image;
			$this->title = $bundleItem->title;
			$this->hoverTip = $bundleItem->hoverTip;
		}

		/**
		 * @return string
		 */
		public function getItemData()
		{
			$result = '';
			$result .= '<div class="item-id">' . $this->id . '</div>';
			$result .= '<div class="item-type">' . $this->itemType . '</div>';
			$result .= '<div class="item-title">' . $this->title . '</div>';
			return $result;
		}

		/**
		 * @param $bundleItem BaseLinkBundleItem
		 * @return LinkBundlePreviewBaseItem
		 */
		public static function Create($bundleItem)
		{
			/** @var LinkBundlePreviewBaseItem $bundlePreviewItem */
			$bundlePreviewItem = null;
			switch ($bundleItem->itemType)
			{
				case 1:
					/** @var LibraryLinkBundleItem $bundleItem */
					$linkRecord = LinkRecord::getLinkById($bundleItem->libraryLinkId);
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library, false);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					$link->load($linkRecord);
					if ($link->isDirectUrl)
						$bundlePreviewItem = LinkBundlePreviewUrlItem::fromLibraryLinkItem($bundleItem, $link->fileLink);
					else
						$bundlePreviewItem = new LinkBundlePreviewLinkItem($bundleItem);
					break;
				case 2:
					/** @var LinkBundleInfoItem $bundleItem */
					$bundlePreviewItem = new LinkBundlePreviewInfoItem($bundleItem);
					break;
				case 3:
					/** @var LinkBundleRevenueItem $bundleItem */
					$bundlePreviewItem = new LinkBundlePreviewRevenueItem($bundleItem);
					break;
				case 4:
					/** @var LinkBundleStrategyItem $bundleItem */
					$bundlePreviewItem = new LinkBundlePreviewStrategyItem($bundleItem);
					break;
				case 5:
					/** @var UrlLinkBundleItem $bundleItem */
					$bundlePreviewItem = LinkBundlePreviewUrlItem::fromUrlLinkBundleItem($bundleItem);
					break;
				case 6:
					/** @var LinkBundleLaunchScreenItem $bundleItem */
					$bundlePreviewItem = new LinkBundlePreviewLaunchScreenItem($bundleItem);
					break;
				case 7:
					/** @var LinkBundleCoverItem $bundleItem */
					$bundlePreviewItem = new LinkBundlePreviewCoverItem($bundleItem);
					break;
			}
			return $bundlePreviewItem;
		}
	}