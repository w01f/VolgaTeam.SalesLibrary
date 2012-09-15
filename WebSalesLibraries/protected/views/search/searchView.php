<table id ="search-container">
    <tr>
        <td id ="right-navbar"><div><?php $this->widget('application.components.widgets.SearchControlPanel', array()); ?></div></td>
        <td id ="search-result"><div><?php $this->renderPartial('searchResult', array('links' => null), false, true); ?></div></td>
    </tr>
</table>
