<?
	/**
	 * @var $quizItems QuizItem[]
	 * @var $selectedQuizItemBreadcrumbs array
	 */
?>
<ul class="quizzes-list nav nav-list">
	<? if (isset($quizItems)): ?>
		<? foreach ($quizItems as $quizItem): ?>
			<? $selected = isset($selectedQuizItemBreadcrumbs) && in_array($quizItem->id, $selectedQuizItemBreadcrumbs); ?>
			<li class="<? echo $quizItem->isGroup ? 'quiz-group' : ('quiz-item' . ($quizItem->isPassed ? ' passed' : ' not-passed')) ?>">
				<? if ($quizItem->isGroup): ?>
					<a href="#"><span class="<? echo $selected ? 'glyphicon glyphicon-folder-open' : 'glyphicon glyphicon-folder-close'; ?>"></span><span><? echo $quizItem->name; ?></span></a>
				<? else: ?>
					<a href="#" <? if ($selected): ?>class="opened" <? endif; ?>><span class="glyphicon glyphicon-file"></span><span><? echo $quizItem->name; ?></span></a>
				<?endif; ?>
				<div class="service-data">
					<div class="item-id"><? echo $quizItem->id; ?></div>
				</div>
				<?
					if ($selected && isset($quizItem->childItems))
						echo $this->renderPartial('../quizzes/quizzesList', array('quizItems' => $quizItem->childItems, 'selectedQuizItemBreadcrumbs' => $selectedQuizItemBreadcrumbs), true);
				?>
			</li>
		<? endforeach; ?>
	<? endif; ?>
</ul>