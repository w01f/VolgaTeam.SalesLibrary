<?php if (isset($libraries)): ?>
    <table class="layout-group">
        <tr>
            <td class="on-left">
                <a id="search-libraries-select-button" href="#" data-role="button" data-mini="true" data-icon="check" data-inline="true" data-theme="b">Select All</a>
            </td>
            <td class="on-right">
                <a id="search-libraries-clear-button" href="#" data-role="button" data-mini="true" data-icon="delete" data-inline="true" data-theme="b">Clear All</a>
            </td>
        </tr>
    </table>
    <div id="search-libraries-container" >
        <fieldset data-role="controlgroup">
            <?php foreach ($libraries as $library): ?>
                <input type="checkbox" name="<?php echo $library['name']; ?>" id="<?php echo $library['id']; ?>" class="custom" <?php if ($library['selected']) echo 'checked'; ?>/>
                <label for="<?php echo $library['id']; ?>"><?php echo $library['name']; ?></label>
            <?php endforeach; ?>
        </fieldset>
    </div >
<?php endif; ?>
