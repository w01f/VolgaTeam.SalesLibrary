<?

	/**
	 * Class LinkBundlePreviewRevenueItem
	 */
	class LinkBundlePreviewRevenueItem extends LinkBundlePreviewContentItem
	{
		public $header;
		public $additionalInfo;
		public $foreColor;
		public $backColor;
		/**
		 * @var \Font
		 */
		public $font;

		public $revenueItems;


		/**
		 * @param $bundleItem LinkBundleRevenueItem
		 */
		public function __construct($bundleItem)
		{
			parent::__construct($bundleItem);
			$this->contentView = 'linkBundleRevenueContent';

			switch ($bundleItem->revenueType)
			{
				case 1:
					$this->header = 'Revenue Generated';
					break;
				case 2:
					$this->header = 'Revenue Goal';
					break;
			}

			$this->additionalInfo = $bundleItem->additionalInfo;

			$this->foreColor = $bundleItem->foreColor;
			$this->backColor = $bundleItem->backColor;
			$this->font = $bundleItem->font;

			$this->revenueItems = array();
			foreach ($bundleItem->infoItems as $infoItem)
			{
				$this->revenueItems[] = array(
					'title' => $infoItem->infoType,
					'value' => '$' . number_format($infoItem->value, 2, ',', ' ')
				);
			}
		}
	}