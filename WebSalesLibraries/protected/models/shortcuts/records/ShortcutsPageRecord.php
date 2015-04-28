<?php

	/**
	 * Class ShortcutsPageRecord
	 * @property string id
	 * @property string id_tab
	 * @property mixed name
	 * @property mixed order
	 * @property mixed image_path
	 * @property mixed source_path
	 * @property mixed enabled
	 * @property mixed config
	 */
	class ShortcutsPageRecord extends CActiveRecord
	{
		/**
		 * @param string $className
		 * @return ShortcutsPageRecord
		 */
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		/**
		 * @return string
		 */
		public function tableName()
		{
			return '{{shortcut_page}}';
		}

		/**
		 * @param $login string
		 * @return bool
		 */
		public function isEnabled($login)
		{
			$approvedUsers = array();
			$pageConfig = new DOMDocument();
			$pageConfig->loadXML($this->config);
			$xpath = new DomXPath($pageConfig);
			$queryResult = $xpath->query('//Config/ApprovedUsers/User');
			foreach ($queryResult as $userNode)
				$approvedUsers[] = trim($userNode->nodeValue);
			return $this->enabled && (count($approvedUsers) == 0 || in_array($login, $approvedUsers));
		}

		/**
		 * @return ShortcutsLinkRecord[]
		 */
		public function getLinks()
		{
			return ShortcutsLinkRecord::model()->findAll(array('order' => '`order`', 'condition' => 'id_page=:id_page', 'params' => array(':id_page' => $this->id)));
		}

		/**
		 * @param $predefinedType string
		 * @return PageModel
		 */
		public function getModel($predefinedType = null)
		{
			$pageConfig = new DOMDocument();
			$pageConfig->loadXML($this->config);
			$xpath = new DomXPath($pageConfig);
			$queryResult = $xpath->query('//Config/Type');
			$originalType = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : '';

			if (!isset($predefinedType))
				$type = $originalType;
			else
				$type = $predefinedType;

			switch ($type)
			{
				case 'grid':
					switch ($originalType)
					{
						case 'carousel':
							return new CarouselGridPage($this);
						default:
							return new GridPage($this);
					}
				case 'carousel':
					return new CarouselPage($this);
				default:
					return new GridPage($this);
			}
		}

		public static function clearData()
		{
			self::model()->deleteAll();
		}
	}