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
    <div class ="group-title">Stations:</div>
    <div class ="group-body">
        <table id="library-selector">
            <tr>                
                <td id="library-selected-container">
                    <span>Selected:</span>
                    <span id="library-selected">All</span>
                </td>
                <td id="library-select-container"><input type="submit" id="library-select" value ="Select"/></td>
            </tr>
        </table>
    </div>
</div>
<div class ="group-panel">
    <div class ="group-title">Search By:</div>
    <div id="condition-type">
        <ul>
            <li><a href="#condition-type-content">Keyword</a></li>
            <li><a href="#condition-type-date">Date</a></li>
            <li><a href="#condition-type-category">Category</a></li>
        </ul>
        <div id="condition-type-content">
            <div>What Are You Looking For?</div>      
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
        <div id="condition-type-date"></div>
        <div id="condition-type-category"></div>
    </div>
</div>