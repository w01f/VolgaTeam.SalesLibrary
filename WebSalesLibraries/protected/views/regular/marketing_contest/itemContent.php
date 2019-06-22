<?

	use application\models\marketing_contest\models\Content;
	use application\models\marketing_contest\models\ItemEditModel;

	/**
	 * @var $item ItemEditModel
	 * @var $itemUrl string
	 * @var $isAdminRole boolean
	 */

	$marketList = \application\models\marketing_contest\models\Dictionaries::getMarketList();
	$strategiesList = \application\models\marketing_contest\models\Dictionaries::getStrategiesList();

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
            <div class="col col-xs-12 marketing-contest-item-content-additional-sections">
                <ul class="nav nav-tabs" style="margin-top: 20px;">
                    <li class="active" style="max-width: 150px;"><a data-toggle="tab" href="#marketing-contest-item-tab-info">Info</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="marketing-contest-item-tab-info">
                        <div class="row">
                            <div class="col col-md-6 col-sm-12">
                                <div class="form-group item-content-group">
                                    <label class="control-label" for="marketing-contest-item-contact-name">A. Primary Contact Name:</label>
                                    <div class="controls">
                                        <input id="marketing-contest-item-contact-name" type="text" class="form-control" data-role="text"
                                               value="<? echo $item->content->contactName; ?>" placeholder="type"
			                                   <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>
                                    </div>
                                </div>
                            </div>
                            <div class="col col-md-6 col-sm-12">
                                <div class="form-group item-content-group">
                                    <label class="control-label" for="marketing-contest-item-contact-email">B. Primary Contact Email:</label>
                                    <div class="controls">
                                        <input id="marketing-contest-item-contact-email" type="text" class="form-control" data-role="text"
                                               value="<? echo $item->content->contactEmail; ?>" placeholder="type"
				                               <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col col-md-6 col-sm-12">
                                <div class="form-group item-content-group">
                                    <div class="controls">
                                        <label class="control-label" for="marketing-contest-item-market">C. Media property / Market:</label>
				                        <? if ($item->allowEdit || $isAdminRole): ?>
                                            <div class="input-group popup-list-editor-container marketing-contest-item-market-container">
                                                <div class="service-data popup-list-content">
                                                    <img src="<? echo Yii::app()->baseUrl . '/images/marketing-contest/market-selector-logo.svg' ?>"
                                                         style="height: 100px;">
                                                    <div class="list-group"
                                                         style="height: 450px; overflow-y: auto !important;">
								                        <? foreach ($marketList as $strategyItem): ?>
                                                            <a href="#" class="list-group-item" data-value="<? echo $strategyItem; ?>"><? echo $strategyItem; ?></a>
								                        <? endforeach; ?>
                                                    </div>
                                                </div>
                                                <input type="text" id="marketing-contest-item-market"
                                                       class="form-control editor"
                                                       value="<? echo $item->content->market; ?>"
                                                       placeholder="select or type">
                                                <span role="button" class="input-group-addon popup-toggle"><span class="caret"></span></span>
                                            </div>
				                        <? else: ?>
                                            <input type="text" id="marketing-contest-item-market"
                                                   class="form-control"
                                                   value="<? echo $item->content->market; ?>"
                                                   disabled="disabled">
				                        <? endif; ?>
                                    </div>
                                </div>
                            </div>
                            <div class="col col-md-6 col-sm-12">
                                <div class="form-group item-content-group">
                                    <div class="controls">
                                        <label class="control-label" for="marketing-contest-item-strategy">D. Marketing Strategy:</label>
					                    <? if ($item->allowEdit || $isAdminRole): ?>
                                            <div class="input-group popup-list-editor-container marketing-contest-item-strategy-container">
                                                <div class="service-data popup-list-content">
                                                    <img src="<? echo Yii::app()->baseUrl . '/images/marketing-contest/strategy-selector-logo.svg' ?>"
                                                         style="height: 100px;">
                                                    <div class="list-group"
                                                         style="height: 450px; overflow-y: auto !important;">
									                    <? foreach ($strategiesList as $strategyItem): ?>
                                                            <a href="#" class="list-group-item" data-value="<? echo $strategyItem; ?>"><? echo $strategyItem; ?></a>
									                    <? endforeach; ?>
                                                    </div>
                                                </div>
                                                <input type="text" id="marketing-contest-item-strategy"
                                                       class="form-control editor"
                                                       value="<? echo $item->content->strategy; ?>"
                                                       placeholder="select or type">
                                                <span role="button" class="input-group-addon popup-toggle"><span class="caret"></span></span>
                                            </div>
					                    <? else: ?>
                                            <input type="text" id="marketing-contest-item-strategy"
                                                   class="form-control"
                                                   value="<? echo $item->content->strategy; ?>"
                                                   disabled="disabled">
					                    <? endif; ?>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col col-xs-12">
                                <div class="form-group item-content-group">
                                    <label class="control-label" for="marketing-contest-item-info">E. Basic Campaign Overview or important info:</label>
                                    <div class="controls">
                                        <textarea id="marketing-contest-item-info" rows="3"
                                                  class="form-control"
                                                  data-role="textarea" <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>><? echo $item->content->info; ?></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col col-xs-12">
                                <div class="panel panel-default marketing-contest-item-attachments" data-role="panel" style="margin-top: 60px;">
                                    <div class="panel-heading">F. Drag and drop the compressed MP4 video(s) here (h.264 codec)
                                    </div>
                                    <div class="panel-body" style="min-height: 150px;">
                                        <ul class="nav nav-pills dropzone<? if (!($item->allowEdit || $isAdminRole)): ?> disabled<? endif; ?>"
                                            id="marketing-contest-item-attachments-data" style="min-height: 170px;">
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
                </div>
            </div>
        </div>
    </form>
</div>