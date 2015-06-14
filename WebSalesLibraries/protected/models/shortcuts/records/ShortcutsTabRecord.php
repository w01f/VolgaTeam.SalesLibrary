<?php

	/**
	 * Class ShortcutsTabRecord
	 * @property string id
	 * @property mixed name
	 * @property mixed order
	 * @property mixed enabled
	 * @property mixed image_path
	 */
	class ShortcutsTabRecord extends CActiveRecord
	{
		/**
		 * @param string $className
		 * @return CActiveRecord
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
			return '{{shortcut_tab}}';
		}

		public static function clearData()
		{
			ShortcutsLinkRecord::clearData();
			ShortcutsPageRecord::clearData();
			self::model()->deleteAll();
		}

		/**
		 * @return string
		 */
		public function getPhoneName()
		{
			$tabConfig = new DOMDocument();
			$tabConfig->loadXML($this->config);
			$xpath = new DomXPath($tabConfig);
			$queryResult = $xpath->query('//Config/JQMName');
			return $queryResult->length > 0 ? $queryResult->item(0)->nodeValue : $this->name;
		}

		/**
		 * @param $user CWebUser
		 * @return bool
		 */
		public function isAvailable($user)
		{
			$tabConfig = new DOMDocument();
			$tabConfig->loadXML($this->config);
			$xpath = new DomXPath($tabConfig);

			$isAvailable = true;

			$approvedUsers = array();
			$queryResult = $xpath->query('//Config/ApprovedUsers/User');
			foreach ($queryResult as $groupNode)
				$approvedUsers[] = trim($groupNode->nodeValue);
			$approvedGroups = array();
			$queryResult = $xpath->query('//Config/ApprovedGroups/Group');
			foreach ($queryResult as $groupNode)
				$approvedGroups[] = trim($groupNode->nodeValue);

			if (count($approvedUsers) > 0 || count($approvedGroups) > 0)
			{
				$isAvailable = false;

				if (isset($user))
				{
					$isAvailable |= in_array($user->login, $approvedUsers);

					$userGroups = $user->getState('groups');
					if (count($userGroups) > 0)
						$isAvailable |= array_intersect($userGroups, $approvedGroups);
				}
			}

			return $isAvailable;
		}

	}
