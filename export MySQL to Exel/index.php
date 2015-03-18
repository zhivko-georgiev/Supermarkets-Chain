<?php
$connection=mysqli_connect('localhost','root','','testdb');
mysqli_query($connection,'SET NAMES utf8');
mb_internal_encoding('UTF-8');

$result = mysqli_query($connection, 'SHOW TABLES');
$tables = [];
$counter = 0;

while ($row = mysqli_fetch_row($result)) {
    $tables[$counter] =  $row[0];
	
	$counter++;
}
$counter--;

foreach($tables as $table) {
	$filename = "uploads/$table.csv";

	$fp = fopen($filename, "w");

	$sql = mysqli_query($connection,"SELECT * FROM $table");
	$row = mysqli_fetch_assoc($sql);

	$separator = "";
	$comma = "";

	foreach($row as $name => $value) {
		$separator .= $comma. '' . str_replace('', '""', $name);
		$comma = ",";
	}

	$separator .= "\n";

	fputs($fp, $separator);

	mysqli_data_seek($sql, 0);

	while($row = mysqli_fetch_assoc($sql)) {

	$separator = "";
	$comma = "";

	foreach($row as $name => $value) {
		$separator .= $comma. '' . str_replace('', '""', $value);
		$comma = ",";
	}

	$separator .= "\n";

	fputs($fp, $separator);
	}
}

?>