<table id="tool-dialog">
    <tr>
        <td colspan="2">
            <legend>Add to Favorites</legend>
        </td>
    </tr>
    <tr>
        <td class="title">
            <label class="control-label">Name:</label>
        </td>
        <td>
            <input type="text" id="favorites-link-name" value="<?php echo $link->name; ?>">
        </td>
    </tr>
    <tr>
        <td class="title">
            <label class="control-label">Folder:</label>
        </td>
        <td>
            <div class="input-append btn-group dropdown">
                <input type="text" id="favorites-folder-name" class="span2" value="" style="width: 270px;">
                <button class="btn dropdown-toggle <?php if (!isset($folders)): ?>disabled<?php endif; ?>"
                        data-toggle="dropdown">
                    <span class="caret"></span>
                </button>
                <ul class="dropdown-menu">
                    <?php if (isset($folders)): ?>
                        <?php foreach ($folders as $folder): ?>
                            <li><a href="#"><?php echo $folder; ?></a></a></li>
                        <?php endforeach; ?>
                    <?php endif; ?>
                </ul>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="buttons-area">
            <button class="btn" id="accept-button" type="button">OK</button>
            <button class="btn" id="cancel-button" type="button">Cancel</button>
        </td>
    </tr>
</table>