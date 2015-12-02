<?

	/**
	 * Class FileManagerDataController
	 */
	class AdSalesDataController extends LocalAppDataController
	{
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