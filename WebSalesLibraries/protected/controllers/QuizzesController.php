<?php

	/**
	 * Class QuizzesController
	 */
	class QuizzesController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'quizzes');
		}

		public function actionGetQuizList()
		{
			$parentId = Yii::app()->request->getPost('parentId');
			if (isset($parentId))
			{
				$quizItems = QuizGroupRecord::getChildItems($parentId);
				$this->renderPartial('quizzesList', array('quizItems' => $quizItems), false, true);
			}
		}

		public function actionGetQuizPanel()
		{
			$userId = UserIdentity::getCurrentUserId();
			$quizId = Yii::app()->request->getPost('quizId');
			if (isset($quizId))
			{
				/** @var $quizRecord QuizRecord */
				$quizRecord = QuizRecord::model()->findByPk($quizId);
				$quiz = $quizRecord->getEntity();
				$quizResults = QuizResultRecord::getUserResults($userId, $quiz);
				$passed = false;
				if (isset($quizResults))
					foreach ($quizResults as $quizResult)
						if ($quizResult->successful)
						{
							$passed = true;
							break;
						}
				$this->renderPartial('quizPanel', array('quiz' => $quiz, 'quizResults' => $quizResults, 'passed' => $passed), false, true);
			}
		}

		public function actionGetQuizCover()
		{
			$quizId = Yii::app()->request->getPost('quizId');
			if (isset($quizId))
			{
				/** @var $quizRecord QuizRecord */
				$quizRecord = QuizRecord::model()->findByPk($quizId);
				$quiz = $quizRecord->getEntity();
				$this->renderPartial('quizCover', array('quiz' => $quiz), false, true);
			}
		}

		public function actionGetQuizEnd()
		{
			$userId = UserIdentity::getCurrentUserId();
			$quizId = Yii::app()->request->getPost('quizId');
			$results = Yii::app()->request->getPost('results');
			if (isset($results))
				$results = CJSON::decode($results);
			else
				$results = array();
			if (isset($quizId) && isset($results))
			{
				/** @var $quizRecord QuizRecord */
				$quizRecord = QuizRecord::model()->findByPk($quizId);
				$quiz = $quizRecord->getEntity();
				$quizSet = $quiz->saveResults($userId, $results);
				$quizResults = QuizResultRecord::getQuizResult($userId, $quiz, $quizSet);
				if ($quizResults->successful)
					StatisticActivityRecord::writeCommonActivity('Quizzes', 'Quiz Passed', array('name' => $quiz->subtitle . ' - ' . $quiz->title . ' - ' . $quiz->date, 'id' => $quiz->uniqId, 'score' => $quizResults->score . '%'));
				else
					StatisticActivityRecord::writeCommonActivity('Quizzes', 'Quiz Failed', array('name' => $quiz->subtitle . ' - ' . $quiz->title . ' - ' . $quiz->date, 'id' => $quiz->uniqId, 'score' => $quizResults->score . '%'));
				if ($quiz->sendScoreToAdmin && isset($quiz->adminEmails) && isset(Yii::app()->user->email))
				{
					$message = Yii::app()->email;
					$to = $quiz->adminEmails;
					$to[] = Yii::app()->user->email;
					$message->to = $to;
					$groups = UserRecord::getGroupNames(UserIdentity::getCurrentUserId());
					$message->subject = $quiz->title . ' - ' . Yii::app()->user->firstName . ' ' . Yii::app()->user->lastName . (count($groups) > 0 ? (' - ' . implode(', ', $groups)) : '');
					$message->from = Yii::app()->params['email']['quiz']['from'];
					if (Yii::app()->params['email']['quiz']['copy_enabled'])
						$message->cc = Yii::app()->params['email']['quiz']['copy'];
					$message->view = 'sendQuizResult';
					$message->viewVars = array('quizResults' => $quizResults);
					$message->send();
				}
				$this->renderPartial('quizEnd', array('quiz' => $quiz, 'quizResults' => $quizResults), false, true);
			}
		}

		public function actionGetQuizQuestion()
		{
			$quizId = Yii::app()->request->getPost('quizId');
			$quizQuestion = Yii::app()->request->getPost('quizQuestion');
			if (isset($quizId) && isset($quizQuestion))
			{
				/** @var $quizRecord QuizRecord */
				$quizRecord = QuizRecord::model()->findByPk($quizId);
				$quiz = $quizRecord->getEntity();
				$questionOrder = intval($quizQuestion);
				/** @var $selectedQuestion Question */
				foreach ($quiz->questions as $question)
				{
					if ($question->order == $questionOrder)
					{
						$selectedQuestion = $question;
						break;
					}
				}
				$this->renderPartial('quizQuestion', array('quiz' => $quiz, 'question' => $selectedQuestion), false, true);
			}
		}
	}