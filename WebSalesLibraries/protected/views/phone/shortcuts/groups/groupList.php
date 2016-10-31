<?
	$menuGroups = ShortcutsManager::getAvailableGroups($this->isPhone);
?>
<? foreach ($menuGroups as $group): ?>
	<? if ($group->enabled == true): ?>
		<? $identifier = 'group-menu-item-' . $group->id; ?>
		<style>
			<?echo '#'.$identifier;?> .logo
			{
				color: <?echo '#'.$group->groupAppearance->iconColor;?> !important;
			}
			<?echo '#'.$identifier;?> .title
			{
			  	color: <?echo '#'.$group->groupAppearance->textColor;?> !important;
			}
		</style>
		<li data-icon="false">
			<a id="<? echo $identifier; ?>" class="group-menu-item" data-ajax="false" href="<? echo $group->getUrl(); ?>">
				<div>
					<? if ($group->useIcon == true): ?>
						<i class="logo <? echo $group->iconClass; ?>"></i>
					<? else: ?>
						<img class="logo" src="<? echo $group->imageContent; ?>" alt=""/>
					<?endif; ?>
					<span class="title"><? echo $group->title; ?></span>
				</div>
			</a>
		</li>
	<? endif; ?>
<? endforeach; ?>