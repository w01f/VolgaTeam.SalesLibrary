<?	/** @var $libraryPage LibraryPage */?>
<div id="page-content-container">
    <?
    for ($i = 0; $i < 3; $i++):
        ?>  
        <? $folders = $libraryPage->getFoldersByColumn($i); ?>
        <? if (isset($folders)): ?>
            <div class="page-column" id="column<? echo $i; ?>">
                <? foreach ($folders as $folder): ?>
                    <div class="folder-container">
                        <?
                        $isAdmin = false;
                        $userId = null;
                        if (isset(Yii::app()->user))
                        {
                            $userId = Yii::app()->user->getId();
                            if (isset(Yii::app()->user->role))
                                $isAdmin = Yii::app()->user->role == 2;
                            else
                                $isAdmin = true;
                        }
                        $linksNumber = $folder->getRealLinksNumber();
                        $linksCaption = '';
                        if ($linksNumber == 1)
                            $linksCaption = '(1 Link)';
                        else if ($linksNumber > 1)
                            $linksCaption = '(' . $linksNumber . ' Links)';
                        ?>
                        <button type="button" class="btn btn-block folder-header" id="folder-<? echo $folder->id; ?>"><? echo $folder->name; ?>  <span class="links-number"><? echo $linksCaption; ?></span></button>
                        <div class="folder-links"></div>
                    </div>
                <? endforeach; ?>
            </div>
        <? endif; ?>                            
    <? endfor; ?>    
</div>
