<?php if (isset($link)): ?>
	<li>
		<a class="<?php echo $link->isFolder ? 'folder-content-link' : 'file-link'; ?>" href="#link<?php echo $link->id; ?>">
			<? if ($link->enableFileCard || $link->enableAttachments): ?>
				<img class="ui-li-has-thumb file-link-detail" src="<? echo Yii::app()->baseUrl . '/images/search/expand-phone.png'; ?>"/>
			<? endif; ?>
			<h3 class="name"><? if (isset($link->name) && $link->name != '') echo $link->name;
				else if (isset($link->fileName) && $link->fileName != '') echo $link->fileName; ?></h3>
			<? if (isset($link->name) && $link->name != '' && !$link->isFolder): ?>
				<p class="file"><?php echo $link->fileName; ?></p>
			<? endif; ?>
		</a>
	</li>
<?php endif; ?>