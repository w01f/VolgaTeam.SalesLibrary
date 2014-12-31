<?
	/** @var $linkRecords ShortcutsLinkRecord[] */
	foreach ($linkRecords as $linkRecord):
		?>
		<?
		$linkContent = '';
		switch ($linkRecord->type)
		{
			case 'url':
			case 'file':
			case 'download':
			case 'mp4':
			case 'quicklist':
			case 'search':
				$linkContent = $this->renderPartial('directLink', array('link' => $linkRecord->GetModel()), true);
				break;
			case 'libraryfile':
				$linkContent = $this->renderPartial('libraryFileLink', array('link' => $linkRecord->GetModel()), true);
				break;
			case 'window':
				$linkContent = $this->renderPartial('windowLink', array('link' => $linkRecord->GetModel()), true);
				break;
			case 'page':
				$linkContent = $this->renderPartial('pageLink', array('link' => $linkRecord->GetModel()), true);
				break;
			case 'none':
				$linkContent = $this->renderPartial('emptyLink', array('link' => $linkRecord->GetModel()), true);
				break;
		}
		echo $linkContent;
		?>
	<? endforeach; ?>
