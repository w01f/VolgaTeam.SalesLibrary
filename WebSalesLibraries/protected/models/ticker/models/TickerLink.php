<?php

	/**
	 * Class TickerLink
	 */
	class TickerLink
	{
		/**
		 * @var string
		 * @soap
		 */
		public $id;
		/**
		 * @var string
		 * @soap
		 */
		public $type;
		/**
		 * @var int
		 * @soap
		 */
		public $order;
		/**
		 * @var string
		 * @soap
		 */
		public $text;
		/**
		 * @var KeyValuePair[]
		 * @soap
		 */
		public $details;

		/**
		 * @param $key
		 * @return string
		 */
		public function getDetailByKey($key)
		{
			if (isset($this->details))
				foreach ($this->details as $detail)
					if ($detail->tag == $key)
						return $detail->data;
			return '';
		}
	}