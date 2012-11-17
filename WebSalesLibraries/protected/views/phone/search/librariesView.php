<?php if (isset($libraryGroups)): ?>
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
    <?php if (count($libraryGroups) > 1): ?>
        <?php foreach ($libraryGroups as $group): ?>
            <div class="search-libraries-group" data-role="collapsible" data-collapsed="true" data-inset="false">
                <h3>
                    <?php echo $group->name; ?>
                </h3>
                <fieldset data-role="controlgroup">
                    <?php foreach ($group->libraries as $library): ?>
                        <input class="search-libraries-item" type="checkbox" name="<?php echo $library->name; ?>" id="<?php echo $library->id; ?>" class="custom" <?php if ($library->selected) echo 'checked'; ?>/>
                        <label for="<?php echo $library->id; ?>"><?php echo $library->name; ?></label>
                    <?php endforeach; ?>
                </fieldset>
            </div>
        <?php endforeach; ?>
    <?php else: ?>
        <fieldset data-role="controlgroup">
            <?php foreach ($libraryGroups[0]->libraries as $library): ?>
                <input class="search-libraries-item" type="checkbox" name="<?php echo $library->name; ?>" id="<?php echo $library->id; ?>" class="custom" <?php if ($library->selected) echo 'checked'; ?>/>
                <label for="<?php echo $library->id; ?>"><?php echo $library->name; ?></label>
            <?php endforeach; ?>
        </fieldset>
    <?php endif; ?>
<?php endif; ?>