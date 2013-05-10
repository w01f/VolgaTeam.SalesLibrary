<div id="page-content-container">
    <?php
    for ($i = 0; $i < 3; $i++):
        ?>  
        <?php $folders = $libraryPage->getFoldersByColumn($i); ?>
        <?php if (isset($folders)): ?>
            <div class="page-column" id="column<?php echo $i; ?>">
                <?php foreach ($folders as $folder): ?>
                    <div class="folder-container">
                        <?php
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
                        $linksNumber = $folder->getRealLinksNumber($isAdmin,$userId);
                        $linksCaption = '';
                        if ($linksNumber == 1)
                            $linksCaption = '(1 Link)';
                        else if ($linksNumber > 1)
                            $linksCaption = '(' . $linksNumber . ' Links)';
                        ?>
                        <button type="button" class="btn btn-block folder-header" id="folder-<?php echo $folder->id; ?>"><?php echo $folder->name; ?>  <span class="links-number"><?php echo $linksCaption; ?></span></button>
                        <div class="folder-links"></div>
                    </div>
                <?php endforeach; ?>
            </div>
        <?php endif; ?>                            
    <?php endfor; ?>    
</div>
