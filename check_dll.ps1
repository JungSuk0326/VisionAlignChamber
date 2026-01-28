$dllPath = "D:\#01_프로젝트\VisionAlignChamber_Repository\VisionAlignChamber\Vision\eMotion\DLLs\eMotionAlign.dll"
$asm = [Reflection.Assembly]::LoadFrom($dllPath)
$type = $asm.GetType("eMotion.ClassAlign")

Write-Host "=== ClassAlign Methods ==="
$type.GetMethods() | ForEach-Object { Write-Host $_.Name } | Sort-Object -Unique

Write-Host ""
Write-Host "=== ClassAlign Properties ==="
$type.GetProperties() | ForEach-Object { Write-Host $_.Name }
