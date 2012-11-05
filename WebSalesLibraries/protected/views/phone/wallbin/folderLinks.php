<ul data-role="listview" data-theme="c" data-divider-theme="d">
    <li data-role="list-divider" >
        <h4><?php echo $folder->name; ?></h4>
    </li>
    <?php if (isset($folder->files)): ?>
        <?php foreach ($folder->files as $link): ?>
            <?php if ($link->type != 5 && $link->type != 6 && ((isset($link->name) && $link->name != '') || (isset($link->fileName) && $link->fileName != ''))): ?>
                <li>
                    <a class ="file-link" href="#folder<?php echo $folder->id; ?>-link-<?php echo $link->id; ?>">
                        <table class ="link-container">
                            <tr>
                                <td>                        
                                    <span class ="name">
                                        <?php
                                        if (isset($link->name) && $link->name != '')
                                            echo $link->name;
                                        else if (isset($link->fileName) && $link->fileName != '')
                                            echo $link->fileName;
                                        ?>
                                    </span>
                                </td>
                            </tr>
                            <?php if (isset($link->name) && $link->name != ''): ?>                            
                                <tr>
                                    <td>                       
                                        <span class ="file"><?php echo $link->fileName; ?></span>
                                    </td>
                                </tr>
                            <?php endif; ?>                    
                        </table>                           
                    </a>
                </li>                    
            <?php endif; ?>                    
        <?php endforeach; ?>                    
    <?php endif; ?>                    
</ul>
