<?php

	/**
	 * Class LinkSuperFilter
	 */
	class LinkSuperFilter
	{
		/**
		 * @var string
		 * @soap
		 */
		public $linkId;
		/**
		 * @var string
		 * @soap
		 */
		public $libraryId;
		/**
		 * @var string
		 * @soap
		 */
		public $value;

		/**
		 * @param $linkSuperFilterRecord
		 */
		public function load($linkSuperFilterRecord)
		{
			$this->linkId = $linkSuperFilterRecord->id_link;
			$this->libraryId = $linkSuperFilterRecord->id_library;
			$this->value = $linkSuperFilterRecord->value;
		}
	}
