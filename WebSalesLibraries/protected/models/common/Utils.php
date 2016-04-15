<?
	/**
	 * Class PreviewData
	 */
	class Utils
	{
		/**
		 * @return string
		 */
		public static function getBrowser()
		{
			if (Yii::app()->browser->isMobile())
				$browser = 'mobile';
			else
			{
				$browser = Yii::app()->browser->getBrowser();
				switch ($browser)
				{
					case 'Internet Explorer':
						$browser = 'ie';
						break;
					case 'Chrome':
					case 'Safari':
						$browser = 'webkit';
						break;
					case 'Firefox':
						$browser = 'firefox';
						break;
					case 'Opera':
						$browser = 'opera';
						break;
					default:
						$browser = 'webkit';
						break;
				}
			}
			return $browser;
		}

		/**
		 * @param string $hexColor
		 * @return bool
		 */
		public static function isColorLight($hexColor)
		{
			// returns brightness value from 0 to 255
			// strip off any leading #
			$hex = str_replace('#', '', $hexColor);

			$c_r = hexdec(substr($hex, 0, 2));
			$c_g = hexdec(substr($hex, 2, 2));
			$c_b = hexdec(substr($hex, 4, 2));

			return ((($c_r * 299) + ($c_g * 587) + ($c_b * 114)) / 1000) <= 130;
		}

		/**
		 * @param string $url
		 * @return string
		 */
		public static function formatUrl($url)
		{
			return htmlspecialchars(
				str_replace('&', '%26',
					str_replace('&amp;', '%26',
						str_replace(' ', '%20',
							str_replace('\\', '/',$url)))));
		}
	}