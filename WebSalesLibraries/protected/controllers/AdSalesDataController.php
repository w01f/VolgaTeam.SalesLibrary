<?

	/**
	 * Class AdSalesDataController
	 */
	class AdSalesDataController extends LocalAppDataController
	{
		/** return boolean */
		protected function getIsPublicController()
		{
			return true;
		}

		/**
		 * @return array
		 */
		public function actions()
		{
			return array(
				'quote' => array(
					'class' => 'CWebServiceAction',
					'classMap' => array(),
				),
			);
		}
	}