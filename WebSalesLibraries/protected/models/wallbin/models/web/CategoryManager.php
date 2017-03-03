<?
	use application\models\wallbin\models\web\Category as Category;
	use application\models\wallbin\models\web\SuperFilter as SuperFilter;

	/**
	 * Class CategoryManager
	 */
	class CategoryManager
	{
		public $superFilters;

		/** @var  Category[] */
		public $tags;

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

			$this->tags = array();
			$this->categories = array();
			$this->groups = array();
			$categoryRecords = CategoryRecord::getData();
			if (isset($categoryRecords))
			{
				foreach ($categoryRecords as $categoryRecord)
				{
					$category = new Category();
					$category->group = $categoryRecord->group;
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
					$this->tags[] = $category;

					if (!in_array($category->category, $this->categories))
						$this->categories[] = $category->category;
					if (!in_array($category->group, $this->groups))
						$this->groups[] = $category->group;
				}
			}
			$cookieId = 'selectedSuperFilters' . $selectedTab;
			if (isset(Yii::app()->request->cookies[$cookieId]->value))
				$selectedSuperFilters = CJSON::decode(Yii::app()->request->cookies[$cookieId]->value);

			$this->superFilters = array();
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
		 * @return array
		 */
		public function getCategoriesByGroup($group)
		{
			$categories = array();
			foreach ($this->tags as $tagItem)
				if ($tagItem->group == $group && !in_array($tagItem->category, $categories))
					$categories[] = $tagItem->category;
			return $categories;
		}

		/**
		 * @param $category
		 * @return array
		 */
		public function getTagsByCategory($category)
		{
			$tags = array();
			foreach ($this->tags as $tagItem)
				if ($tagItem->category == $category)
					$tags[] = array('tag' => $tagItem->tag, 'selected' => $tagItem->selected);
			return $tags;
		}

		/**
		 * @param $category
		 * @return bool
		 */
		public function isCategorySelected($category)
		{
			$groupSelected = true;
			foreach ($this->tags as $tagItem)
				if ($tagItem->category == $category && !$tagItem->selected)
				{
					$groupSelected = false;
					break;
				}
			return $groupSelected;
		}
	}
