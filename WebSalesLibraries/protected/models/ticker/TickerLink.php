<?php
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

		public function getDetailByKey($key)
		{
			foreach ($this->details as $detail)
				if ($detail->tag == $key)
					return $detail->data;
			return '';
		}
	}