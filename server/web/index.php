<?php
// enable error reporting
error_reporting(E_ALL);
ini_set('display_errors', '1');

// load application
require_once("../private/bootstrap.php");

// OOP entry point
Bootstrap::init();