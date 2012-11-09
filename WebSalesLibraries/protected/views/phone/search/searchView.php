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
    <table class="layout-group">
        <tr>
            <td class="on-left">Start Date:</td>
            <td class="on-right">
                <a id="search-clear-button" href="#" data-role="button" data-mini="true" data-icon="delete" data-inline="true" data-theme="b">Clear Dates</a>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="on-left">
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
    </table>
</div>  
<div id="search-file-type-container" data-role="collapsible" data-collapsed="true" data-inset="false">
    <h3>
        <span class="layout-group-title">File Types:</span>
    </h3>
    <fieldset data-role="controlgroup">
        <input type="checkbox" name="search-file-type-powerpoint" id="search-file-type-powerpoint" class="custom" />
        <label for="search-file-type-powerpoint">PowerPoint</label>
        <input type="checkbox" name="search-file-type-word" id="search-file-type-word" class="custom" />
        <label for="search-file-type-word">Word</label>    
        <input type="checkbox" name="search-file-type-excel" id="search-file-type-excel" class="custom" />
        <label for="search-file-type-excel">Excel</label>        
        <input type="checkbox" name="search-file-type-pdf" id="search-file-type-pdf" class="custom" />
        <label for="search-file-type-pdf">PDF</label>            
        <input type="checkbox" name="search-file-type-video" id="search-file-type-video" class="custom" />
        <label for="search-file-type-video">Video</label>                
    </fieldset>
</div>  
<?php
$libraryManager = new LibraryManager();
$libraryObjects = $libraryManager->getLibraries();

if (isset(Yii::app()->request->cookies['selectedLibraryIds']->value))
    $checkedLibraryIds = CJSON::decode(Yii::app()->request->cookies['selectedLibraryIds']->value);
foreach ($libraryObjects as $libraryObject)
{
    $library['id'] = $libraryObject->id;
    $library['name'] = $libraryObject->name;
    if (isset($checkedLibraryIds))
        $library['selected'] = in_array($libraryObject->id, $checkedLibraryIds);
    else
        $library['selected'] = true;
    $libraries[] = $library;
}
if (!isset($libraries))
    $libraries[] = 'All';
?>
<?php if (isset($libraries)): ?>
    <div id="search-libraries-container" data-role="collapsible" data-collapsed="true" data-inset="false">
        <h3><span class="layout-group-title">Stations:</span></h3>
        <fieldset data-role="controlgroup">
            <?php foreach ($libraries as $library): ?>
                <input type="checkbox" name="<?php echo $library['name']; ?>" id="<?php echo $library['id']; ?>" class="custom" <?php if ($library['selected']) echo 'checked'; ?>/>
                <label for="<?php echo $library['id']; ?>"><?php echo $library['name']; ?></label>
            <?php endforeach; ?>
        </fieldset>
    </div>  
<?php endif; ?>

