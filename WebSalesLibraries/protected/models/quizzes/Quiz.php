<?php

	class Quiz
	{
		public $id;
		public $uniqId;
		public $title;
		public $date;
		public $subtitle;
		public $header;
		public $footer;
		public $isActive;
		public $allowRetake;
		public $passScore;
		public $sendScoreToAdmin;
		public $adminEmails;
		public $coverLogoPath;
		public $endLogoPath;

		public $questions;

		public $config;

		public $hasResults;

		public function __construct($quizRecord)
		{
			$quizConfig = new DOMDocument();
			$quizConfig->loadXML($quizRecord->config);
			$xpath = new DomXPath($quizConfig);

			$sourcePath = Yii::app()->getBaseUrl(true) . $quizRecord->source_path;

			$this->id = $quizRecord->id;
			$this->config = $quizRecord->config;
			$this->title = $quizRecord->name;
			$this->passScore = $quizRecord->pass_score;
			$queryResult = $xpath->query('//Quiz/ID');
			$this->uniqId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			$queryResult = $xpath->query('//Quiz/Date');
			$this->date = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			$queryResult = $xpath->query('//Quiz/Subtitle');
			$this->subtitle = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			$queryResult = $xpath->query('//Quiz/Header');
			$this->header = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			$queryResult = $xpath->query('//Quiz/Footer');
			$this->footer = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			$queryResult = $xpath->query('//Quiz/Active');
			$this->isActive = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			$queryResult = $xpath->query('//Quiz/AllowRetake');
			$this->allowRetake = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			$queryResult = $xpath->query('//Quiz/SendUserEmailScore');
			$this->sendScoreToAdmin = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			$queryResult = $xpath->query('//Quiz/AdminEmail');
			foreach ($queryResult as $node)
				$this->adminEmails[] = trim($node->nodeValue);
			$queryResult = $xpath->query('//Quiz/CoverLogo');
			$this->coverLogoPath = $queryResult->length > 0 ? ($sourcePath . '/' . trim($queryResult->item(0)->nodeValue)) : null;
			$queryResult = $xpath->query('//Quiz/EndLogo');
			$this->endLogoPath = $queryResult->length > 0 ? ($sourcePath . '/' . trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('//Quiz/Question');
			foreach ($queryResult as $node)
			{
				$question = new Question($node, $sourcePath);
				$questions[] = $question;
			}
			$sortHelper = new ObjectSortHelper('order', 'asc');
			usort($questions, array($sortHelper, 'sort'));
			$this->questions = $questions;
		}

		public function saveResults($userId, $results)
		{
			$quizDate = date("Y-m-d H:i:s");
			$quizSet = uniqid();
			foreach ($results as $result)
			{
				foreach ($this->questions as $question)
				{
					if ($question->order == $result['question'])
					{
						$quizResult = new QuizResultStorage();
						$quizResult->id = uniqid();
						$quizResult->id_user = $userId;
						$quizResult->id_quiz = $this->uniqId;
						$quizResult->quiz_set = $quizSet;
						$quizResult->id_question = $question->order;
						$quizResult->question_result = intval($result['answer']);
						$quizResult->successful = $question->isCorrectAnswer($quizResult->question_result);
						$quizResult->date = $quizDate;
						$quizResult->save();
						break;
					}
				}
			}
			return $quizSet;
		}
	}

