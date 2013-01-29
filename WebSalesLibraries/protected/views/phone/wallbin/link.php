<?php if (isset($link)): ?>
<li>
    <a class="<?php echo isset($link->folderContent) ? 'folder-content-link' : 'file-link'; ?>"
       href="#link<?php echo $link->id; ?>">
        <table class="link-container">
            <tr>
                <?php if ($link->enableFileCard || $link->enableAttachments): ?>
                <td rowspan="2" class="link-details-container">
                    <a class="file-link-detail" href="#link<?php echo $link->id; ?>">
                        <img src="<?php echo Yii::app()->baseUrl . '/images/search/expand.png'; ?>"/>
                    </a>
                </td>
                <?php endif; ?>
                <td>
                                    <span class="name">
                                        <?php
                                        if (isset($link->name) && $link->name != '')
                                            echo $link->name;
                                        else if (isset($link->fileName) && $link->fileName != '')
                                            echo $link->fileName;
                                        ?>
                                    </span>
                </td>
            </tr>
            <?php if (isset($link->name) && $link->name != '' && !isset($link->folderContent)): ?>
            <tr>
                <td>
                    <span class="file"><?php echo $link->fileName; ?></span>
                </td>
            </tr>
            <?php endif; ?>
        </table>
    </a>
</li>
<?php endif; ?>