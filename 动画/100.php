<?php
$filepath = "C:/Program Files/Adobe/Adobe Photoshop 2023/Resources/PSIconsHighRes.dat";

$fp = fopen($filepath, "r");

$pngBuffer = "";
$pngHeader = sprintf("%c%c%c%c%c%c%c%c", 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A);
$pngHeaderLength = strlen($pngHeader);
$count = 0;
global $pngOffset;

while (!feof($fp)) {
	$char = fread($fp, 1);

	// check for start of magic
	if ($char != chr(0x89)) {
		continue;
	}
	
	// backtrack 1
	fseek($fp, -1, SEEK_CUR);
	printf("checking for full header\n");
	// check for full header
	$search = fread($fp, $pngHeaderLength);
	if ($search == $pngHeader) {
		fseek ($fp, 0 - $pngHeaderLength, SEEK_CUR);
		$pngFile = readPNGFile($fp);
	}
	
	// write out file
	$pngfp = @fopen("C:/Users/Administrator/Desktop/代码/脚本/动画/icons/$pngOffset.png", "x");
	@fwrite($pngfp, $pngFile);
	@fclose($pngfp);
	$count++;
	
}

function readPNGFile($fp) {
	global $pngHeader, $pngHeaderLength, $pngOffset;
	$pos = ftell($fp);
	$pngOffset = $pos;
	echo "Reading PNG @ $pos\n";
	
	$contents = fread($fp, $pngHeaderLength);
	
	while ( ($chunk = readChunk($fp) ) ) {
		$contents .= $chunk['data'];
		
		if ($chunk['name'] == "IEND") {
			break;
		}
	}
	
	return $contents;
}

function readChunk($fp) {
	$chunk = "";
	
	// Read length
	$length = fread($fp, 4);
	$chunk .= $length;
	// turn length into int
	$length = unpack("N", $length);
	$length = array_pop($length);
	
	// Read chunk name
	$name = fread($fp, 4);
	$chunk .= $name;
	printf("reading chunk $name with length %d\n", $length);
	if ($length > 0) {
		$chunk .= fread($fp, $length);
	}
	
	$crc = fread($fp, 4);
	$chunk .= $crc;
	
	return array("name" => $name, "data" => $chunk);
}

?>
// https://gist.github.com/JohnCoates/ece63fa29abee3276a4c
// C:\php\php.exe 100.php