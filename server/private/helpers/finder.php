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
		
		if(count($data) > 0) {
			return array("helper" => "finder", "message" => "result_found", "result" => $data);
		} else {
			return array("helper" => "finder", "message" => "result_not_found");
		}		
	}
	
	/**
	 * Search a sheet by year, location, firstname, lastname, job, regiment
	 */
	public static function search_sheet($year, $location, $firstname, $lastname, $job, $regiment, $other)
	{
		if(!is_numeric($year))
			return array("helper" => "finder", "message" => "year_not_numeric");

		$data = array();

		$argumentsNumber = 0;
		$argumentsSql = "";
		$argumentsValues = array($year, $location);

		if($firstname != "") {
			$argumentsNumber++;
			$argumentsSql .= "(t.label = 'Prénom' AND a.text LIKE ?)";
			array_push($argumentsValues, '%' . $firstname . '%');
		}

		if($lastname != "") {			
			if($argumentsNumber > 0) {
				$argumentsSql .= " OR ";
			}
			$argumentsNumber++;
			$argumentsSql .= "(t.label = 'Nom' AND a.text LIKE ?)";
			array_push($argumentsValues, '%' . $lastname . '%');
		}

		if($job != "") {			
			if($argumentsNumber > 0) {
				$argumentsSql .= " OR ";
			}
			$argumentsNumber++;
			$argumentsSql .= "(t.label = 'Profession' AND a.text LIKE ?)";
			array_push($argumentsValues, '%' . $job . '%');
		}

		if($regiment != "") {			
			if($argumentsNumber > 0) {
				$argumentsSql .= " OR ";
			}
			$argumentsNumber++;
			$argumentsSql .= "(t.label = 'Régiment' AND a.text LIKE ?)";
			array_push($argumentsValues, '%' . $regiment . '%');
		}
		
		if($other != "") {			
			if($argumentsNumber > 0) {
				$argumentsSql .= " OR ";
			}
			$argumentsNumber++;
			$argumentsSql .= "(a.text LIKE ?)";
			array_push($argumentsValues, '%' . $other . '%');
		}
		
		if($argumentsNumber > 0) {
			$sql = "SELECT * FROM Register r, AnnotationSheet a, Sheet s, Type t WHERE r.id_register = s.id_register AND a.id_sheet = s.id_sheet AND a.id_type = t.id_type AND year = ? AND location = ? AND (" . $argumentsSql . ") GROUP BY s.id_sheet LIMIT 3";
		} else {
			$sql = "SELECT * FROM Register r, Sheet s WHERE r.id_register = s.id_register AND year = ? AND location = ? LIMIT 3";
		}
		
		$resultSheets = Database::query($sql, $argumentsValues);
		foreach ($resultSheets as $sheet)
		{
			$idSheet = $sheet['id_sheet'];

			$data[$idSheet]['url'] = $sheet['url'];
		}		

		if(count($data) > 0) {
			return array("helper" => "finder", "message" => "result_found", "result" => $data);
		} else {
			return array("helper" => "finder", "message" => "result_not_found");
		}		
	}
}