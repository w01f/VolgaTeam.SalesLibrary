<?

	namespace application\models\trending;

	use application\models\wallbin\models\web\LibraryManager;

	/**
	 * Class TrendingManager
	 */
	class TrendingBarManager
	{
		/**
		 * @param TrendingSettings $trendingSettings
		 * @return TrendingLink[]
		 */
		public static function queryTrendingLinks($trendingSettings)
		{
			$trendingLinks = array();

			$today = date(\Yii::app()->params['mysqlDateFormat']);
			$startDate = $today;
			$endDate = date(\Yii::app()->params['mysqlDateFormat'], strtotime($today . ' + 1 days'));
			switch ($trendingSettings->dateRangeType)
			{
				case "today":
					$startDate = $today;
					break;
				case "week":
					$startDate = date(\Yii::app()->params['mysqlDateFormat'], strtotime("last Monday"));
					break;
				case "month":
					$startDate = date(\Yii::app()->params['mysqlDateFormat'], strtotime(date('Y-m-1')));
					break;
			}

			$queryFormats = array();
			foreach ($trendingSettings->linkFormats as $linkFormat)
				switch ($linkFormat)
				{
					case TrendingSettings::LinkFormatPowerPoint:
					case TrendingSettings::LinkFormatVideo:
					case TrendingSettings::LinkFormatPdf:
					case TrendingSettings::LinkFormatWord:
						$queryFormats[] = $linkFormat;
						break;
					case TrendingSettings::LinkFormatDocument:
						$queryFormats[] = implode(',', array(TrendingSettings::LinkFormatWord, TrendingSettings::LinkFormatPdf));
						break;
				}
			$formatsCondition = sprintf("%s", implode(",", $queryFormats));

			$librariesCondition = null;
			if (count($trendingSettings->libraries) > 0)
				$librariesCondition = sprintf("%s", implode(",", $trendingSettings->libraries));

			$command = \Yii::app()->db->createCommand("call sp_get_trending_links(:start_date,:end_date,:formats,:libraries,:max_links,:thumbnail_mode)");
			$command->bindValue(":start_date", $startDate, \PDO::PARAM_STR);
			$command->bindValue(":end_date", $endDate, \PDO::PARAM_STR);
			$command->bindValue(":formats", $formatsCondition, \PDO::PARAM_STR);
			$command->bindValue(":libraries", $librariesCondition, \PDO::PARAM_STR);
			$command->bindValue(":max_links", $trendingSettings->maxLinks, \PDO::PARAM_INT);
			$command->bindValue(":thumbnail_mode", $trendingSettings->thumbnailMode, \PDO::PARAM_STR);
			$resultRecords = $command->queryAll();

			$libraryManager = new LibraryManager();

			foreach ($resultRecords as $resultRecord)
			{
				$trendingLink = new TrendingLink();
				$trendingLink->linkId = $resultRecord['link_id'];
				$trendingLink->linkName = $resultRecord['link_name'];
				$trendingLink->format = $resultRecord['link_format'];
				$trendingLink->libraryName = $resultRecord['lib_name'];
				$trendingLink->viewsCount = $resultRecord['link_views'];

				$libraryId = $resultRecord['lib_id'];
				$library = $libraryManager->getLibraryById($libraryId);
				$thumbnailRelativePath = $resultRecord['thumbnail'];
				$trendingLink->thumbnail = \Utils::formatUrl($library->storageLink . '//' . $thumbnailRelativePath);

				$trendingLinks[] = $trendingLink;
			}

			return $trendingLinks;
		}
	}