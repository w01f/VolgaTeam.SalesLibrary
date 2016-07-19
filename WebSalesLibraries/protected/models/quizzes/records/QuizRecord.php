<?php

	/**
	 * Class QuizRecord
	 * @property mixed id
	 * @property string id_group
	 * @property mixed unique_id
	 * @property mixed source_path
	 * @property mixed name
	 * @property mixed order
	 * @property mixed pass_score
	 * @property string config
	 */
	class QuizRecord extends CActiveRecord
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
			return '{{quiz}}';
		}

		/**
		 * @return array
		 */
		public function relations()
		{
			return array(
				'results' => array(self::HAS_MANY, 'QuizResultRecord', array('id_quiz' => 'unique_id'), 'together' => false),
			);
		}

		/**
		 * @param $userId
		 * @return bool
		 */
		public function isPassed($userId)
		{
			$criteria = new CDbCriteria;
			$criteria->select = '(sum(t.successful)/count(t.successful))*100 as quiz_score'; // select fields which you want in output
			$criteria->condition = 't.id_user=:userId and t.id_quiz=:quizId';
			$criteria->params = array(':userId' => $userId, ':quizId' => $this->unique_id);
			$criteria->group = 't.quiz_set';
			/** @var  $quizResults QuizResultRecord[] */
			$quizResults = QuizResultRecord::model()->findAll($criteria);
			foreach ($quizResults as $quizResult)
				if ($quizResult->quiz_score >= $this->pass_score)
					return true;
			return false;
		}

		/**
		 * @return Quiz
		 */
		public function getEntity()
		{
			$quiz = new Quiz($this);
			return $quiz;
		}

		/**
		 * @param $groupId
		 * @return QuizItem[]
		 */
		public static function getByGroup($groupId)
		{
			$quizItems = null;
			$userId = UserIdentity::getCurrentUserId();
			/** @var $quizRecords QuizRecord[] */
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

		/**
		 *
		 */
		public static function clearData()
		{
			self::model()->deleteAll();
		}

	}
