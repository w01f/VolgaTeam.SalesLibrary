<div id="search-control-panel">
    <ul>
        <li><a href="#search-options-basic">Search</a></li>
        <li><a href="#search-options-files">File Type</a></li>        
        <li><a href="#search-options-tags">Tags</a></li>        
        <li><a href="#search-options-stations"><?php echo Yii::app()->params['stations']['tab_name']; ?></a></li>
    </ul>
    <div id="search-options-basic">
        <div class ="group-panel">
            <button type="button" class="btn btn-block" id="clear-content-dates-value">Clear All Search Settings</button>
            <br>
            <br>
            <div class ="group-title">What Are You Looking For?</div>
            <table class="button-edit input-append">
                <tr>
                    <td class="editor"><input type="text" id="condition-content-value" placeholder="Type Here..."></td>
                    <td class="buttons">
                        <a class ="btn" id ="clear-content-value" href="#"><i class="icon-remove-sign"/></a>
                    </td>
                </tr>
            </table>                
            <div class="btn-group" id="content-compare-type">
                <button type="button" class="btn" id="content-compare-exact">Exact Match</button>
                <button type="button" class="btn" id="content-compare-partial">Partial Match</button>
            </div>
        </div>
        <br>
        <br>        
        <div class ="group-panel" id="condition-date-panel">
            <div class ="group-title">Date Range:</div>
            <table class="button-edit input-append" id="condition-date-range">
                <tr>
                    <td class="editor"><input type="text" placeholder="Select Date Range..."></td>
                    <td class="buttons">
                        <a class ="btn" id ="select-date-range" href="#"><i class="icon-calendar"/></a>
                        <a class ="btn" id ="clear-date-range" href="#"><i class="icon-remove-sign"/></a>
                    </td>
                </tr>
            </table>
            <button type="button" class="btn btn-block" id="condition-date-file">Date file was created</button>
            <button type="button" class="btn btn-block" id="condition-date-link">Date file was uploaded</button>
        </div> 
    </div>
    <div id="search-options-files">
        <div class ="group-panel">
            <table id ="file-types">
                <tr>
                    <td>
                        <button class ="search-file-type btn btn-block" id="search-file-type-powerpoint">
                            <table class="caption">
                                <tr>
                                    <td><img class="icon-search powerpoint" src="images/search/search-powerpoint.png"/></td>
                                    <td><h3>PowerPoint</h3></td>
                                </tr>
                            </table>
                        </button>
                    </td>
                </tr>                        
                <tr>                        
                    <td>
                        <button class ="search-file-type btn btn-block" id="search-file-type-video">
                            <table class="caption">
                                <tr>
                                    <td><img class="icon-search video" src="images/search/search-video.png"/></td>
                                    <td><h3>Video</h3></td>
                                </tr>
                            </table>
                        </button>
                    </td>
                </tr>                
                <tr>                        
                    <td>
                        <button class ="search-file-type btn btn-block" id="search-file-type-pdf">
                            <table class="caption">
                                <tr>
                                    <td><img class="icon-search video" src="images/search/search-pdf.png"/></td>
                                    <td><h3>PDF</h3></td>
                                </tr>
                            </table>                            
                        </button>
                    </td>
                </tr>                        
                <tr>
                    <td>
                        <button class ="search-file-type btn btn-block" id="search-file-type-word">
                            <table class="caption">
                                <tr>
                                    <td><img class="icon-search word" src="images/search/search-word.png"/></td>
                                    <td><h3>Word</h3></td>
                                </tr>
                            </table>
                        </button>
                    </td>
                </tr>                        
                <tr>                        
                    <td>
                        <button class ="search-file-type btn btn-block" id="search-file-type-excel">
                            <table class="caption">
                                <tr>
                                    <td><img class="icon-search excel" src="images/search/search-excel.png"/></td>
                                    <td><h3>Excel</h3></td>
                                </tr>
                            </table>                            
                        </button>
                    </td>
                </tr>                                        
            </table>
        </div>                
    </div>
    <div id="search-options-tags">
        <?php if (isset($categories->groups)): ?>
            <div class ="group-panel">
                <button type="button" class="btn btn-block" id="tags-clear-all">Clear All Tags</button>        
                <div class="btn-group" id="tags-compare-type">
                    <button type="button" class="btn" id="tags-compare-exact">Exact Match</button>
                    <button type="button" class="btn" id="tags-compare-partial">Partial Match</button>
                </div>
            </div>
            <div id="categories-container">
                <div class="accordion" id="categories">
                    <?php foreach ($categories->groups as $group): ?>
                        <h3><span><?php echo $group; ?></span></h3>
                        <div>
                            <?php foreach ($categories->getTagsByGroup($group) as $tag): ?>
                                <label class="checkbox">
                                    <input type="checkbox" value="<?php echo $group . '------' . $tag; ?>">
                                    <?php echo $tag; ?>
                                </label>                        
                            <?php endforeach; ?>
                        </div>
                    <?php endforeach; ?>
                </div>                    
            </div>                                                
        <?php endif; ?>
    </div>    
    <div id="search-options-stations">
        <div class ="group-panel">
            <button type="button" class="btn btn-block" id="library-select-all">Select All <?php echo Yii::app()->params['stations']['tab_name']; ?></button>
            <button type="button" class="btn btn-block" id="library-clear-all">Clear All <?php echo Yii::app()->params['stations']['tab_name']; ?></button>
        </div>                
        <div id="libraries-container">
            <div class="accordion" id="libraries">
                <?php foreach ($libraryGroups as $group): ?>
                    <h3><span><?php echo $group->name; ?></span></h3>
                    <div>
                        <?php foreach ($group->libraries as $library): ?>
                            <label class="checkbox">
                                <input type="checkbox" value="<?php echo $library->id; ?>" <?php echo $library->selected ? 'checked="checked"' : '' ?>>
                                <?php echo $library->name; ?>
                            </label>                        
                        <?php endforeach; ?>
                    </div>
                <?php endforeach; ?>
            </div>                    
        </div>                                                        
    </div>
</div>

