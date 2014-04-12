<?php
	class QuizResultStorage extends CActiveRecord
	{
		public $quiz_score;
		
		public static function model($className = __CLASS__)
		{
			return parent::model($className);
		}

		public function tableName()
		{
			return '{{quiz_result}}';
		}

		public static function getQuizResult($userId, $quiz, $quizSet)
		{
			$resultRecords = self::model()->findAll('id_user=? and id_quiz=? and quiz_set=?', array($userId, $quiz->uniqId, $quizSet));
			$result = new QuizResult($resultRecords, $quiz);
			return $result;
		}

		public static function getUserResults($userId, $quiz)
		{
			$quizResults = null;
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
