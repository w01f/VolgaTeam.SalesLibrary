<input type="search" name="search" id="search-keyword" value="" />
<table class="layout-group" id="search-match-selector">
    <tr>
        <td class="on-center">
            <div id="search-match-selector" data-role="navbar">
                <ul>
                    <li><a href="#" id="search-match-exact" data-corners="true" data-shadow="true">Exact</a></li>
                    <li><a href="#" id="search-match-partial" data-corners="true" data-shadow="true">Partial</a></li>
                </ul>
            </div>        
        </td>
    </tr>        
    <tr>
        <td class="on-center">
            <input type="checkbox" name="search-only-filecards" id="search-only-filecards" class="custom" data-mini="true"/>
            <label for="search-only-filecards">Show Me the Money!</label>
        </td>
    </tr>            
</table>
<br>
<div id="search-date-container"  data-role="collapsible" data-collapsed="true" data-inset="false">
    <h3><span class="layout-group-title">Dates: Not set</span></h3>
    <fieldset data-role="controlgroup"  data-mini="true">
        <input type="radio" name="radio-choice" id="search-date-file" value="choice-1"/>
        <label for="search-date-file">Search by Date File was Created</label>
        <input type="radio" name="radio-choice" id="search-date-link" value="choice-2"/>
        <label for="search-date-link">Search by Date File was Uploaded</label>
    </fieldset>
    <table class="layout-group">
        <tr>
            <td class="on-left">Start Date:</td>
        </tr>
        <tr>
            <td class="on-left">
                <input id="search-date-start" name="search-date-start" type="text" value="" data-mini="true"/>
            </td>
        </tr>        
        <tr>
            <td colspan="2" class="on-left">End Date:</td>
        </tr>                
        <tr>
            <td colspan="2" class="on-left">
                <input id="search-date-end" name="search-date-end" type="text" value="" data-mini="true"/>
            </td>
        </tr>                        
        <tr>
            <td colspan="2" class="on-center">
                <a id="search-clear-date-button" href="#" data-role="button" data-mini="true" data-icon="delete" data-inline="true" data-theme="b">Clear Dates</a>
            </td>
        </tr>                        
    </table>
</div>