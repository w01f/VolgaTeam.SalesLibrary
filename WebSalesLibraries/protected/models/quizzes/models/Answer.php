<?php

	/**
	 * Class Answer
	 */
	class Answer
	{
		public $value;
		public $order;
		public $letter;
		public $isCorrect;

		/**
		 * @param $answerNode DOMElement
		 */
		public function __construct($answerNode)
		{
			$queryResult = $answerNode->getAttribute('Value');
			$this->value = isset($queryResult) ? trim($queryResult) : '';

			$queryResult = $answerNode->getAttribute('Order');
			$this->order = isset($queryResult) ? intval(trim($queryResult)) : 0;

			$letters = str_split('ABCDEFGHIJKLMNOPQRSTUVWXYZ');
			$this->letter = $letters[$this->order - 1];

			$queryResult = $answerNode->getAttribute('IsCorrect');
			$this->isCorrect = isset($queryResult) ? filter_var(trim($queryResult), FILTER_VALIDATE_BOOLEAN) : false;
		}
	}