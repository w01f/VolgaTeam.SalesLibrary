<ul class="nav nav-pills">
	<? foreach ($links as $link): ?>
		<li id="<? echo $link['id']; ?>">
			<a href="#"><img src="<? echo $link['file_type'];?>"><? echo $link['name'];?></a>
		</li>
	<?php endforeach;?>
</ul>