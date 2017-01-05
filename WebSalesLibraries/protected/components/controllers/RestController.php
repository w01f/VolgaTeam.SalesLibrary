<?

	/**
	 * Class RestController
	 */
	abstract class RestController extends IsdController
	{
		protected function sendResponse($status = 200, $body = '', $content_type = 'application/json; charset=UTF-8')
		{
			// set the status
			$status_header = 'HTTP/1.1 ' . $status . ' ' . $this->getStatusCodeMessage($status);
			header($status_header);
			// and the content type
			header('Content-type: ' . $content_type);
			echo $body;
			Yii::app()->end();
		}

		protected function getStatusCodeMessage($status)
		{
			// these could be stored in a .ini file and loaded
			// via parse_ini_file()... however, this will suffice
			// for an example
			$codes = Array(
				200 => 'OK',
				400 => 'Bad Request',
				401 => 'Unauthorized',
				402 => 'Payment Required',
				403 => 'Forbidden',
				404 => 'Not Found',
				500 => 'Internal Server Error',
				501 => 'Not Implemented',
			);
			return (isset($codes[$status])) ? $codes[$status] : '';
		}
	}