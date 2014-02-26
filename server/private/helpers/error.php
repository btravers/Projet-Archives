<?php

/**
 * Error class
 * Error helper
 */
class Error
{
	const HelperNotFound = 0;

	/**
	 * Parse request and exec right function
	 */
	public static function exec($request)
	{
		switch($request->function) {
			case Error::HelperNotFound:
				return array("error" => array("message" => "helper_not_found"));
		}
	}
}