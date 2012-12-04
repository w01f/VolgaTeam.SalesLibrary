<?php if (isset($linkRecords)): ?>
    <ul class="help-links">
        <?php foreach ($linkRecords as $linkRecord): ?>
            <li>
                <div class="help-link">
                    <img src="<?php echo Yii::app()->baseUrl . $linkRecord->image_path ?>">
                    <div class ="service-data">
                        <div class ="link-id"><?php echo $linkRecord->id; ?></div>
                        <div class ="file-type"><?php echo $linkRecord->type == 'mp4' || $linkRecord->type == 'url' ? $linkRecord->type : 'other'; ?></div>
                        <div class ="view-type"><?php echo $linkRecord->type == 'mp4' || $linkRecord->type == 'url' ? $linkRecord->type : 'other'; ?></div>
                        <?php if ($linkRecord->type == 'mp4'): ?>                              
                            <div class ="links"><?php echo json_encode(array(array('src' => Yii::app()->baseUrl . $linkRecord->source_path, 'href' => Yii::app()->baseUrl . $linkRecord->source_path, 'title' => $linkRecord->name, 'type' => 'video/mp4', 'swf' => Yii::app()->baseUrl . '/vendor/video-js/video-js.swf'))); ?></div>
                        <?php elseif ($linkRecord->type == 'url'): ?>
                            <div class ="links"><?php echo json_encode(array(array('href' => $linkRecord->source_path))); ?></div>
                        <?php else: ?>
                            <div class ="links"><?php echo json_encode(array(array('href' => Yii::app()->baseUrl . $linkRecord->source_path))); ?></div>
                        <?php endif; ?>                            
                    </div>
                </div>
            </li>
        <?php endforeach; ?>
    </ul>
<?php endif; ?>
