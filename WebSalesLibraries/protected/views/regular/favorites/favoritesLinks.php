<?php
echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/linksGrid.php', array('links' => isset($links) ? $links : null), true);