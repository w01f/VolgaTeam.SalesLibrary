<?php

	/**
	 * Class SearchHelper
	 */
	class SearchHelper
	{
		/**
		 * @param $condition
		 * @return array
		 */
		public static function prepareTextCondition($condition)
		{
			$result = array();
			if (!isset($condition)) return $result;

			$configPath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'search_config.xml';
			if (!file_exists($configPath))
			{
				if ($condition != "" && $condition != '""')
					$result[] = $condition;
			}
			else
			{
				try
				{
					$configContent = file_get_contents($configPath);
					$config = new DOMDocument();
					$config->loadXML($configContent);
					$xpath = new DomXPath($config);

					$condition = preg_replace('!\s+!', ' ', $condition);
					$conditionToCompare = trim(str_replace('"', '', $condition));

					/** @var $queryResult DOMNodeList */
					$queryResult = $xpath->query('//Config/Ignore');
					foreach ($queryResult as $node)
					{
						/** @var $node DomElement */
						$ignoreValue = trim($node->nodeValue);
						if (strlen($conditionToCompare) == strlen($ignoreValue))
						{
							$condition = str_replace($ignoreValue, '', $condition);
							$condition = str_replace(strtolower($ignoreValue), '', $condition);
						}

						$ignoreValue = trim($node->nodeValue) . ' ';
						$condition = str_replace($ignoreValue, '', $condition);
						$condition = str_replace(strtolower($ignoreValue), '', $condition);

						$ignoreValue = ' ' . trim($node->nodeValue);
						$condition = str_replace($ignoreValue, '', $condition);
						$condition = str_replace(strtolower($ignoreValue), '', $condition);
					}

					if ($condition != "" && $condition != '""')
						$result[] = $condition;
					$conditionToCompare = trim(str_replace('"', '', $condition));
					$queryResult = $xpath->query('//Config/AliasFor');
					foreach ($queryResult as $node)
					{
						/** @var $node DomElement */
						$target = strtolower(trim($groupName = $node->getAttribute('Target')));
						if ($target != strtolower($conditionToCompare)) continue;
						$aliasNodes = $node->getElementsByTagName('Item');
						foreach ($aliasNodes as $aliasNode)
							$result[] = str_replace($conditionToCompare, trim($aliasNode->nodeValue), $condition);
					}
				} catch (Exception $e)
				{
				}
			}
			return $result;
		}
	}