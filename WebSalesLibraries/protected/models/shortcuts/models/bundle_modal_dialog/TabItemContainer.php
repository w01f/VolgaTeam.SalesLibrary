<?
	namespace application\models\shortcuts\models\bundle_modal_dialog;

	class TabItemContainer extends BaseItemContainer
	{
		public $title;
		public $imageUrl;

		public $columnsCount;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @param $shortcut \BundleModalDialogShortcut
		 */
		public function __construct($xpath, $contextNode, $shortcut)
		{
			parent::__construct($xpath, $contextNode, $shortcut);

			$queryResult = $xpath->query('./Title', $contextNode);
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./ColumnsCount', $contextNode);
			$this->columnsCount = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 1;

			$queryResult = $xpath->query('./Image', $contextNode);
			$imageFile = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			$this->imageUrl = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . $this->parentShortcut->relativeLink . '/images/' . $imageFile);
		}
	}