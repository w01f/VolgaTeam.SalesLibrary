<?php

	/**
	 * Class ArraySortHelper
	 */
	class ArraySortHelper
	{
		/**
		 * @var string
		 */
		public $column;
		/**
		 * @var string
		 */
		public $direction;

		/**
		 * @param $column
		 * @param $direction
		 */
		public function __construct($column, $direction)
		{
			$this->column = isset($column) ? $column : 'name';
			$this->direction = isset($direction) ? $direction : 'asc';
		}

		/**
		 * @param $a
		 * @param $b
		 * @return int
		 */
		public function sort($a, $b)
		{
			if (isset($this->column) && isset($this->direction) && array_key_exists($this->column, $a) && array_key_exists($this->column, $b))
			{
				if ($this->direction == 'asc')
					return strnatcmp($a[$this->column], $b[$this->column]);
				else
					return strnatcmp($b[$this->column], $a[$this->column]);
			}
			else
				return 0;
		}
	}