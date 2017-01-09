<?
	/** @var $folder FolderRecord */
	$links = $folder->getChildLinkIds();

	$lineBreaksCount = 0;
	foreach ($links as $link)
		if ($link->type == 6)
			$lineBreaksCount++;
	$linksCount = count($links) - $lineBreaksCount;
?>
<style>
    .prepare-link-cart-form .row {
        margin-bottom: 20px;
    }

    .prepare-link-cart-form .row.buttons {
        margin-bottom: 0;
    }

    .prepare-link-cart-form .header {
        display: table;
        width: 100%;
        margin-bottom: 20px;
    }

    .prepare-link-cart-form .header > div {
        display: table-cell;
        vertical-align: middle;
        width: 50%;
    }

    .prepare-link-cart-form .header .logo {
        text-align: left;
    }

    .prepare-link-cart-form .header .title {
        text-align: right;
    }

    .prepare-link-cart-form .header .logo img {
        height: 48px;
    }

    .prepare-link-cart-form .link-selectors {
        font-size: 1.4em;
    }

    .prepare-link-cart-form .links-list {
        height: 300px;
        background-color: #ffffff;
        border: 1px solid #cecece;
        padding-left: 10px;
        margin-top: 10px;
        margin-bottom: 30px;
        overflow-y: auto;
    }

    .prepare-link-cart-form .links-list .line-break {
        font-style: italic;
    }

    .prepare-link-cart-form .accept-button,
    .prepare-link-cart-form .cancel-button {
        width: 150px;
    }
</style>
<div class="prepare-link-cart-form logger-form" data-log-group="QBuilder" data-log-action="QBuilder Activity">
    <div class="row">
        <div class="col-xs-12">
            <div class="header">
                <div class="logo"><img src="<? echo Yii::app()->getBaseUrl(true); ?>/images/qpages/add-prepare.png">
                </div>
                <div class="title"><strong><? echo $folder->name; ?></strong></div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <span class="text-muted">Select the links and linebreaks from the window that you want to add to your QuickSites cart...</span>
        </div>
    </div>
    <div class="row link-selectors">
        <div class="col-xs-6 text-left">
			<? if ($linksCount > 0): ?>
                <div class="checkbox">
                    <label class="text-muted"><input id="prepare-link-cart-select-links" class="log-action"
                                                     type="checkbox" checked>
                        Links: <? echo $linksCount; ?>
                    </label>
                </div>
			<? endif; ?>
        </div>
        <div class="col-xs-6 text-right">
			<? if ($lineBreaksCount > 0): ?>
                <div class="checkbox">
                    <label class="text-muted"><input id="prepare-link-cart-select-linebreaks" class="log-action"
                                                     type="checkbox" checked>
                        Linebreaks: <? echo $lineBreaksCount; ?>
                    </label>
                </div>
			<? endif; ?>
        </div>
    </div>
    <div class="links-list">
        <ul class="nav nav-pills nav-stacked">
			<? foreach ($links as $link): ?>
				<?
				$itemClass = '';
				if ($link->type == 6)
					$itemClass .= 'line-break text-muted ';
				else
					$itemClass .= 'link ';
				?>
                <li>
                    <div class="checkbox link-item <? echo $itemClass; ?>">
                        <label> <input class="log-action" type="checkbox"
                                       value="<? echo $link->id; ?>"
                                       checked>
							<? echo sprintf('%s (%s)', $link->name, $link->original_format); ?>
                        </label>
                    </div>
                </li>
			<? endforeach; ?>
        </ul>
    </div>
    <div class="row buttons">
        <div class="col-xs-6 text-left">
            <button class="btn btn-default log-action accept-button" type="button">Add to Cart</button>
        </div>
        <div class="col-xs-6 text-right">
            <button class="btn btn-default log-action cancel-button" type="button">Cancel</button>
        </div>
    </div>
    <div class="service-data">
        <div class="folder-name"><? echo $folder->name; ?></div>
    </div>
</div>