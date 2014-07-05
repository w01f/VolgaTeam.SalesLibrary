<?php

	/**
	 * Class QuizResult
	 */
	class QuizResult
	{
		public $title;
		public $questionResults;
		public $passScore;

		public $correctAnswers;
		public $wrongAnswers;
		public $totalAnswers;
		public $successful;
		public $score;
		public $date;

		/**
		 * @param $resultRecords
		 * @param $quiz
		 */
		public function __construct($resultRecords, $quiz)
		{
			$this->title = $quiz->title;
			$this->passScore = $quiz->passScore;
			foreach ($resultRecords as $resultRecord)
			{
				$this->date = $resultRecord->date;

				$questionResult = new QuestionResult();
				foreach ($quiz->questions as $question)
				{
					if ($question->order == $resultRecord->id_question)
					{
						$questionResult->questionText = $question->text;
						foreach ($question->answers as $answer)
						{
							if ($answer->order == $resultRecord->question_result)
							{
								$questionResult->answerValue = $answer->value;
								$questionResult->successful = $answer->isCorrect;
								break;
							}
						}
						break;
					}
				}
				$this->questionResults[] = $questionResult;


			}
			$this->calcScore();
		}

		private function calcScore()
		{
			$this->correctAnswers = 0;
			$this->wrongAnswers = 0;
			foreach ($this->questionResults as $questionResult)
			{
				if ($questionResult->successful)
					$this->correctAnswers++;
				else
					$this->wrongAnswers++;
			}
			$this->totalAnswers = $this->correctAnswers + $this->wrongAnswers;
			$this->score = $this->totalAnswers > 0 ? round(($this->correctAnswers / $this->totalAnswers) * 100) : 0;
			$this->successful = $this->score >= $this->passScore;
		}
	}