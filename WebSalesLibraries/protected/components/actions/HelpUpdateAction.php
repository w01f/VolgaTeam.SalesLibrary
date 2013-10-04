<?php
	class HelpUpdateAction extends CAction
	{
		public function run()
		{
			echo "Job started...\n";

			HelpTabStorage::clearData();

			$rootFolderPath = Yii::app()->params['appRoot'] . DIRECTORY_SEPARATOR . Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Help';
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

							$tabHelpRecord = new HelpTabStorage();
							$tabHelpId = uniqid();
							$tabHelpRecord->id = $tabHelpId;
							$tabHelpRecord->name = trim($tabConfig->getElementsByTagName("Name")->item(0)->nodeValue);
							$tabHelpRecord->order = intval(trim($tabConfig->getElementsByTagName("Order")->item(0)->nodeValue));
							$tabHelpRecord->enabled = filter_var(trim($tabConfig->getElementsByTagName("Enabled")->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN);
							$tabHelpRecord->image_path = '/' . str_replace('\\', '/', str_replace($rootFolderPath, Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Help', $tabImageFile));
							$tabHelpRecord->save();

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
										$pageConfig = new DOMDocument();
										$pageConfig->load($pageConfigFile);

										$pageHelpRecord = new HelpPageStorage();
										$pageHelpId = uniqid();
										$pageHelpRecord->id = $pageHelpId;
										$pageHelpRecord->id_tab = $tabHelpId;
										$pageHelpRecord->name = trim($pageConfig->getElementsByTagName("Name")->item(0)->nodeValue);
										$pageHelpRecord->order = intval(trim($pageConfig->getElementsByTagName("Order")->item(0)->nodeValue));
										$pageHelpRecord->enabled = filter_var(trim($pageConfig->getElementsByTagName("Enabled")->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN);
										$pageHelpRecord->image_path = '/' . str_replace('\\', '/', str_replace($rootFolderPath, Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Help', $pageImageFile));
										$pageHelpRecord->save();


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
													$linkImageFile = realpath($linkPath . DIRECTORY_SEPARATOR . 'image.png');
													if (file_exists($linkConfigFile))
													{
														$linkConfig = new DOMDocument();
														$linkConfig->load($linkConfigFile);

														$linkHelpRecord = new HelpLinkStorage();
														$linkHelpId = uniqid();
														$linkHelpRecord->id = $linkHelpId;
														$linkHelpRecord->id_tab = $tabHelpId;
														$linkHelpRecord->id_page = $pageHelpId;
														$linkHelpRecord->name = trim($linkConfig->getElementsByTagName("Source")->item(0)->nodeValue);
														$linkHelpRecord->order = intval(trim($linkConfig->getElementsByTagName("Order")->item(0)->nodeValue));
														$linkHelpRecord->type = trim($linkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
														if ($linkHelpRecord->type != 'url')
															$linkHelpRecord->source_path = '/' . str_replace('\\', '/', str_replace($rootFolderPath, Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Help', $linkPath . DIRECTORY_SEPARATOR . trim($linkConfig->getElementsByTagName("Source")->item(0)->nodeValue)));
														else
															$linkHelpRecord->source_path = trim($linkConfig->getElementsByTagName("Source")->item(0)->nodeValue);
														$linkHelpRecord->enabled = true;
														$linkHelpRecord->image_path = '/' . str_replace('\\', '/', str_replace($rootFolderPath, Yii::app()->params['librariesRoot'] . DIRECTORY_SEPARATOR . 'Help', $linkImageFile));
														$linkHelpRecord->save();
													}
												}
											}
										}
										$schedulePath = realpath($pagePath . DIRECTORY_SEPARATOR . 'schedule.php');
										if (file_exists($schedulePath))
										{
											$linkHelpRecord = new HelpLinkStorage();
											$linkHelpId = uniqid();
											$linkHelpRecord->id = $linkHelpId;
											$linkHelpRecord->id_tab = $tabHelpId;
											$linkHelpRecord->id_page = $pageHelpId;
											$linkHelpRecord->name = "Calendar";
											$linkHelpRecord->order = 0;
											$linkHelpRecord->type = "calendar";
											$linkHelpRecord->source_path = $schedulePath;
											$linkHelpRecord->enabled = true;
											$linkHelpRecord->save();
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