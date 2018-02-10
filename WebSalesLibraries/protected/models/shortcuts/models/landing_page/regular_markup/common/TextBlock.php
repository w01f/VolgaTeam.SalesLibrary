<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\common;

	/**
	 * Class TextBlock
	 */
	class TextBlock extends ContentBlock
	{
		public $text;
		public $htmlTag;
		public $htmlClass;

		/** @var  \HideCondition */
		public $hideCondition;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'text';
			$this->hideCondition = new \HideCondition();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./Text', $contextNode);
			$this->text = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./HtmlTag', $contextNode);
			$this->htmlTag = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : 'span';

			$queryResult = $xpath->query('./HtmlClass', $contextNode);
			$this->htmlClass = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./Hide', $contextNode);
			if ($queryResult->length > 0)
				$this->hideCondition = \HideCondition::fromXml($xpath, $queryResult->item(0));
		}
	}