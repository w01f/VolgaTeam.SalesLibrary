<?php

	class SearchHelper
	{
		public static function prepareTextCondition($condition)
		{
			$result = array();
			$result[] = $condition;

			$configPath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'search_config.xml';
			if (!file_exists($configPath)) return $result;
			try
			{
				$configContent = file_get_contents($configPath);
				$config = new DOMDocument();
				$config->loadXML($configContent);
				$xpath = new DomXPath($config);

				$queryResult = $xpath->query('//Config/Ignore');
				foreach ($queryResult as $node)
				{
					$ignoreValue = trim($node->nodeValue) . ' ';
					$condition = str_replace($ignoreValue, '', $condition);
					$condition = str_replace(strtolower($ignoreValue), '', $condition);
					$ignoreValue = ' ' . trim($node->nodeValue);
					$condition = str_replace($ignoreValue, '', $condition);
					$condition = str_replace(strtolower($ignoreValue), '', $condition);
				}
				$condition = preg_replace('!\s+!', ' ', $condition);

				$conditionToCompare = trim(str_replace('"', '', $condition));
				$queryResult = $xpath->query('//Config/AliasFor');
				foreach ($queryResult as $node)
				{
					$target = strtolower(trim($groupName = $node->getAttribute('Target')));
					if ($target != strtolower($conditionToCompare)) continue;
					$aliasNodes = $node->getElementsByTagName('Item');
					foreach ($aliasNodes as $aliasNode)
						$result[] = str_replace($conditionToCompare, trim($aliasNode->nodeValue), $condition);
				}
			} catch (Exception $e)
			{
			}
			return $result;
		}
	}