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
		// header ("Content-Type:text/xml");
		header("Content-Type:text/plain");

		$request = $this->parse($_GET);
		$data = $this->exec($request);
		$this->printXML($data);
	}

	/**
	 * Transform url in php function
	 * $url must be $_GET by default
	 */
	public function parse($url)
	{
		if(isset($url['helper']))
			$helper = $url['helper'];
		else
			$helper = "";
		if(isset($url['function']))
			$function = $url['function'];
		else
			$function = "";
		if(isset($url['args']))
			$args = explode("/", $url['args']);
		else
			$args = array();		

		// Transform helper string in abstract class HelperEnum
		switch($helper) {
			case "authentificator":
				$helperType = HelperEnum::Authentificator;
				break;
			case "annotator":
				$helperType = HelperEnum::Annotator;
				break;
			case "browser":
				$helperType = HelperEnum::Browser;
				break;
			case "finder":
				$helperType = HelperEnum::Finder;
				break;
			default:
				$helperType = HelperEnum::Error;
				$function = Error::HelperNotFound;
				break;
		}

		// Check if user is not using url rewritting
		if($_SERVER["SCRIPT_NAME"] == "/index.php") {
			$helperType = HelperEnum::Error;
			$function = Error::SecurityException;
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
	 * Print XML
	 */
	public function printXML($data)
	{
		$xml = new SimpleXMLElement('<response/>');
		$this->array_to_xml($data, $xml);
		print $xml->asXML();
	}

	/**
	 * Transform data to XML
	 * @see SimpleXMLElement
	 */
	function array_to_xml($student_info, &$xml_student_info) {
	    foreach($student_info as $key => $value) {
	        if(is_array($value)) {
	            if(!is_numeric($key)){
	                $subnode = $xml_student_info->addChild("$key");
	                $this->array_to_xml($value, $subnode);
	            }
	            else{
	                $subnode = $xml_student_info->addChild("item$key");
	                $this->array_to_xml($value, $subnode);
	            }
	        }
	        else {
	            $xml_student_info->addChild("$key","$value");
	        }
	    }
	}
}