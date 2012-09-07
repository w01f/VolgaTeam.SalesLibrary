<div class ="group-panel">
    <div class ="group-title">File Types</div>
    <div id ="file-types">
        <input type="checkbox" id="search-file-type-powerpoint" checked="checked"/><label for="search-file-type-powerpoint">Power Point</label>
        <input type="checkbox" id="search-file-type-word" checked="checked"/><label for="search-file-type-word">Word</label>
        <input type="checkbox" id="search-file-type-excel" checked="checked"/><label for="search-file-type-excel">Excel</label>
        <input type="checkbox" id="search-file-type-pdf" checked="checked"/><label for="search-file-type-pdf">PDF</label>
        <input type="checkbox" id="search-file-type-video" checked="checked"/><label for="search-file-type-video">Video</label>
    </div>
</div>

<div class ="group-panel">
    <div class ="group-title">Search Condition</div>
    <div id="condition-type">
        <ul>
            <li><a href="#condition-type-content">Content</a></li>
            <li><a href="#condition-type-date">Date</a></li>
            <li><a href="#condition-type-category">Category</a></li>
        </ul>
        <div id="condition-type-content">
            <div>What Are You Looking For?</div>      
            <input id="condition-content-value">
            <br>
            <br>
            <div id="content-compare-type">
                <input type="radio" id="content-compare-exact" name ="radio" checked="checked"/><label for="content-compare-exact">Exact Match</label>
                <input type="radio" id="content-compare-partial" name ="radio"/><label for="content-compare-partial">Partial Match</label>                
            </div>
        </div>
        <div id="condition-type-date"></div>
        <div id="condition-type-category"></div>
    </div>
</div>