<?php if (isset($libraries)): ?>
        <fieldset data-role="controlgroup">
            <?php foreach ($libraries as $library): ?>
                <input type="checkbox" name="<?php echo $library['name']; ?>" id="<?php echo $library['id']; ?>" class="custom" <?php if ($library['selected']) echo 'checked'; ?>/>
                <label for="<?php echo $library['id']; ?>"><?php echo $library['name']; ?></label>
            <?php endforeach; ?>
        </fieldset>
<?php endif; ?>
