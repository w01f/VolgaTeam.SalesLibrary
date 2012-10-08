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
                <input id="condition-content-value">
                <br>
                <br>
                <table id="content-compare-type">
                    <tr>                
                        <td><input type="radio" id="content-compare-exact" name ="radio" /><label for="content-compare-exact">Exact Match</label></td>
                        <td><input type="radio" id="content-compare-partial" name ="radio"/><label for="content-compare-partial">Partial Match</label></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class ="group-panel">
            <div class ="group-title">File Types:</div>
            <div class ="group-body">
                <table id ="file-types">
                    <tr>
                        <td><input type="checkbox" class ="search-file-type" id="search-file-type-powerpoint" name="checkbox"/><label for="search-file-type-powerpoint">Power Point</label></td>
                        <td><input type="checkbox" class ="search-file-type" id="search-file-type-word" name="checkbox" /><label for="search-file-type-word">Word</label></td>
                        <td><input type="checkbox" class ="search-file-type" id="search-file-type-excel" name="checkbox" /><label for="search-file-type-excel">Excel</label></td>
                        <td><input type="checkbox" class ="search-file-type" id="search-file-type-pdf" name="checkbox" /><label for="search-file-type-pdf">PDF</label></td>
                        <td><input type="checkbox" class ="search-file-type" id="search-file-type-video" name="checkbox" /><label for="search-file-type-video">Video</label></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class ="group-panel">
            <div class ="group-title">Date Range:</div>
<!--            <div class ="group-body">
                <div id="condition-date-value" data-date="12-02-2012" data-date-format="dd-mm-yyyy">
                    <input class="span2" size="16" type="text" value="12-02-2012" readonly="">
                    <span class="add-on"><i class="icon-calendar"></i></span>
                </div>
            </div>-->
        </div>            
    </div>
    <div id="search-options-stations">
        <div class ="group-panel">
            <table class ="library-checkbox-list-buttons">
                <tr>                
                    <td class ="left"><input type="submit" id="library-select-all" value ="Select All"/></td>
                    <td class ="right"><input type="submit" id="library-clear-all" value ="Clear All"/></td>
                </tr>
            </table>  
            <ul id="library-checkbox-list">
                <?php
                foreach ($libraries as $library)
                {
                    echo CHtml::openTag('li');
                    echo CHtml::checkBox($library['id'], $library['selected']);
                    echo CHtml::label($library['name'], $library['id']);
                    echo CHtml::closeTag('li');
                }
                ?>
            </ul>
        </div>                
    </div>
    <div id="search-options-advanced"></div>
</div>