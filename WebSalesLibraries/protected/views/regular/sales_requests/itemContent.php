<?

	use application\models\sales_requests\models\ItemEditModel;

	/**
	 * @var $item ItemEditModel
	 * @var $itemUrl string
	 * @var $isAdminRole boolean
	 */

	$assignedToList = \application\models\sales_requests\models\Dictionaries::getAssignedList();
	$statusList = \application\models\sales_requests\models\Dictionaries::getStatusList();
	$categoriesList = \application\models\sales_requests\models\Dictionaries::getCategoriesList();
	$demosList = \application\models\sales_requests\models\Dictionaries::getDemoList();
	$reasonsList = \application\models\sales_requests\models\Dictionaries::getReasonsList();
?>
<div <? if ($item->allowEdit || $isAdminRole): ?>class="editable"<? else: ?>ondragover="return false;" ondrop="return false;"<? endif; ?>>
    <form class="form-horizontal">
        <div class="row" style="margin-top: 15px; margin-bottom: 20px;">
            <div class="col col-md-6">
                <span class="item-content-sub-text">Request ID: <? echo $item->title; ?></span>
            </div>
            <div class="col col-md-6">
				<? if (!empty($item->dateSubmit)): ?>
                    <?
                        /** @var $submittedByUser UserRecord */
                        $submittedByUser = UserRecord::model()->findByPk($item->content->submittedByUserId);
                    ?>
                    <span class="item-content-sub-text">Submitted by <? echo isset($submittedByUser) ? $submittedByUser->login : ""; ?>: <? echo date(\Yii::app()->params['outputDateFormat'], strtotime($item->dateSubmit)) . ' at ' . date(\Yii::app()->params['outputTimeFormat'], strtotime($item->dateSubmit)); ?></span>
                    <div class="service-data submit-data">
                        <div class="submitted-date"><? echo $item->dateSubmit; ?></div>
                        <div class="submitted-by"><? echo $item->content->submittedByUserId; ?></div>
                    </div>
				<? endif; ?>
            </div>
            <div class="col col-md-12" style="padding-top: 15px;">
                <a href="<? echo $itemUrl?>" target="_blank" style="font-size: 0.9em;"><? echo $itemUrl?></a>
            </div>
        </div>
        <div class="row">
            <div class="col col-md-4">
                <div class="form-group item-content-group">
                    <label class="control-label" for="sales-requests-item-assigned-to">Assigned to:</label>
                    <div class="controls">
	                    <? if ($isAdminRole): ?>
                            <select id="sales-requests-item-assigned-to" class="form-control">
                                <? foreach ($assignedToList as $assignedToItem): ?>
                                    <option <? if ($item->assignedTo === $assignedToItem): ?> selected<? endif; ?> value="<? echo $assignedToItem; ?>"><? echo $assignedToItem; ?></option>
                                <? endforeach; ?>
                            </select>
                        <? else: ?>
                            <input type="text" id="sales-requests-item-assigned-to" class="form-control"
                                   value="<? echo $item->assignedTo; ?>" disabled="disabled" placeholder="tbd...">
	                    <? endif; ?>
                    </div>
                </div>
            </div>
            <div class="col col-md-4">
                <div class="form-group item-content-group">
                    <label class="control-label" for="sales-requests-item-status">Status:</label>
                    <div class="controls">
	                    <? if ($isAdminRole): ?>
                            <select id="sales-requests-item-status" class="form-control">
			                    <? foreach ($statusList as $statusItem): ?>
                                    <option <? if ($item->status === $statusItem): ?> selected<? endif; ?> value="<? echo $statusItem; ?>"><? echo $statusItem; ?></option>
			                    <? endforeach; ?>
                            </select>
	                    <? else: ?>
                            <input type="text" id="sales-requests-item-status" class="form-control"
                                   value="<? echo $item->status; ?>" disabled="disabled" placeholder="tbd..">
	                    <? endif; ?>
                    </div>
                </div>
            </div>
            <div class="col col-md-4 col-sm-6">
                <div class="form-group item-content-group date-complete-container" <? if ($item->status !== "complete"): ?>style="display: none;" <? endif; ?>>
                    <label class="control-label" for="sales-requests-item-date-complete">Completed on:</label>
                    <div class="controls">
                        <div class="input-group sales-requests-item-date-complete-container">
                            <input id="sales-requests-item-date-complete" class="form-control log-action" type="text"
                                   placeholder="Select Date..."
                                   value="<? echo isset($item->dateCompleted) ? date('m/d/Y', strtotime($item->dateCompleted)) : date('m/d/Y',strtotime(date(\Yii::app()->params['mysqlDateTimeFormat']))); ?>"
                                   <? if (!$isAdminRole): ?>disabled="disabled"<? endif; ?>>
	                        <? if ($isAdminRole): ?>
                                <div class="input-group-btn">
                                    <button class="btn btn-default select-date-toggle log-action" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </div>
	                        <? endif; ?>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col col-md-12">
                <div style="border-bottom: 1px solid #ddd; margin-top: 10px; margin-bottom: 10px"></div>
            </div>
        </div>
        <div class="row">
            <div class="col col-md-4 col-sm-6 col-xs-12">
                <div class="form-group item-content-group">
                    <label class="control-label" for="sales-requests-item-date-needed">A. Date Needed</label>
                    <div class="controls">
                        <div class="input-group input-group sales-requests-item-date-needed-container">
                            <input id="sales-requests-item-date-needed" class="form-control log-action" type="text"
                                   placeholder="Select Date..."
                                   value="<? echo date('m/d/Y', strtotime($item->dateNeeded)); ?>"
                                   <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>
	                        <? if ($item->allowEdit || $isAdminRole): ?>
                                <div class="input-group-btn">
                                    <button class="btn btn-default select-date-toggle log-action" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </div>
	                        <? endif; ?>
                        </div>
                    </div>
                    <span class="item-content-sub-text">(allow 3 days for larger requests)</span>
                </div>
            </div>
            <div class="col col-md-2 col-sm-6 hidden-xs">
            </div>
            <div class="col col-md-6 col-sm-12">
                <div class="form-group item-content-group">
                    <label class="control-label" for="sales-requests-item-advertiser">B. Advertiser</label>
                    <div class="controls">
                        <input id="sales-requests-item-advertiser" type="text" class="form-control" data-role="text"
                               value="<? echo $item->content->advertiser; ?>" placeholder="type" <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col col-md-6">
                <div class="form-group item-content-group">
                    <label class="control-label" for="sales-requests-item-agency">C. Agency</label>
                    <div class="controls">
                        <input id="sales-requests-item-agency" type="text" class="form-control" data-role="text"
                               value="<? echo $item->content->agency; ?>" placeholder="type" <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>
                    </div>
                </div>
            </div>
            <div class="col col-md-6">
                <div class="form-group item-content-group">
                    <label class="control-label" for="sales-requests-item-category">D. Advertiser Category</label>
                    <div class="controls">
	                    <? if ($item->allowEdit || $isAdminRole): ?>
                            <div class="input-group dropdown sales-requests-item-category-container">
                                <input type="text" id="sales-requests-item-category"
                                       class="form-control dropdown-toggle"
                                       value="<? echo $item->content->category; ?>" placeholder="select or type">
                                <ul class="dropdown-menu">
	                                <? foreach ($categoriesList as $categoryItem): ?>
                                        <li><a href="#" data-value="<? echo $categoryItem; ?>"><? echo $categoryItem; ?></a></li>
				                    <? endforeach; ?>
                                </ul>
                                <span role="button" class="input-group-addon dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span class="caret"></span></span>
                            </div>
	                    <? else:?>
                            <input type="text" id="sales-requests-item-category" class="form-control"
                                   value="<? echo $item->content->category; ?>" disabled="disabled">
	                    <? endif; ?>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col col-md-6">
                <div class="form-group item-content-group">
                    <label class="control-label" for="sales-requests-item-meeting-with">E. Who are you meeting with?</label>
                    <div class="controls">
                        <input id="sales-requests-item-meeting-with" type="text" class="form-control" data-role="text"
                               value="<? echo $item->content->meetingWith; ?>" placeholder="type" <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>>
                    </div>
                </div>
            </div>
            <div class="col col-md-6">
                <div class="form-group item-content-group">
                    <label class="control-label" for="sales-requests-item-demos">F. Demo(s)</label>
                    <div class="controls">
	                    <? if ($item->allowEdit || $isAdminRole): ?>
                            <div class="input-group dropdown sales-requests-item-demos-container">
                                <input type="text" id="sales-requests-item-demos"
                                       class="form-control dropdown-toggle"
                                       value="<? echo $item->content->demos; ?>" placeholder="select or type">
                                <ul class="dropdown-menu">
                                    <? foreach ($demosList as $demoItem): ?>
                                        <li><a href="#" data-value="<? echo $demoItem; ?>"><? echo $demoItem; ?></a></li>
                                    <? endforeach; ?>
                                </ul>
                                <span role="button" class="input-group-addon dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span class="caret"></span></span>
                            </div>
                        <? else:?>
                            <input type="text" id="sales-requests-item-demos" class="form-control"
                                   value="<? echo $item->content->demos; ?>" disabled="disabled">
	                    <? endif; ?>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 30px;">
            <div class="col col-xs-12 sales-requests-item-content-additional-sections">
                <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="tab" href="#sales-requests-item-details">details</a></li>
                    <li><a data-toggle="tab" href="#sales-requests-item-attachments">attachments<span class="attachments-count"></span></a></li>
                    <li><a data-toggle="tab" href="#sales-requests-item-deliverables">deliverables<span class="deliverables-count"></span></a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="sales-requests-item-details">
                        <div class="row">
                            <div class="col col-xs-12">
                                <div class="form-group item-content-group">
                                    <label class="control-label" for="sales-requests-item-details-data" style="margin-bottom: 20px;margin-top: 10px;">G. What are the specific details of this request?</label>
                                    <div class="controls">
                                        <div class="row">
                                            <div class="col col-lg-5" style="padding-left: 0; margin-bottom: 30px;">
                                                <textarea id="sales-requests-item-details-data" rows="8"
                                                          class="form-control"
                                                          data-role="textarea" <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>><? echo $item->content->details; ?></textarea>
                                            </div>
                                            <div class="col col-lg-2 hidden-md hidden-sm hidden-xs">
                                            </div>
                                            <div class="col col-lg-5" style="padding-left: 0; margin-top: -10px;">
                                                <? $requestReasonItemIndex = 1;?>
                                                <? foreach ($reasonsList as $reason): ?>
                                                    <label class="checkbox-inline"
                                                           for="sales-requests-item-request-reason-<? echo $requestReasonItemIndex; ?>"><input
                                                                type="checkbox"
                                                                name="sales-requests-item-request-reason"
                                                                value="<? echo $reason; ?>"
                                                                class="sales-requests-item-request-reason"
                                                                id="sales-requests-item-request-reason-<? echo $requestReasonItemIndex; ?>"
                                                            <? if (in_array($reason, $item->content->requestReason)): ?> checked<? endif; ?>
                                                                <? if (!($item->allowEdit || $isAdminRole)): ?>disabled="disabled"<? endif; ?>><? echo $reason; ?>
                                                    </label>
                                                    <? $requestReasonItemIndex++; ?>
                                                <? endforeach; ?>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="sales-requests-item-attachments">
                        <div class="row">
                            <div class="col col-md-12">
                                <div class="form-group item-content-group">
                                    <label class="control-label" for="sales-requests-item-attachments-data" style="margin-bottom: 20px;margin-top: 10px;color: gray;">Drag and Drop files you need to share with your research & support team:</label>
                                    <div class="controls">
                                        <ul class="nav nav-pills dropzone<? if (!($item->allowEdit || $isAdminRole)): ?> disabled<? endif; ?>" id="sales-requests-item-attachments-data">
                                            <? echo $this->renderPartial('itemFiles', array('files' => $item->attachments), true); ?>
                                        </ul>
                                        <div class="progress" style="display: none;">
                                            <div class="progress-bar" style="width: 0;"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="sales-requests-item-deliverables">
                        <div class="row">
                            <div class="col col-md-12">
                                <div class="form-group item-content-group">
                                    <label class="control-label" for="sales-requests-item-deliverables-data" style="margin-bottom: 20px;margin-top: 10px;color: gray;">These are your files created by the research and support team. You can drag them to your desktop</label>
                                    <div class="controls">
                                        <ul class="nav nav-pills dropzone<? if (!$isAdminRole): ?> disabled<? endif; ?>" id="sales-requests-item-deliverables-data">
                                            <? echo $this->renderPartial('itemFiles', array('files' => $item->deliverables), true); ?>
                                        </ul>
                                        <div class="progress" style="display: none;">
                                            <div class="progress-bar" style="width: 0;"></div>
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