<?
	/**
	 * Class SoapShortcutModel
	 */
	class SoapShortcutModel
	{
		/**
		 * @var string id
		 * @soap
		 */
		public $id;
		/**
		 * @var string title
		 * @soap
		 */
		public $title;
		/**
		 * @var string title
		 * @soap
		 */
		public $description;


		/**
		 * @param $linkRecord \ShortcutLinkRecord
		 * @return SoapShortcutModel
		 */
		public static function fromLinkRecord($linkRecord)
		{
			$model = new SoapShortcutModel();
			$model->id = $linkRecord->id;

			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/Regular/Title');
			$model->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';

			$queryResult = $xpath->query('//Config/Regular/Description');
			$model->description = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';

			return $model;
		}
	}