<?php $selectedLibrary = $libraryManager->getSelectedLibrary(); ?>
<a class="btn btn-small btn-block dropdown-toggle" data-toggle="dropdown" href="#">
    <span class="list-item-name"><?php echo $selectedLibrary->name; ?></span>
    <div class="list-item-info">
        <div class="list-item-image-path"><?php echo $selectedLibrary->logoPath; ?></div>
    </div>
    <span class="caret"></span>
</a>
<ul class="dropdown-menu">
    <?php foreach ($libraryManager->getLibraries() as $library): ?>      
        <li>
            <a class="list-item-name" href="#"><?php echo $library->name; ?></a>
            <div class="list-item-info">
                <div class="list-item-image-path"><?php echo $library->logoPath; ?></div>
            </div>
        </li>
    <?php endforeach; ?>        
</ul>