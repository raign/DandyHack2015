<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<?php
session_start();

$table = $_GET['table'];
$email = $_GET['email'];
$time_ordered = $_GET['time_ordered'];
/*
echo $table;
echo $email;
echo $time_ordered;
*/

?>
<head>
<link rel="stylesheet" type="text/css" href="hans_style.css">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
</head>

   <body>
	<h2>Show</h2>

	<?php
	require_once 'dbsetup.php';
?>
<?php
	try {
	    echo '<p>', 'Query pull (emclaug2) as of ', date("Y-m-d H:i:s"), '</p>';

	    echo '<table>';
	    // print column headers
	    echo '<tr>',
	    '<th>email</th>',
	    '<th>time_ordered</th>',
	    '<th>name</th>',
	    '<th>quantity</th>',
		'<th>special_instructions</th>',
	    '<th>total_price</th>',
	    '<th>payment_type</th>',
		'<th>ready_by</th>',
	    '<th>active</th>',
	    "</tr>\n";
		echo "Query Generated:";
	    $sql = "select * from $table where email = '$email' and time_ordered = '$time_ordered'";
		echo PHP_EOL;
		echo $sql;
	    foreach ($db->query($sql) as $row)
	    {
			// Going on to the next team?
			if (null != $row['email'])
			{
				// first finish up the empty rows of the previous team (if any)
				$email = $row['email'];
				$time_ordered = $row['time_ordered'];
				$name = $row['name'];
				$quantity = $row['quantity'];
				$special_instructions = $row['special_instructions'];
				$total_price = $row['total_price'];
				$payment_type = $row['payment_type'];
				$ready_by = $row['ready_by'];
				$active = $row['active'];
				$link = 'show';
				
				$rowsremaining = $row['max_size'];
				echo '<tr>';
				// make the entry listing the team span across n+1 rows, where n = size of the team
				echo "<td class='firstrow'>$email</td>";
				echo "<td class='firstrow'>$time_ordered</td>";
				echo "<td class='firstrow'>$name</td>";
				echo "<td class='firstrow'>$quantity</td>";
				echo "<td class='firstrow'>$special_instructions</td>";
				echo "<td class='firstrow'>$total_price</td>";
				echo "<td class='firstrow'>$payment_type</td>";
				echo "<td class='firstrow'>$ready_by</td>";
				echo "<td class='firstrow'>$active</td>";
				echo "</tr>\n";
			}
		}
		echo '</table>';
		}
		catch (PDOException $e) {
			print "Error!: " . $e->getMessage() . "<br/>";
			die();
		}
		?>
    </body>
</html>