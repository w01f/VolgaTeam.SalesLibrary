<?
	/**
	 * Class BaseServiceProfile
	 */
	abstract class BaseServiceProfile
	{
		const ServiceTypeDataQueryCache = 'data-query-cache';

		/**
		 * @var string
		 * @soap
		 */
		public $id;
		/**
		 * @var string
		 * @soap
		 */
		public $name;
		/**
		 * @var string
		 * @soap
		 */
		public $type;

		/**
		 * @param $encodedConfig
		 */
		public abstract function loadConfig($encodedConfig);

		/**
		 * @return string
		 */
		public abstract function getEncodedConfig();
	}