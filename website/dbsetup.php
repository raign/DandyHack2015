<?php
// File: dbsetup.php
// Purpose: initialize database handle ($db)
// Modified: koomen / 2014-02-20
$dbtype = 'mysql';
$dbuser = 'sql372823'; $dbpass = 'kT1%wQ6*';
$dbname = 'sql372823'; $dbhost = 'sql3.freemysqlhosting.net';
$dsn = "$dbtype:host=$dbhost;dbname=$dbname";
try {
 $db = new PDO($dsn, $dbuser, $dbpass);
}
catch (PDOException $e) {
 print "DB Connection Error!: " . $e->getMessage();
 die();
}
?>