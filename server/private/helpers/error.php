<?php

/**
 * Error class
 * Error helper
 */
class Error
{
	const HelperNotFound = 0;
	const SecurityException = 1;

	/**
	 * Parse request and exec right function
	 */
	public static function exec($request)
	{
		switch($request->function) {
			case Error::HelperNotFound:
				return array("helper" => "error", "message" => "helper_not_found");
			case Error::SecurityException:
				return array("helper" => "error", "message" => "security_exception");
		}
	}
}