<?php
	class ObjectSortHelper
	{
		public $column;
		public $direction;

		public function __construct($column, $direction)
		{
			$this->column = isset($column) ? $column : 'name';
			$this->direction = isset($direction) ? $direction : 'asc';
		}

		public function sort($a, $b)
		{
			$a_arr = (array)$a;
			$b_arr = (array)$b;
			if (isset($this->column) && isset($this->direction) && array_key_exists($this->column, $a_arr) && array_key_exists($this->column, $b_arr))
			{
				if ($this->direction == 'asc')
					return strnatcmp($a_arr[$this->column], $b_arr[$this->column]);
				else
					return strnatcmp($b_arr[$this->column], $a_arr[$this->column]);
			}
			else
				return 0;
		}
	}