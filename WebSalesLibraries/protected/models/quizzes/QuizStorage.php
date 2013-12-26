<?php
	class QuizStorage extends CActiveRecord
	{
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{quiz}}';
		}

		public function getEntity()
		{
			$quiz = new Quiz($this);
			return $quiz;
		}

		public static function getByGroup($groupId)
		{
			$quizItems = null;
			$quizRecords = isset($groupId) ?
				self::model()->findAll("id_group=? order by 'order'", array($groupId)) :
				self::model()->findAll("id_group is null order by 'order'");
			foreach ($quizRecords as $quizRecord)
			{
				$quizItem = new QuizItem();
				$quizItem->id = $quizRecord->id;
				$quizItem->name = $quizRecord->name;
				$quizItem->isGroup = false;
				$quizItems[] = $quizItem;
			}
			return $quizItems;
		}

		public static function clearData()
		{
			self::model()->deleteAll();
		}

	}
