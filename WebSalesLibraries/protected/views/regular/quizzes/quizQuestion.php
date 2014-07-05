<?
	/**
	 * @var $quiz Quiz
	 * @var $question Question
	 */
?>
<table class="quiz-question">
	<tr>
		<td class="title"><label><? echo $quiz->title; ?></label></td>
		<td class="logo"><img src="<? echo $question->logoPath; ?>"></td>
	</tr>
	<tr>
		<td colspan="2" class="question-title"><h3><? echo $question->text; ?></h3></td>
	</tr>
</table>
<table class="quiz-question">
	<? foreach ($question->answers as $answer): ?>
		<tr>
			<td class="answer-selector">
				<button id="<? echo 'quiz' . $quiz->id . 'question' . $question->order . 'answer' . $answer->order; ?>" class="btn btn-default"><? echo $answer->letter; ?></button>
			</td>
			<td class="answer-text"><p class="lead"><? echo $answer->value; ?></p></td>
		</tr>
	<? endforeach ?>
</table>
<table class="quiz-question">
	<tr>
		<td class="prev-question">
			<button class="btn btn-default btn-lg">Previous</button>
		</td>
		<td class="next-question">
			<button class="btn btn-default btn-lg">Next</button>
		</td>
	</tr>
</table>
<p class="text-center"><em><small class="text-muted"><? echo $quiz->subtitle; ?></small></em></p>