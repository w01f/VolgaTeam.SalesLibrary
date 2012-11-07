<ul data-role="listview" data-theme="c" data-divider-theme="d">
    <li data-role="list-divider" >
        <h4><?php echo $page->name; ?></h4>
    </li>
    <?php if (isset($page->folders)): ?>
        <?php foreach ($page->folders as $folder): ?>
                <li>
                    <a class ="folder-link" href="#folder<?php echo $folder->id; ?>"><?php echo $folder->name; ?></a>
                </li>                    
        <?php endforeach; ?>                    
    <?php endif; ?>                    
</ul>