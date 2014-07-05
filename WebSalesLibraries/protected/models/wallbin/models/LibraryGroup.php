<?php

	/**
	 * Class LibraryGroup
	 */
	class LibraryGroup
	{
		public $id;
		public $name;
		public $order;
		public $libraries;

		/**
		 * @param $x
		 * @param $y
		 * @return int
		 */
		public static function libraryGroupComparer($x, $y)
		{
			if ($x->order == $y->order)
				return 0;
			else
				return ($x->order < $y->order) ? -1 : 1;
		}
	}
