<? /** @var $link LibraryLink */ ?>
<ul data-role="listview" data-theme="c" data-divider-theme="c">
	<li data-role="list-divider">
		<h4>
			<table class="link-container">
				<tr>
					<td>
                        <span class="name">
                            <?
								if (isset($link->name) && $link->name != '')
									echo $link->name;
								else if (isset($link->fileName) && $link->fileName != '')
									echo $link->fileName;
							?>
                        </span>
					</td>
				</tr>
				<? if (isset($link->name) && $link->name != ''): ?>
					<tr>
						<td>
							<span class="file"><? echo $link->fileName; ?></span>
						</td>
					</tr>
				<? endif; ?>
			</table>
		</h4>
	</li>
</ul>
<?
	if (isset($link->fileCard)):
		$fileCard = $link->fileCard;
		?>
		<div class="file-card-body">
			<table class="details">
				<? if (isset($fileCard->advertiser)): ?>
					<tr>
						<td class="title">Advertiser:</td>
						<td><? echo $fileCard->advertiser ?><br></td>
					</tr>
				<? endif; ?>

				<? if (isset($fileCard->dateSold)): ?>
					<tr>
						<td class="title">Date Sold:</td>
						<td><? echo date(Yii::app()->params['outputDateFormat'], strtotime($fileCard->dateSold)) ?><br>
						</td>
					</tr>
				<? endif; ?>

				<? if (isset($fileCard->broadcastClosed)): ?>
					<tr>
						<td class="title">Broadcast $ Closed:</td>
						<td><? echo '$' . number_format($fileCard->broadcastClosed, 2, '.', ',') ?><br></td>
					</tr>
				<? endif; ?>

				<? if (isset($fileCard->digitalClosed)): ?>
					<tr>
						<td class="title">Digital $ Closed:</td>
						<td><? echo '$' . number_format($fileCard->digitalClosed, 2, '.', ',') ?><br></td>
					</tr>
				<? endif; ?>

				<? if (isset($fileCard->publishingClosed)): ?>
					<tr>
						<td class="title">Publishing $ Closed:</td>
						<td><? echo '$' . number_format($fileCard->publishingClosed, 2, '.', ',') ?><br></td>
					</tr>
				<? endif; ?>

				<? if (isset($fileCard->notes)): ?>
					<tr>
						<td class="title" colspan="2">Important Info:</td>
					</tr>
					<?
					foreach ($fileCard->notes as $note)
					{
						echo CHtml::openTag('tr', array());
						{
							echo CHtml::openTag('td', array('colspan' => '2'));
							{
								echo $note;
							}
							echo CHtml::closeTag('td');
						}
						echo CHtml::closeTag('tr');
					}
					?>
				<? endif; ?>
				<tr>
					<td class="separator" colspan="2"></td>
				</tr>
				<? if (isset($fileCard->salesStation)): ?>
					<tr>
						<td class="title">Station or Newspaper:</td>
						<td><? echo $fileCard->salesStation ?><br></td>
					</tr>
				<? endif; ?>

				<? if (isset($fileCard->salesName)): ?>
					<tr>
						<td class="title">Sales Contact:</td>
						<td><? echo $fileCard->salesName ?><br></td>
					</tr>
				<? endif; ?>

				<? if (isset($fileCard->salesEmail)): ?>
					<tr>
						<td class="title">Email:</td>
						<td><? echo $fileCard->salesEmail ?><br></td>
					</tr>
				<? endif; ?>

				<? if (isset($fileCard->salesPhone)): ?>
					<tr>
						<td class="title">Phone #:</td>
						<td><? echo $fileCard->salesPhone ?><br></td>
					</tr>
				<? endif; ?>
			</table>
		</div>
	<? endif; ?>
