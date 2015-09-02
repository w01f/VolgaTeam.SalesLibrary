<?
	/**
	 * @var $shortcut QuizzesShortcut
	 * @var $quizItems QuizItem[]
	 * @var $selectedQuizItemBreadcrumbs array
	 */
?>
<table id="quizzes-container">
	<tr>
		<td id="quizzes-navigator">
			<div>
				<? echo $this->renderPartial('../quizzes/quizzesList', array('quizItems' => $quizItems, 'selectedQuizItemBreadcrumbs' => $selectedQuizItemBreadcrumbs), true); ?>
			</div>
		</td>
		<td id="quiz-panel">
		</td>
	</tr>
</table>