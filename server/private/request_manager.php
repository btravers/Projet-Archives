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
		$helper = $url['helper'];
		$function = $url['function'];
		$args = explode("/", $url['args']);

		// Transform helper string in abstract class HelperEnum
		switch($helper) {
			case "authentificator":
				$helperType = HelperEnum::Authentificator;
				break;
			case "annotator":
				$helperType = HelperEnum::Annotator;
				break;
			default:
				$helperType = HelperEnum::Error;
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