<?php

	/**
	 * Class NotifyDeadLinksAction
	 */
	class NotifyDeadLinksAction extends CAction
	{
		public function run()
		{
			echo "Job started...\n";

			$libraryRecords = LibraryRecord::model()->findAll();
			foreach ($libraryRecords as $libraryRecord)
			{
				if (isset($deadLinks))
					unset($deadLinks);
				if (isset($notConvertedVideos))
					unset($notConvertedVideos);

				$deadLinkRecords = LinkRecord::model()->findAll('is_dead=1 and id_library=?', array($libraryRecord->id));
				foreach ($deadLinkRecords as $deadLinkRecord)
					$deadLinks[] = ' - ' . $deadLinkRecord->name . ' (' . basename($deadLinkRecord->file_relative_path) . ')';

				$deadAttachmentRecords = AttachmentRecord::model()->findAll('is_dead=1 and id_library=?', array($libraryRecord->id));
				foreach ($deadAttachmentRecords as $deadAttachmentRecord)
					$deadLinks[] = ' - ' . $deadAttachmentRecord->name . ' (' . basename($deadAttachmentRecord->path) . ')';

				$videoLinkRecords = LinkRecord::model()->findAll('is_preview_not_ready=1 and id_library=?', array($libraryRecord->id));
				foreach ($videoLinkRecords as $videoLinkRecord)
					$notConvertedVideos[] = ' - ' . $videoLinkRecord->name . ' (' . $videoLinkRecord->file_name . ')';

				$videoAttachmentRecords = AttachmentRecord::model()->findAll('is_preview_not_ready=1 and id_library=?', array($libraryRecord->id));
				foreach ($videoAttachmentRecords as $videoAttachmentRecord)
					$notConvertedVideos[] = ' - ' . $videoAttachmentRecord->name . ' (' . basename($videoAttachmentRecord->path) . ')';

				if (isset($deadLinks) || isset($notConvertedVideos))
				{
					/** @var $libraryConfigRecord LibraryConfigRecord */
					$libraryConfigRecord = LibraryConfigRecord::model()->find('id_library=?', array($libraryRecord->id));
					if (isset($libraryConfigRecord))
					{
						$sender = $libraryConfigRecord->dead_link_sender;
						$recipients = explode(";", $libraryConfigRecord->dead_link_recipients);
						$subject = $libraryConfigRecord->dead_link_subject;
						$body = $libraryConfigRecord->dead_link_body;
						$body = str_replace(PHP_EOL, '<br>', $body);

						$body = str_replace('{dead}', isset($deadLinks) ? implode('<br>', $deadLinks) : 'none', $body);

						$body = str_replace('{not_converted}', isset($notConvertedVideos) ? implode('<br>', $notConvertedVideos) : 'none', $body);

						if (isset($sender) && isset($recipients) && isset($subject) && isset($body))
						{
							$message = Yii::app()->email;
							$message->to = $recipients;
							$message->subject = $subject;
							$message->from = $sender;
							$message->message = $body;
							$message->send();
						}
					}
				}
			}
			echo "Job completed.\n";
		}
	}