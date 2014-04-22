<?php

	class ShortcutsUpdateAction extends CAction
	{
		public function run()
		{
			echo "Job started...\n";

			ShortcutsTabStorage::clearData();

			$rootFolderPath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Shortcuts';
			if (file_exists($rootFolderPath))
			{
				$rootFolder = new DirectoryIterator($rootFolderPath);
				foreach ($rootFolder as $tabFolder)
				{
					if ($tabFolder->isDir() && !$tabFolder->isDot())
					{
						$tabPath = $tabFolder->getPathname();
						$tabConfigFile = realpath($tabPath . DIRECTORY_SEPARATOR . 'config.xml');
						$tabImageFile = realpath($tabPath . DIRECTORY_SEPARATOR . 'rbnlogo.png');
						if (file_exists($tabConfigFile))
						{
							$tabConfig = new DOMDocument();
							$tabConfig->load($tabConfigFile);

							$tabShortcutsRecord = new ShortcutsTabStorage();
							$tabShortcutsId = uniqid();
							$tabShortcutsRecord->id = $tabShortcutsId;
							$tabShortcutsRecord->name = trim($tabConfig->getElementsByTagName("Name")->item(0)->nodeValue);
							$tabShortcutsRecord->order = intval(trim($tabConfig->getElementsByTagName("Order")->item(0)->nodeValue));
							$tabShortcutsRecord->enabled = filter_var(trim($tabConfig->getElementsByTagName("Enabled")->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN);
							$tabShortcutsRecord->image_path = '/' . str_replace('\\', '/', str_replace($rootFolderPath, Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Shortcuts', $tabImageFile));
							$tabShortcutsRecord->save();

							$pagesRootPath = realpath($tabPath . DIRECTORY_SEPARATOR . 'pages');
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

										$pageShortcutsRecord = new ShortcutsPageStorage();
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

														$linkShortcutsRecord = new ShortcutsLinkStorage();
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