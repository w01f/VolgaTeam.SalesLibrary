<?php
	class SearchController extends IsdController
	{
		public function getViewPath()
		{
			return YiiBase::getPathOfAlias($this->pathPrefix . 'search');
		}

		public function actionGetSearchView()
		{
			$this->renderPartial('searchView', array(), false, true);
		}

		public function actionGetFileTypesView()
		{
			$this->renderPartial('fileTypesView', array(), false, true);
		}

		public function actionGetTagsView()
		{
			$categories = new CategoryManager();
			$categories->loadCategories();

			$this->renderPartial('tagsView', array('categories' => $categories), false, true);
		}

		public function actionGetDateView()
		{
			$this->renderPartial('dateView', array(), false, true);
		}

		public function actionGetLibrariesView()
		{
			$libraryManager = new LibraryManager();
			$libraries = $libraryManager->getLibraries();
			$libraryGroups = $libraryManager->getLibraryGroups();

			if (isset(Yii::app()->request->cookies['selectedLibraryIds']->value))
			{
				if (count($libraryGroups) > 1 || count($libraries) > 1)
					$checkedLibraryIds = CJSON::decode(Yii::app()->request->cookies['selectedLibraryIds']->value);
				else
					unset(Yii::app()->request->cookies['selectedLibraryIds']);
			}

			foreach ($libraryGroups as $libraryGroup)
				foreach ($libraryGroup->libraries as $library)
					if (isset($checkedLibraryIds))
						$library->selected = in_array($library->id, $checkedLibraryIds);
					else
						$library->selected = true;
			$this->renderPartial('librariesView', array('libraryGroups' => $libraryGroups), false, true);
		}

		public function actionSearchByContent()
		{
			$isClear = Yii::app()->request->getPost('isClear');
			if (!isset($isClear))
			{
				$fileTypes = Yii::app()->request->getPost('fileTypes');
				$condition = Yii::app()->request->getPost('condition');
				$startDate = Yii::app()->request->getPost('startDate');
				$endDate = Yii::app()->request->getPost('endDate');
				$dateFile = Yii::app()->request->getPost('dateFile');
				$onlyFileCards = intval(Yii::app()->request->getPost('onlyFileCards'));
				$isSort = intval(Yii::app()->request->getPost('isSort'));
				if (isset(Yii::app()->request->cookies['selectedLibraryIds']->value))
					$checkedLibraryIds = CJSON::decode(Yii::app()->request->cookies['selectedLibraryIds']->value);
				else
					$checkedLibraryIds = array();

				$categories = Yii::app()->request->getPost('categories');
				if (isset($categories))
					$categories = CJSON::decode($categories);
				$categoriesExactMatch = Yii::app()->request->getPost('categoriesExactMatch');
				if (!isset($categoriesExactMatch))
					$categoriesExactMatch = true;

				$hideDuplicated = Yii::app()->request->getPost('hideDuplicated');
				if (isset($hideDuplicated) && $hideDuplicated == 'true')
					$hideDuplicated = true;
				else
					$hideDuplicated = false;

				$onlyByName = Yii::app()->request->getPost('onlyByName');
				if (isset($onlyByName) && $onlyByName == 'true')
					$onlyByName = true;
				else
					$onlyByName = false;

				$onlyByContent = Yii::app()->request->getPost('onlyByContent');
				if (isset($onlyByContent) && $onlyByContent == 'true')
					$onlyByContent = true;
				else
					$onlyByContent = false;

				if (isset($fileTypes) && isset($condition) && isset($isSort))
					$links = LinkStorage::searchByContent($condition, $fileTypes, $startDate, $endDate, $dateFile, $checkedLibraryIds, $onlyFileCards, $categories, $categoriesExactMatch, $hideDuplicated,$onlyByName,$onlyByContent, $isSort);

				if (!isset($links))
					$links = null;

				if (isset($links))
					$searchInfo['count'] = 'Files: ' . count($links);
				else
					$searchInfo['count'] = 'No Files Meet your Criteria';
				if (isset($condition) && !($condition == '""' || $condition == ''))
					$searchInfo['condition'] = '<b>Keyword (' . (strstr($condition, '"') ? 'Exact match' : 'Partial match') . '):</b> ' . str_replace('"', '', $condition);
				if (isset($fileTypes))
					$searchInfo['file_types'] = '<b>File Types:</b> ' . implode(', ', $fileTypes);
				else
					$searchInfo['file_types'] = '<b>File Types:</b> None';
				if (isset($startDate) && isset($endDate))
					$searchInfo['dates'] = '<b>Dates:</b> ' . $startDate . ' - ' . $endDate;
				else
					$searchInfo['dates'] = '<b>Dates:</b> ALL';
				if (isset($categories))
				{
					foreach ($categories as $category)
						$groups[] = $category['category'];
					if (isset($groups))
					{
						$groups = array_unique($groups);
						foreach ($groups as $group)
						{
							foreach ($categories as $category)
								if ($category['category'] == $group)
									$tags[] = $category['tag'];
							if (isset($tags))
							{
								$categoryTags[] = '<b>' . $group . ':</b> ' . implode(', ', $tags);
								unset($tags);
							}
						}
						if (isset($categoryTags))
							$searchInfo['categories'] = implode('; ', $categoryTags);
					}
				}
				if (isset($checkedLibraryIds))
				{
					$allLibraryRecords = LibraryStorage::model()->findAll();
					foreach ($checkedLibraryIds as $libraryId)
					{
						$libraryRecord = LibraryStorage::model()->findByPk($libraryId);
						if (isset($libraryRecord))
							$libraries[] = $libraryRecord->name;
					}
					if (isset($libraries))
					{
						if (count($allLibraryRecords) == count($libraries))
							$searchInfo['libraries'] = '<b>Libraries:</b> ALL';
						else
							$searchInfo['libraries'] = '<b>Libraries:</b> ' . implode(", ", $libraries);
					}
					else
						$searchInfo['libraries'] = '<b>Libraries:</b> Not selected';
				}
				else
					$searchInfo['libraries'] = 'Libraries: Not selected';


				if (!isset($searchInfo))
					$searchInfo = null;
				StatisticActivityStorage::WriteActivity('Search', 'Run', array(
					'Condition' => array_key_exists('condition', $searchInfo) ? $searchInfo['condition'] : null,
					'Types' => str_replace('File Types:', '', $searchInfo['file_types']),
					'Dates' => str_replace('Dates:', '', $searchInfo['dates']),
					'Tags' => array_key_exists('categories', $searchInfo) ? $searchInfo['categories'] : null,
					'Libraries' => array_key_exists('libraries', $searchInfo) ? str_replace('Libraries:', '', $searchInfo['libraries']) : null,
					'Found' => isset($links) ? count($links) : null,
				));
				$this->renderPartial('searchResult', array('searchInfo' => $searchInfo, 'links' => $links), false, true);
			}
			else
				$this->renderPartial('searchResult', array(), false, true);
		}

		public function actionViewLink()
		{
			$rendered = false;
			$linkId = Yii::app()->request->getPost('linkId');
			if (isset($linkId))
			{
				$linkRecord = LinkStorage::getLinkById($linkId);
				if (isset($linkRecord))
				{
					$libraryManager = new LibraryManager();
					$library = $libraryManager->getLibraryById($linkRecord->id_library);
					$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
					if (Yii::app()->browser->isMobile())
					{
						$link->browser = 'mobile';
					}
					else
					{
						$browser = Yii::app()->browser->getBrowser();
						switch ($browser)
						{
							case 'Internet Explorer':
								$link->browser = 'ie';
								break;
							case 'Chrome':
							case 'Safari':
								$link->browser = 'webkit';
								break;
							case 'Firefox':
								$link->browser = 'firefox';
								break;
							case 'Opera':
								$link->browser = 'opera';
								break;
							default:
								$link->browser = 'webkit';
								break;
						}
					}
					$link->load($linkRecord);
					$this->renderPartial('application.views.regular.wallbin.viewDialog', array('link' => $link), false, true);
					$rendered = true;
				}
			}
			if (!$rendered)
				$this->renderPartial('empty', array(), false, true);
		}

	}
