<?

	/**
	 * Class QuizzesShortcut
	 */
	class QuizzesShortcut extends PageContentShortcut
	{
		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);
		}

		/**
		 * @return array
		 */
		public function getViewParameters()
		{
			$viewParameters = parent::getViewParameters();
			$quizItems = QuizGroupRecord::getChildItems(null);
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
			$viewParameters['quizItems'] = $quizItems;
			$viewParameters['selectedQuizItemBreadcrumbs'] = $selectedQuizItemBreadcrumbs;
			return $viewParameters;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Quizzes';
		}
	}