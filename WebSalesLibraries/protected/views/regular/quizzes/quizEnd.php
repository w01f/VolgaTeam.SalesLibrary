<table class="quiz-spash">
	<tr>
		<td colspan="2"><label><? echo $quiz->header; ?></label></td>
	</tr>
	<tr>
		<td colspan="2"><img src="<? echo $quiz->coverLogoPath; ?>"></td>
	</tr>
	<tr>
		<td colspan="2">
			<h3><? echo $quiz->title; ?></h3>
			<h4><? echo $quiz->subtitle; ?></h4>
		</td>
	</tr>
	<? if ($quizResults->successful): ?>
		<tr>
			<td colspan="2"><h2 class="success">Congratulations! You PASSED!</h2></td>
		</tr>
		<tr>
			<td colspan="2">
				<button class="btn btn-large quiz-exit">EXIT</button>
			</td>
		</tr>
	<? else: ?>
		<tr>
			<td colspan="2"><h2 class="wrong">Sorry… You did not Pass…</h2></td>
		</tr>
		<tr>
			<td><label>Correct Answers: <? echo $quizResults->correctAnswers; ?></label></td>
			<td><label>Wrong Answers: <? echo $quizResults->wrongAnswers; ?></label></td>
		</tr>
		<tr>
			<? if ($quiz->allowRetake): ?>
				<td>
					<button class="btn btn-large quiz-start">RE-TAKE</button>
				</td>
				<td>
					<button class="btn btn-large quiz-exit">EXIT</button>
				</td>
			<? else: ?>
				<td colspan="2">
					<button class="btn btn-large quiz-start">EXIT</button>
				</td>
			<? endif ?>
		</tr>
	<? endif ?>
</table>