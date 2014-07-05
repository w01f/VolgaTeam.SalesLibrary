<?php

	/**
	 * Class QuizItem
	 */
	class QuizItem
	{
		public $id;
		public $name;
		public $isGroup;
		public $isPassed;
		/**
		 * @var QuizItem[]
		 */
		public $childItems;

		/**
		 * @param $itemName
		 * @return array
		 */
		public function getSelectedItemBreadcrumbs($itemName)
		{
			$itemIds = array();
			if ($this->name == $itemName)
				$itemIds[] = $this->id;
			else if (isset($this->childItems))
			{
				foreach ($this->childItems as $childItem)
				{
					$itemIds = $childItem->getSelectedItemBreadcrumbs($itemName);
					if (isset($itemIds))
					{
						$itemIds[] = $this->id;
						break;
					}
				}
			}
			return $itemIds;
		}
	}