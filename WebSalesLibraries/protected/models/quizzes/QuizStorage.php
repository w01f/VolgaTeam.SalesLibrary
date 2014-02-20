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

		public function relations()
		{
			return array(
				'results' => array(self::HAS_MANY, 'QuizResultStorage', array('id_quiz' => 'unique_id'), 'together' => false),
			);
		}

		public function isPassed($userId)
		{
			$all = self::model()->with(array(
				'results' => array(
					'select' => false,
					'joinType' => 'INNER JOIN',
					'condition' => 'results.id_user=' . $userId . " and t.id='" . $this->id."'",
				),
			))->count();
			if ($all == 0)
				return false;
			$successful = self::model()->with(array(
				'results' => array(
					'select' => false,
					'joinType' => 'INNER JOIN',
					'condition' => 'results.successful=1 and results.id_user=' . $userId . " and t.id='" . $this->id."'",
				),
			))->count();
			return $all == $successful;
		}

		public function getEntity()
		{
			$quiz = new Quiz($this);
			return $quiz;
		}

		public static function getByGroup($groupId)
		{
			$quizItems = null;
			$userId = Yii::app()->user->getId();
			$quizRecords = isset($groupId) ?
				self::model()->findAll(array('order'=>"'order'", 'condition'=>'id_group=:x', 'params'=>array(':x'=>$groupId))):
				self::model()->findAll(array('order'=>"'order'", 'condition'=>'id_group is null'));
			foreach ($quizRecords as $quizRecord)
			{
				$quizItem = new QuizItem();
				$quizItem->id = $quizRecord->id;
				$quizItem->name = $quizRecord->name;
				$quizItem->isGroup = false;
				$quizItem->isPassed = $quizRecord->isPassed($userId);
				$quizItems[] = $quizItem;
			}
			return $quizItems;
		}

		public static function clearData()
		{
			self::model()->deleteAll();
		}

	}
