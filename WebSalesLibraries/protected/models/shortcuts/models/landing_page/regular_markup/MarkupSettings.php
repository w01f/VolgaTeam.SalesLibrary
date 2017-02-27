<?
	namespace application\models\shortcuts\models\landing_page\regular_markup;

	/**
	 * Class MarkupSettings
	 */
	class MarkupSettings
	{
		/** @var  ContentBlock[] */
		public $contentBlocks;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return MarkupSettings
		 */
		public static function fromXml($parentShortcut, $xpath, $contextNode)
		{
			$markupSettings = new MarkupSettings();

			$queryResult = $xpath->query('./ContentBlock', $contextNode);
			foreach ($queryResult as $node)
			{
				$contentBlock = ContentBlock::fromXml($parentShortcut, null, $xpath, $node);
				if ($contentBlock->isAccessGranted)
					$markupSettings->contentBlocks[] = $contentBlock;
			}

			return $markupSettings;
		}
	}