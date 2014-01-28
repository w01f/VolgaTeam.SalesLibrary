<?php

	class QuizzesController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'quizzes');
		}

		public function actionGetQuizzesView()
		{
			$userId = Yii::app()->user->getId();
			if (isset($userId))
			{
				$quizItems = QuizGroupStorage::getChildItems(null);

				$selectedQuizItemName = isset(Yii::app()->request->cookies['selectedQuizItemName']) ? Yii::app()->request->cookies['selectedQuizItemName']->value : null;
				$selectedQuizItemBreadcrumbs = null;
				if (isset($selectedQuizItemName))
				{
					foreach ($quizItems as $quizItem)
					{
						$selectedQuizItemBreadcrumbs = $quizItem->getSelectedItemBreadcrumbs($selectedQuizItemName);
						if (isset($selectedQuizItemBreadcrumbs))
							break;
					}
				}
				$this->renderPartial('quizzesView', array('quizItems' => $quizItems, 'selectedQuizItemBreadcrumbs' => $selectedQuizItemBreadcrumbs), false, true);
			}
		}

		public function actionGetQuizList()
		{
			$userId = Yii::app()->user->getId();
			$parentId = Yii::app()->request->getPost('parentId');
			if (isset($userId) && isset($parentId))
			{
				$quizItems = QuizGroupStorage::getChildItems($parentId);
				$this->renderPartial('quizzesList', array('quizItems' => $quizItems), false, true);
			}
		}

		public function actionGetQuizPanel()
		{
			$userId = Yii::app()->user->getId();
			$quizId = Yii::app()->request->getPost('quizId');
			if (isset($userId) && isset($quizId))
			{
				$quizRecord = QuizStorage::model()->findByPk($quizId);
				$quiz = $quizRecord->getEntity();

				$quizResults = QuizResultStorage::getUserResults($userId, $quiz);
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
			$userId = Yii::app()->user->getId();
			$quizId = Yii::app()->request->getPost('quizId');
			if (isset($userId) && isset($quizId))
			{
				$quizRecord = QuizStorage::model()->findByPk($quizId);
				$quiz = $quizRecord->getEntity();
				$this->renderPartial('quizCover', array('quiz' => $quiz), false, true);
			}
		}

		public function actionGetQuizEnd()
		{
			$userId = Yii::app()->user->getId();
			$quizId = Yii::app()->request->getPost('quizId');
			$results = Yii::app()->request->getPost('results');
			if (isset($results))
				$results = CJSON::decode($results);
			else
				$results = array();
			if (isset($userId) && isset($quizId) && isset($results))
			{
				$quizRecord = QuizStorage::model()->findByPk($quizId);
				$quiz = $quizRecord->getEntity();
				$quizSet = $quiz->saveResults($userId, $results);
				$quizResults = QuizResultStorage::getQuizResult($userId, $quiz, $quizSet);
				if ($quizResults->successful)
					StatisticActivityStorage::WriteActivity('Quizzes', 'Quiz Passed', array('Name' => $quiz->title, 'Score' => $quizResults->score . '%'));
				else
					StatisticActivityStorage::WriteActivity('Quizzes', 'Quiz Failed', array('Name' => $quiz->title, 'Score' => $quizResults->score . '%'));
				if ($quiz->sendScoreToAdmin && isset($quiz->adminEmails) && isset(Yii::app()->user->email))
				{
					$message = Yii::app()->email;
					$to = $quiz->adminEmails;
					$to[] = Yii::app()->user->email;
					$message->to = $to;
					$groups = UserStorage::getGroupNames(Yii::app()->user->id);
					$message->subject = $quiz->title . ' - ' . Yii::app()->user->firstName . ' ' . Yii::app()->user->lastName . (isset($groups) ? (' - ' . $groups) : '');
					$message->from = Yii::app()->params['email']['quiz']['from'];
					if (Yii::app()->params['email']['quiz']['copy_enabled'])
						$message->cc = Yii::app()->params['email']['quiz']['copy'];
					$message->view = 'sendQuizResult';
					$message->viewVars = array('quizResults' => $quizResults);
					//$message->send();
				}
				$this->renderPartial('quizEnd', array('quiz' => $quiz, 'quizResults' => $quizResults), false, true);
			}
		}

		public function actionGetQuizQuestion()
		{
			$userId = Yii::app()->user->getId();
			$quizId = Yii::app()->request->getPost('quizId');
			$quizQuestion = Yii::app()->request->getPost('quizQuestion');
			if (isset($userId) && isset($quizId) && isset($quizQuestion))
			{
				$quizRecord = QuizStorage::model()->findByPk($quizId);
				$quiz = $quizRecord->getEntity();
				$questionOrder = intval($quizQuestion);
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