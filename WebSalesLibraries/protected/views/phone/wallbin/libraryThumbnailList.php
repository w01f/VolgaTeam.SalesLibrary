<div data-role="collapsible-set" data-theme="b" data-content-theme="b" data-inset="false" class="ui-collapsible-set" data-collapsed-icon="arrow-r" data-expanded-icon="arrow-l" data-iconpos="right">
    <?php foreach ($libraryManager->getLibraries() as $library): ?>
        <div data-role="collapsible">
            <h4>
                <table class="library-item-container">
                    <tr>
                        <td>
                            <img src="<?php echo $library->logoPath; ?>">
                        </td>
                        <td>
                            <?php echo $library->name; ?>
                        </td>
                    </tr>
                </table>                        
            </h4>            
            <ul data-role="listview" class="ui-listview">
                <?php foreach ($library->pages as $page): ?>
                    <li data-corners="false" data-shadow="false" data-iconshadow="true" data-icon="arrow-r" data-iconpos="right" class="ui-btn ui-btn-up-d ui-btn-icon-right ui-li-has-arrow ui-li">
                        <div class="ui-btn-inner ui-li">
                            <div class="ui-btn-text">
                                <a href="#" class="ui-link-inherit">
                                    <?php echo $page->name; ?>
                                </a>
                            </div>
                            <span class="ui-icon ui-icon-arrow-r ui-icon-shadow">&nbsp;</span>
                        </div>
                    </li>
                <?php endforeach; ?>                    
            </ul>
        </div>                                
    <?php endforeach; ?>
</div>