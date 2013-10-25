<?php
	class HomeBar
	{
		public $configured;
		public $imagePath;
		public $imageAlignment;
		public $backColor;
		public $foreColor;
		public $font;

		public function __construct($pageRecord)
		{
			$this->imagePath = Yii::app()->getBaseUrl(true) . $pageRecord->source_path . '/HomeBar.png?' . $pageRecord->id;

			$pageConfig = new DOMDocument();
			$pageConfig->loadXML($pageRecord->config);
			$xpath = new DomXPath($pageConfig);
			$queryResult = $xpath->query('//Config/HomeBar');
			$this->configured = $queryResult->length > 0;
			$queryResult = $xpath->query('//Config/HomeBar/ImageAlignment');
			$this->imageAlignment = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : 'left';
			$queryResult = $xpath->query('//Config/HomeBar/TextColor');
			$this->foreColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : '#000000';
			$queryResult = $xpath->query('//Config/HomeBar/BackColor');
			$this->backColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : '#ffffff';

			$this->font = Font::getDefault();
			$queryResult = $xpath->query('//Config/HomeBar/Font/Name');
			if ($queryResult->length > 0) $this->font->name = trim($queryResult->item(0)->nodeValue);
			$queryResult = $xpath->query('//Config/HomeBar/Font/Size');
			if ($queryResult->length > 0) $this->font->size = (int)trim($queryResult->item(0)->nodeValue);
			$queryResult = $xpath->query('//Config/HomeBar/Font/Bold');
			if ($queryResult->length > 0) $this->font->isBold = filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN);
			$queryResult = $xpath->query('//Config/HomeBar/Font/Italic');
			if ($queryResult->length > 0) $this->font->isItalic = filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN);
		}
	}