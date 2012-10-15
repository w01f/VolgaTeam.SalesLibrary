<a class="btn btn-small btn-block dropdown-toggle" data-toggle="dropdown" href="#">
    <span class="list-item-name"><?php echo $selectedPage->name; ?></span>
    <div class="list-item-info">
        <div class="list-item-image-path"><?php echo $selectedPage->logoPath; ?></div>
    </div>
    <span class="caret"></span>    
</a>
<ul class="dropdown-menu">
    <?php foreach ($selectedLibrary->pages as $page): ?>      
        <li>
            <a class="list-item-name" href="#"><?php echo $page->name; ?></a>
            <div class="list-item-info">
                <div class="list-item-image-path"><?php echo $page->logoPath; ?></div>
            </div>
        </li>
    <?php endforeach; ?>        
</ul>