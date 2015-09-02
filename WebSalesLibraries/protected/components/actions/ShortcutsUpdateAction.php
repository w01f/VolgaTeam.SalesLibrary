<?php

	/**
	 * Class ShortcutsUpdateAction
	 */
	class ShortcutsUpdateAction extends CAction
	{
		public function run()
		{
			echo "Job started...\n";

			ShortcutGroupRecord::clearData();
			ShortcutLinkRecord::clearData();

			$rootFolderPath = ShortcutGroupRecord::getShortcutsRoot();
			if (file_exists($rootFolderPath))
			{
				/** @var $rootFolder DirectoryIterator[] */
				$rootFolder = new DirectoryIterator($rootFolderPath);
				foreach ($rootFolder as $groupFolder)
				{
					if ($groupFolder->isDir() && !$groupFolder->isDot())
					{
						$groupPath = $groupFolder->getPathname();
						$groupConfigFile = realpath($groupPath . DIRECTORY_SEPARATOR . 'config.xml');
						if (file_exists($groupConfigFile))
						{
							$groupConfigContent = file_get_contents($groupConfigFile);
							$groupConfig = new DOMDocument();
							$groupConfig->loadXML($groupConfigContent);

							$groupRecord = new ShortcutGroupRecord();
							$groupId = uniqid();
							$groupRecord->id = $groupId;
							$groupRecord->order = intval(trim($groupConfig->getElementsByTagName("Order")->item(0)->nodeValue));
							$groupRecord->source_path = $groupPath;
							$groupRecord->config = $groupConfigContent;
							$groupRecord->save();

							$linksRootPath = realpath($groupPath . DIRECTORY_SEPARATOR . 'Items');
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

											$linkShortcutsRecord = new ShortcutLinkRecord();
											$shortcutsIdTags = $linkConfig->getElementsByTagName("StaticID");
											$linkShortcutsId = $shortcutsIdTags->length > 0 ? trim($shortcutsIdTags->item(0)->nodeValue) : null;
											if (!isset($linkShortcutsId))
												$linkShortcutsId = uniqid();
											$linkShortcutsRecord->id = $linkShortcutsId;
											$linkShortcutsRecord->id_group = $groupId;
											$linkShortcutsRecord->type = trim($linkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
											$linkShortcutsRecord->order = intval(trim($linkConfig->getElementsByTagName("Order")->item(0)->nodeValue));
											$linkShortcutsRecord->source_path = $linkPath;
											$linkShortcutsRecord->config = $linkConfigContent;
											$linkShortcutsRecord->save();

											$subLinksRootPath = realpath($linkPath . DIRECTORY_SEPARATOR . 'Items');
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

															$subLinkShortcutsRecord = new ShortcutLinkRecord();
															$shortcutsIdTags = $subLinkConfig->getElementsByTagName("StaticID");
															$subLinkShortcutsId = $shortcutsIdTags->length > 0 ? trim($shortcutsIdTags->item(0)->nodeValue) : null;
															if (!isset($subLinkShortcutsId))
																$subLinkShortcutsId = uniqid();
															$subLinkShortcutsRecord->id = $subLinkShortcutsId;
															$subLinkShortcutsRecord->id_group = $groupId;
															$subLinkShortcutsRecord->id_parent = $linkShortcutsId;
															$subLinkShortcutsRecord->type = trim($subLinkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
															$subLinkShortcutsRecord->order = intval(trim($subLinkConfig->getElementsByTagName("Order")->item(0)->nodeValue));
															$subLinkShortcutsRecord->source_path = $subLinkPath;
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
				echo "Job completed...\n";
			}
		}
	}