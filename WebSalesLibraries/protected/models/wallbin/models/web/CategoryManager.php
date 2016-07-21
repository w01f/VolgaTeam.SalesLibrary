<?
	use application\models\wallbin\models\web\Category as Category;
	use application\models\wallbin\models\web\SuperFilter as SuperFilter;

	/**
	 * Class CategoryManager
	 */
	class CategoryManager
	{
		public $superFilters;
		public $categories;
		public $groups;

		public function loadCategories()
		{
			$selectedTab = '';
			if (isset(Yii::app()->request->cookies['selectedRibbonTabId']->value))
				$selectedTab = Yii::app()->request->cookies['selectedRibbonTabId']->value;

			$cookieId = 'selectedCategories' . $selectedTab;
			if (isset(Yii::app()->request->cookies[$cookieId]->value))
				$selectedCategories = CJSON::decode(Yii::app()->request->cookies[$cookieId]->value);

			$categoryRecords = CategoryRecord::getData();
			if (isset($categoryRecords))
			{
				foreach ($categoryRecords as $categoryRecord)
				{
					$category = new Category();
					$category->category = $categoryRecord->category;
					$category->description = $categoryRecord->description;
					$category->tag = $categoryRecord->tag;
					$category->selected = false;
					if (isset($selectedCategories))
						foreach ($selectedCategories as $selectedCategory)
							if ($selectedCategory['category'] == $category->category && $selectedCategory['tag'] == $category->tag)
							{
								$category->selected = true;
								break;
							}
					$this->categories[] = $category;

					if (!(isset($this->groups) && in_array($category->category, $this->groups)))
						$this->groups[] = $category->category;
				}
			}
			$cookieId = 'selectedSuperFilters' . $selectedTab;
			if (isset(Yii::app()->request->cookies[$cookieId]->value))
				$selectedSuperFilters = CJSON::decode(Yii::app()->request->cookies[$cookieId]->value);

			/** @var $superFilterRecords SuperFilterRecord */
			$superFilterRecords = SuperFilterRecord::model()->findAll();
			foreach ($superFilterRecords as $superFilterRecord)
			{
				$superFilter = new SuperFilter();
				$superFilter->value = $superFilterRecord->value;
				$superFilter->selected = isset($selectedSuperFilters) && in_array($superFilter->value, $selectedSuperFilters);
				$this->superFilters[] = $superFilter;
			}
		}

		/**
		 * @param $group
		 * @return array|null
		 */
		public function getTagsByGroup($group)
		{
			if (isset($this->groups) && isset($this->categories))
			{
				foreach ($this->categories as $category)
					if ($category->category == $group)
						$tags[] = array('tag' => $category->tag, 'selected' => $category->selected);
				if (isset($tags))
					return $tags;
			}
			return null;
		}

		/**
		 * @param $group
		 * @return bool
		 */
		public function isGroupSelected($group)
		{
			if (isset($this->groups) && isset($this->categories))
			{
				$groupSelected = true;
				foreach ($this->categories as $category)
					if ($category->category == $group && !$category->selected)
					{
						$groupSelected = false;
						break;
					}
				return $groupSelected;
			}
			return false;
		}
	}
