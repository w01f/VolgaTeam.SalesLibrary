<table class="quiz-spash">
	<tr>
		<td><label><? echo $quiz->header; ?></label></td>
	</tr>
	<tr>
		<td><img src="<? echo $quiz->coverLogoPath; ?>"></td>
	</tr>
	<tr>
		<td>
			<h3><? echo $quiz->title; ?></h3>
			<h4><? echo $quiz->subtitle; ?></h4>
		</td>
	</tr>
	<tr>
		<td class="next-question">
			<button class="btn btn-large btn-block">START</button>
		</td>
	</tr>
	<tr>
		<td><label><? echo $quiz->footer; ?></label></td>
	</tr>
</table>