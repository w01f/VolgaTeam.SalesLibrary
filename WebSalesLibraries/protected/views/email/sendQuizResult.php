<?
	/**
	 * @var $quizResults QuizResult
	 * @var $page QPageRecord
	 */
?>
	<h4> Below are your Quiz Results for:</
		h4 >
	<p><? echo $quizResults->title; ?> completed on: <? echo date(Yii::app()->params['outputDateFormat'] . ' ' . Yii::app()->params['outputTimeFormat'], strtotime($quizResults->date)); ?></p>
	<table style="th{text-align: left;}">
		<thead>
		<th>Question</th>
		<th>Answer</th>
		<th>Result</th>
		</thead>
		<? foreach ($quizResults->questionResults as $questionResult): ?>
			<tr>
				<td><? echo $questionResult->questionText; ?></td>
				<td><? echo $questionResult->answerValue; ?></td>
				<td><? echo $questionResult->successful ? 'Correct' : 'Wrong'; ?></td>
			</tr>
		<? endforeach ?>
	</table>
	<h4>Score: <? echo $quizResults->score; ?>%</h4>
<? if (!$quizResults->successful): ?>
	<p>100% is required to pass this course</p>
<? endif ?>