#***************************************************************************************************************
#** TestInfluxDb.ps1
#***************************************************************************************************************
#** 
#***************************************************************************************************************

param (
	[switch]$copyFiles = $true
)

$host.ui.rawui.WindowTitle = "Test InfluxDb"

if($copyFiles)
{
	$srcPath = "..\Build\*"
	$dstPath = ".\Modules\InfluxDBPS"
	new-Item -Path ".\Modules" -ItemType:Directory -Force
	new-Item -Path ".\Modules\InfluxDBPS" -ItemType:Directory -Force
	write-host "Copy '$srcPath' to '$dstPath'"
	copy-item $srcPath $dstPath -container -recurse -force -erroraction:Stop
}

import-module .\modules\InfluxDBPS -force
get-command -module:InfluxDBPS | select Name | ft 


$db = Open-InfluxDb -Uri:"http://192.168.1.107:8086" -User:"root" -Password:"root"
$version = Get-VersionInfluxDb -dbConnection:$db
$version
$pong = Ping-InfluxDb -dbConnection:$db
$pong
Add-InfluxDb -dbConnection:$db -name:"TheTest"

Write-InfluxDb -dbConnection:$db -dbName:"TheTest" -seriesName:"TheSeries" -columns:@("value1", "value2") -values:@(23, 45)
 


#***************************************************************************************************************
