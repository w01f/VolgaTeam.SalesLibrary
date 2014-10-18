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