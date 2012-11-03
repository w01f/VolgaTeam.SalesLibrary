<div data-role="collapsible-set" data-inset="false" data-theme="b">
    <?php foreach ($libraryManager->getLibraries() as $library): ?>
        <div class="library-item-container" data-role="collapsible" >
            <h3><span class="library-title"><?php echo $library->name; ?></span></h3>
            <ul data-role="listview" data-divider-theme="d">
                <?php foreach ($library->pages as $page): ?>
                    <li data-role="list-divider" >
                        <h4><?php echo $page->name; ?></h4>
                    </li>
                    <?php $page->loadData('phone'); ?>
                    <?php foreach ($page->folders as $folder): ?>
                        <li>
                            <a class ="folder-link" href="#<?php echo str_replace('/', '----------', $page->name); ?>-folder-<?php echo $folder->id; ?>">
                                <span><?php echo $folder->name; ?></span>
                            </a>
                        </li>                    
                    <?php endforeach; ?>                    
                <?php endforeach; ?>                    
            </ul>
        </div>                                
    <?php endforeach; ?>
</div>