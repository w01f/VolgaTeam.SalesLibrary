<?

	use application\models\star_steals\models\ItemEditModel;
	use application\models\star_steals\models\Content;
	use application\models\star_steals\models\DetailsData;

	/**
	 * @var $item ItemEditModel
	 */

	$marketListFilePath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'starsteal_markets.txt';
	if (file_exists($marketListFilePath))
		$marketList = file($marketListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);

	$stationsListFilePath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'starsteal_stations.txt';
	if (file_exists($stationsListFilePath))
		$stationsList = file($stationsListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);

	$categoriesListFilePath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . 'starsteal_categories.txt';
	if (file_exists($categoriesListFilePath))
		$categoriesList = file($categoriesListFilePath, FILE_IGNORE_NEW_LINES | FILE_SKIP_EMPTY_LINES);
?>
<div>
    <form class="form-horizontal">
        <div class="row">
            <div class="col col-md-12">
                <div class="form-group  item-content-group" style="border-bottom: 1px solid #ddd">
                    <h3>Gray Stars &amp; Steals!</h3>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col col-md-12">
                <div class="form-group item-content-group">
                    <label class="control-label">this sale is a:</label>
                    <div class="controls">
                        <label class="radio-inline" for="star-steals-item-sale-type-1"><input type="radio"
                                                                                              name="star-steals-item-sale-type"
                                                                                              value="<? echo Content::SaleTypeStar; ?>"
                                                                                              class="star-steals-item-sale-type"
                                                                                              id="star-steals-item-sale-type-1"
								<? if ($item->content->saleType === Content::SaleTypeStar): ?> checked<? endif; ?>>star</label>
                        <label class="radio-inline" for="star-steals-item-sale-type-2"><input type="radio"
                                                                                              value="<? echo Content::SaleTypeSteal; ?>"
                                                                                              name="star-steals-item-sale-type"
                                                                                              class="star-steals-item-sale-type"
                                                                                              id="star-steals-item-sale-type-2"
								<? if ($item->content->saleType === Content::SaleTypeSteal): ?> checked<? endif; ?>>steal</label>
                        <label class="radio-inline" for="star-steals-item-sale-type-3"><input type="radio"
                                                                                              value="<? echo Content::SaleTypeIdea; ?>"
                                                                                              name="star-steals-item-sale-type"
                                                                                              class="star-steals-item-sale-type"
                                                                                              id="star-steals-item-sale-type-3"
								<? if ($item->content->saleType === Content::SaleTypeIdea): ?> checked<? endif; ?>>original
                            idea</label>
                        <label class="radio-inline" for="star-steals-item-sale-type-4"><input type="radio"
                                                                                              value="<? echo Content::SaleTypeWeekStar; ?>"
                                                                                              name="star-steals-item-sale-type"
                                                                                              class="star-steals-item-sale-type"
                                                                                              id="star-steals-item-sale-type-4"
								<? if ($item->content->saleType === Content::SaleTypeWeekStar): ?> checked<? endif; ?>>star
                            of the week</label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col col-md-6">
                <div class="form-group item-content-group">
                    <label class="control-label" for="star-steals-item-revenue">A. revenue</label>
                    <div class="controls">
                        <div class="input-group">
                            <span class="input-group-addon">$</span>
                            <input id="star-steals-item-revenue" type="text" class="form-control" data-role="text"
                                   value="<? echo $item->content->revenue; ?>">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col col-md-6">
                <div class="form-group item-content-group">
                    <label class="control-label" for="star-steals-item-client">B. client</label>
                    <div class="controls">
                        <input id="star-steals-item-client" type="text" class="form-control" data-role="text"
                               value="<? echo $item->content->client; ?>">
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col col-md-6">
                <div class="form-group item-content-group">
                    <label class="control-label" for="star-steals-item-seller">C. seller</label>
                    <div class="controls">
                        <input id="star-steals-item-seller" type="text" class="form-control" data-role="text"
                               value="<? echo $item->content->seller; ?>">
                    </div>
                </div>
            </div>
            <div class="col col-md-3 col-sm-6">
                <div class="form-group item-content-group">
                    <label class="control-label" for="star-steals-item-closed-date">D. date closed</label>
                    <div class="controls">
                        <div class="input-group input-group-sm star-steals-item-closed-date-container">
                            <input id="star-steals-item-closed-date" class="form-control log-action" type="text"
                                   placeholder="Select Date..." value="<? echo $item->content->closedDate; ?>">
                            <div class="input-group-btn">
                                <button class="btn btn-default select-date-toggle log-action" type="button">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col col-md-6">
                <div class="form-group item-content-group">
                    <label class="control-label" for="star-steals-item-market">E. market</label>
                    <div class="controls">
                        <select id="star-steals-item-market" class="form-control">
							<? foreach ($marketList as $marketItem): ?>
                                <option <? if ($item->content->market === $marketItem): ?> selected<? endif; ?>><? echo $marketItem; ?></option>
							<? endforeach; ?>
                        </select>
                    </div>
                </div>
            </div>
            <div class="col col-md-3 col-sm-6">
                <div class="form-group item-content-group">
                    <label class="control-label" for="star-steals-item-station">F. station</label>
                    <div class="controls">
                        <select id="star-steals-item-station" class="form-control">
							<? foreach ($stationsList as $stationItem): ?>
                                <option <? if ($item->content->station === $stationItem): ?> selected<? endif; ?>><? echo $stationItem; ?></option>
							<? endforeach; ?>
                        </select>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col col-md-12">
                <div class="form-group item-content-group">
                    <label class="control-label">G. revenue type:</label>
                    <div class="controls">
                        <label class="radio-inline" for="star-steals-item-revenue-type-1"><input type="radio"
                                                                                                 value="<? echo Content::RevenueTypeNew; ?>"
                                                                                                 name="star-steals-item-revenue-type"
                                                                                                 class="star-steals-item-revenue-type"
                                                                                                 id="star-steals-item-revenue-type-1"
								<? if ($item->content->revenueType === Content::RevenueTypeNew): ?> checked<? endif; ?>>new</label>
                        <label class="radio-inline" for="star-steals-item-revenue-type-2"><input type="radio"
                                                                                                 value="<? echo Content::RevenueTypeIncremental; ?>"
                                                                                                 name="star-steals-item-revenue-type"
                                                                                                 class="star-steals-item-revenue-type"
                                                                                                 id="star-steals-item-revenue-type-2"
								<? if ($item->content->revenueType === Content::RevenueTypeIncremental): ?> checked<? endif; ?>>incremental</label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col col-md-12">
                <div class="panel panel-default star-steals-item-files" data-role="panel">
                    <div class="panel-heading">H. files (drag &amp; drop any presentations, videos, &amp; sales assets
                        here)
                    </div>
                    <div class="panel-body" style="min-height: 100px;"></div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col col-md-12">
                <div class="row" data-role="tab">
                    <ul class="nav nav-tabs">
                        <li class="active"><a data-toggle="tab" href="#star-steals-item-details">Details</a></li>
                        <li><a data-toggle="tab" href="#star-steals-item-team">Team</a></li>
                        <li><a data-toggle="tab" href="#star-steals-item-success">Success</a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" id="star-steals-item-details">
                            <div class="row">
                                <div class="col col-md-6">
                                    <div class="row">
                                        <div class="col col-md-12">
                                            <div class="form-group item-content-group">
                                                <label class="control-label" for="star-steals-item-details-category">I.
                                                    advertiser category</label>
                                                <div class="controls">
                                                    <select id="star-steals-item-details-category" class="form-control">
														<? foreach ($categoriesList as $categoryItem): ?>
                                                            <option <? if ($item->content->details->advertiserCategory === $categoryItem): ?> selected<? endif; ?>><? echo $categoryItem; ?></option>
														<? endforeach; ?>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col col-md-12">
                                            <div class="form-group item-content-group">
                                                <label class="control-label"
                                                       for="star-steals-item-details-revenue-digital">J. digital
                                                    revenue</label>
                                                <div class="controls">
                                                    <div class="input-group">
                                                        <span class="input-group-addon">$</span>
                                                        <input id="star-steals-item-details-revenue-digital" type="text"
                                                               class="form-control" data-role="text"
                                                               value="<? echo $item->content->details->digitalRevenue; ?>">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col col-md-12">
                                            <div class="form-group item-content-group">
                                                <label class="control-label"
                                                       for="star-steals-item-details-revenue-media">K. tv
                                                    revenue</label>
                                                <div class="controls">
                                                    <div class="input-group">
                                                        <span class="input-group-addon">$</span>
                                                        <input id="star-steals-item-details-revenue-media" type="text"
                                                               class="form-control" data-role="text"
                                                               value="<? echo $item->content->details->mediaRevenue; ?>">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col col-md-6">
                                    <div class="row">
                                        <div class="col col-md-12">
                                            <div class="form-group item-content-group">
                                                <label class="control-label">L. digital platforms used:</label>
                                                <div class="controls" style="margin-left: 35px;">
                                                    <label class="checkbox"
                                                           for="star-steals-item-details-platform-1"><input
                                                                type="checkbox"
                                                                value="<? echo DetailsData::DigitalPlatformNetwork; ?>"
                                                                class="star-steals-item-details-platform"
                                                                id="star-steals-item-details-platform-1"
															<? if (in_array(DetailsData::DigitalPlatformNetwork, $item->content->details->digitalPlatforms)): ?> checked<? endif; ?>>run
                                                        of network</label>
                                                    <label class="checkbox"
                                                           for="star-steals-item-details-platform-2"><input
                                                                type="checkbox"
                                                                value="<? echo DetailsData::DigitalPlatformSite; ?>"
                                                                class="star-steals-item-details-platform"
                                                                id="star-steals-item-details-platform-2"
															<? if (in_array(DetailsData::DigitalPlatformSite, $item->content->details->digitalPlatforms)): ?> checked<? endif; ?>>run
                                                        of site</label>
                                                    <label class="checkbox"
                                                           for="star-steals-item-details-platform-3"><input
                                                                type="checkbox"
                                                                value="<? echo DetailsData::DigitalPlatformNativeAdvertorial; ?>"
                                                                class="star-steals-item-details-platform"
                                                                id="star-steals-item-details-platform-3"
															<? if (in_array(DetailsData::DigitalPlatformNativeAdvertorial, $item->content->details->digitalPlatforms)): ?> checked<? endif; ?>>native
                                                        advertorial</label>
                                                    <label class="checkbox"
                                                           for="star-steals-item-details-platform-4"><input
                                                                type="checkbox"
                                                                value="<? echo DetailsData::DigitalPlatformMobile; ?>"
                                                                class="star-steals-item-details-platform"
                                                                id="star-steals-item-details-platform-4"
															<? if (in_array(DetailsData::DigitalPlatformMobile, $item->content->details->digitalPlatforms)): ?> checked<? endif; ?>>mobile</label>
                                                    <label class="checkbox"
                                                           for="star-steals-item-details-platform-5"><input
                                                                type="checkbox"
                                                                value="<? echo DetailsData::DigitalPlatformGeoTargeting; ?>"
                                                                class="star-steals-item-details-platform"
                                                                id="star-steals-item-details-platform-5"
															<? if (in_array(DetailsData::DigitalPlatformGeoTargeting, $item->content->details->digitalPlatforms)): ?> checked<? endif; ?>>geo-targeting</label>
                                                    <label class="checkbox"
                                                           for="star-steals-item-details-platform-6"><input
                                                                type="checkbox"
                                                                value="<? echo DetailsData::DigitalPlatformOtt; ?>"
                                                                class="star-steals-item-details-platform"
                                                                id="star-steals-item-details-platform-6"
															<? if (in_array(DetailsData::DigitalPlatformOtt, $item->content->details->digitalPlatforms)): ?> checked<? endif; ?>>ott</label>
                                                    <label class="checkbox"
                                                           for="star-steals-item-details-platform-7"><input
                                                                type="checkbox"
                                                                value="<? echo DetailsData::DigitalPlatformAdvancedWeb; ?>"
                                                                class="star-steals-item-details-platform"
                                                                id="star-steals-item-details-platform-7"
															<? if (in_array(DetailsData::DigitalPlatformAdvancedWeb, $item->content->details->digitalPlatforms)): ?> checked<? endif; ?>>advanced
                                                        web</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="star-steals-item-team">
                            <div class="row">
                                <div class="col col-md-12">
                                    <div class="row">
                                        <div class="col col-md-12">
                                            <div class="form-group item-content-group">
                                                <label class="control-label brdbot">M. Who else contributed to this
                                                    sale?</label>
                                                <div class="controls">
                                                    <label for="star-steals-item-team-item-1"
                                                           class="sr-only"></label><input
                                                            id="star-steals-item-team-item-1" type="text"
                                                            class="form-control star-steals-item-team-item"
                                                            data-role="text"
                                                            value="<? echo count($item->content->teamMates) > 0 ? $item->content->teamMates[0] : ""; ?>">
                                                    <label for="star-steals-item-team-item-2"
                                                           class="sr-only"></label><input
                                                            id="star-steals-item-team-item-2" type="text"
                                                            class="form-control star-steals-item-team-item"
                                                            data-role="text"
                                                            value="<? echo count($item->content->teamMates) > 1 ? $item->content->teamMates[1] : ""; ?>">
                                                    <label for="star-steals-item-team-item-3"
                                                           class="sr-only"></label><input
                                                            id="star-steals-item-team-item-3" type="text"
                                                            class="form-control star-steals-item-team-item"
                                                            data-role="text"
                                                            value="<? echo count($item->content->teamMates) > 2 ? $item->content->teamMates[2] : ""; ?>">
                                                    <label for="star-steals-item-team-item-4"
                                                           class="sr-only"></label><input
                                                            id="star-steals-item-team-item-4" type="text"
                                                            class="form-control star-steals-item-team-item"
                                                            data-role="text"
                                                            value="<? echo count($item->content->teamMates) > 3 ? $item->content->teamMates[3] : ""; ?>">
                                                    <label for="star-steals-item-team-item-5"
                                                           class="sr-only"></label><input
                                                            id="star-steals-item-team-item-5" type="text"
                                                            class="form-control star-steals-item-team-item"
                                                            data-role="text"
                                                            value="<? echo count($item->content->teamMates) > 4 ? $item->content->teamMates[4] : ""; ?>">
                                                    <label for="star-steals-item-team-item-6"
                                                           class="sr-only"></label><input
                                                            id="star-steals-item-team-item-6" type="text"
                                                            class="form-control star-steals-item-team-item"
                                                            data-role="text"
                                                            value="<? echo count($item->content->teamMates) > 5 ? $item->content->teamMates[5] : ""; ?>">
                                                    <label for="star-steals-item-team-item-7"
                                                           class="sr-only"></label><input
                                                            id="star-steals-item-team-item-7" type="text"
                                                            class="form-control star-steals-item-team-item"
                                                            data-role="text"
                                                            value="<? echo count($item->content->teamMates) > 6 ? $item->content->teamMates[6] : ""; ?>">
                                                    <label for="star-steals-item-team-item-8"
                                                           class="sr-only"></label><input
                                                            id="star-steals-item-team-item-8" type="text"
                                                            class="form-control star-steals-item-team-item"
                                                            data-role="text"
                                                            value="<? echo count($item->content->teamMates) > 7 ? $item->content->teamMates[7] : ""; ?>">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="star-steals-item-success">
                            <div class="row">
                                <div class="col col-md-12">
                                    <div class="row">
                                        <div class="col col-md-12">
                                            <div class="form-group item-content-group">
                                                <label class="control-label" for="star-steals-item-success-why">N.
                                                    Explain why this sales success can be easily exported to other
                                                    markets:</label>
                                                <div class="controls">
                                                    <textarea id="star-steals-item-success-why" rows="3"
                                                              class="form-control"
                                                              data-role="textarea"><? echo $item->content->success->why; ?></textarea>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col col-md-12">
                                            <div class="form-group item-content-group">
                                                <label class="control-label" for="star-steals-item-success-what">O. What
                                                    made this a "Creative" Sale? What are the key milestones that helped
                                                    advance the sales process with this client?</label>
                                                <div class="controls">
                                                    <textarea id="star-steals-item-success-what" rows="3"
                                                              class="form-control"
                                                              data-role="textarea"><? echo $item->content->success->what; ?></textarea>
                                                </div>
                                            </div>
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