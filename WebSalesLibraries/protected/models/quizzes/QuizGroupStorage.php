<?php
	class QuizGroupStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{quiz_group}}';
		}

		public static function clearData()
		{
			QuizStorage::clearData();
			self::model()->deleteAll();
		}

		public static function getChildItems($parentId)
		{
			$quizItems = null;
			$groupRecords = isset($parentId) ?
				self::model()->findAll(array('order'=>"'order'", 'condition'=>'id_parent=:x', 'params'=>array(':x'=>$parentId))):
				self::model()->findAll(array('order'=>"'order'", 'condition'=>'id_parent is null'));
			foreach ($groupRecords as $groupRecord)
			{
				$groupItem = new QuizItem();
				$groupItem->id = $groupRecord->id;
				$groupItem->name = $groupRecord->name;
				$groupItem->isGroup = true;
				$groupItem->childItems = self::getChildItems($groupRecord->id);
				$quizItems[] = $groupItem;
			}

			$childQuizItems = QuizStorage::getByGroup($parentId);
			if (isset($childQuizItems))
				foreach ($childQuizItems as $childQuizItem)
					$quizItems[] = $childQuizItem;
			return $quizItems;
		}
	}
