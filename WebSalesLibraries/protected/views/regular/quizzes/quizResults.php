<table class="table table-bordered table-hover quiz-results">
	<thead>
	<tr>
		<th>Date</th>
		<th>Score</th>
	</tr>
	</thead>
	<tbody>
	<? if (isset($quizResults)): ?>
		<? foreach ($quizResults as $quizResult): ?>
			<tr class="<? echo $quizResult->successful ? 'success' : 'error'; ?>">
				<td><? echo date(Yii::app()->params['outputDateFormat'] . ' ' . Yii::app()->params['outputTimeFormat'], strtotime($quizResult->date)); ?></td>
				<td><? echo $quizResult->score; ?>%</td>
			</tr>
		<? endforeach ?>
	<? endif ?>
	</tbody>
</table>