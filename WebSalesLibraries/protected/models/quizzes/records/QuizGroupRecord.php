<?php

	/**
	 * Class QuizGroupRecord
	 * @property mixed id
	 * @property mixed name
	 * @property int order
	 * @property string id_parent
	 * @property string id_top_level
	 * @property mixed config
	 */
	class QuizGroupRecord extends CActiveRecord
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
			return '{{quiz_group}}';
		}

		public static function clearData()
		{
			QuizRecord::clearData();
			self::model()->deleteAll();
		}

		/**
		 * @param $userId
		 * @return bool
		 */
		public function isEnabled($userId)
		{
			$approvedGroups = array();
			if (isset($this->config))
			{
				$groupConfig = new DOMDocument();
				$groupConfig->loadXML($this->config);
				$xpath = new DomXPath($groupConfig);
				$queryResult = $xpath->query('//Config/ApprovedGroups/Group');
				foreach ($queryResult as $groupNode)
					$approvedGroups[] = trim($groupNode->nodeValue);
			}
			if (count($approvedGroups) == 0) return true;
			$enabled = false;
			$userGroups = UserGroupRecord::getGroupNamesByUser($userId);
			foreach ($approvedGroups as $approvedGroup)
				$enabled |= in_array($approvedGroup, $userGroups);
			return $enabled;
		}

		/**
		 * @param $parentId
		 * @return QuizItem[]
		 */
		public static function getChildItems($parentId)
		{
			$quizItems = array();
			/** @var $groupRecords QuizGroupRecord[] */
			$groupRecords = isset($parentId) ?
				self::model()->findAll(array('order' => "'order'", 'condition' => 'id_parent=:x', 'params' => array(':x' => $parentId))) :
				self::model()->findAll(array('order' => "'order'", 'condition' => 'id_parent is null'));
			foreach ($groupRecords as $groupRecord)
			{
				$groupItem = new QuizItem();
				$groupItem->id = $groupRecord->id;
				$groupItem->name = $groupRecord->name;
				$groupItem->isGroup = true;

				$userId = Yii::app()->user->getId();
				if ($groupRecord->isEnabled($userId))
				{
					$groupItem->childItems = self::getChildItems($groupRecord->id);
					$quizItems[] = $groupItem;
				}
			}

			$childQuizItems = QuizRecord::getByGroup($parentId);
			if (isset($childQuizItems))
				foreach ($childQuizItems as $childQuizItem)
					$quizItems[] = $childQuizItem;
			return $quizItems;
		}
	}
