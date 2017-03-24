<?

	/**
	 * Class  FontReplacementTable
	 */
	class FontReplacementHelper
	{
		const ReplacementTableStorageKey = "FontReplacementTable";

		public static function replaceFont($originalFontName)
		{
			$browser = isset(Yii::app()->browser) ? Yii::app()->browser->getBrowser() : null;
			if ($browser !== Browser::BROWSER_IPAD)
				return $originalFontName;

			$replacementTable = array();
			if (isset(\Yii::app()->session[self::ReplacementTableStorageKey]))
				$replacementTable = \Yii::app()->session[self::ReplacementTableStorageKey];
			else
			{
				$sourceFilePath = \Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'FontReplacementTable.xml';
				if (file_exists($sourceFilePath))
				{
					$sourceFileContent = file_get_contents($sourceFilePath);
					$categories = new DOMDocument();
					$categories->loadXML($sourceFileContent);
					$xpath = new DomXPath($categories);

					$fontNodes = $xpath->query('//Config/Font');
					foreach ($fontNodes as $fontNode)
					{
						$originalNode = $xpath->query('./OriginalName', $fontNode);
						$replaceNode = $xpath->query('./ReplaceWithName', $fontNode);
						if ($originalNode->length > 0 && $replaceNode->length > 0)
						{
							$replacementTable[] = array(
								'original' => strtolower(trim($originalNode->item(0)->nodeValue)),
								'replaceWith' => strtolower(trim($replaceNode->item(0)->nodeValue))
							);
						}
					}
				}
				\Yii::app()->session[self::ReplacementTableStorageKey] = $replacementTable;
			}

			$forCompare = strtolower(trim($originalFontName));
			foreach ($replacementTable as $tableItem)
				if ($tableItem['original'] === $forCompare)
					return $tableItem['replaceWith'];

			return $originalFontName;
		}
	}