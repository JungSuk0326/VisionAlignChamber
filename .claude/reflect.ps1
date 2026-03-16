$asm = [System.Reflection.Assembly]::LoadFrom('D:\#01_Projects\VisionAlignChamber_Repository\VisionAlignChamber\Vision\eMotion\DLLs\eMotionAlign.dll')
$types = $asm.GetTypes()
foreach($t in $types) {
    if($t.Name -like '*Multicam*' -or $t.Name -like '*Grabber*') {
        Write-Output "=== Type: $($t.FullName) ==="
        $ctors = $t.GetConstructors()
        foreach($c in $ctors) {
            Write-Output "  CTOR: $($c.ToString())"
        }
        $props = $t.GetProperties()
        foreach($p in $props) {
            Write-Output "  PROP: $($p.Name) ($($p.PropertyType.Name))"
        }
    }
}
