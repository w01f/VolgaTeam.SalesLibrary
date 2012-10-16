<div id="search-control-panel">
    <ul>
        <li><a href="#search-options-basic">Search</a></li>
        <li><a href="#search-options-stations">Stations</a></li>
        <li><a href="#search-options-advanced">Advanced</a></li>
    </ul>
    <div id="search-options-basic">
        <div class ="group-panel">
            <div class ="group-title">What Are You Looking For?</div>
            <div class ="group-body">
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
        </div>
        <div class ="group-panel">
            <div class ="group-title">File Types:</div>
            <div class ="group-body">
                <table id ="file-types">
                    <tr>
                        <td><button class ="search-file-type btn" id="search-file-type-powerpoint"><i class="icon-search-powerpoint"/></button></td>
                        <td><button class ="search-file-type btn" id="search-file-type-word"><i class="icon-search-word"/></button></td>
                        <td><button class ="search-file-type btn" id="search-file-type-excel"><i class="icon-search-excel"/></button></td>
                        <td><button class ="search-file-type btn" id="search-file-type-pdf"><i class="icon-search-pdf"/></button></td>
                        <td><button class ="search-file-type btn" id="search-file-type-video"><i class="icon-search-video"/></button></td>
                    </tr>
                </table>
            </div>
        </div>        
        <div class ="group-panel" id="condition-date-panel">
            <div class ="group-title">Date Range:</div>
            <div class ="group-body">
                <table class="button-edit input-append" id="condition-date-range">
                    <tr>
                        <td class="editor"><input type="text" placeholder="Select Date Range..."></td>
                        <td class="buttons">
                            <a class ="btn" id ="select-date-range" href="#"><i class="icon-calendar"/></a>
                            <a class ="btn" id ="clear-date-range" href="#"><i class="icon-remove-sign"/></a>
                        </td>
                    </tr>
                </table>
            </div>            
        </div> 
        <div class ="group-panel" id ="search-links-number">
            <span></span>
        </div>                
    </div>
    <div id="search-options-stations">
        <div class ="group-panel">
            <table class ="library-checkbox-list-buttons">
                <tr>                
                    <td class ="left"><button type="button" class="btn btn-block" id="library-select-all">Select All</button></td>
                    <td class ="right"><button type="button" class="btn btn-block" id="library-clear-all">Clear All</button></td>
                </tr>
            </table>  
            <ul id="library-checkbox-list">
                <?php
                foreach ($libraries as $library)
                {
                    echo CHtml::openTag('li');
                    {
                        $btnClass = 'btn' . ($library['selected'] ? ' active' : '');
                        echo CHtml::openTag('button', array('class' => $btnClass, 'id' => $library['id']));
                        echo $library['name'];
                        echo CHtml::closeTag('button');
                    }
                    echo CHtml::closeTag('li');
                }
                ?>
            </ul>
        </div>                
    </div>
    <div id="search-options-advanced"></div>
</div>

