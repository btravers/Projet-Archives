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
		$request = $this->parse($_GET);
		$data = $this->exec($request);
		$this->printXML($data);
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

		// Transform helper string in abstract class Helper
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
		$execFunction = Bootstrap::loadHelper($request->helper);
		return $execFunction($request);
	}

	/**
	 * Transform data to XML and print
	 */
	public function printXML($data)
	{		
		print_r($data);
	}
}