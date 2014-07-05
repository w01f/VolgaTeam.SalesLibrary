<?php

	/**
	 * Class QuizResultRecord
	 * @property mixed id
	 * @property int id_user
	 * @property mixed id_quiz
	 * @property string quiz_set
	 * @property mixed id_question
	 * @property mixed question_result
	 * @property mixed successful
	 * @property bool|string date
	 */
	class QuizResultRecord extends CActiveRecord
	{
		public $quiz_score;

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
			return '{{quiz_result}}';
		}

		/**
		 * @param $userId
		 * @param $quiz
		 * @param $quizSet
		 * @return QuizResult
		 */
		public static function getQuizResult($userId, $quiz, $quizSet)
		{
			$resultRecords = self::model()->findAll('id_user=? and id_quiz=? and quiz_set=?', array($userId, $quiz->uniqId, $quizSet));
			$result = new QuizResult($resultRecords, $quiz);
			return $result;
		}

		/**
		 * @param $userId
		 * @param $quiz
		 * @return QuizResult[]
		 */
		public static function getUserResults($userId, $quiz)
		{
			$quizResults = array();
			$quizSetRecords = Yii::app()->db->createCommand()
				->select("qr.quiz_set as quiz_set")
				->from('tbl_quiz_result qr')
				->group('qr.quiz_set')
				->where("qr.id_user=" . $userId . " and qr.id_quiz='" . $quiz->uniqId . "'")
				->order('qr.date desc')
				->queryAll();
			foreach ($quizSetRecords as $quizSetRecord)
			{
				$resultRecords = self::model()->findAll('id_user=? and id_quiz = ? and quiz_set=?', array($userId, $quiz->uniqId, $quizSetRecord['quiz_set']));

				$result = new QuizResult($resultRecords, $quiz);
				$quizResults[] = $result;
			}
			return $quizResults;
		}
	}
