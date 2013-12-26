<?php
	class Question
	{
		public $text;
		public $order;
		public $logoPath;

		public $answers;

		public function __construct($questionNode, $sourcePath)
		{
			$queryResult = $questionNode->getElementsByTagName("Text");
			$this->text = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';

			$queryResult = $questionNode->getElementsByTagName("Order");
			$this->order = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;

			$queryResult = $questionNode->getElementsByTagName("Logo");
			$this->logoPath = $queryResult->length > 0 ? ($sourcePath . '/' . trim($queryResult->item(0)->nodeValue)) : null;

			$answerNodes = $questionNode->getElementsByTagName('Answer');
			foreach ($answerNodes as $answerNode)
			{
				$answer = new Answer($answerNode);
				$answers[] = $answer;
			}
			$sortHelper = new ObjectSortHelper('order', 'asc');
			usort($answers, array($sortHelper, 'sort'));
			$this->answers = $answers;
		}

		public function isCorrectAnswer($answerOrder)
		{
			foreach ($this->answers as $answer)
				if ($answer->order == $answerOrder)
					return $answer->isCorrect;
			return false;
		}
	}