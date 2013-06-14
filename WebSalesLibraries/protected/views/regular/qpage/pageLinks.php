<ul class="nav nav-pills">
	<? foreach ($links as $link): ?>
		<li id="<? echo $link['id']; ?>">
			<a href="#"><img src="<? echo 'data:image/png;base64,' . $link['file_type'];?>"><? echo $link['name'];?></a>
		</li>
	<?php endforeach;?>
</ul>