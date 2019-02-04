<?

	use application\models\sales_contest\models\Content;
	use application\models\sales_contest\models\ItemEditModel;

	/**
	 * @var $item ItemEditModel
	 * @var $itemUrl string
	 * @var $isAdminRole boolean
	 */

	$categoriesList = \application\models\sales_contest\models\Dictionaries::getCategoriesList();
	$marketList = \application\models\sales_contest\models\Dictionaries::getMarketList();

	$isSubmitted = !empty($item->dateSubmit);
?>
<div <? if ($item->allowEdit || $isAdminRole): ?>class="editable" <? else: ?>ondragover="return false;"
     ondrop="return false;"<? endif; ?>>
    <form class="form-horizontal">
        <div class="row" style="margin-top: 15px; margin-bottom: 20px;">
            <div class="col col-md-6">
                <span class="item-content-sub-text">ID: <? echo $item->title; ?></span>
            </div>
            <div class="col col-md-6 text-right">
				<? if ($isSubmitted): ?>
					<?
					/** @var $submittedByUser UserRecord */
					$submittedByUser = UserRecord::model()->findByPk($item->content->submittedByUserId);
					?>
                    <span class="item-content-sub-text">Submitted by <? echo isset($submittedByUser) ? $submittedByUser->login : ""; ?>
                        : <? echo date(\Yii::app()->params['outputDateFormat'], strtotime($item->dateSubmit)) . ' at ' . date(\Yii::app()->params['outputTimeFormat'], strtotime($item->dateSubmit)); ?></span>
                    <div class="service-data submit-data">
                        <div class="submitted-date"><? echo $item->dateSubmit; ?></div>
                        <div class="submitted-by"><? echo $item->content->submittedByUserId; ?></div>
                    </div>
				<? endif; ?>
            </div>
            <div class="col col-md-12" style="padding-top: 15px;">
                <a href="<? echo $itemUrl ?>" target="_blank" style="font-size: 0.9em;"><? echo $itemUrl ?></a>
            </div>
        </div>
        <div class="row">
            <div class="col col-xs-12 sales-contest-item-content-additional-sections">
                <ul class="nav nav-tabs" style="margin-top: 20px;">
                    <li class="active" style="max-width: 150px;"><a data-toggle="tab" href="#sales-contest-item-tab-info">Info</a></li>
                    <li><a data-toggle="tab" href="#sales-contest-item-tab-details">Details</a></li>
                    <li><a data-toggle="tab" href="#sales-contest-item-tab-strategy">Strategy</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="sales-contest-item-tab-info">
                        <div class="row">
                            <div class="col col-md-3 col-sm-12">
                                <div class="form-group item-content-group" style="margin-top: 20px;">
                                    <label class="control-label" for="sales-contest-item-revenue">A. revenue</label>
                                    <div class="controls">
                                        <div class="input-group">
                                            <span class="input-group-addon">$</span>
                                            <input id="sales-contest-item-revenue" type="text" class="form-control" data-role="text"
                                                   value="<? echo !empty($item->revenue) ? (float)$item->revenue : ''; ?>"
						                           <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col col-md-1 hidden-sm hidden-xs">
                            </div>
                            <div class="col col-md-8 col-sm-12 text-right">
                                <div class="form-group item-content-group" style="margin-top: 30px;">
                                    <label class="control-label"></label>
                                    <div class="controls">
                                        <label class="radio-inline sales-contest-item-nomination-type-label" for="sales-contest-item-nomination-type-1"><input type="radio"
                                                                                                                                                               name="sales-contest-item-nomination-type"
                                                                                                                                                               value="<? echo Content::NominationTypeShared; ?>"
                                                                                                                                                               class="sales-contest-item-nomination-type"
                                                                                                                                                               id="sales-contest-item-nomination-type-1"
					                            <? if ($item->content->nominationType === Content::NominationTypeShared): ?> checked<? endif; ?>
					                                                                                                                                           <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>Stolen/Adapted GSTV Idea</label>
                                        <label class="radio-inline sales-contest-item-nomination-type-label" for="sales-contest-item-nomination-type-2"><input type="radio"
                                                                                                                                                               value="<? echo Content::NominationTypeOriginal; ?>"
                                                                                                                                                               name="sales-contest-item-nomination-type"
                                                                                                                                                               class="sales-contest-item-nomination-type"
                                                                                                                                                               id="sales-contest-item-nomination-type-2"
					                            <? if ($item->content->nominationType === Content::NominationTypeOriginal): ?> checked<? endif; ?>
					                                                                                                                                           <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>Organic/Home-Grown Idea</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col col-md-6 col-sm-12">
                                <div class="form-group item-content-group">
                                    <label class="control-label" for="sales-contest-item-advertiser">B. client/advertiser</label>
                                    <div class="controls">
                                        <input id="sales-contest-item-advertiser" type="text" class="form-control" data-role="text"
                                               value="<? echo $item->advertiser; ?>" placeholder="type"
			                                   <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>
                                    </div>
                                </div>
                            </div>
                            <div class="col col-md-6 col-sm-12">
                                <div class="form-group item-content-group">
                                    <div class="controls">
                                        <label class="control-label" for="sales-contest-item-category">C. advertiser category</label>
			                            <? if ($item->allowEdit || $isAdminRole): ?>
                                            <div class="input-group popup-list-editor-container sales-contest-item-category-container">
                                                <div class="service-data popup-list-content">
                                                    <img src="<? echo Yii::app()->baseUrl . '/images/sales-contest/category-selector-logo.svg' ?>"
                                                         style="height: 100px;">
                                                    <div class="list-group"
                                                         style="height: 450px; overflow-y: auto !important;">
							                            <? foreach ($categoriesList as $categoryItem): ?>
                                                            <a href="#" class="list-group-item" data-value="<? echo $categoryItem; ?>"><? echo $categoryItem; ?></a>
							                            <? endforeach; ?>
                                                    </div>
                                                </div>
                                                <input type="text" id="sales-contest-item-category"
                                                       class="form-control editor"
                                                       value="<? echo $item->content->category; ?>"
                                                       placeholder="select or type">
                                                <span role="button" class="input-group-addon popup-toggle"><span class="caret"></span></span>
                                            </div>
			                            <? else: ?>
                                            <input type="text" id="sales-contest-item-category"
                                                   class="form-control"
                                                   value="<? echo $item->content->category; ?>"
                                                   disabled="disabled">
			                            <? endif; ?>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col col-md-6 col-sm-12">
                                <div class="form-group item-content-group">
                                    <label class="control-label" for="sales-contest-item-seller">D. seller</label>
                                    <div class="controls">
                                        <input id="sales-contest-item-seller" type="text" class="form-control" data-role="text"
                                               value="<? echo $item->content->seller; ?>"
			                                   <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>
                                    </div>
                                </div>
                            </div>
                            <div class="col col-md-6 col-sm-12">
                                <div class="form-group item-content-group">
                                    <label class="control-label" for="sales-contest-item-market">E. media property</label>
                                    <div class="controls">
			                            <? if ($item->allowEdit || $isAdminRole): ?>
                                            <div class="input-group popup-list-editor-container sales-contest-item-market-container">
                                                <div class="service-data popup-list-content">
                                                    <img src="<? echo Yii::app()->baseUrl . '/images/sales-contest/market-selector-logo.svg' ?>"
                                                         style="height: 100px;">
                                                    <div class="list-group"
                                                         style="height: 450px; overflow-y: auto !important;">
			                                            <? foreach ($marketList as $marketItem): ?>
                                                            <a href="#" class="list-group-item"
                                                               data-value="<? echo $marketItem; ?>"><? echo $marketItem; ?></a>
			                                            <? endforeach; ?>
                                                    </div>
                                                </div>
                                                <input type="text" id="sales-contest-item-market"
                                                       class="form-control editor"
                                                       value="<? echo $item->content->market; ?>"
                                                       placeholder="select or type">
                                                <span role="button" class="input-group-addon popup-toggle"><span class="caret"></span></span>
                                            </div>
			                            <? else: ?>
                                            <input type="text" id="sales-contest-item-market" class="form-control"
                                                   value="<? echo $item->content->market; ?>" disabled="disabled">
			                            <? endif; ?>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col col-xs-12">
                                <div class="panel panel-default sales-contest-item-attachments" data-role="panel" style="margin-top: 60px;">
                                    <div class="panel-heading">F. files (drag &amp; drop any presentations, videos, &amp; sales assets here)
                                    </div>
                                    <div class="panel-body" style="min-height: 170px;">
                                        <ul class="nav nav-pills dropzone<? if (!($item->allowEdit || $isAdminRole)): ?> disabled<? endif; ?>"
                                            id="sales-contest-item-attachments-data" style="min-height: 170px;">
						                    <? echo $this->renderPartial('itemFiles', array('files' => $item->attachments), true); ?>
                                        </ul>
                                        <div class="progress" style="position: relative; width: 100%; display: none;">
                                            <div class="progress-text text-center" style="width: 100%; position: absolute"><span class="file-name">Test</span>: <span class="progress-percent">90</span>%</div>
                                            <div class="progress-bar" style="width: 0; height: 20px;"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="sales-contest-item-tab-details">
                        <div class="row">
                            <div class="col col-xs-12">
                                <div class="form-group item-content-group" style="margin-top: 20px;">
                                    <label class="control-label">G. revenue type:</label>
                                    <div class="controls">
                                        <label class="radio-inline" for="sales-contest-item-revenue-type-1"><input type="radio"
                                                                                                                   value="<? echo Content::RevenueTypeNew; ?>"
                                                                                                                   name="sales-contest-item-revenue-type"
                                                                                                                   class="sales-contest-item-revenue-type"
                                                                                                                   id="sales-contest-item-revenue-type-1"
					                            <? if ($item->content->revenueType === Content::RevenueTypeNew): ?> checked<? endif; ?>
					                                                                                               <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>new</label>
                                        <label class="radio-inline" for="sales-contest-item-revenue-type-2"><input type="radio"
                                                                                                                   value="<? echo Content::RevenueTypeIncremental; ?>"
                                                                                                                   name="sales-contest-item-revenue-type"
                                                                                                                   class="sales-contest-item-revenue-type"
                                                                                                                   id="sales-contest-item-revenue-type-2"
					                            <? if ($item->content->revenueType === Content::RevenueTypeIncremental): ?> checked<? endif; ?>
					                                                                                               <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>incremental</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col col-md-6 col-sm-12">
                                <div class="form-group item-content-group">
                                    <label class="control-label"
                                           for="sales-contest-item-revenue-digital">H. digital revenue</label>
                                    <div class="controls">
                                        <div class="input-group">
                                            <span class="input-group-addon">$</span>
                                            <input id="sales-contest-item-revenue-digital" type="text"
                                                   class="form-control" data-role="text"
                                                   value="<? echo !empty($item->content->digitalRevenue) ? (float)$item->content->digitalRevenue : ''; ?>" <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col col-md-6 col-sm-12">
                                <div class="form-group item-content-group">
                                    <label class="control-label"
                                           for="sales-contest-item-revenue-media">I. tv revenue</label>
                                    <div class="controls">
                                        <div class="input-group">
                                            <span class="input-group-addon">$</span>
                                            <input id="sales-contest-item-revenue-media" type="text"
                                                   class="form-control" data-role="text"
                                                   value="<? echo !empty($item->content->mediaRevenue) ? (float)$item->content->mediaRevenue : ''; ?>" <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col col-md-6 col-sm-12">
                                <div class="form-group item-content-group">
                                    <label class="control-label"
                                           for="sales-contest-item-revenue-production">J. production revenue</label>
                                    <div class="controls">
                                        <div class="input-group">
                                            <span class="input-group-addon">$</span>
                                            <input id="sales-contest-item-revenue-production" type="text"
                                                   class="form-control" data-role="text"
                                                   value="<? echo !empty($item->content->productionRevenue) ? (float)$item->content->productionRevenue : ''; ?>" <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col col-md-6 col-sm-12">
                                <div class="form-group item-content-group">
                                    <label class="control-label"
                                           for="sales-contest-item-revenue-other">K. other revenue</label>
                                    <div class="controls">
                                        <div class="input-group">
                                            <span class="input-group-addon">$</span>
                                            <input id="sales-contest-item-revenue-other" type="text"
                                                   class="form-control" data-role="text"
                                                   value="<? echo !empty($item->content->otherRevenue) ? (float)$item->content->otherRevenue : ''; ?>" <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col col-xs-12">
                                <style>
                                    #content .sales-contest-main-page .item-content .item-content-group.platform-container .checkbox
                                    {
                                        margin-top: 20px;
                                    }
                                </style>
                                <div class="form-group item-content-group platform-container">
                                    <label class="control-label">L. digital platforms used:</label>
                                    <div class="controls" style="margin-left: 5px;">
                                        <div class="row">
                                            <div class="col col-md-3 col-sm-6 col-xs-12">
                                                <label class="checkbox"
                                                       for="sales-contest-item-details-platform-1"><input
                                                            type="checkbox"
                                                            value="<? echo Content::DigitalPlatformNetwork; ?>"
                                                            class="sales-contest-item-details-platform"
                                                            id="sales-contest-item-details-platform-1"
							                            <? if (in_array(Content::DigitalPlatformNetwork, $item->content->platforms)): ?> checked<? endif; ?> <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>run of network</label>
                                                <label class="checkbox"
                                                       for="sales-contest-item-details-platform-3"><input
                                                            type="checkbox"
                                                            value="<? echo Content::DigitalPlatformNativeAdvertorial; ?>"
                                                            class="sales-contest-item-details-platform"
                                                            id="sales-contest-item-details-platform-3"
			                                            <? if (in_array(Content::DigitalPlatformNativeAdvertorial, $item->content->platforms)): ?> checked<? endif; ?> <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>native advertorial</label>
                                            </div>
                                            <div class="col col-md-3 col-sm-6 col-xs-12">
                                                <label class="checkbox"
                                                       for="sales-contest-item-details-platform-4"><input
                                                            type="checkbox"
                                                            value="<? echo Content::DigitalPlatformMobile; ?>"
                                                            class="sales-contest-item-details-platform"
                                                            id="sales-contest-item-details-platform-4"
			                                            <? if (in_array(Content::DigitalPlatformMobile, $item->content->platforms)): ?> checked<? endif; ?> <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>mobile</label>
                                                <label class="checkbox"
                                                       for="sales-contest-item-details-platform-6"><input
                                                            type="checkbox"
                                                            value="<? echo Content::DigitalPlatformOtt; ?>"
                                                            class="sales-contest-item-details-platform"
                                                            id="sales-contest-item-details-platform-6"
			                                            <? if (in_array(Content::DigitalPlatformOtt, $item->content->platforms)): ?> checked<? endif; ?> <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>ott</label>
                                            </div>
                                            <div class="col col-md-3 col-sm-6 col-xs-12">
                                                <label class="checkbox"
                                                       for="sales-contest-item-details-platform-2"><input
                                                            type="checkbox"
                                                            value="<? echo Content::DigitalPlatformSite; ?>"
                                                            class="sales-contest-item-details-platform"
                                                            id="sales-contest-item-details-platform-2"
				                                        <? if (in_array(Content::DigitalPlatformSite, $item->content->platforms)): ?> checked<? endif; ?> <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>run of site</label>
                                                <label class="checkbox"
                                                       for="sales-contest-item-details-platform-7"><input
                                                            type="checkbox"
                                                            value="<? echo Content::DigitalPlatformAdvancedWeb; ?>"
                                                            class="sales-contest-item-details-platform"
                                                            id="sales-contest-item-details-platform-7"
				                                        <? if (in_array(Content::DigitalPlatformAdvancedWeb, $item->content->platforms)): ?> checked<? endif; ?> <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>advanced web</label>
                                            </div>
                                            <div class="col col-md-3 col-sm-6 col-xs-12">
                                                <label class="checkbox"
                                                       for="sales-contest-item-details-platform-5"><input
                                                            type="checkbox"
                                                            value="<? echo Content::DigitalPlatformGeoTargeting; ?>"
                                                            class="sales-contest-item-details-platform"
                                                            id="sales-contest-item-details-platform-5"
							                            <? if (in_array(Content::DigitalPlatformGeoTargeting, $item->content->platforms)): ?> checked<? endif; ?> <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>geo-targeting</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="sales-contest-item-tab-strategy">
                        <div class="row">
                            <div class="col col-xs-12">
                                <div class="form-group item-content-group" style="margin-top: 20px;">
                                    <label class="control-label" for="sales-contest-item-team-members">M. Did any other key team members contribute to this sale?</label>
                                    <div class="controls">
                                            <textarea id="sales-contest-item-team-members" rows="3"
                                                      class="form-control"
                                                      data-role="textarea" <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>><? echo $item->content->teamMembers; ?></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col col-xs-12">
                                <div class="form-group item-content-group">
                                    <label class="control-label" for="sales-contest-item-success-story">N. What is it about this success story or BIG Idea that makes it a great opportunity for other Gray Markets?</label>
                                    <div class="controls">
                                            <textarea id="sales-contest-item-success-story" rows="3"
                                                      class="form-control"
                                                      data-role="textarea" <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>><? echo $item->content->successStory; ?></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col col-xs-12">
                                <div class="form-group item-content-group">
                                    <label class="control-label" for="sales-contest-item-milestones">O. What made this a “Creative” sale? What key milestones did you accomplish to close this deal?</label>
                                    <div class="controls">
                                            <textarea id="sales-contest-item-milestones" rows="3"
                                                      class="form-control"
                                                      data-role="textarea" <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>><? echo $item->content->milestones; ?></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</div>