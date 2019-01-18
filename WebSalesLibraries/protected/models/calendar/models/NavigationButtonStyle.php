<?php
	/**
	 * Created by PhpStorm.
	 * User: akrup
	 * Date: 1/13/2019
	 * Time: 6:51 PM
	 */

	namespace application\models\calendar\models;


	class NavigationButtonStyle
	{
		public $borderColor;
		public $backColorRegular;
		public $backColorSelected;
		public $foreColorRegular;
		public $foreColorSelected;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return NavigationButtonStyle
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$style = self::createDefault();

			$queryResult = $xpath->query('./BorderColor', $contextNode);
			$style->borderColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $style->borderColor;

			$queryResult = $xpath->query('./BackgroundColor', $contextNode);
			$style->backColorRegular = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $style->backColorRegular;

			$queryResult = $xpath->query('./SelectedBackgroundColor', $contextNode);
			$style->backColorSelected = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $style->backColorSelected;

			$queryResult = $xpath->query('./TextColor', $contextNode);
			$style->foreColorRegular = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $style->foreColorRegular;

			$queryResult = $xpath->query('./SelectedTextColor', $contextNode);
			$style->foreColorSelected = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $style->foreColorSelected;

			return $style;
		}

		/**
		 * @return NavigationButtonStyle
		 */
		public static function createDefault()
		{
			$instance = new self();
			$instance->borderColor = '8c8c8c';
			$instance->backColorRegular = 'ffffff';
			$instance->backColorSelected = 'd4d4d4';
			$instance->foreColorRegular = '000000';
			$instance->foreColorSelected = '000000';
			return $instance;
		}
	}