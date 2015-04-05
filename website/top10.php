<?php
require_once 'dbsetup.php';
session_start();
$var_val = 'Test';
$_SESSION['varname'] = $var_val;
$_SESSION['table'];
$_SESSION['email'];
$_SESSION['time_ordered'];
$q = $_GET['name'];
$path = "img/";

?>
<!DOCTYPE html>
<html lang="en">
	<head>
        <style>
            .clicker { 
            -moz-transition: all .2s ease-in;
            -o-transition: all .2s ease-in;
            -webkit-transition: all .2s ease-in;
            transition: all .2s ease-in;
            background: #eee; 
            padding: 20px;
            }
        </style>
        
        <script src="swipe.js"></script>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
        <link rel="stylesheet" href="css/bootstrap.min.css">
        
        <title>CAT: Cute Animal Tinder</title>
    </head>

    <body>
		<nav class="navbar navbar-inverse navbar-static-top">
		  <div class="container-fluid">
		    <!-- Brand and toggle get grouped for better mobile display -->
		    <div class="navbar-header">
		      <a class="navbar-brand" href="#">CAT: Top 10 Cutest Pictures</a>
              <a href = "index.php">
              <button class="btn btn-warning btn-lg" onclick="index.php"><span class="glyphicon glyphicon-home" aria-hidden="true"></span></button>
		   	</a>
            </div>
		  </div><!-- /.container-fluid -->
		</nav>
        
		<div class="container">
        	<div class = "clicker">
				<?php
				try {
					$sql = "SELECT * FROM images ORDER BY upvotes DESC limit 10";
					foreach ($db->query($sql) as $row) {
				
						$id = $row['id'];
						echo "<img id = 'image_shown' data-num=$id src='$path$id.jpg' height=auto width='100%'>";
						echo '<h2>Upvotes: '. $row['upvotes'].'</h2>';
						echo '</br>';
					}
				} catch(PDOException $e) {
					print "Error!: " . $e->getMessage() . "<br/>";
					die();
				}
				?>		
			</div>
		</div>
    </body>
</html>