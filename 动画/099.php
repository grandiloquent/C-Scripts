<?php
$filepath = "C:/Program Files/Adobe/Adobe Photoshop 2023/Resources/IconResources.idx";
$headerMagic = "Photoshop Icon Resource Index 1.0\n";

$fp = fopen($filepath, "r");

$magic = fread($fp, strlen($headerMagic));

if ($magic != $headerMagic) {
	exit("error: wrong magic");
}

// low res
$dat = fgets($fp);
echo $dat;

// high res
$dat = fgets($fp);
echo $dat;

// XLowRes : 80 extra bytes
$dat = fgets($fp);
echo $dat;

// XHighRes : 80 extra bytes
$dat = fgets($fp);
echo $dat;

$list = array();

$entryLength = 368;

function readOffsetFromEntry($entry, $position = 112) {
	$name = substr($entry, 0, strpos($entry, "\0"));
	$offset = substr($entry, $position, 4);
	$hex = bin2hex($offset);
	$offset = unpack("V", $offset);
	$offset = array_pop($offset);
	echo "$name : $offset 0x$hex\n";
	return $offset;
}

while (!feof($fp)) {
	$entry = fread ($fp, $entryLength);

	if( strlen($entry) != $entryLength) {
		break;
	}

	$name = substr($entry, 0, strpos($entry, "\0"));
	$list[] = $name;

	$offset = readOffsetFromEntry($entry, 144);

	if ($offset == 0) {
		continue;
	}

	$original = "C:/Users/Administrator/Desktop/代码/脚本/动画/icons/$offset.png";

	// exit;
	if (file_exists($original)) {
		rename($original, "C:/Users/Administrator/Desktop/代码/脚本/动画/icons/$name.png");
	}
}




?>