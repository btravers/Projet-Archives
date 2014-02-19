<?php

/**
 * Request class
 * Result of an url parsing
 */
class Request
{
	/**
	 * Helper name
	 * @see abstract class Module
	 */
	public $helper;

	/**
	 * Function name
	 */
	public $function;

	/**
	 * Arguments array
	 */
	public $arguments;

	/**
	 * Constructor
	 */
	public function __construct($helper, $function, $arguments)
	{
		$this->helper = $helper;
		$this->function = $function;
		$this->arguments = $arguments;		
	}

	/**
	 * To String
	 */
	public function __toString()
    {
        return "Request : [ helper : " . $this->helper . ", function : " . $this->function . ", args : " . implode(", ", $this->arguments) . "]\n";
    }
}