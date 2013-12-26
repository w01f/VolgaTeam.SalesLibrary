<?php
	class QuizItem
	{
		public $id;
		public $name;
		public $isGroup;
		public $childItems;

		public function getSelectedItemBreadcrumbs($itemName)
		{
			$itemIds = null;
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