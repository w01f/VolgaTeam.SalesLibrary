<table>
	<tr class="header <? echo $passed ? 'passed' : 'not-passed'; ?>">
		<td class="title">
			<h3><? echo $quiz->title; ?></h3>
			<p class="muted"><? echo $quiz->footer; ?></p>
		</td>
		<? if ($quiz->isActive && (!$quiz->hasResults || $quiz->allowRetake)): ?>
			<td class="buttons">
				<button type="button" class="btn btn-large quiz-run">Take Quiz</button>
			</td>
		<? endif; ?>
	</tr>
	<tr>
		<td colspan="2"><? echo $this->renderPartial('quizResults', array('quizResults' => $quizResults, 'passScore' => $quiz->passScore), true); ?></td>
	</tr>
</table>
<div class="quiz-data">
	<div class="config"><? echo $quiz->config; ?></div>
</div>