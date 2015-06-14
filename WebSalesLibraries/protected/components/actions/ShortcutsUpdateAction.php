<?php

	/**
	 * Class ShortcutsUpdateAction
	 */
	class ShortcutsUpdateAction extends CAction
	{
		public function run()
		{
			echo "Job started...\n";

			ShortcutsTabRecord::clearData();

			$rootFolderPath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Shortcuts';
			if (file_exists($rootFolderPath))
			{
				/** @var $rootFolder DirectoryIterator[] */
				$rootFolder = new DirectoryIterator($rootFolderPath);
				foreach ($rootFolder as $tabFolder)
				{
					if ($tabFolder->isDir() && !$tabFolder->isDot())
					{
						$tabPath = $tabFolder->getPathname();
						$tabConfigFile = realpath($tabPath . DIRECTORY_SEPARATOR . 'config.xml');
						$tabImageFile = realpath($tabPath);
						if (file_exists($tabConfigFile))
						{
							$tabConfigContent = file_get_contents($tabConfigFile);
							$tabConfig = new DOMDocument();
							$tabConfig->loadXML($tabConfigContent);

							$tabShortcutsRecord = new ShortcutsTabRecord();
							$tabShortcutsId = uniqid();
							$tabShortcutsRecord->id = $tabShortcutsId;
							$tabShortcutsRecord->name = trim($tabConfig->getElementsByTagName("Name")->item(0)->nodeValue);
							$tabShortcutsRecord->order = intval(trim($tabConfig->getElementsByTagName("Order")->item(0)->nodeValue));
							$tabShortcutsRecord->enabled = filter_var(trim($tabConfig->getElementsByTagName("Enabled")->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN);
							$tabShortcutsRecord->image_path = '/' . str_replace('\\', '/', str_replace($rootFolderPath, Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Shortcuts', $tabImageFile));
							$tabShortcutsRecord->config = $tabConfigContent;
							$tabShortcutsRecord->save();

							$pagesRootPath = realpath($tabPath . DIRECTORY_SEPARATOR . 'pages');
							/** @var $pagesRoot DirectoryIterator[] */
							$pagesRoot = new DirectoryIterator($pagesRootPath);
							foreach ($pagesRoot as $pageFolder)
							{
								if ($pageFolder->isDir() && !$pageFolder->isDot())
								{
									$pagePath = $pageFolder->getPathname();
									$pageConfigFile = realpath($pagePath . DIRECTORY_SEPARATOR . 'config.xml');
									$pageImageFile = realpath($pagePath . DIRECTORY_SEPARATOR . 'image.png');
									if (file_exists($pageConfigFile))
									{
										$pageConfigContent = file_get_contents($pageConfigFile);
										$pageConfig = new DOMDocument();
										$pageConfig->loadXML($pageConfigContent);

										$pageShortcutsRecord = new ShortcutsPageRecord();
										$pageShortcutsId = uniqid();
										$pageShortcutsRecord->id = $pageShortcutsId;
										$pageShortcutsRecord->id_tab = $tabShortcutsId;
										$pageShortcutsRecord->name = trim($pageConfig->getElementsByTagName("Name")->item(0)->nodeValue);
										$pageShortcutsRecord->order = intval(trim($pageConfig->getElementsByTagName("Order")->item(0)->nodeValue));
										$pageShortcutsRecord->enabled = filter_var(trim($pageConfig->getElementsByTagName("Enabled")->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN);
										$pageShortcutsRecord->image_path = '/' . str_replace('\\', '/', str_replace($rootFolderPath, Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Shortcuts', $pageImageFile));
										$pageShortcutsRecord->source_path = '/' . str_replace('\\', '/', str_replace($rootFolderPath, Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Shortcuts', $pagePath));
										$pageShortcutsRecord->config = $pageConfigContent;
										$pageShortcutsRecord->save();

										$linksRootPath = realpath($pagePath . DIRECTORY_SEPARATOR . 'links');
										if (file_exists($linksRootPath))
										{
											/** @var $linksRoot DirectoryIterator[] */
											$linksRoot = new DirectoryIterator($linksRootPath);
											foreach ($linksRoot as $linkFolder)
											{
												if ($linkFolder->isDir() && !$linkFolder->isDot())
												{
													$linkPath = $linkFolder->getPathname();
													$linkConfigFile = realpath($linkPath . DIRECTORY_SEPARATOR . 'config.xml');
													if (file_exists($linkConfigFile))
													{
														$linkConfigContent = file_get_contents($linkConfigFile);
														$linkConfig = new DOMDocument();
														$linkConfig->loadXML($linkConfigContent);

														$linkShortcutsRecord = new ShortcutsLinkRecord();
														$shortcutsIdTags = $linkConfig->getElementsByTagName("StaticID");
														$linkShortcutsId = $shortcutsIdTags->length > 0 ? trim($shortcutsIdTags->item(0)->nodeValue) : null;
														if (!isset($linkShortcutsId))
															$linkShortcutsId = uniqid();
														$linkShortcutsRecord->id = $linkShortcutsId;
														$linkShortcutsRecord->id_tab = $tabShortcutsId;
														$linkShortcutsRecord->id_page = $pageShortcutsId;
														$linkShortcutsRecord->order = intval(trim($linkConfig->getElementsByTagName("Order")->item(0)->nodeValue));
														$linkShortcutsRecord->type = trim($linkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
														$linkShortcutsRecord->source_path = '/' . str_replace('\\', '/', str_replace($rootFolderPath, Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Shortcuts', $linkPath));
														$linkShortcutsRecord->image_path = '/' . str_replace('\\', '/', str_replace($rootFolderPath, Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Shortcuts', realpath($linkPath . DIRECTORY_SEPARATOR . $linkShortcutsRecord->order . '.png')));
														$linkShortcutsRecord->config = $linkConfigContent;
														$linkShortcutsRecord->save();

														$subLinksRootPath = realpath($linkPath . DIRECTORY_SEPARATOR . 'links');
														if (file_exists($subLinksRootPath))
														{
															/** @var $subLinksRoot DirectoryIterator[] */
															$subLinksRoot = new DirectoryIterator($subLinksRootPath);
															foreach ($subLinksRoot as $subLinkFolder)
															{
																if ($subLinkFolder->isDir() && !$subLinkFolder->isDot())
																{
																	$subLinkPath = $subLinkFolder->getPathname();
																	$subLinkConfigFile = realpath($subLinkPath . DIRECTORY_SEPARATOR . 'config.xml');
																	if (file_exists($subLinkConfigFile))
																	{
																		$subLinkConfigContent = file_get_contents($subLinkConfigFile);
																		$subLinkConfig = new DOMDocument();
																		$subLinkConfig->loadXML($subLinkConfigContent);

																		$subLinkShortcutsRecord = new ShortcutsLinkRecord();
																		$shortcutsIdTags = $subLinkConfig->getElementsByTagName("StaticID");
																		$subLinkShortcutsId = $shortcutsIdTags->length > 0 ? trim($shortcutsIdTags->item(0)->nodeValue) : null;
																		if (!isset($subLinkShortcutsId))
																			$subLinkShortcutsId = uniqid();
																		$subLinkShortcutsRecord->id = $subLinkShortcutsId;
																		$subLinkShortcutsRecord->id_tab = $tabShortcutsId;
																		$subLinkShortcutsRecord->id_group = $linkShortcutsId;
																		$subLinkShortcutsRecord->order = intval(trim($subLinkConfig->getElementsByTagName("Order")->item(0)->nodeValue));
																		$subLinkShortcutsRecord->type = trim($subLinkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
																		$subLinkShortcutsRecord->source_path = '/' . str_replace('\\', '/', str_replace($rootFolderPath, Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Shortcuts', $subLinkPath));
																		$subLinkShortcutsRecord->image_path = '/' . str_replace('\\', '/', str_replace($rootFolderPath, Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Shortcuts', realpath($subLinkPath . DIRECTORY_SEPARATOR . $subLinkShortcutsRecord->order . '.png')));
																		$subLinkShortcutsRecord->config = $subLinkConfigContent;
																		$subLinkShortcutsRecord->save();
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			echo "Job completed...\n";
		}
	}