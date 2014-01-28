<ul class="quizzes-list nav nav-list">
	<? foreach ($quizItems as $quizItem): ?>
		<? $selected = isset($selectedQuizItemBreadcrumbs) && in_array($quizItem->id, $selectedQuizItemBreadcrumbs); ?>
		<li class="<? echo $quizItem->isGroup ? 'quiz-group' : ('quiz-item' . ($quizItem->isPassed ? ' passed' : ' not-passed')) ?>">
			<? if ($quizItem->isGroup): ?>
				<a href="#"><i class="<? echo $selected ? 'icon-folder-open' : 'icon-folder-close'; ?>"></i><span><? echo $quizItem->name; ?></span></a>
			<? else: ?>
				<a href="#" <? if ($selected): ?>class="opened" <? endif; ?>><i class="icon-file"></i><span><? echo $quizItem->name; ?></span></a>
			<?endif; ?>
			<div class="service-data">
				<div class="item-id"><? echo $quizItem->id; ?></div>
			</div>
			<?
				if ($selected && isset($quizItem->childItems))
					echo $this->renderPartial('quizzesList', array('quizItems' => $quizItem->childItems, 'selectedQuizItemBreadcrumbs' => $selectedQuizItemBreadcrumbs), true);
			?>
		</li>
	<?php endforeach; ?>
</ul>