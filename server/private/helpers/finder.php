<?php

/**
 * Finder class
 * Finder helper
 */
class Finder
{
	/**
	 * Parse request and exec right function
	 */
	public static function exec($request)
	{
		$function = new ReflectionMethod(get_called_class(), $request->function);
		return $function->invokeArgs(NULL, $request->arguments);
	}

	/**
	 * Search a table by year and location
	 */
	public static function search_table($year, $location)
	{
		if(!is_numeric($year))
			return array("helper" => "finder", "message" => "year_not_numeric");
		
		$data = array();
		
		$resultRegister = Database::query("SELECT * FROM Register WHERE year = ? AND location = ?", array($year, $location));
		foreach ($resultRegister as $register)
		{
			$idRegister = $register['id_register'];
			
			$data[$idRegister]['year'] = $register['year'];
			$data[$idRegister]['location'] = $register['location'];
			$data[$idRegister]['volume'] = $register['volume'];
			
			$resultPageTable = Database::query("SELECT * FROM PageTable WHERE id_register = ?", array($idRegister));
			foreach ($resultPageTable as $pageTable)
			{
				$idPageTable = $pageTable['id_page_table'];
				
				$data[$idRegister]['pages'][$idPageTable]['page'] = $pageTable['page'];
				$data[$idRegister]['pages'][$idPageTable]['url'] = $pageTable['url'];
			}
		}
		
		if(count($data) > 1) {
			return array("helper" => "finder", "message" => "result_found", "result" => $data);
		} else {
			return array("helper" => "finder", "message" => "result_not_found");
		}		
	}
	
	/**
	 * Search a sheet by year, location, firstname, lastname, job, regiment
	 */
	public static function search_sheet($year, $location, $firstname, $lastname, $job, $regiment)
	{
		if(!is_numeric($year))
			return array("helper" => "finder", "message" => "year_not_numeric");
		
		$data = array();
		
		
		
		if(count($data) > 1) {
			return array("helper" => "finder", "message" => "result_found", "result" => $data);
		} else {
			return array("helper" => "finder", "message" => "result_not_found");
		}		
	}
}