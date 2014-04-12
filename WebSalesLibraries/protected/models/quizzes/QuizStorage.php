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
			$criteria = new CDbCriteria;
			$criteria->select = '(sum(t.successful)/count(t.successful))*100 as quiz_score'; // select fields which you want in output
			$criteria->condition = 't.id_user=:userId and t.id_quiz=:quizId';
			$criteria->params = array(':userId' => $userId, ':quizId' => $this->unique_id);
			$criteria->group = 't.quiz_set';
			$quizResults = QuizResultStorage::model()->findAll($criteria);
			foreach ($quizResults as $quizResult)
				if ($quizResult->quiz_score >= $this->pass_score)
					return true;
			return false;
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
				self::model()->findAll(array('order' => "'order'", 'condition' => 'id_group=:x', 'params' => array(':x' => $groupId))) :
				self::model()->findAll(array('order' => "'order'", 'condition' => 'id_group is null'));
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
