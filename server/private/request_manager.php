<?php

/**
 * RequestManager class
 * Server manager
 */
class RequestManager
{
	/**
	 * Entry point
	 */
	public function main()
	{
		echo $this->parse($_GET);
		echo "done.";
	}

	/**
	 * Transform url in php function
	 * $url must be $_GET by default
	 */
	public function parse($url)
	{
		$helper = $url['helper'];
		$function = $url['function'];
		$args = explode("/", $url['args']);

		switch($helper) {
			case "authentificator":
				$helperType = Helper::Authentificator;
				break;
			default:
				$helperType = Helper::Error;
				$function = Error::HelperNotFound;
				break;
		}

		return new Request($helperType, $function, $args);
	}

	/**
	 * Exec the parsed request
	 */
	public function exec($request)
	{

	}

	/**
	 * Check if the session id exists in database
	 */
	public function isConnected($sessionId)
	{

	}

	/**
	 * Transform data to XML and print
	 */
	public function printXML($data)
	{

	}
}